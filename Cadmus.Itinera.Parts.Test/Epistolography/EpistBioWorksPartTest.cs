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
    public sealed class EpistBioWorksPartTest
    {
        private static EpistBioWorksPart GetPart(int count)
        {
            EpistBioWorksPart part = new EpistBioWorksPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
            for (int n = 1; n <= count; n++)
            {
                part.Works.Add(new LitBioWork
                {
                    Genre = n % 2 == 0? "even" : "odd",
                    Language = "eng",
                    Title = $"title {n}",
                    Rank = 1,
                    IsLost = n % 2 == 0,
                    Date = HistoricalDate.Parse($"{1200 + n} AD"),
                    ExternalIds = new List<string>(new[] { "alpha", "beta" })
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            EpistBioWorksPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            EpistBioWorksPart part2 =
                TestHelper.DeserializePart<EpistBioWorksPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Works.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoWorks_Ok()
        {
            EpistBioWorksPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("tot-count", pin.Name);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Works_Ok()
        {
            EpistBioWorksPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(8, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "language" && p.Value == "eng");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "title"
                    && p.Value == $"title {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                double expected = HistoricalDate.Parse($"{1200 + n} AD")
                    .GetSortValue();
                pin = pins.Find(p => p.Name == "date-value"
                    && p.Value == expected.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }

    }
}
