using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsSignaturesPartTest
    {
        private static MsSignaturesPart GetPart(int count)
        {
            MsSignaturesPart part = new MsSignaturesPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Signatures.Add(new MsSignature
                {
                    Tag = n % 2 == 0? "even" : "odd",
                    City = "Rome",
                    Library = $"Library {n}",
                    Fund = "A",
                    Location = $"A.{n}"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsSignaturesPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsSignaturesPart part2 =
                TestHelper.DeserializePart<MsSignaturesPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Signatures.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoSignature_Ok()
        {
            MsSignaturesPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Signatures_Ok()
        {
            MsSignaturesPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(7, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "tag-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "tag-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);

            pin = pins.Find(p => p.Name == "city");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("rome", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                // library
                pin = pins.Find(p => p.Name == "library" && p.Value == $"library {n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }

    }
}
