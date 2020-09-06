using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class CorrExchangesPartTest
    {
        private static List<TaggedId> GetParticipants(int count)
        {
            List<TaggedId> ids = new List<TaggedId>();
            for (int n = 1; n <= count; n++)
            {
                ids.Add(new TaggedId
                {
                    Id = $"guy-{n}",
                    Tag = n % 2 == 0? "even" : "odd"
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
                    IsFromParticipant = n % 2 == 0,
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
                    Sources = TestHelper.GetCitations(2),
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

        // TODO: pins
    }
}
