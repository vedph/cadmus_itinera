using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class EpistAttachmentsPartTest
    {
        public static EpistAttachmentsPart GetPart(int count)
        {
            EpistAttachmentsPart part = new EpistAttachmentsPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };

            for (int n = 1; n <= count; n++)
            {
                part.Attachments.Add(new EpistAttachment
                {
                    Type = n % 2 == 0 ? "even" : "odd",
                    Name = $"Attachment {n}",
                    Portion = "portion",
                    Note = "A note"
                });
            }

            return part;
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            EpistAttachmentsPart part = GetPart(2);

            string json = TestHelper.SerializePart(part);
            EpistAttachmentsPart part2 =
                TestHelper.DeserializePart<EpistAttachmentsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(2, part.Attachments.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoAttachments_Ok()
        {
            EpistAttachmentsPart part = GetPart(0);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Attachments_Ok()
        {
            EpistAttachmentsPart part = GetPart(3);

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(3, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            pin = pins.Find(p => p.Name == "att-odd-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("2", pin.Value);

            pin = pins.Find(p => p.Name == "att-even-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("1", pin.Value);
        }
    }
}
