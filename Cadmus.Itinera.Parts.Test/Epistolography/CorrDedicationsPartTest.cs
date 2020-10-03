using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class CorrDedicationsPartTest
    {
        private static CorrDedicationsPart GetPart(int count)
        {
            CorrDedicationsPart part = new CorrDedicationsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                var dedication = new CorrDedication
                {
                    Title = $"Dedication {n}",
                    Date = date,
                    DateSent = n % 2 == 0? date : null,
                    IsByAuthor = n % 2 == 0,
                    Sources = TestHelper.GetDocReferences(2)
                };
                part.Dedications.Add(dedication);
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CorrDedicationsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CorrDedicationsPart part2 =
                TestHelper.DeserializePart<CorrDedicationsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Dedications.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoDedication_Ok()
        {
            CorrDedicationsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            TestHelper.AssertValidDataPinNames(pins);

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Dedications_Ok()
        {
            CorrDedicationsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(9, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            // auth-count
            DataPin pin = pins.Find(p => p.Name == "auth-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            // corr-count
            pin = pins.Find(p => p.Name == "corr-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            // tot-count
            pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                // title
                pin = pins.Find(p => p.Name == "title" && p.Value == $"dedication {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                // date-value
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                double value = date.GetSortValue();
                pin = pins.Find(p => p.Name == "date-value"
                    && p.Value == value.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
