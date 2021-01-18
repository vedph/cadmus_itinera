using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsLayoutsPartTest
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

        private static MsLayoutsPart GetPart(int count)
        {
            List<MsLayout> layouts = new List<MsLayout>();
            for (int n = 1; n <= count; n++)
            {
                layouts.Add(new MsLayout
                {
                    Sample = new MsLocation
                    {
                        N = n,
                        S = "r"
                    },
                    ColumnCount = n,
                    RulingTechnique = "ruling",
                    Counts = GetCounts(1),
                    Dimensions = GetDimensions(1),
                });
            }

            return new MsLayoutsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Layouts = layouts
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsLayoutsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsLayoutsPart part2 =
                TestHelper.DeserializePart<MsLayoutsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Layouts.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_Empty_NoPin()
        {
            MsLayoutsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Empty(pins);
        }

        [Fact]
        public void GetDataPins_NotEmpty_Ok()
        {
            MsLayoutsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(5, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "d.a"
                && p.Value == "01.0");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "ruling" && p.Value == "ruling");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "cols" && p.Value == $"{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
