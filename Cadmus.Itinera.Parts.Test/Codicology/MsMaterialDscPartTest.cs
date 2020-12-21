using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsMaterialDscPartTest
    {
        private static MsMaterialDscPart GetPart()
        {
            return new MsMaterialDscPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Material = "material",
                Format = "format",
                State = "state",
                StateNote = "A note about state.",
                Counts = new List<DecoratedCount>(new[]
                    {
                    new DecoratedCount
                    {
                        Id = "sheets", Value = 24, Note = "Note"
                    },
                    new DecoratedCount
                    {
                        Id = "guard-sheets", Value = 2, Note = "Note"
                    }
                }),
                Palimpsests = new List<MsPalimpsest>(new[]
                {
                    new MsPalimpsest
                    {
                        Location = new MsLocation
                        {
                            N = 3,
                            S = MsLocationSides.Verso,
                            L = 1
                        },
                        Date = HistoricalDate.Parse("1200 AD"),
                        Note = "Note"
                    }
                })
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsMaterialDscPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsMaterialDscPart part2 =
                TestHelper.DeserializePart<MsMaterialDscPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
            // TODO: check parts data here...
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            MsMaterialDscPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(6, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "material");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("material", pin.Value);

            pin = pins.Find(p => p.Name == "format");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("format", pin.Value);

            pin = pins.Find(p => p.Name == "state");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("state", pin.Value);

            pin = pins.Find(p => p.Name == "palimpsest-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "c-sheets-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("24", pin.Value);

            pin = pins.Find(p => p.Name == "c-guard-sheets-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);
        }
    }
}
