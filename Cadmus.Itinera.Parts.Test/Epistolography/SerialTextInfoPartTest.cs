using Cadmus.Bricks;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class SerialTextInfoPartTest
    {
        private static SerialTextInfoPart GetPart()
        {
            return new SerialTextInfoPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                TextId = "id-1",
                Authors = new List<CitedPerson>(new[]{
                    new CitedPerson
                    {
                        Name = new PersonName
                        {
                            Language = "lat",
                            Parts = new List<PersonNamePart>(new[]
                            {
                                new PersonNamePart
                                {
                                    Type = "name",
                                    Value = "Pusillus"
                                }
                            })
                        }
                    }
                }),
                CreatorId = "zeus",
                UserId = "another",
                Language = "eng",
                Subject = "Subject",
                Headings = new List<string>(new[] { "Heading" }),
                Recipients = new List<DecoratedId>(new[]
                {
                    new DecoratedId
                    {
                        Id = "alpha",
                        Rank = 1,
                        Tag = "tag",
                        Sources = new List<DocReference>(new[] {
                            new DocReference
                            {
                                Author = "author",
                                Work = "work",
                                Location = "loc"
                            }
                        })
                    }
                }),
                ReplyingTo = new List<DecoratedId>(new[]
                {
                    new DecoratedId
                    {
                        Id = "beta",
                        Rank = 1,
                        Tag = "tag",
                        Sources = new List<DocReference>(new[] {
                            new DocReference
                            {
                                Author = "author",
                                Work = "work",
                                Location = "loc"
                            }
                        })
                    }
                }),
                Note = "A note"
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            SerialTextInfoPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            SerialTextInfoPart part2 =
                TestHelper.DeserializePart<SerialTextInfoPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.TextId, part2.TextId);
            Assert.Equal(part.Authors.Count, part2.Authors.Count);
            Assert.Equal(part.Language, part2.Language);
            Assert.Equal(part.Subject, part2.Subject);
            Assert.Equal(part.Headings?.Count, part2.Headings?.Count);
            Assert.Equal(part.Recipients?.Count, part2.Recipients?.Count);
            Assert.Equal(part.ReplyingTo?.Count, part2.ReplyingTo?.Count);
            Assert.Equal(part.Note, part2.Note);
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            SerialTextInfoPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "id");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("id-1", pin.Value);

            pin = pins.Find(p => p.Name == "language");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("eng", pin.Value);

            pin = pins.Find(p => p.Name == "subject");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("subject", pin.Value);

            pin = pins.Find(p => p.Name == "author");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("pusillus", pin.Value);

            pin = pins.Find(p => p.Name == "heading");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("heading", pin.Value);

            pin = pins.Find(p => p.Name == "recipient");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("alpha", pin.Value);

            pin = pins.Find(p => p.Name == "reply-to");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("beta", pin.Value);
        }
    }
}
