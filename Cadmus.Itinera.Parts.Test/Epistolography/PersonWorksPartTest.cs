using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class PersonWorksPartTest
    {
        public static PersonWorksPart GetPart(int count)
        {
            PersonWorksPart part = new PersonWorksPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };

            for (int n = 1; n <= count; n++)
            {
                part.Works.Add(new PersonWork
                {
                    Titles = new List<string>(new[] { $"title-{n}" }),
                    Genre = (n & 1) == 1 ? "odd" : "even",
                    IsDubious = n == 3,
                    IsLost = n == 3,
                    Language = (n & 1) == 1 ? "eng" : "ita",
                    Note = "note",
                    Chronotopes = TestHelper.GetChronotopes(2),
                    References = TestHelper.GetDocReferences(2)
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            PersonWorksPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            PersonWorksPart part2 =
                TestHelper.DeserializePart<PersonWorksPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Works.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoWorks_Ok()
        {
            PersonWorksPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            TestHelper.AssertValidDataPinNames(pins);

            Assert.Equal(3, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);

            pin = pins.Find(p => p.Name == "dubious-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);

            pin = pins.Find(p => p.Name == "lost-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Works_Ok()
        {
            PersonWorksPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            // counts
            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "dubious-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "lost-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            // genre
            pin = pins.Find(p => p.Name == "genre" && p.Value == "odd");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "genre" && p.Value == "even");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // language
            pin = pins.Find(p => p.Name == "language" && p.Value == "eng");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "language" && p.Value == "ita");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            for (int n = 1; n <= 3; n++)
            {
                // title
                pin = pins.Find(p => p.Name == "title" && p.Value == $"title{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
