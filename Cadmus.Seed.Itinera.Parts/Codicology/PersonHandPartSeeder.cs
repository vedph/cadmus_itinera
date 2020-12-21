using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Person's hand part seeder.
    /// Tag: <c>seed.it.vedph.itinera.person-hand</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.person-hand")]
    public sealed class PersonHandPartSeeder : PartSeederBase
    {
        private List<MsRubrication> GetRubrications(int count)
        {
            List<MsRubrication> rubrications = new List<MsRubrication>();

            for (int n = 1; n <= count; n++)
            {
                rubrications.Add(new Faker<MsRubrication>()
                    .RuleFor(r => r.Location, f => new MsLocation
                    {
                        N = (short)n,
                        S = n % 2 == 0 ? MsLocationSides.Verso : MsLocationSides.Recto,
                        L = (short)f.Random.Number(1, 20)
                    })
                    .RuleFor(r => r.Type, f => f.Lorem.Word())
                    .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                    .RuleFor(r => r.Issues, f => f.Lorem.Sentence())
                    .Generate());
            }

            return rubrications;
        }

        private List<MsSubscription> GetSubscriptions(int count)
        {
            List<MsSubscription> subscriptions = new List<MsSubscription>();

            for (int n = 1; n <= count; n++)
            {
                subscriptions.Add(new Faker<MsSubscription>()
                    .RuleFor(r => r.Location, f => new MsLocation
                    {
                        N = (short)n,
                        S = n % 2 == 0 ? MsLocationSides.Verso : MsLocationSides.Recto,
                        L = (short)f.Random.Number(1, 20)
                    })
                    .RuleFor(r => r.Language, f => f.PickRandom("lat", "ita"))
                    .RuleFor(r => r.Text, f => f.Lorem.Sentence())
                    .Generate());
            }

            return subscriptions;
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

            PersonHandPart part = new Faker<PersonHandPart>()
                .RuleFor(p => p.PersonId, f => f.Lorem.Word())
                .RuleFor(p => p.Job, f => f.PickRandom("copyst", "writer", "poet"))
                .RuleFor(p => p.Type, f => f.Lorem.Word())
                .RuleFor(p => p.ExtentNote, f => f.Lorem.Sentence())
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Initials, f => f.Lorem.Sentence())
                .RuleFor(p => p.Corrections, f => f.Lorem.Sentence())
                .RuleFor(p => p.Punctuation, f => f.Lorem.Sentence())
                .RuleFor(p => p.Abbreviations, f => f.Lorem.Sentence())
                .RuleFor(p => p.Rubrications,
                    f => GetRubrications(f.Random.Number(1, 3)))
                .RuleFor(p => p.Subscriptions,
                    f => GetSubscriptions(f.Random.Number(1, 3)))
                .Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
