using System;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Cadmus.Itinera.Parts.Codicology;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsNumberingsPartTest
    {
        private static MsNumberingsPart GetPart()
        {
            return new MsNumberingsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Era = "ancient",
                System = "roman",
                Technique = "some-technique",
                Century = 13,
                Position = "bottom-center"
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsNumberingsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsNumberingsPart part2 =
                TestHelper.DeserializePart<MsNumberingsPart>(json);

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
            MsNumberingsPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "era");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("ancient", pin.Value);

            pin = pins.Find(p => p.Name == "system");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("roman", pin.Value);

            pin = pins.Find(p => p.Name == "technique");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("some-technique", pin.Value);

            pin = pins.Find(p => p.Name == "century");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("13", pin.Value);

            pin = pins.Find(p => p.Name == "position");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("bottom-center", pin.Value);
        }
    }
}
