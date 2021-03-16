using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Attachments part seeder.
    /// Tag: <c>seed.it.vedph.itinera.attachments</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.attachments")]
    public sealed class AttachmentsPartSeeder : PartSeederBase
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

            AttachmentsPart part = new AttachmentsPart();
            SetPartMetadata(part, roleId, item);

            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Attachments.Add(new Faker<Attachment>()
                    .RuleFor(a => a.Id, f => f.Lorem.Word())
                    .RuleFor(a => a.IsLost, f => f.Random.Bool(0.2F))
                    .RuleFor(a => a.IsUnknown, f => f.Random.Bool(0.2F))
                    .RuleFor(a => a.ExternalIds,
                        f => f.Random.Bool(0.2F)
                            ? new List<string>(new[] { f.Lorem.Word() })
                            : null)
                    .RuleFor(a => a.Type, f => f.PickRandom("manuscript", "work"))
                    .RuleFor(a => a.Name, f => f.Lorem.Word())
                    .RuleFor(a => a.Portion,
                        f => f.Random.ReplaceNumbers("#.##-#.##"))
                    .RuleFor(a => a.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return part;
        }
    }
}
