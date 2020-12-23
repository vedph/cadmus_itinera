using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's hands part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-hands</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-hands")]
    public sealed class MsHandsPartSeeder : PartSeederBase
    {
        private static List<MsLocation> GetLocations(int count)
        {
            List<MsLocation> locations = new List<MsLocation>();
            for (int n = 1; n <= count; n++)
            {
                locations.Add(new Faker<MsLocation>()
                    .RuleFor(l => l.N, f => f.Random.Number(1, 50))
                    .RuleFor(l => l.S, n % 2 == 0
                        ? MsLocationSides.Verso
                        : MsLocationSides.Recto)
                    .RuleFor(l => l.L, f => f.Random.Number(1, 20))
                    .Generate());
            }
            return locations;
        }

        private List<MsRubrication> GetRubrications(int count)
        {
            List<MsRubrication> rubrications = new List<MsRubrication>();

            for (int n = 1; n <= count; n++)
            {
                rubrications.Add(new Faker<MsRubrication>()
                    .RuleFor(r => r.Locations, f => GetLocations(f.Random.Number(1, 2)))
                    .RuleFor(r => r.Type, f => f.Lorem.Word())
                    .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                    .RuleFor(r => r.Issues, f => f.Lorem.Sentence())
                    .Generate());
            }

            return rubrications;
        }

        private MsSubscription GetSubscription()
        {
            return new Faker<MsSubscription>()
                .RuleFor(r => r.Locations, f => GetLocations(f.Random.Number(1, 2)))
                .RuleFor(r => r.Language, f => f.PickRandom("lat", "ita"))
                .RuleFor(r => r.Text, f => f.Lorem.Sentence())
                .Generate();
        }

        private List<MsHandSign> GetSigns(int count)
        {
            List<MsHandSign> signs = new List<MsHandSign>();

            for (int n = 1; n <= count; n++)
            {
                signs.Add(new Faker<MsHandSign>()
                    .RuleFor(s => s.Id,
                        f => new string((char)('A' + f.Random.Number(0, 25)), 1))
                    .RuleFor(s => s.Type, f => f.PickRandom("let", "pct"))
                    .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                    .RuleFor(s => s.ImageId, f => "img" + f.Random.Number(1, 100) + "-" )
                    .Generate());
            }

            return signs;
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

            MsHandsPart part = new MsHandsPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 3);
            for (int n = 1; n <= count; n++)
            {
                MsHand hand = new Faker<MsHand>()
                    .RuleFor(h => h.Id, f => f.Lorem.Word())
                    .RuleFor(h => h.Types, f => new List<string>(
                        new[] { f.PickRandom("got", "mea") }))
                    .RuleFor(h => h.PersonId, f => f.Name.FirstName().ToLowerInvariant())
                    .RuleFor(h => h.Description, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Initials, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Corrections, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Punctuation, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Abbreviations, f => f.Lorem.Sentence())
                    .RuleFor(h => h.IdReason, f => f.Lorem.Word())
                    .RuleFor(h => h.Ranges, f =>
                        new List<MsLocationRange>(new[] {
                            new MsLocationRange
                            {
                                Start = new MsLocation
                                {
                                    N = f.Random.Number(1, 30),
                                    S = n % 2 == 0 ?
                                        MsLocationSides.Verso : MsLocationSides.Recto,
                                    L = f.Random.Number(1, 20)
                                },
                                End = new MsLocation
                                {
                                    N = f.Random.Number(31, 60),
                                    S = n % 2 == 0 ?
                                        MsLocationSides.Verso : MsLocationSides.Recto,
                                    L = f.Random.Number(1, 20)
                                }
                            }
                        }))
                    .RuleFor(h => h.ExtentNote, f => f.Lorem.Sentence())
                    .RuleFor(h => h.Rubrications,
                        f => GetRubrications(f.Random.Number(1, 2)))
                    .RuleFor(h => h.Subscription, GetSubscription())
                    .RuleFor(h => h.Signs, f => GetSigns(f.Random.Number(1, 2)))
                    .RuleFor(h => h.ImageIds,
                        f => new List<string>(new[] { "img" + f.Random.Number(1, 100) + "-" }))
                    .Generate();

                part.Hands.Add(hand);
            }

            return part;
        }
    }
}
