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
                part.Hands.Add(new MsHandInstance
                {
                    Id = $"hand-{n}",
                    IdReason = "Reason",
                    Start = new MsLocation { N = 2, V = true, L = 3 },
                    End = new MsLocation { N = 4, V = true, L = 5 },
                    ExtentNote = "Extent",
                    Rubrications = new List<MsRubrication>(new[]
                    {
                        new MsRubrication
                        {
                            Type = "type",
                            Location = new MsLocation { N = 2, V = true, L = 3 },
                            Description = "Description",
                            Issues = "Issues"
                        }
                    }),
                    Subscriptions = new List<MsSubscription>(new[]
                    {
                        new MsSubscription
                        {
                            Location = new MsLocation { N = 4, V = true, L = 1 },
                            Language = "eng",
                            Text = "Text"
                        }
                    })
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

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "id" && p.Value == $"hand-{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
