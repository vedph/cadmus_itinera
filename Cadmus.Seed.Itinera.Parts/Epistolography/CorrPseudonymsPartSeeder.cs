using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for <see cref="CorrPseudonymsPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.corr-pseudonyms</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.corr-pseudonyms")]
    public sealed class CorrPseudonymsPartSeeder : PartSeederBase
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

            CorrPseudonymsPart part = new CorrPseudonymsPart();
            SetPartMetadata(part, roleId, item);

            part.Pseudonyms.Add(new Faker<CorrPseudonym>()
                .RuleFor(p => p.Language, f => f.PickRandom("lat", "ita"))
                .RuleFor(p => p.Value, f => f.Lorem.Word())
                .RuleFor(p => p.IsAuthor, f => f.Random.Bool())
                .RuleFor(p => p.Sources, SeederHelper.GetDocReferences(1, 3))
                .Generate());

            return part;
        }
    }
}
