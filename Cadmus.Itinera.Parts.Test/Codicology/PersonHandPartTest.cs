using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class PersonHandPartTest
    {
        private static List<MsRubrication> GetRubrications(int count)
        {
            List<MsRubrication> rubrications = new List<MsRubrication>();

            for (int n = 1; n <= count; n++)
            {
                rubrications.Add(new MsRubrication
                {
                    Locations = new List<MsLocation>(new[] {
                        new MsLocation
                        {
                            N = n,
                            S = n % 2 == 0 ? "v" : "r",
                            L = n * 5
                        }
                    }),
                    Type = "type",
                    Description = "description",
                    Issues = "issues",
                });
            }

            return rubrications;
        }

        private static List<MsSubscription> GetSubscriptions(int count)
        {
            List<MsSubscription> subscriptions = new List<MsSubscription>();

            for (int n = 1; n <= count; n++)
            {
                subscriptions.Add(new MsSubscription
                {
                    Locations = new List<MsLocation>(new[] {
                        new MsLocation
                        {
                            N = n,
                            S = n % 2 == 0 ? "v" : "r",
                            L = n * 5
                        }
                    }),
                    Language = "lat",
                    Text = "Subscription text"
                });
            }

            return subscriptions;
        }


        private static PersonHandPart GetPart()
        {
            return new PersonHandPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                PersonId = "pusillus",
                Job = "copyst",
                Others = TestHelper.GetDocReferences(1)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            PersonHandPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PersonHandPart part2 =
                TestHelper.DeserializePart<PersonHandPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            // TODO: details
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            PersonHandPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(2, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "person-id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("pusillus", pin.Value);

            pin = pins.Find(p => p.Name == "job");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("copyst", pin.Value);
        }
    }
}
