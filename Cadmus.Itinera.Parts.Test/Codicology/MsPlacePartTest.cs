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
                Area = "France",
                City = "Paris",
                Site = "Saint Genevieve",
                Subscriber = "Pusillus",
                SubscriptionLoc = new MsLocation { N = 21, V = true, L = 10 }
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

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "area");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("france", pin.Value);

            pin = pins.Find(p => p.Name == "city");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("paris", pin.Value);

            pin = pins.Find(p => p.Name == "site");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("saint genevieve", pin.Value);

            pin = pins.Find(p => p.Name == "subscriber");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("pusillus", pin.Value);
        }
    }
}
