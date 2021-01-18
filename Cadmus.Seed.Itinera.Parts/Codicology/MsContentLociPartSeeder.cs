using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Seeder for <see cref="MsContentLociPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.ms-content-loci</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-content-loci")]
    public sealed class MsContentLociPartSeeder : PartSeederBase
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

            MsContentLociPart part = new MsContentLociPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 3 + 1);

            for (int n = 1; n <= count; n++)
            {
                part.Loci.Add(new Faker<MsContentLocus>()
                    .RuleFor(l => l.Citation,
                        f => $"{f.Random.Number(24)}.{f.Random.Number(800)}")
                    .RuleFor(l => l.Text, f => f.Lorem.Sentence())
                    .RuleFor(l => l.RefSheet, f => new MsLocation
                    {
                        N = n,
                        S = n % 2 == 0? "v" : "r",
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(l => l.ImageId, f => $"img{f.Random.Number(1, 100)}-")
                    .Generate());
            }

            return part;
        }
    }
}
