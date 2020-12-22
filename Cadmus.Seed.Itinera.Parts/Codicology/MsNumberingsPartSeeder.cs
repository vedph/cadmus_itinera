using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's numberings part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-numberings</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-numberings")]
    public sealed class MsNumberingsPartSeeder : PartSeederBase
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

            MsNumberingsPart part = new MsNumberingsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Numberings.Add(new Faker<MsNumbering>()
                    .RuleFor(p => p.IsMain, n == 1)
                    .RuleFor(p => p.IsPagination, n == 1)
                    .RuleFor(p => p.Era, f => f.PickRandom("ancient", "modern"))
                    .RuleFor(p => p.System, f => f.PickRandom("roman", "arabic"))
                    .RuleFor(p => p.Technique, f => f.Lorem.Word())
                    .RuleFor(p => p.Century, f => f.Random.Short(12, 15))
                    .RuleFor(p => p.Position, f => f.PickRandom("bottom", "top"))
                    .RuleFor(p => p.Issues, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .Generate());
            }

            return part;
        }
    }
}
