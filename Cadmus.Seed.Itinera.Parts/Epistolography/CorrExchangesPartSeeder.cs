using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

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
        private static Chronotope GetChronotope(int year)
        {
            return new Faker<Chronotope>()
                .RuleFor(c => c.Tag, f => f.PickRandom(null, f.Lorem.Word()))
                .RuleFor(c => c.Place, f => f.Lorem.Word())
                .RuleFor(c => c.Date, HistoricalDate.Parse($"{year} AD"))
                .RuleFor(c => c.TextDate, f => f.Lorem.Sentence(3))
                .RuleFor(c => c.Sources, SeederHelper.GetDocReferences(1, 3))
                .Generate();
        }

        private static List<Attachment> GetAttachments()
        {
            List<Attachment> attachments = new List<Attachment>();

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                attachments.Add(new Faker<Attachment>()
                    .RuleFor(a => a.Type, f => f.PickRandom("manuscript", "work"))
                    .RuleFor(a => a.Name, f => f.Lorem.Word())
                    .RuleFor(a => a.Portion,
                        f => $"{f.Random.Number(1,20)}-{f.Random.Number(21, 40)}")
                    .RuleFor(a => a.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return attachments;
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

            CorrExchangesPart part = new CorrExchangesPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Exchanges.Add(new Faker<CorrExchange>()
                    .RuleFor(e => e.IsDubious, f => f.Random.Bool(0.3f))
                    .RuleFor(e => e.IsIndirect, f => f.Random.Bool())
                    .RuleFor(e => e.IsFromParticipant, f => f.Random.Bool())
                    .RuleFor(e => e.From, GetChronotope(1200))
                    .RuleFor(e => e.To, GetChronotope(1201))
                    .RuleFor(p => p.Participants, SeederHelper.GetDecoratedIds(1, 2))
                    .RuleFor(d => d.Sources, SeederHelper.GetDocReferences(1, 3))
                    .RuleFor(e => e.Attachments, GetAttachments())
                    .Generate());
            }

            return part;
        }
    }
}
