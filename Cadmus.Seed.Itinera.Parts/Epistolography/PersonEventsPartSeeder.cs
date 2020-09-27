using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for <see cref="PersonEventsPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.person-events</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.person-events")]
    public sealed class PersonEventsPartSeeder : PartSeederBase
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

            PersonEventsPart part = new PersonEventsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Events.Add(new Faker<BioEvent>()
                    .RuleFor(e => e.Type, f => n == 1?
                        "birth" : f.PickRandom("work", "marriage"))
                    .RuleFor(e => e.Date, HistoricalDate.Parse($"{n} AD"))
                    .RuleFor(e => e.Places,
                        f => new List<string>(new[] { f.Lorem.Word() }))
                    .RuleFor(e => e.Description, f => f.Lorem.Sentence())
                    .RuleFor(e => e.Sources, SeederHelper.GetDocReferences(1, 3))
                    .RuleFor(e => e.Participants, SeederHelper.GetDecoratedIds(1, 3))
                    .RuleFor(e => e.Work, f => f.Lorem.Sentence(1, 3))
                    .RuleFor(e => e.Rank, f => f.Random.Short(0, 3))
                    .RuleFor(e => e.IsWorkLost, f => f.Random.Bool())
                    .RuleFor(e => e.ExternalIds, SeederHelper.GetExternalIds(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
