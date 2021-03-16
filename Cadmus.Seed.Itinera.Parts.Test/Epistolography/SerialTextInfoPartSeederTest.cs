using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Epistolography
{
    public sealed class SerialTextInfoPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static SerialTextInfoPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(SerialTextInfoPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.serial-text-info", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            SerialTextInfoPartSeeder seeder = new SerialTextInfoPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            SerialTextInfoPart ap = part as SerialTextInfoPart;
            Assert.NotNull(ap);

            TestHelper.AssertPartMetadata(ap);
            Assert.NotNull(ap.TextId);
            Assert.NotNull(ap.Language);
            Assert.NotNull(ap.Subject);
            Assert.NotNull(ap.Genre);
            Assert.NotNull(ap.Verse);
            Assert.NotNull(ap.Rhyme);
            Assert.NotEmpty(ap.Headings);
            Assert.NotEmpty(ap.Authors);
            Assert.NotEmpty(ap.Recipients);
            Assert.NotEmpty(ap.ReplyingTo);
            Assert.NotNull(ap.Related);
            Assert.NotNull(ap.Note);
        }
    }
}
