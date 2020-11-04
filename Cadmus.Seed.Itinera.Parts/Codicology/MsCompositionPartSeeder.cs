using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Globalization;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's composition part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-composition</c>.
    /// </summary>
    [Tag("seed.it.vedph.itinera.ms-composition")]
    public sealed class MsCompositionPartSeeder : PartSeederBase
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

            MsCompositionPart part = new Faker<MsCompositionPart>()
                .RuleFor(p => p.SheetCount, f => f.Random.Number(16, 64))
                .RuleFor(p => p.GuardSheetCount, f => f.Random.Number(1, 4))
                .Generate();
            SetPartMetadata(part, roleId, item);

            // guard sheets
            for (int n = 1; n <= part.GuardSheetCount; n++)
            {
                part.GuardSheets.Add(new Faker<MsGuardSheet>()
                    .RuleFor(s => s.IsBack, n % 2 == 0)
                    .RuleFor(s => s.Material,
                        f => f.PickRandom("paper", "parchment"))
                    .RuleFor(s => s.Location, f => new MsLocation
                    {
                        N = (short)n,
                        V = n % 2 == 0,
                        L = (short)(f.Random.Number(1, 20))
                    })
                    .RuleFor(s => s.Date,
                        HistoricalDate.Parse((1300 + n)
                        .ToString(CultureInfo.InvariantCulture)))
                    .RuleFor(s => s.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            // sections
            int count = Randomizer.Seed.Next(1, 6);
            for (int n = 1; n <= count; n++)
            {
                part.Sections.Add(new Faker<MsSection>()
                    .RuleFor(s => s.Tag, f => f.PickRandom(null, f.Lorem.Word()))
                    .RuleFor(s => s.Label, f => f.Lorem.Sentence(1, 3))
                    .RuleFor(s => s.Start, f => new MsLocation
                    {
                        N = (short)((n - 1) * 2),
                        V = n % 2 == 0,
                        L = (short)(f.Random.Number(1, 20))
                    })
                    .RuleFor(s => s.End, f => new MsLocation
                    {
                        N = (short)((n - 1) * 2 + 1),
                        V = n % 2 == 0,
                        L = (short)(f.Random.Number(1, 20))
                    })
                    .RuleFor(s => s.Date,
                        HistoricalDate.Parse((1300 + n)
                            .ToString(CultureInfo.InvariantCulture)))
                    .Generate());
            }

            return part;
        }
    }
}
