using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class CorrDedicationsPartTest
    {
        private static CorrDedicationsPart GetPart(int count)
        {
            CorrDedicationsPart part = new CorrDedicationsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                HistoricalDate date = HistoricalDate.Parse(n + 1200 + " AD");
                var dedication = new CorrDedication
                {
                    Title = $"Dedication {n}",
                    Date = date,
                    DateSent = count % 2 == 0? date : null,
                    IsByAuthor = count % 2 == 0,
                };
                for (int j = 1; j <= 2; j++)
                {
                    dedication.Sources.Add(new LitCitation
                    {
                        Author = "Hom.",
                        Work = "Il.",
                        Location = "1.23",
                        Note = $"Note {j}",
                        Tag = j % 2 == 0 ? "even" : "odd"
                    });
                }
                part.Dedications.Add(dedication);
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CorrDedicationsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CorrDedicationsPart part2 =
                TestHelper.DeserializePart<CorrDedicationsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Dedications.Count);
            // TODO: details
        }

        // TODO: check pins here, e.g. for the NotePart we get a single pin
        // when the tag is set, with name=tag and value=tag value:
        // [Fact]
        // public void GetDataPins_NoTag_Empty()
        // {
        //     CorrDedicationsPart part = GetPart();
        //     part.Tag = null;

        //     Assert.Empty(part.GetDataPins());
        // }

        // [Fact]
        // public void GetDataPins_Tag_1()
        // {
        //     CorrDedicationsPart part = GetPart();

        //     List<DataPin> pins = part.GetDataPins().ToList();
        //     Assert.Single(pins);

        //     DataPin pin = pins[0];
        //     Assert.Equal(part.ItemId, pin.ItemId);
        //     Assert.Equal(part.Id, pin.PartId);
        //     Assert.Equal(part.RoleId, pin.RoleId);
        //     Assert.Equal("tag", pin.Name);
        //     Assert.Equal("some-tag", pin.Value);
        // }
    }
}
