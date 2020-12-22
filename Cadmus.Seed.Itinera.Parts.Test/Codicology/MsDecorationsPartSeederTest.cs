using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsDecorationsPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsDecorationsPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsDecorationsPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-decorations", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsDecorationsPartSeeder seeder = new MsDecorationsPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsDecorationsPart dp = part as MsDecorationsPart;
            Assert.NotNull(dp);

            TestHelper.AssertPartMetadata(dp);
            Assert.NotEmpty(dp.Decorations);

            foreach (MsDecoration decoration in dp.Decorations)
            {
                Assert.NotNull(decoration.Type);
                Assert.NotNull(decoration.Subject);
                Assert.NotEmpty(decoration.Colors);
                Assert.NotNull(decoration.Tool);
                Assert.NotNull(decoration.Start);
                Assert.NotNull(decoration.End);
                Assert.NotNull(decoration.Position);
                Assert.NotNull(decoration.Size);
                Assert.NotNull(decoration.Description);
                Assert.NotNull(decoration.TextRelation);
                Assert.NotNull(decoration.ImageId);
                Assert.NotEmpty(decoration.GuideLetters);
                Assert.NotNull(decoration.Artist);
            }
        }
    }
}
