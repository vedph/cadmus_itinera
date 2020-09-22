using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsCatchwordsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsCatchwordsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsCatchwordsPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-catchwords", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsCatchwordsPartSeeder seeder = new MsCatchwordsPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsCatchwordsPart cp = part as MsCatchwordsPart;
            Assert.NotNull(cp);

            TestHelper.AssertPartMetadata(cp);
            Assert.NotEmpty(cp.Catchwords);

            foreach (MsCatchword catchword in cp.Catchwords)
            {
                Assert.NotNull(catchword.Position);
                Assert.NotNull(catchword.Decoration);
                Assert.NotNull(catchword.Register);
            }
        }
    }
}
