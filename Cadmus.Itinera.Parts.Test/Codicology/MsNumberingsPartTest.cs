using System;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Cadmus.Itinera.Parts.Codicology;
using System.Globalization;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsNumberingsPartTest
    {
        private static MsNumberingsPart GetPart(int count)
        {
            MsNumberingsPart part = new MsNumberingsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };

            for (int n = 1; n <= count; n++)
            {
                string eo = n % 2 == 0 ? "even" : "odd";
                part.Numberings.Add(new MsNumbering
                {
                    Era = eo,
                    System = "s-" + eo,
                    Technique = "t-" + eo,
                    Century = (short)(10 + n),
                    Position = $"pos-{n}"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsNumberingsPart part = GetPart(2);

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
        public void GetDataPins_NoNumberings_Ok()
        {
            MsNumberingsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);

            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Numberings_Ok()
        {
            MsNumberingsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(13, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "era-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "era-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "sys-s-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "sys-s-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "tech-t-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "tech-t-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "century"
                    && p.Value == (n + 10).ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "position" && p.Value == $"pos-{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
