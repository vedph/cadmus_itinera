using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Codicology
{
    public sealed class MsContentLociPartTest
    {
        private static MsContentLociPart GetPart(int count)
        {
            MsContentLociPart part = new MsContentLociPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Loci.Add(new MsContentLocus
                {
                    Citation = $"{n}.1",
                    Text = $"text {n}"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsContentLociPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            MsContentLociPart part2 =
                TestHelper.DeserializePart<MsContentLociPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Loci.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoLoci_Ok()
        {
            MsContentLociPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Loci_Ok()
        {
            MsContentLociPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "citation" && p.Value == $"{n}.1");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
