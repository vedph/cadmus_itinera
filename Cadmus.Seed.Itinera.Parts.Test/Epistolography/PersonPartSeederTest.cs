using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Seed.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Epistolography
{
    public sealed class PersonPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static PersonPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(PersonPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.person", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            PersonPartSeeder seeder = new PersonPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            PersonPart p = part as PersonPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotNull(p.PersonId);
            Assert.NotEmpty(p.ExternalIds);
            Assert.NotEmpty(p.Names);
            Assert.True(p.Sex == 'M' || p.Sex == 'F');
            Assert.NotEmpty(p.Chronotopes);
            Assert.NotNull(p.Bio);
        }
    }
}
