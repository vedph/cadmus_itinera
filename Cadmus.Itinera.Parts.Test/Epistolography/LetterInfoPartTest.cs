using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Itinera.Parts.Test.Epistolography
{
    public sealed class LetterInfoPartTest
    {
        private static LetterInfoPart GetPart()
        {
            return new LetterInfoPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
                Language = "eng",
                Subject = "Subject",
                Heading = "Heading",
                Note = "A note"
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            LetterInfoPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            LetterInfoPart part2 =
                TestHelper.DeserializePart<LetterInfoPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Language, part2.Language);
            Assert.Equal(part.Subject, part2.Subject);
            Assert.Equal(part.Heading, part2.Heading);
            Assert.Equal(part.Note, part2.Note);
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            LetterInfoPart part = GetPart();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(3, pins.Count);
            TestHelper.AssertValidDataPinNames(pins);

            DataPin pin = pins.Find(p => p.Name == "language");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("eng", pin.Value);

            pin = pins.Find(p => p.Name == "subject");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("subject", pin.Value);

            pin = pins.Find(p => p.Name == "heading");
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("heading", pin.Value);
        }
    }
}
