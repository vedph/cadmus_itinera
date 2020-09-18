using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// LetterInfo part seeder.
    /// Tag: <c>seed.it.vedph.itinera.letter-info</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.letter-info")]
    public sealed class LetterInfoPartSeeder : PartSeederBase
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

            LetterInfoPart part = new Faker<LetterInfoPart>()
                .RuleFor(p => p.Language, f => f.PickRandom("ita", "fra", "lat"))
                .RuleFor(p => p.Subject, f => f.Lorem.Sentence(2, 4))
                .RuleFor(p => p.Heading, f => f.Lorem.Sentence(3, 5))
                .RuleFor(p => p.Note, f => f.Lorem.Sentence())
                .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
