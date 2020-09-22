using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's catchwords part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-catchwords</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-catchwords")]
    public sealed class MsCatchwordsPartSeeder : PartSeederBase
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

            MsCatchwordsPart part = new MsCatchwordsPart();
            int count = Randomizer.Seed.Next(1, 3 + 1);

            for (int n = 1; n <= count; n++)
            {
                part.Catchwords.Add(new Faker<MsCatchword>()
                    .RuleFor(c => c.Position, f => f.PickRandom("bottom", "right"))
                    .RuleFor(c => c.IsVertical, f => f.Random.Bool(0.5f))
                    .RuleFor(c => c.Decoration, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Register, f => f.Lorem.Sentence())
                    .Generate());
            }

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
