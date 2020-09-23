using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's material description part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-material-dsc</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-material-dsc")]
    public sealed class MsMaterialDscPartSeeder : PartSeederBase
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

            MsMaterialDscPart part = new Faker<MsMaterialDscPart>()
                .RuleFor(p => p.Material, f => f.PickRandom("paper", "parchment"))
                .RuleFor(p => p.Format, f => f.Lorem.Word())
                .RuleFor(p => p.State, f => f.Lorem.Word())
                .RuleFor(p => p.StateNote, f => f.Lorem.Sentence())
                .Generate();
            SetPartMetadata(part, roleId, item);

            // counts
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Counts.Add(new Faker<DecoratedCount>()
                    .RuleFor(d => d.Id, f => f.Lorem.Word())
                    .RuleFor(d => d.Value, f => f.Random.Number(1, 100))
                    .RuleFor(d => d.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .Generate());
            }

            // palimpsests
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Palimpsests.Add(new Faker<MsPalimpsest>()
                    .RuleFor(p => p.Location, f => new MsLocation
                    {
                        N = n,
                        V = n % 2 == 0,
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(p => p.Date, HistoricalDate.Parse($"{1300 + n} AD"))
                    .Generate());
            }

            return part;
        }
    }
}
