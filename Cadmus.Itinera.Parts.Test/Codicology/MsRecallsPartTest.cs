using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsRecallsPartTest
    {
        private static MsRecallsPart GetPart(int count)
        {
            MsRecallsPart part = new MsRecallsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Recalls.Add(new MsRecall
                {
                    Position = n % 2 == 0? "right" : "bottom",
                    IsVertical = n % 2 == 0,
                    Decoration = "Decoration",
                    Register = "Register"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsRecallsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsRecallsPart part2 =
                TestHelper.DeserializePart<MsRecallsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Recalls.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoRecalls_Ok()
        {
            MsRecallsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Recalls_Ok()
        {
            MsRecallsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "pos-bottom-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "pos-right-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "vrt-0-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "vrt-1-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);
        }
    }
}
