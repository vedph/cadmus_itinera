using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Epistolography
{
    public sealed class LetterInfoPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static LetterInfoPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(LetterInfoPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.letter-info", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            LetterInfoPartSeeder seeder = new LetterInfoPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            LetterInfoPart ap = part as LetterInfoPart;
            Assert.NotNull(ap);

            TestHelper.AssertPartMetadata(ap);
            Assert.NotNull(ap.Language);
            Assert.NotNull(ap.Subject);
            Assert.NotNull(ap.Headings);
            Assert.NotNull(ap.Note);
        }
    }
}
