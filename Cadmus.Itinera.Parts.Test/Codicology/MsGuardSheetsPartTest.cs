using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsGuardSheetsPartTest
    {
        private static MsGuardSheetsPart GetPart(int count)
        {
            MsGuardSheetsPart part = new MsGuardSheetsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.GuardSheets.Add(new MsGuardSheet
                {
                    IsBack = n % 2 == 0,
                    Material = n % 2 == 0? "parchment" : "paper",
                    Date = HistoricalDate.Parse(n + 1200 + " AD"),
                    Note = "A note."
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsGuardSheetsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsGuardSheetsPart part2 =
                TestHelper.DeserializePart<MsGuardSheetsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.GuardSheets.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoSheet_Ok()
        {
            MsGuardSheetsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Sheets_Ok()
        {
            MsGuardSheetsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(8, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "back-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "front-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "material-parchment-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "material-paper-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "date-value"
                    && p.Value == HistoricalDate.Parse(n + 1200 + " AD")
                    .GetSortValue().ToString());
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
