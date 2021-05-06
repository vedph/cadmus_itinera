using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsDecorationsPartTest
    {
        private static List<MsDecorationElement> GetElements(int count)
        {
            List<MsDecorationElement> elements = new List<MsDecorationElement>();

            for (int n = 1; n <= count; n++)
            {
                string alt = n % 2 == 0 ? "even" : "odd";

                elements.Add(new MsDecorationElement
                {
                    Key = n == 1? "e1" : null,
                    ParentKey = n == 2? "e1" : null,
                    Type = alt,
                    Flags = new List<string>(new[] {"f-" + alt}),
                    Ranges = new List<MsLocationRange>(new[]
                    {
                        new MsLocationRange
                        {
                            Start = new MsLocation
                            {
                                N = 2,
                                S = n % 2 == 0
                                    ? "v"
                                    : "r",
                                L = 3
                            },
                            End = new MsLocation
                            {
                                N = 4,
                                S = n % 2 == 0
                                    ? "v"
                                    : "r",
                                L = 5
                            }
                        }
                    }),
                    Typologies = new List<string>(new[] {"t-" + alt}),
                    Subject = "s" + n,
                    Colors = new List<string>(new[] {"c" + n}),
                    Gilding = "gilding",
                    Technique = "technique",
                    Tool = "tool",
                    Position = "position",
                    LineHeight = (short)n,
                    TextRelation = "relation",
                    Description = "description",
                    ImageId = "iid",
                    Note = "note"
                });
            }

            return elements;
        }

        private static MsDecorationsPart GetPart(int count)
        {
            MsDecorationsPart part = new MsDecorationsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                string alt = n % 2 == 0 ? "even" : "odd";
                part.Decorations.Add(new MsDecoration
                {
                    Id = $"d{n}",
                    Name = "Decoration " + n,
                    Type = n % 2 == 0? "even" : "odd",
                    Date = HistoricalDate.Parse($"{1300 + n}"),
                    Flags = new List<string>(new[] { "f-" + alt }),
                    Place = "Paris",
                    Note = "Note",
                    Artist = new MsDecorationArtist
                    {
                        Id = "artist",
                        Name = "name",
                        Note = "note",
                        Type = "type",
                        Sources = TestHelper.GetDocReferences(2)
                    },
                    References = TestHelper.GetDocReferences(1),
                    Elements = GetElements(3)
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsDecorationsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsDecorationsPart part2 =
                TestHelper.DeserializePart<MsDecorationsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Decorations.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsDecorationsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            TestHelper.AssertValidDataPinNames(pins);

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Entries_Ok()
        {
            MsDecorationsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(13, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "type-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "type-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "artist-id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("artist", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == $"subject-s{n}-count");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
                Assert.Equal("3", pin.Value);

                pin = pins.Find(p => p.Name == $"color-c{n}-count");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
                Assert.Equal("3", pin.Value);

                pin = pins.Find(p => p.Name == "date-value" &&
                    p.Value == $"{1300 + n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
