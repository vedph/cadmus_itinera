using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsHistoryPartTest
    {
        private static List<MsHistoryPerson> GetPersons(int count)
        {
            List<MsHistoryPerson> persons = new List<MsHistoryPerson>();

            for (int n = 1; n <= count; n++)
            {
                persons.Add(new MsHistoryPerson
                {
                    Role = "owner",
                    Name = new PersonName
                    {
                        Language = "eng",
                        Parts = new List<PersonNamePart>
                        {
                            new PersonNamePart{ Type = "first", Value = "Robert" },
                            new PersonNamePart{ Type = "last", Value = $"Mc{n}" }
                        }
                    },
                    Date = HistoricalDate.Parse((1200 + n)
                        .ToString(CultureInfo.InvariantCulture) + " AD"),
                    Note = "Note",
                    ExternalIds = new List<string>(new[] { "uri" })
                });
            }

            return persons;
        }

        private static List<MsAnnotation> GetAnnotations(int count)
        {
            List<MsAnnotation> annotations = new List<MsAnnotation>();

            for (int n = 1; n <= count; n++)
            {
                annotations.Add(new MsAnnotation
                {
                    Type = n % 2 == 0? "even" : "odd",
                    Text = "Annotation.",
                    Language = "eng",
                    Start = new MsLocation { N = 2, V = true, L = 1 },
                    End = new MsLocation { N = 2, V = true, L = 12 }
                });
            }

            return annotations;
        }

        private static List<MsRestoration> GetRestorations(int count)
        {
            List<MsRestoration> annotations = new List<MsRestoration>();

            for (int n = 1; n <= count; n++)
            {
                annotations.Add(new MsRestoration
                {
                    Type = n % 2 == 0 ? "even" : "odd",
                    Place = "place",
                    Date = HistoricalDate.Parse((1200 + n).ToString() + " AD"),
                    Note = "A note.",
                    Sources = TestHelper.GetDocReferences(2)
                });
            }

            return annotations;
        }

        private static MsHistoryPart GetPart(int count)
        {
            return new MsHistoryPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Provenances = new List<GeoAddress>(new[]
                {
                    new GeoAddress
                    {
                        Area = "Provence",
                        Address = "Toulon, Bibliothéque Civique"
                    }
                }),
                History = "The history.",
                Persons = GetPersons(count),
                Annotations = GetAnnotations(count),
                Restorations = GetRestorations(count)
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsHistoryPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsHistoryPart part2 =
                TestHelper.DeserializePart<MsHistoryPart>(json);

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
            MsHistoryPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(17, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "area");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("provence", pin.Value);

            pin = pins.Find(p => p.Name == "pers-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "ann-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "rest-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pins.Find(p => p.Name == "pers-name"
                    && p.Value == $"robert mc{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                var date = HistoricalDate.Parse(
                    (1200 + n).ToString(CultureInfo.InvariantCulture) + " AD");
                string expectedDate =
                    date.GetSortValue().ToString(CultureInfo.InvariantCulture);

                pin = pins.Find(p => p.Name == "pers-date-value"
                    && p.Value == expectedDate);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);

                pin = pins.Find(p => p.Name == "rest-date-value"
                    && p.Value == expectedDate);
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }

            pin = pins.Find(p => p.Name == "ann-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "ann-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "rest-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "rest-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);
        }
    }
}
