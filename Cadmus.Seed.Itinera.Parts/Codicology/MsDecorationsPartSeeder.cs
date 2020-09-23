using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Parts.General;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's decorations description seeder.
    /// <para>Tag: <c>seed.it.vedph.itinera.ms-decorations</c>.</para>
    /// </summary>
    [Tag("seed.it.vedph.itinera.ms-decorations")]
    public sealed class MsDecorationsPartSeeder : PartSeederBase
    {
        private readonly string[] _colors;

        public MsDecorationsPartSeeder()
        {
            _colors = new[] { "red", "green", "blue", "violet", "gold" };
        }

        private List<string> GetColors()
        {
            List<string> colors = new List<string>();
            int count = Randomizer.Seed.Next(1, 3 + 1);

            for (int n = 1; n <= count; n++)
                colors.Add(_colors[Randomizer.Seed.Next(0, _colors.Length)]);
            return colors;
        }

        private static List<MsGuideLetter> GetGuideLetters()
        {
            List<MsGuideLetter> letters = new List<MsGuideLetter>();
            int count = Randomizer.Seed.Next(1, 3 + 1);

            for (int n = 1; n <= count; n++)
            {
                letters.Add(new Faker<MsGuideLetter>()
                    .RuleFor(l => l.Position, f => f.PickRandom("top", "bottom"))
                    .RuleFor(l => l.Morphology, f => f.Lorem.Word())
                    .Generate());
            }

            return letters;
        }

        private MsDecorationArtist GetArtist()
        {
            return new Faker<MsDecorationArtist>()
                .RuleFor(a => a.Type, f => f.PickRandom("draftsman", "miniator"))
                .RuleFor(a => a.Id, f => f.Lorem.Word())
                .RuleFor(a => a.Name, f => f.Lorem.Sentence(2))
                .RuleFor(a => a.Note, f => f.Lorem.Sentence())
                .RuleFor(a => a.Sources, SeederHelper.GetDocReferences(1, 3))
                .Generate();
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

            MsDecorationsPart part = new MsDecorationsPart();
            SetPartMetadata(part, roleId, item);

            int count = Randomizer.Seed.Next(1, 3);
            for (int n = 1; n <= count; n++)
            {
                int sn = (n - 1) * 2;

                part.Decorations.Add(new Faker<MsDecoration>()
                    .RuleFor(d => d.Type, f => f.Lorem.Word())
                    .RuleFor(d => d.Subject, f => f.Lorem.Sentence(1, 5))
                    .RuleFor(d => d.Colors, GetColors())
                    .RuleFor(d => d.Layout, f => f.Lorem.Word())
                    .RuleFor(d => d.Tool, f => f.Lorem.Word())
                    .RuleFor(d => d.Start, f => new MsLocation
                    {
                        N = sn,
                        V = sn % 2 == 0,
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(d => d.End, f => new MsLocation
                    {
                        N = sn + 1,
                        V = (sn + 1) % 2 == 0,
                        L = f.Random.Number(1, 20)
                    })
                    .RuleFor(d => d.Position,
                        f => f.PickRandom("bottom", "top", "left", "right"))
                    .RuleFor(d => d.Size, f => new PhysicalSize
                    {
                        W = new PhysicalDimension
                        {
                            Value = f.Random.Float(2, 6),
                            Unit = "cm"
                        },
                        H = new PhysicalDimension
                        {
                            Value = f.Random.Float(2, 6),
                            Unit = "cm"
                        }
                    })
                    .RuleFor(d => d.Description, f => f.Lorem.Sentence())
                    .RuleFor(d => d.TextRelation, f => f.Lorem.Sentence())
                    .RuleFor(d => d.ImageId, f => f.Lorem.Word())
                    .RuleFor(d => d.GuideLetters, GetGuideLetters())
                    .RuleFor(d => d.Artist, GetArtist())
                    .Generate());
            }

            return part;
        }
    }
}
