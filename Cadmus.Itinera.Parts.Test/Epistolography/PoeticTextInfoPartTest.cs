using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class PoeticTextInfoPartTest
    {
        private static List<CitedPerson> GetAuthors(int count)
        {
            List<CitedPerson> authors = new List<CitedPerson>();

            for (int n = 1; n <= count; n++)
            {
                string name = new string((char)('A' + n - 1), 3);

                authors.Add(new CitedPerson
                {
                    Name = new PersonName
                    {
                        Language = "eng",
                        Parts = new List<PersonNamePart>(new[]
                        {
                            new PersonNamePart{ Type = "last", Value = name}
                        })
                    },
                    Ids = new List<DecoratedId>(new[]
                    {
                        new DecoratedId
                        {
                            Id = $"author {n}",
                            Rank = (short)n,
                            Tag = "tag",
                            Sources = TestHelper.GetDocReferences(2)
                        }
                    })
                });
            }

            return authors;
        }

        private static PoeticTextInfoPart GetPart()
        {
            return new PoeticTextInfoPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Language = "eng",
                Subject = "The Subject",
                Metre = "metre",
                Authors = GetAuthors(2),
                Related = TestHelper.GetDocReferences(2)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            PoeticTextInfoPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            PoeticTextInfoPart part2 =
                TestHelper.DeserializePart<PoeticTextInfoPart>(json);

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
            PoeticTextInfoPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "language");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("eng", pin.Value);

            pin = pins.Find(p => p.Name == "subject");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("the subject", pin.Value);

            pin = pins.Find(p => p.Name == "metre");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("metre", pin.Value);

            for (int n = 1; n <= 2; n++)
            {
                string name = new string((char)('a' + n - 1), 3);

                pin = pins.Find(p => p.Name == "author-name"
                    && p.Value == name);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "author-id"
                    && p.Value == $"{n}:author {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
