using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class CorrPseudonymsPartTest
    {
        private static CorrPseudonymsPart GetPart(int count)
        {
            CorrPseudonymsPart part = new CorrPseudonymsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
            for (int n = 1; n <= count; n++)
            {
                part.Pseudonyms.Add(new CorrPseudonym
                {
                    Language = "eng",
                    Value = $"pseudo-{n}",
                    IsAuthor = n % 2 == 0,
                    Sources = TestHelper.GetDocReferences(2)
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            CorrPseudonymsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            CorrPseudonymsPart part2 =
                TestHelper.DeserializePart<CorrPseudonymsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Pseudonyms.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoPseudonyms_Ok()
        {
            CorrPseudonymsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            TestHelper.AssertValidDataPinNames(pins);

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            Assert.Equal("0", pin.Value);
            TestHelper.AssertPinIds(part, pin);
        }

        [Fact]
        public void GetDataPins_Pseudonyms_Ok()
        {
            CorrPseudonymsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(3 + 1, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "pseudonym"
                && p.Value == "-pseudo1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "pseudonym"
                && p.Value == "+pseudo2");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "pseudonym"
                && p.Value == "-pseudo3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
        }
    }
}
