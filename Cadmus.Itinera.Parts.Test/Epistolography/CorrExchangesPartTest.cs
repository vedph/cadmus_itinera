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
    public sealed class CorrExchangesPartTest
    {
        private static List<DecoratedId> GetParticipants(int count)
        {
            List<DecoratedId> ids = new List<DecoratedId>();
            for (int n = 1; n <= count; n++)
            {
                ids.Add(new DecoratedId
                {
                    Id = $"guy-{n}",
                    Tag = n % 2 == 0? "even" : "odd",
                    Rank = 1
                });
            }
            return ids;
        }

        private static List<EpistAttachment> GetAttachments(int count)
        {
            List<EpistAttachment> attachments = new List<EpistAttachment>();

            for (int n = 1; n <= count; n++)
            {
                attachments.Add(new EpistAttachment
                {
                    Type = n % 2 == 0? "even" : "odd",
                    Name = $"attachment-{n}",
                    Portion = "1-2",
                    Note = "A note"
                });
            }

            return attachments;
        }

        private static CorrExchangesPart GetPart(int count)
        {
            CorrExchangesPart part = new CorrExchangesPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };

            for (int n = 1; n <= count; n++)
            {
                part.Exchanges.Add(new CorrExchange
                {
                    IsDubious = n % 2 == 0,
                    IsIndirect = n % 2 == 0,
                    IsFromParticipant = n % 2 != 0,
                    From = new EpistDatePlace
                    {
                        Date = HistoricalDate.Parse(n + 1200 + " AD"),
                        Place = (n % 2 == 0? "Even" : "Odd") + " town"
                    },
                    To = new EpistDatePlace
                    {
                        Date = HistoricalDate.Parse(n + 1201 + " AD"),
                        Place = (n % 2 == 0 ? "Even" : "Odd") + " lake"
                    },
                    Participants = GetParticipants(2),
                    Sources = TestHelper.GetDocReferences(2),
                    Attachments = GetAttachments(2)
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CorrExchangesPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CorrExchangesPart part2 =
                TestHelper.DeserializePart<CorrExchangesPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Exchanges.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoExchange_Ok()
        {
            CorrExchangesPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Exchanges_Ok()
        {
            CorrExchangesPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(19, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "dubious-count");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "indirect-count");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "incoming-count");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                double value = date.GetSortValue();
                pin = pins.Find(p => p.Name == "from-date-value"
                    && p.Value == value.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                date = HistoricalDate.Parse(n + 1201 + " AD");
                value = date.GetSortValue();
                pin = pins.Find(p => p.Name == "to-date-value"
                    && p.Value == value.ToString(CultureInfo.InvariantCulture));
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }

            pin = pins.Find(p => p.Name == "from-place" && p.Value == "even town");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "from-place" && p.Value == "odd town");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "to-place" && p.Value == "even lake");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "to-place" && p.Value == "odd lake");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 2; n++)
            {
                pin = pins.Find(p =>
                    p.Name == $"participant.{(n % 2 == 0 ? "even" : "odd")}"
                    && p.Value == $"guy{n}");
                TestHelper.AssertPinIds(part, pin);
            }

            pin = pins.Find(p => p.Name == "att-odd-count" && p.Value == "3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "att-even-count" && p.Value == "3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "att-tot-count" && p.Value == "6");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
