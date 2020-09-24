using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Chronotopics part seeder.
    /// Tag: <c>seed.it.vedph.itinera.chronotopics</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.chronotopics")]
    public sealed class ChronotopicsPartSeeder : PartSeederBase
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

            ChronotopicsPart part = new ChronotopicsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Chronotopes.Add(new Faker<Chronotope>()
                    .RuleFor(c => c.Tag, f => f.Lorem.Word())
                    .RuleFor(c => c.Place, f => f.Lorem.Word())
                    .RuleFor(c => c.Date, HistoricalDate.Parse($"{1200 + n} AD"))
                    .Generate());
            }

            return part;
        }
    }
}
