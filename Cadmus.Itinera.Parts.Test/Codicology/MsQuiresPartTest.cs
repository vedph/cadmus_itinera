using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsQuiresPartTest
    {
        private static MsQuiresPart GetPart(int count)
        {
            MsQuiresPart part = new MsQuiresPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Quires.Add(new MsQuire
                {
                    IsNormal = n == 1,
                    StartNr = (short)(n * 4),
                    EndNr = (short)(n * 4 + 3),
                    SheetCount = 4,
                    SheetDelta = 1,
                    Note = "note"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsQuiresPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsQuiresPart part2 =
                TestHelper.DeserializePart<MsQuiresPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Quires.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsQuiresPart part = GetPart(0);

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
            MsQuiresPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(2, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "sheet-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("4", pin.Value);
        }
    }
}
