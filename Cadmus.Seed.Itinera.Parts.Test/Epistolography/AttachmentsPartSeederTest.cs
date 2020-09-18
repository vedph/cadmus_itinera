using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Epistolography
{
    public sealed class AttachmentsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static AttachmentsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(AttachmentsPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.attachments", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            AttachmentsPartSeeder seeder = new AttachmentsPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            AttachmentsPart ap = part as AttachmentsPart;
            Assert.NotNull(ap);

            TestHelper.AssertPartMetadata(ap);
            Assert.NotNull(ap.Attachments);
            Assert.NotEmpty(ap.Attachments);
            foreach (Attachment attachment in ap.Attachments)
            {
                Assert.NotNull(attachment.Type);
                Assert.NotNull(attachment.Name);
                Assert.NotNull(attachment.Portion);
                Assert.NotNull(attachment.Note);
            }
        }
    }
}
