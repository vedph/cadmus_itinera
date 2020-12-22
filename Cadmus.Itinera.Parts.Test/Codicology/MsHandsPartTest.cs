using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsHandsPartTest
    {
        private static List<MsHandSign> GetSigns(int count)
        {
            List<MsHandSign> signs = new List<MsHandSign>();

            for (int n = 1; n <= count; n++)
            {
                signs.Add(new MsHandSign
                {
                    Id = "s" + n,
                    Type = n % 2 == 0 ? "even" : "odd",
                    Description = "description",
                    ImageId = "s" + n
                });
            }

            return signs;
        }

        private static MsHandsPart GetPart(int count)
        {
            MsHandsPart part = new MsHandsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Hands.Add(new MsHand
                {
                    Id = $"hand-{n}",
                    Types = new List<string>(new[] { $"type-{n}" }),
                    PersonId = $"person-{n}",
                    Description = "Description",
                    Initials = "Initials",
                    Corrections = "Corrections",
                    Punctuation = "Punctuation",
                    Abbreviations = "Abbreviations",
                    IdReason = "Reason",
                    Ranges = new List<MsLocationRange>(new[]
                    {
                        new MsLocationRange
                        {
                            Start = new MsLocation
                            {
                                N = 2,
                                S = n % 2 == 0
                                    ? MsLocationSides.Verso
                                    : MsLocationSides.Recto,
                                L = 3
                            },
                            End = new MsLocation
                            {
                                N = 4,
                                S = n % 2 == 0
                                    ? MsLocationSides.Verso
                                    : MsLocationSides.Recto,
                                L = 5
                            }
                        }
                    }),
                    ExtentNote = "Extent",
                    Rubrications = new List<MsRubrication>(new[]
                    {
                        new MsRubrication
                        {
                            Type = "type",
                            Location = new MsLocation
                            {
                                N = 2,
                                S = n % 2 == 0? MsLocationSides.Verso : MsLocationSides.Recto,
                                L = 3
                            },
                            Description = "Description",
                            Issues = "Issues"
                        }
                    }),
                    Subscription = new MsSubscription
                    {
                        Location = new MsLocation
                        {
                            N = 4,
                            S = n % 2 == 0? MsLocationSides.Verso : MsLocationSides.Recto,
                            L = 1
                        },
                        Language = "eng",
                        Text = "Text"
                    },
                    Signs = GetSigns(1),
                    ImageIds = new List<string>(new string[] { "draco" })
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsHandsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsHandsPart part2 =
                TestHelper.DeserializePart<MsHandsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Hands.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsHandsPart part = GetPart(0);

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
            MsHandsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "id" && p.Value == $"hand-{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "person-id" && p.Value == $"person-{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "type" && p.Value == $"type-{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
