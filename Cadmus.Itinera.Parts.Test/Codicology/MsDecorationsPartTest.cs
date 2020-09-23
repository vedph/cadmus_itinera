using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsDecorationsPartTest
    {
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
                part.Decorations.Add(new MsDecoration
                {
                    Type = n % 2 == 0? "even" : "odd",
                    Subject = $"s{n}",
                    Colors = new List<string>(new[]{ $"c{n}" }),
                    Layout = $"l{n}",
                    Tool = $"t{n}",
                    Start = new MsLocation { N = 2, V = true, L = 1 },
                    End = new MsLocation { N = 4, V = true, L = 12 },
                    Position = "position",
                    Size = new PhysicalSize
                    {
                        W = new PhysicalDimension { Value = n, Unit = "cm" },
                        H = new PhysicalDimension { Value = n * 2, Unit = "cm" }
                    },
                    Description = "description",
                    TextRelation = "text relation",
                    Tag = n % 2 == 0 ? "even" : "odd",
                    GuideLetters = new List<MsGuideLetter>(new[]
                    {
                        new MsGuideLetter
                        {
                            Morphology = "morphology", Position = "position"
                        }
                    }),
                    ImageId = "imageid",
                    Artist = new MsDecorationArtist
                    {
                        Id = "artist",
                        Name = "name",
                        Note = "note",
                        Type = "type",
                        Sources = TestHelper.GetDocReferences(2)
                    }
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

            pin = pins.Find(p => p.Name == "tag-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "tag-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "golden-count");
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
                Assert.Equal("1", pin.Value);

                pin = pins.Find(p => p.Name == $"color-c{n}-count");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
                Assert.Equal("1", pin.Value);
            }
        }
    }
}
