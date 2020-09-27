using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for <see cref="CitedPersonsPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.cited-persons</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.cited-persons")]
    public sealed class CitedPersonsPartSeeder : PartSeederBase
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

            CitedPersonsPart part = new CitedPersonsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3); n++)
            {
                part.Persons.Add(new Faker<CitedPerson>()
                    .RuleFor(p => p.Name, f => new PersonName
                    {
                        Language = f.PickRandom("lat", "ita"),
                        Tag = f.PickRandom(null, f.Lorem.Word()),
                        Parts = new List<PersonNamePart>(new[]
                        {
                            new PersonNamePart
                            {
                                Type = "first",
                                Value = f.Lorem.Word()
                            },
                            new PersonNamePart
                            {
                                Type = "last",
                                Value = f.Lorem.Word()
                            }
                        })
                    })
                    .RuleFor(p => p.Ids, SeederHelper.GetDecoratedIds(1, 2))
                    .RuleFor(p => p.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
