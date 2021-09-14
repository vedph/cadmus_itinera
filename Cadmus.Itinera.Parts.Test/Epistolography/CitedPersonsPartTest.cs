using Cadmus.Bricks;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class CitedPersonsPartTest
    {
        private static CitedPersonsPart GetPart(int count)
        {
            CitedPersonsPart part = new CitedPersonsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                var person = new CitedPerson
                {
                    Name = new PersonName
                    {
                        Language = "eng",
                        Tag = "tag",
                        Parts = new List<PersonNamePart>(new[]
                        {
                            new PersonNamePart{ Type = "first", Value = "Joe" },
                            new PersonNamePart{ Type = "last", Value =
                                new string((char)('A' + n - 1), 3)}
                        })
                    },
                    Ids = new List<DecoratedId>(new[]
                    {
                        new DecoratedId
                        {
                            Id = $"person {n}",
                            Tag = "tag",
                            Rank = 1,
                            Sources = TestHelper.GetDocReferences(2)
                        }
                    }),
                    Sources = TestHelper.GetDocReferences(2)
                };
                part.Persons.Add(person);
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CitedPersonsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CitedPersonsPart part2 =
                TestHelper.DeserializePart<CitedPersonsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Persons.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoPersons_Ok()
        {
            CitedPersonsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            TestHelper.AssertValidDataPinNames(pins);

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Persons_Ok()
        {
            CitedPersonsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            // tot-count
            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                string name = "joe " + new string((char)('a' + n - 1), 3);
                pin = pins.Find(p => p.Name == "name" && p.Value == name);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "id" && p.Value == $"person {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
