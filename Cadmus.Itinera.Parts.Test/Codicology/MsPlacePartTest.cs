using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsPlacePartTest
    {
        private static MsPlacePart GetPart()
        {
            return new MsPlacePart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Area = "Provence",
                Address = "Toulon, Bibliothéque Civique",
                Subscriber = "Pusillus",
                SubscriptionLoc = new MsLocation
                {
                    N = 21,
                    S = "v",
                    L = 10
                }
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsPlacePart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsPlacePart part2 =
                TestHelper.DeserializePart<MsPlacePart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            // TODO: details
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            MsPlacePart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "area");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("provence", pin.Value);

            pin = pins.Find(p => p.Name == "address");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("toulon bibliotheque civique", pin.Value);

            pin = pins.Find(p => p.Name == "address-1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("toulon", pin.Value);

            pin = pins.Find(p => p.Name == "address-2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("bibliotheque civique", pin.Value);

            pin = pins.Find(p => p.Name == "subscriber");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("pusillus", pin.Value);
        }
    }
}
