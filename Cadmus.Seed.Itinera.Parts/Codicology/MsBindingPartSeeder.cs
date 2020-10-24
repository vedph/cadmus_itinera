using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's binding part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-binding</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-binding")]
    public sealed class MsBindingPartSeeder : PartSeederBase
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

            MsBindingPart part = new Faker<MsBindingPart>()
                .RuleFor(p => p.Century, f => f.Random.Short(8, 15))
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.CoverMaterial,
                    f => f.PickRandom("parchment", "paper"))
                .RuleFor(p => p.SupportMaterial,
                    f => f.PickRandom("parchment", "paper"))
                .RuleFor(p => p.Size, f =>
                {
                    return new PhysicalSize
                    {
                        W = new PhysicalDimension
                        {
                            Value = (float)SeederHelper.Truncate(
                                f.Random.Float(6, 21), 2),
                            Unit = "cm"
                        },
                        H = new PhysicalDimension
                        {
                            Value = (float)SeederHelper.Truncate(
                                f.Random.Float(8, 29), 2),
                            Unit = "cm"
                        },
                        D = new PhysicalDimension
                        {
                            Value = (float)SeederHelper.Truncate(
                                f.Random.Float(0.5f, 1.5f), 2),
                            Unit = "cm"
                        },
                    };
                }).Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
