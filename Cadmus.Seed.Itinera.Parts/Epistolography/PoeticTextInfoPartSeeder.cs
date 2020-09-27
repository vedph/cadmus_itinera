using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Part seeder for <see cref="PoeticTextInfoPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.poetic-text-info</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.poetic-text-info")]
    public sealed class PoeticTextInfoPartSeeder : PartSeederBase
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

            PoeticTextInfoPart part = new Faker<PoeticTextInfoPart>()
                .RuleFor(p => p.Language, f => f.PickRandom("lat", "ita"))
                .RuleFor(p => p.Subject, f => f.Lorem.Word())
                .RuleFor(p => p.Metre, f => f.Lorem.Word())
                .RuleFor(p => p.Authors, SeederHelper.GetCitedPersons(1, 2))
                .RuleFor(p => p.Related, SeederHelper.GetDocReferences(1, 3))
                .Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
