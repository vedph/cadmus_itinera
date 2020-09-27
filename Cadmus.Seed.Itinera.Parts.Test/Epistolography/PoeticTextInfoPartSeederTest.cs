using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class PoeticTextInfoPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static PoeticTextInfoPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(PoeticTextInfoPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.poetic-text-info", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            PoeticTextInfoPartSeeder seeder = new PoeticTextInfoPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            PoeticTextInfoPart p = part as PoeticTextInfoPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotNull(p.Language);
            Assert.NotNull(p.Subject);
            Assert.NotNull(p.Metre);
            Assert.NotEmpty(p.Authors);
            Assert.NotEmpty(p.Related);
        }
    }
}
