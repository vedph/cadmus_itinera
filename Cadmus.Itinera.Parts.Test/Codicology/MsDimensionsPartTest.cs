using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsDimensionsPartTest
    {
        private static List<PhysicalDimension> GetDimensions(int count)
        {
            List<PhysicalDimension> measurements =
                new List<PhysicalDimension>();

            for (int n = 1; n <= count; n++)
            {
                measurements.Add(new PhysicalDimension
                {
                    Tag = new string((char)('a' + n - 1), 1),
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

        private static MsDimensionsPart GetPart(int count)
        {
            return new MsDimensionsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Dimensions = GetDimensions(count),
                Counts = GetCounts(count)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsDimensionsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsDimensionsPart part2 =
                TestHelper.DeserializePart<MsDimensionsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Dimensions.Count);
            Assert.Equal(2, part.Counts.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_Empty_NoPin()
        {
            MsDimensionsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Empty(pins);
        }

        [Fact]
        public void GetDataPins_NotEmpty_Ok()
        {
            MsDimensionsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(6, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            for (int n = 1; n <= 3; n++)
            {
                string id = new string((char)('a' + n - 1), 1);

                DataPin pin = pins.Find(p => p.Name == "d." + id
                    && p.Value == n.ToString("00.00", CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "count-id" && p.Value == id);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
