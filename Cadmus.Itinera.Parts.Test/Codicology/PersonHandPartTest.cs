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
                    Location = new MsLocation
                    {
                        N = (short)n,
                        V = n % 2 == 0,
                        L = (short)(n * 5)
                    },
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
                    Location = new MsLocation
                    {
                        N = (short)n,
                        V = n % 2 == 0,
                        L = (short)(n * 5)
                    },
                    Language = "lat",
                    Text = "Subscription text"
                });
            }

            return subscriptions;
        }

        private static List<MsHandSign> GetSigns(int count)
        {
            List<MsHandSign> signs = new List<MsHandSign>();

            for (int n = 1; n <= count; n++)
            {
                signs.Add(new MsHandSign
                {
                    Id = "s" + n,
                    Type = n % 2 == 0 ? "even" : "odd",
                    Description = "description",
                    ImageId = "s" + n
                });
            }

            return signs;
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
                Type = "capital",
                ExtentNote = "Extent note",
                Description = "Description",
                Initials = "Initials",
                Corrections = "Corrections",
                Punctuation = "Punctuation",
                Abbreviations = "Abbreviations",
                Rubrications = GetRubrications(2),
                Subscriptions = GetSubscriptions(2),
                ImageIds = new List<string>(new string[] { "draco" }),
                Signs = GetSigns(3)
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

            Assert.Equal(8, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("pusillus", pin.Value);

            pin = pins.Find(p => p.Name == "job");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("copyst", pin.Value);

            pin = pins.Find(p => p.Name == "type");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("capital", pin.Value);

            pin = pins.Find(p => p.Name == "img-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "sign-tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == $"sign-s{n}-count");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
                Assert.Equal("1", pin.Value);
            }
        }
    }
}
