using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
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
        private readonly string[] _types;
        private readonly string[] _flags;
        private readonly string[] _colors;

        public MsDecorationsPartSeeder()
        {
            _types = new[] { "pag-inc", "pag-dec", "ill" };
            _flags = new[] { "original", "unitary", "complete" };
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

        private List<MsDecorationElement> GetElements(int count, Faker faker)
        {
            List<MsDecorationElement> elements = new List<MsDecorationElement>();
            string[] typologies = new[] { "frieze", "frame" };
            string[] gildings = new[] { "leaf", "powder" };
            string[] positions = new[]
            {
                "top-left", "top-right", "bottom-left", "bottom-right"
            };

            for (int n = 1; n <= count; n++)
            {
                elements.Add(new MsDecorationElement
                {
                    Key = n == 1 ? "e1" : null,
                    ParentKey = n == 2 ? "e1" : null,
                    Type = faker.PickRandom(new[] { "pag-inc", "ill" }),
                    Flags = new List<string>(new[] { faker.PickRandom(_flags) }),
                    Ranges = new List<MsLocationRange>(new[]
                    {
                        new MsLocationRange
                        {
                            Start = new MsLocation
                            {
                                N = n,
                                S = n % 2 == 0
                                    ? "v"
                                    : "r",
                                L = faker.Random.Number(1, 30)
                            },
                            End = new MsLocation
                            {
                                N = n * 2,
                                S = "r",
                                L = faker.Random.Number(1, 30)
                            }
                        }
                    }),
                    Typologies = new List<string>(new[] { faker.PickRandom(typologies) }),
                    Subject = faker.Lorem.Word(),
                    Colors = GetColors(),
                    Gilding = faker.PickRandom(gildings),
                    // TODO: add from thesaurus
                    Technique = faker.Lorem.Word(),
                    Tool = faker.Lorem.Word(),
                    Position = faker.PickRandom(positions),
                    LineHeight = faker.Random.Short(1, 10),
                    TextRelation = faker.Lorem.Sentence(),
                    Description = faker.Lorem.Sentence(),
                    ImageId = "e" + n,
                    Note = faker.Random.Bool(0.25F)? faker.Lorem.Sentence() : null
                });
            }

            return elements;
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
                    .RuleFor(d => d.Id, f => "d" + f.UniqueIndex)
                    .RuleFor(d => d.Name, f => f.Lorem.Word())
                    .RuleFor(d => d.Type, f => f.PickRandom(_types))
                    .RuleFor(d => d.Flags,
                        f => new List<string>(new[] { f.PickRandom(_flags) }))
                    .RuleFor(d => d.Place, f => f.Address.Country())
                    .RuleFor(d => d.Artist, GetArtist())
                    .RuleFor(d => d.Note, f => f.Random.Bool(0.25f)
                        ? f.Lorem.Sentence() : null)
                    .RuleFor(d => d.References, SeederHelper.GetDocReferences(1, 3))
                    .RuleFor(d => d.Elements, f => GetElements(f.Random.Number(1, 3), f))
                    .Generate());
            }

            return part;
        }
    }
}
