using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsMeasurementsPartTest
    {
        private static List<PhysicalMeasurement> GetMeasurements(int count)
        {
            List<PhysicalMeasurement> measurements =
                new List<PhysicalMeasurement>();

            for (int n = 1; n <= count; n++)
            {
                measurements.Add(new PhysicalMeasurement
                {
                    Id = new string((char)('a' + n - 1), 1),
                    IsApproximate = n % 2 == 0,
                    IsIncomplete = n % 2 == 0,
                    Unit = "cm",
                    Value = n
                });
            }

            return measurements;
        }

        private static List<DecoratedCount> GetCounts(int count)
        {
            List<DecoratedCount> counts = new List<DecoratedCount>();

            for (int n = 1; n <= count; n++)
            {
                counts.Add(new DecoratedCount
                {
                    Id = new string((char)('a' + n - 1), 1),
                    Value = n,
                    Note = "A note."
                });
            }

            return counts;
        }

        private static MsMeasurementsPart GetPart(int count)
        {
            return new MsMeasurementsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Measurements = GetMeasurements(count),
                Counts = GetCounts(count)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsMeasurementsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsMeasurementsPart part2 =
                TestHelper.DeserializePart<MsMeasurementsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Measurements.Count);
            Assert.Equal(2, part.Counts.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_Empty_NoPin()
        {
            MsMeasurementsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Empty(pins);
        }

        [Fact]
        public void GetDataPins_NotEmpty_Ok()
        {
            MsMeasurementsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(6, pins.Count);

            for (int n = 1; n <= 3; n++)
            {
                string id = new string((char)('a' + n - 1), 1);

                DataPin pin = pins.Find(p => p.Name == "m-" + id
                    && p.Value == n.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "count-id" && p.Value == id);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
