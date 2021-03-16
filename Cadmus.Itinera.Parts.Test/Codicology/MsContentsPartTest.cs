using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsContentsPartTest
    {
        private static List<MsContent> GetContents(int count)
        {
            List<MsContent> contents = new List<MsContent>();

            for (int n = 1; n <= count; n++)
            {
                string suffix = n % 2 == 0 ? "even" : "odd";

                contents.Add(new MsContent
                {
                    Author = $"author-{suffix}",
                    ClaimedAuthor = $"claimed-{suffix}",
                    Work = $"work-{suffix}",
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
                    State = "state",
                    Note = "note",
                    Units = new List<MsContentUnit>(new[]
                    {
                        new MsContentUnit
                        {
                            Label = "label",
                            Incipit = "incipit liber alicuius",
                            Explicit = "explicit liber alicuius"
                        }
                    })
                });
            }

            return contents;
        }

        private static MsContentsPart GetPart(int count)
        {
            return new MsContentsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Contents = GetContents(count)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsContentsPart part = GetPart(3);

            string json = TestHelper.SerializePart(part);
            MsContentsPart part2 = TestHelper.DeserializePart<MsContentsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
            Assert.NotEmpty(part.Contents);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoContents_Ok()
        {
            MsContentsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            TestHelper.AssertValidDataPinNames(pins);

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Contents_Ok()
        {
            MsContentsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "author" && p.Value == "authorodd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "author" && p.Value == "authoreven");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "claimed-author"
                && p.Value == "claimedodd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "claimed-author"
                && p.Value == "claimedeven");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "work" && p.Value == "workodd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "work" && p.Value == "workeven");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
