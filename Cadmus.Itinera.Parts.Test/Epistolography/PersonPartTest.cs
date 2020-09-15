using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class PersonPartTest
    {
        private static PersonPart GetPart()
        {
            return new PersonPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                PersonId = "dillinger",
                ExternalIds = new List<string>(
                    new[] { "dillinger", "villains.com/dillinger"}),
                Names = new List<PersonName>(new[]{
                    new PersonName
                    {
                        Language = "eng",
                        Tag = "tag",
                        Parts = new List<PersonNamePart>(
                            new[]
                            {
                                new PersonNamePart
                                {
                                    Type = "first",
                                    Value = "John"
                                },
                                new PersonNamePart
                                {
                                    Type = "last",
                                    Value = "Dillinger"
                                }
                            })
                    }
                }),
                Sex = 'M',
                BirthDate = HistoricalDate.Parse("1903"),
                BirthPlace = "Indianapolis",
                DeathDate = HistoricalDate.Parse("1934"),
                DeathPlace = "Chicago",
                Bio = "The bio."
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            PersonPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PersonPart part2 =
                TestHelper.DeserializePart<PersonPart>(json);

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
            PersonPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "person-id");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("dillinger", pin.Value);

            pin = pins.Find(p => p.Name == "ext-id" && p.Value == "dillinger");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            pin = pins.Find(p => p.Name == "ext-id" && p.Value == "villains.com/dillinger");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "name");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("john dillinger", pin.Value);

            pin = pins.Find(p => p.Name == "sex");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("M", pin.Value);

            pin = pins.Find(p => p.Name == "birth-date-value");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1903", pin.Value);

            pin = pins.Find(p => p.Name == "birth-place");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("indianapolis", pin.Value);

            pin = pins.Find(p => p.Name == "death-date-value");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1934", pin.Value);

            pin = pins.Find(p => p.Name == "death-place");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("chicago", pin.Value);

            pin = pins.Find(p => p.Name == "bio-length");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("8", pin.Value);
        }
    }
}
