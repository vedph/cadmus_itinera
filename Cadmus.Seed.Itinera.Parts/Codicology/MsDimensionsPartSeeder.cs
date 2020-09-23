using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's dimensions part seeder.
    /// <para>Tag: <c>seed.it.vedph.itinera.ms-dimensions</c>.</para>
    /// </summary>
    [Tag("seed.it.vedph.itinera.ms-dimensions")]
    public sealed class MsDimensionsPartSeeder : PartSeederBase
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

            MsDimensionsPart part = new Faker<MsDimensionsPart>()
                .RuleFor(p => p.Sample,
                    f => new MsLocation
                    {
                        N = f.Random.Number(1, 60),
                        V = f.Random.Bool()
                    })
                .Generate();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 3);
            for (int n = 1; n <= count; n++)
            {
                part.Dimensions.Add(new Faker<PhysicalDimension>()
                    .RuleFor(d => d.Tag, f => f.Lorem.Word())
                    .RuleFor(d => d.Value, f => f.Random.Float(2, 10))
                    .RuleFor(d => d.Unit, "cm")
                    .Generate());
            }

            count = Randomizer.Seed.Next(1, 3);
            for (int n = 1; n <= count; n++)
            {
                part.Counts.Add(new Faker<DecoratedCount>()
                    .RuleFor(d => d.Id, f => f.Lorem.Word())
                    .RuleFor(d => d.Value, f => f.Random.Number(1, 100))
                    .RuleFor(d => d.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .Generate());
            }
            return part;
        }
    }
}
