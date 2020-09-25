using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Epistolography
{
    public sealed class CitedPersonsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static CitedPersonsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(CitedPersonsPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.cited-persons", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            CitedPersonsPartSeeder seeder = new CitedPersonsPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            CitedPersonsPart p = part as CitedPersonsPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotEmpty(p.Persons);
        }
    }
}
