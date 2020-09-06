using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class EpistBioEventsPartTest
    {
        private static List<LitCitedPlace> GetPlaces(int count)
        {
            List<LitCitedPlace> events = new List<LitCitedPlace>();
            for (int n = 1; n <= count; n++)
            {
                events.Add(new LitCitedPlace
                {
                    Name = $"Place-{n}",
                    Idents = new List<RankedIdent>
                    {
                        new RankedIdent
                        {
                            Id = $"id-{n}",
                            Rank = n % 2 == 0? 1:0
                        }
                    },
                    Sources = TestHelper.GetCitations(2)
                });
            }
            return events;
        }

        private static List<LitCitedPerson> GetPersons(int count)
        {
            List<LitCitedPerson> persons = new List<LitCitedPerson>();

            for (int n = 1; n <= count; n++)
            {
                persons.Add(new LitCitedPerson
                {
                    Name = new PersonName
                    {
                        Language = "eng",
                        Parts = new List<PersonNamePart>
                        {
                            new PersonNamePart
                            {
                                Type = "name",
                                Value = $"name-{n}"
                            }
                        },
                        Tag = "tag"
                    },
                    Idents = new List<RankedIdent>
                    {
                        new RankedIdent
                        {
                            Id = $"id-{n}",
                            Rank = n % 2 == 0? 1:0
                        }
                    },
                    Sources = TestHelper.GetCitations(2)
                });
            }

            return persons;
        }

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
                    Date = HistoricalDate.Parse("{1200 + n} AD"),
                    Places = GetPlaces(2),
                    Description = "A description.",
                    Sources = TestHelper.GetCitations(2),
                    Participants = GetPersons(2)
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
        public void GetDataPins_NoEvents_Empty()
        {
            EpistBioEventsPart part = GetPart(0);

            Assert.Empty(part.GetDataPins(null));
        }

    }
}
