using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

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
                MsHandInstance hand = new Faker<MsHandInstance>()
                    .RuleFor(i => i.Id, f => f.Lorem.Word())
                    .RuleFor(i => i.IdReason, f => f.Lorem.Sentence())
                    .RuleFor(i => i.Start, f => new MsLocation
                    {
                        N = f.Random.Number(1, 30),
                        V = f.Random.Bool(),
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(i => i.End, f => new MsLocation
                    {
                        N = f.Random.Number(31, 60),
                        V = f.Random.Bool(),
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(i => i.ExtentNote, f => f.Lorem.Sentence())
                    .Generate();

                // rubrications
                for (int rn = 1; rn <= Randomizer.Seed.Next(1, 3); rn++)
                {
                    hand.Rubrications.Add(new Faker<MsRubrication>()
                        .RuleFor(r => r.Location, f => new MsLocation
                        {
                            N = f.Random.Number(1, 60),
                            V = f.Random.Bool(),
                            L = f.Random.Number(1, 20)
                        })
                        .RuleFor(r => r.Type, f => f.Lorem.Word())
                        .RuleFor(r => r.Description, f => f.Lorem.Sentence())
                        .RuleFor(r => r.Issues,
                            f => f.PickRandom(null, f.Lorem.Sentence()))
                        .Generate());
                }

                // subscriptions
                for (int sn = 1; sn <= Randomizer.Seed.Next(1, 3); sn++)
                {
                    hand.Subscriptions.Add(new Faker<MsSubscription>()
                        .RuleFor(s => s.Location, f => new MsLocation
                        {
                            N = f.Random.Number(1, 60),
                            V = f.Random.Bool(),
                            L = f.Random.Number(1, 20)
                        })
                        .RuleFor(s => s.Language, f => f.PickRandom("lat", "ita"))
                        .RuleFor(s => s.Text, f => f.Lorem.Sentence())
                        .Generate());
                }

                part.Hands.Add(hand);
            }

            return part;
        }
    }
}
