using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for the correspondent/author dedications part.
    /// Tag: <c>seed.it.vedph.itinera.corr-dedications</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.corr-dedications")]
    public sealed class CorrDedicationsPartSeeder : PartSeederBase
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

            CorrDedicationsPart part = new CorrDedicationsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Dedications.Add(new Faker<CorrDedication>()
                    .RuleFor(d => d.Title, f => f.Lorem.Sentence(1, 3))
                    .RuleFor(d => d.Date, HistoricalDate.Parse($"{1200 + n} AD"))
                    .RuleFor(d => d.Date, HistoricalDate.Parse($"{1201 + n} AD"))
                    .RuleFor(d => d.IsByAuthor, f => f.Random.Bool())
                    .RuleFor(d => d.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
