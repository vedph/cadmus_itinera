using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class PersonHandPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static PersonHandPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(PersonHandPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.person-hand", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            PersonHandPartSeeder seeder = new PersonHandPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            PersonHandPart p = part as PersonHandPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotNull(p.PersonId);
            Assert.NotNull(p.Job);
            Assert.NotNull(p.Type);
            Assert.NotNull(p.ExtentNote);
            Assert.NotNull(p.Description);
            Assert.NotNull(p.Initials);
            Assert.NotNull(p.Corrections);
            Assert.NotNull(p.Punctuation);
            Assert.NotNull(p.Abbreviations);
            Assert.NotEmpty(p.Rubrications);
            Assert.NotEmpty(p.Subscriptions);
        }
    }
}
