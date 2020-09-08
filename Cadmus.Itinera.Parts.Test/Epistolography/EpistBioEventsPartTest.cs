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
    public sealed class EpistBioEventsPartTest
    {
        private static EpistBioEventsPart GetPart(int count)
        {
            EpistBioEventsPart part = new EpistBioEventsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
            for (int n = 1; n <= count; n++)
            {
                part.Events.Add(new LitBioEvent
                {
                    Type = n % 2 == 0 ? "even" : "odd",
                    Date = HistoricalDate.Parse($"{1200 + n} AD"),
                    Places = new List<string>(new[] { "place-1", "place-2" }),
                    Description = "A description.",
                    Sources = TestHelper.GetCitations(2),
                    Participants = new List<TaggedId>(new[]
                    {
                        new TaggedId { Id = "guy-1", Tag = "tag" },
                        new TaggedId { Id = "guy-2", Tag = "tag" }
                    })
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            EpistBioEventsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            EpistBioEventsPart part2 =
                TestHelper.DeserializePart<EpistBioEventsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Events.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoEvents_Ok()
        {
            EpistBioEventsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Single(pins);
            DataPin pin = pins[0];
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("tot-count", pin.Name);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Events_Ok()
        {
            EpistBioEventsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(10, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "type-odd-count" && p.Value == "2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "type-even-count" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                double expected = HistoricalDate.Parse($"{1200 + n} AD")
                    .GetSortValue();
                pin = pins.Find(p => p.Name == "date-value"
                    && p.Value == expected.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }

            for (int n = 1; n <= 2; n++)
            {
                pin = pins.Find(p => p.Name == "place"
                    && p.Value == $"place{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "participant"
                    && p.Value == $"[tag]guy{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
