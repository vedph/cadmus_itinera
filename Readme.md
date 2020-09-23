# Cadmus Itinera Parts

Components for the Itinera project in Cadmus.

## Templates

Sample template for the codicology namespace (change this as needed).

### Part Seeder

```cs
using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// TODO...
    /// Tag: <c>seed.it.vedph.itinera.TODO</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.TODO")]
    public sealed class __NAME__PartSeeder : PartSeederBase
    {
        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            __NAME__Part part = new __NAME__Part();
            SetPartMetadata(part, roleId, item);

            // TODO: seed...

            return part;
        }
    }
}
```

### Part Seeder Test

```cs
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Seed.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Itinera.Parts.Test.Codicology
{
    public sealed class __NAME__PartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static __NAME__PartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(__NAME__PartSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.itinera.TODO", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            __NAME__PartSeeder seeder = new __NAME__PartSeeder();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            __NAME__Part p = part as __NAME__Part;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            // TODO other assertions...
        }
    }
}
```
