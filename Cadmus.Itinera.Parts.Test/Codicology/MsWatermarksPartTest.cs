using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsWatermarksPartTest
    {
        private static MsWatermarksPart GetPart(int count)
        {
            MsWatermarksPart part = new MsWatermarksPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };

            for (int n = 1; n <= count; n++)
            {
                part.Watermarks.Add(new MsWatermark
                {
                    Subject = n % 2 == 0? "even" : "odd",
                    SimilarityRank = n,
                    Description = "A watermark.",
                    Date = HistoricalDate.Parse(n + 1200 + " AD"),
                    Place = $"Place {n}"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsWatermarksPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsWatermarksPart part2 =
                TestHelper.DeserializePart<MsWatermarksPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Watermarks.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoWatermark_Ok()
        {
            MsWatermarksPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Watermarks_Ok()
        {
            MsWatermarksPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(9, pins.Count);

            // tot-count
            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "subject-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "subject-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                // place
                pin = pins.Find(p => p.Name == "place" && p.Value == $"place {n}");
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
