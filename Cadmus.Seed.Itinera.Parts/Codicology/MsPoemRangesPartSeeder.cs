using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsPoemRangesPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.ms-poem-ranges</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-poem-ranges")]
    public sealed class MsPoemRangesPartSeeder : PartSeederBase
    {
        private List<AlnumRange> GetRanges(int min, int max)
        {
            List<AlnumRange> ranges = new List<AlnumRange>();

            int rn = 1;
            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                AlnumRange range;
                if (n % 2 == 0)
                {
                    range = new Faker<AlnumRange>()
                        .RuleFor(r => r.A, $"{rn}a")
                        .Generate();
                }
                else
                {
                    range = new Faker<AlnumRange>()
                    .RuleFor(r => r.A, rn.ToString(CultureInfo.InvariantCulture))
                    .RuleFor(r => r.B, (rn + 2).ToString(CultureInfo.InvariantCulture))
                    .Generate();
                }
                ranges.Add(range);
                rn += 4;
            }

            return ranges;
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

            MsPoemRangesPart part = new Faker<MsPoemRangesPart>()
                .RuleFor(p => p.Tag, f => f.Lorem.Word())
                .RuleFor(p => p.Ranges, GetRanges(3, 5))
                .RuleFor(p => p.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                .Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}