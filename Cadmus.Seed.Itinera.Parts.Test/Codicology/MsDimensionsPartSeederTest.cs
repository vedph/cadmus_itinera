using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsDimensionsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsDimensionsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsDimensionsPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-dimensions", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsDimensionsPartSeeder seeder = new MsDimensionsPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsDimensionsPart dp = part as MsDimensionsPart;
            Assert.NotNull(dp);

            TestHelper.AssertPartMetadata(dp);
            Assert.NotEmpty(dp.Dimensions);
            Assert.NotEmpty(dp.Counts);
        }
    }
}
