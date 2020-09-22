using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class MsCompositionPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsCompositionPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsCompositionPartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.ms-composition", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsCompositionPartSeeder seeder = new MsCompositionPartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsCompositionPart cp = part as MsCompositionPart;
            Assert.NotNull(cp);

            TestHelper.AssertPartMetadata(cp);
            Assert.True(cp.SheetCount > 0);
            Assert.True(cp.GuardSheetCount > 0);
            Assert.NotEmpty(cp.GuardSheets);
            Assert.NotEmpty(cp.Sections);

            foreach (MsGuardSheet sheet in cp.GuardSheets)
            {
                Assert.NotNull(sheet.Material);
                Assert.NotNull(sheet.Location);
                Assert.NotNull(sheet.Date);
                Assert.NotNull(sheet.Note);
            }

            foreach (MsSection section in cp.Sections)
            {
                Assert.NotNull(section.Label);
                Assert.NotNull(section.Start);
                Assert.NotNull(section.End);
                Assert.NotNull(section.Date);
            }
        }
    }
}
