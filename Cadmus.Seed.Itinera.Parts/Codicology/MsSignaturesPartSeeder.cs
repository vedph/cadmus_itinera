using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's signatures part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-signatures</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-signatures")]
    public sealed class MsSignaturesPartSeeder : PartSeederBase
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

            MsSignaturesPart part = new MsSignaturesPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Signatures.Add(new Faker<MsSignature>()
                    .RuleFor(s => s.Tag, f => f.PickRandom(null, f.Lorem.Word()))
                    .RuleFor(s => s.City, f => f.Lorem.Word())
                    .RuleFor(s => s.Library, f => f.Lorem.Word())
                    .RuleFor(s => s.Fund, f => f.Lorem.Sentence(1, 3))
                    .RuleFor(s => s.Location, f => f.Random.AlphaNumeric(8))
                    .Generate());
            }

            return part;
        }
    }
}
