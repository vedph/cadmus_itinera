using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Seeder for <see cref="PersonPart"/>.
    /// Tag: <c>seed.it.vedph.itinera.person</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.person")]
    public sealed class PersonPartSeeder : PartSeederBase
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

            int birthYear = Randomizer.Seed.Next(1200, 1300);

            PersonPart part = new Faker<PersonPart>()
                .RuleFor(p => p.PersonId, f => f.Lorem.Word())
                .RuleFor(p => p.ExternalIds, SeederHelper.GetExternalIds(1, 2))
                .RuleFor(p => p.Names, new List<PersonName>(new[]
                {
                    new Faker<PersonName>()
                        .RuleFor(pn => pn.Language, "lat")
                        .RuleFor(pn => pn.Parts, f =>
                            new List<PersonNamePart>(new[]
                            {
                                new PersonNamePart
                                {
                                    Type = "first",
                                    Value = f.Lorem.Word(),
                                },
                                new PersonNamePart
                                {
                                    Type = "last",
                                    Value = f.Lorem.Word(),
                                }
                            }))
                        .Generate()
                }))
                .RuleFor(p => p.Sex, f => f.PickRandom('M', 'F'))
                .RuleFor(e => e.Chronotopes, new List<Chronotope>
                {
                    SeederHelper.GetChronotope("birth", birthYear),
                    SeederHelper.GetChronotope("death", birthYear + 80),
                })
                .RuleFor(p => p.Bio, f => f.Lorem.Sentence())
                .Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
