using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsBindingPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsBindingPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsBindingPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-binding", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsBindingPartSeeder seeder = new MsBindingPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsBindingPart bp = part as MsBindingPart;
            Assert.NotNull(bp);

            TestHelper.AssertPartMetadata(bp);
            Assert.True(bp.Century != 0);
            Assert.NotNull(bp.Description);
            Assert.NotNull(bp.CoverMaterial);
            Assert.NotNull(bp.SupportMaterial);
            Assert.NotNull(bp.Size);
            Assert.NotNull(bp.Size.W);
            Assert.NotNull(bp.Size.D);
            Assert.NotNull(bp.Size.H);
        }
    }
}
