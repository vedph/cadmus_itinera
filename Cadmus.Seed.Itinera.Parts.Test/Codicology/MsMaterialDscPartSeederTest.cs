using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsMaterialDscPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsMaterialDscPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsMaterialDscPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-material-dsc", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsMaterialDscPartSeeder seeder = new MsMaterialDscPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsMaterialDscPart p = part as MsMaterialDscPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotNull(p.Material);
            Assert.NotNull(p.Format);
            Assert.NotNull(p.State);
            Assert.NotEmpty(p.Counts);
            Assert.NotEmpty(p.Palimpsests);
        }
    }
}
