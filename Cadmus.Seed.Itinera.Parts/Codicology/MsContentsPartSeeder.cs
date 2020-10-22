using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsContentsPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.ms-contents</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-contents")]
    public sealed class MsContentsPartSeeder : PartSeederBase
    {
        private List<MsContentUnit> GetUnits(int count)
        {
            List<MsContentUnit> units = new List<MsContentUnit>();
            for (int n = 1; n <= count; n++)
            {
                units.Add(new Faker<MsContentUnit>()
                    .RuleFor(u => u.Label, f => f.Lorem.Sentence(5))
                    .RuleFor(u => u.Incipit, f => f.Lorem.Sentence())
                    .RuleFor(u => u.Explicit, f => f.Lorem.Sentence())
                    .Generate());
            }
            return units;
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

            MsContentsPart part = new MsContentsPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 3 + 1);

            for (int n = 1; n <= count; n++)
            {
                int sn = (n - 1) * 2;

                part.Contents.Add(new Faker<MsContent>()
                    .RuleFor(c => c.Author, f => f.Lorem.Word())
                    .RuleFor(c => c.ClaimedAuthor,
                        f => f.PickRandom(null, f.Lorem.Word()))
                    .RuleFor(c => c.Work, f => f.Lorem.Word())
                    .RuleFor(c => c.Start, f => new MsLocation
                    {
                        N = (short)sn,
                        V = (sn % 2 == 0),
                        L = (short)f.Random.Number(1, 20)
                    })
                    .RuleFor(c => c.End, f => new MsLocation
                    {
                        N = (short)(sn + 1),
                        V = (sn + 1) % 2 == 0,
                        L = (short)f.Random.Number(1, 20)
                    })
                    .RuleFor(c => c.State, f => f.PickRandom("partial", "integral"))
                    .RuleFor(c => c.Note, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Units, GetUnits(Randomizer.Seed.Next(1, 3 + 1)))
                    .Generate());
            }

            return part;
        }
    }
}
