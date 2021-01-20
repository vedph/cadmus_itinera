using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's place part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-place</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-place")]
    public sealed class MsPlacePartSeeder : PartSeederBase
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

            MsPlacePart part = new Faker<MsPlacePart>()
                .RuleFor(p => p.Area,
                    f => f.PickRandom("France", "Germany", "Italy"))
                .RuleFor(p => p.Address, f => $"{f.Lorem.Word()}, {f.Lorem.Word()}")
                .RuleFor(p => p.City, f => f.Address.City())
                .RuleFor(p => p.Site, f => f.PickRandom("A library", "A monastery"))
                .RuleFor(p => p.Subscriber, f => f.Lorem.Word())
                .RuleFor(p => p.SubscriptionLoc, f =>
                    new MsLocation
                    {
                        N = (short)f.Random.Number(20, 60),
                        S = f.Random.Bool() ?
                            "v" : "r",
                        L = (short)f.Random.Number(1, 20)
                    })
                .RuleFor(p => p.Sources, SeederHelper.GetDocReferences(1, 3))
                .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
