using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Person works part seeder.
    /// Tag: <c>seed.it.vedph.itinera.person-works</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.person-works")]
    public sealed class PersonWorksPartSeeder : PartSeederBase
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

            PersonWorksPart part = new PersonWorksPart();
            SetPartMetadata(part, roleId, item);
            string[] languages = new[] { "eng", "ita", "fra", "deu", "spa" };

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Works.Add(new Faker<PersonWork>()
                    .RuleFor(a => a.Language, f => f.PickRandom(languages))
                    .RuleFor(a => a.IsDubious, f => f.Random.Bool(0.2f))
                    .RuleFor(a => a.IsLost, f => f.Random.Bool(0.2f))
                    .RuleFor(a => a.Genre, f => f.Lorem.Word())
                    .RuleFor(a => a.Titles, f => new List<string>(new[] {f.Lorem.Sentence()}))
                    .RuleFor(a => a.Chronotopes, SeederHelper.GetChronotopes(1, 3))
                    .RuleFor(a => a.References, SeederHelper.GetDocReferences(1, 3))
                    .RuleFor(a => a.Note,
                        f => f.Random.Bool(0.2f)? f.Lorem.Sentence() : null)
                    .Generate());
            }

            return part;
        }
    }
}
