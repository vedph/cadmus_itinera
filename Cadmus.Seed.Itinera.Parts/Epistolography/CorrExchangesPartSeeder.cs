using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for <see cref="CorrExchangesPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.corr-exchanges</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.corr-exchanges")]
    public sealed class CorrExchangesPartSeeder : PartSeederBase
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

            CorrExchangesPart part = new CorrExchangesPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Exchanges.Add(new Faker<CorrExchange>()
                    .RuleFor(e => e.IsDubious, f => f.Random.Bool(0.3f))
                    .RuleFor(e => e.IsIndirect, f => f.Random.Bool())
                    .RuleFor(e => e.IsFromParticipant, f => f.Random.Bool())
                    // TODO: complete once part has been confirmed
                    .RuleFor(d => d.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
