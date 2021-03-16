using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's dimensions part seeder.
    /// <para>Tag: <c>seed.it.vedph.itinera.ms-layouts</c>.</para>
    /// </summary>
    [Tag("seed.it.vedph.itinera.ms-layouts")]
    public sealed class MsLayoutsPartSeeder : PartSeederBase
    {
        private static List<PhysicalDimension> GetDimensions(int count)
        {
            List<PhysicalDimension> dimensions = new List<PhysicalDimension>();

            for (int n = 1; n <= count; n++)
            {
                dimensions.Add(new Faker<PhysicalDimension>()
                    .RuleFor(d => d.Tag, f => f.Lorem.Word())
                    .RuleFor(d => d.Value, f => (float)SeederHelper.Truncate(
                        f.Random.Float(2, 10), 2))
                    .RuleFor(d => d.Unit, "cm")
                    .Generate());
            }

            return dimensions;
        }

        private static List<DecoratedCount> GetCounts(int count)
        {
            List<DecoratedCount> counts = new List<DecoratedCount>();

            for (int n = 1; n <= count; n++)
            {
                counts.Add(new Faker<DecoratedCount>()
                    .RuleFor(d => d.Id, f => f.Lorem.Word())
                    .RuleFor(d => d.Value, f => f.Random.Number(1, 100))
                    .RuleFor(d => d.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .Generate());
            }

            return counts;
        }

        private static MsLayout GetLayout()
        {
            return new Faker<MsLayout>()
                .RuleFor(p => p.Sample,
                    f => new MsLocation
                    {
                        N = f.Random.Number(1, 60),
                        S = f.Random.Bool() ?
                            "v" : "r",
                    })
                .RuleFor(p => p.Dimensions, f => GetDimensions(f.Random.Number(1, 3)))
                .RuleFor(p => p.ColumnCount, f => f.Random.Number(1, 4))
                .RuleFor(p => p.RulingTechnique, f => f.PickRandom(new[] { "dry", "color" }))
                .RuleFor(p => p.Derolez, f => f.Lorem.Word())
                .RuleFor(p => p.Pricking, f => f.Lorem.Word())
                .RuleFor(p => p.Counts, f => GetCounts(f.Random.Number(1, 3)))
                .Generate();
        }

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

            MsLayoutsPart part = new MsLayoutsPart();

            int count = Randomizer.Seed.Next(1, 3);
            for (int n = 1; n <= count; n++) part.Layouts.Add(GetLayout());

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
