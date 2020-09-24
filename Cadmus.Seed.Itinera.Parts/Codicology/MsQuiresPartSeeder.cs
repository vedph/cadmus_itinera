using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's quires sequence description seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-quires</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-quires")]
    public sealed class MsQuiresPartSeeder : PartSeederBase
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

            MsQuiresPart part = new MsQuiresPart();
            SetPartMetadata(part, roleId, item);

            short nr = 1;
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Quires.Add(new Faker<MsQuire>()
                    .RuleFor(q => q.IsNormal, n == 1)
                    .RuleFor(q => q.StartNr, nr)
                    .RuleFor(q => q.EndNr, (short)(nr + 3))
                    .RuleFor(q => q.SheetCount, (short)4)
                    .RuleFor(q => q.SheetDelta, f => f.Random.Short(0, 2 + 1))
                    .RuleFor(q => q.Note, f => f.PickRandom(null, f.Lorem.Sentence(2, 3)))
                    .Generate());
                nr += 4;
            }

            return part;
        }
    }
}
