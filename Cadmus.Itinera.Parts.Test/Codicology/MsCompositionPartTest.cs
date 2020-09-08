using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsCompositionPartTest
    {
        private static MsCompositionPart GetPart(int sectionCount)
        {
            MsCompositionPart part = new MsCompositionPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                SheetCount = 10,
                GuardSheetCount = 2
            };

            for (int n = 1; n <= sectionCount; n++)
            {
                part.Sections.Add(new MsSection
                {
                    Tag = n % 2 == 0 ? "even" : "odd",
                    Label = $"Section {n}",
                    Date = HistoricalDate.Parse(n + 1200 + " AD"),
                    Start = new MsLocation
                    {
                        N = n,
                        V = n % 2 != 0,
                        L = 1
                    }
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsCompositionPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsCompositionPart part2 =
                TestHelper.DeserializePart<MsCompositionPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Sections.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            MsCompositionPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(11, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "sheet-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("10", pin.Value);

            pin = pins.Find(p => p.Name == "guard-sheet-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "section-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "section-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "section-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "section-label"
                    && p.Value == $"section {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "section-date-value"
                    && p.Value == HistoricalDate.Parse(n + 1200 + " AD")
                    .GetSortValue().ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
