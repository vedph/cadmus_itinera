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
    public sealed class EpistCoordsPartTest
    {
        private static EpistCoordsPart GetPart(int count)
        {
            EpistCoordsPart part = new EpistCoordsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                var coords = new EpistCoords
                {
                    Tag = n % 2 == 0? "even" : "odd",
                    Place = $"place {n}",
                    Date = date,
                    TextDate = $"text date {n}",
                    Sources = TestHelper.GetDocReferences(2)
                };
                part.Coordinates.Add(coords);
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            EpistCoordsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            EpistCoordsPart part2 =
                TestHelper.DeserializePart<EpistCoordsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Coordinates.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoCoordinates_Ok()
        {
            EpistCoordsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Coordinates_Ok()
        {
            EpistCoordsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(12, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "tag-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "tag-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "tag-place" && p.Value == "[odd]place 1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "tag-place" && p.Value == "[even]place 2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "tag-place" && p.Value == "[odd]place 3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                double d = date.GetSortValue();

                pin = pins.Find(p => p.Name == "date-value"
                    && p.Value == d.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                string tag = n % 2 == 0 ? "even" : "odd";
                pin = pins.Find(p => p.Name == "tag-date"
                    && p.Value == $"[{tag}]{+d:0000.00;-d:0000.00}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
