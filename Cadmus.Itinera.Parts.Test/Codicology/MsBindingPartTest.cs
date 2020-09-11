using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsBindingPartTest
    {
        private static MsBindingPart GetPart()
        {
            return new MsBindingPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Century = 13,
                Description = "Description",
                CoverMaterial = "Cover",
                SupportMaterial = "Support",
                Size = new PhysicalSize
                {
                    Tag = "tag",
                    W = new PhysicalDimension { Tag = "t", Value = 21, Unit = "cm"},
                    H = new PhysicalDimension { Tag = "t", Value = 29.7F, Unit = "cm" },
                }
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsBindingPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsBindingPart part2 =
                TestHelper.DeserializePart<MsBindingPart>(json);

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
            MsBindingPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "century");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("13", pin.Value);

            pin = pins.Find(p => p.Name == "cover-mat");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("cover", pin.Value);

            pin = pins.Find(p => p.Name == "support-mat");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("support", pin.Value);

            pin = pins.Find(p => p.Name == "w");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("21.00", pin.Value);

            pin = pins.Find(p => p.Name == "h");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("29.70", pin.Value);
        }
    }
}
