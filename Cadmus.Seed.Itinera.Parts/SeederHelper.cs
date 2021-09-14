using Bogus;
using Cadmus.Bricks;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Epistolography;
using Cadmus.Parts;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts
{
    internal static class SeederHelper
    {
        /// <summary>
        /// Truncates the specified value to the specified number of decimals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>Truncated value.</returns>
        public static double Truncate(double value, int decimals)
        {
            double factor = (double)Math.Pow(10, decimals);
            return Math.Truncate(factor * value) / factor;
        }

        /// <summary>
        /// Gets a random number of document references.
        /// </summary>
        /// <param name="min">The min number of references to get.</param>
        /// <param name="max">The max number of references to get.</param>
        /// <returns>References.</returns>
        public static List<DocReference> GetDocReferences(int min, int max)
        {
            List<DocReference> refs = new List<DocReference>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                refs.Add(new Faker<DocReference>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Author, f => f.Lorem.Word())
                    .RuleFor(r => r.Work, f => f.Lorem.Word())
                    .RuleFor(r => r.Location,
                        f => $"{f.Random.Number(1, 24)}.{f.Random.Number(1, 1000)}")
                    .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return refs;
        }

        /// <summary>
        /// Gets a chronotope.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="year">The year.</param>
        /// <returns>The chronotope.</returns>
        public static Chronotope GetChronotope(string tag, int year)
        {
            return new Faker<Chronotope>()
                .RuleFor(c => c.Tag, tag)
                .RuleFor(c => c.Place, f => f.Address.City())
                .RuleFor(c => c.Date, HistoricalDate.Parse($"{year} AD"))
                .RuleFor(c => c.TextDate, f => f.Lorem.Sentence(3))
                .RuleFor(c => c.Sources, GetDocReferences(1, 3))
                .Generate();
        }

        /// <summary>
        /// Gets a random number of chronotopes.
        /// </summary>
        /// <param name="min">The min number of chronotopes to get.</param>
        /// <param name="max">The max number of chronotopes to get.</param>
        /// <returns>References.</returns>
        public static List<Chronotope> GetChronotopes(int min, int max)
        {
            List<Chronotope> refs = new List<Chronotope>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                refs.Add(new Faker<Chronotope>()
                    .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                    .RuleFor(r => r.Place, f => f.Address.City())
                    .RuleFor(r => r.IsPlaceDubious, f => f.Random.Bool(0.2f))
                    .RuleFor(r => r.Date, HistoricalDate.Parse($"{1300 + n} AD"))
                    .RuleFor(r => r.TextDate, f => f.PickRandom(null, "text date"))
                    .RuleFor(r => r.Sources, GetDocReferences(1, 3))
                    .Generate());
            }

            return refs;
        }

        public static List<DecoratedId> GetDecoratedIds(int min, int max)
        {
            List<DecoratedId> ids = new List<DecoratedId>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                ids.Add(new Faker<DecoratedId>()
                    .RuleFor(i => i.Id, f => f.Lorem.Word())
                    .RuleFor(i => i.Rank, f => f.Random.Short(1, 3))
                    .RuleFor(i => i.Tag, f => f.PickRandom(null, f.Lorem.Word()))
                    .RuleFor(i => i.Sources, GetDocReferences(min, max))
                    .Generate());
            }

            return ids;
        }

        public static List<string> GetExternalIds(int min, int max)
        {
            List<string> ids = new List<string>();

            Faker faker = new Faker();
            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
                ids.Add(faker.Lorem.Word() + n);

            return ids;
        }

        public static List<CitedPerson> GetCitedPersons(int min, int max)
        {
            List<CitedPerson> names = new List<CitedPerson>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                names.Add(new Faker<CitedPerson>()
                    .RuleFor(t => t.Ids, GetDecoratedIds(1, 2))
                    .RuleFor(t => t.Sources, GetDocReferences(1, 3))
                    .RuleFor(t => t.Name, new Faker<PersonName>()
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
                            })))
                    .RuleFor(t => t.Rank, f => f.Random.Short(0, 3))
                    .Generate());
            }

            return names;
        }

        public static List<Attachment> GetAttachments(int min, int max)
        {
            List<Attachment> attachments = new List<Attachment>();
            Faker<Attachment> faker = new Faker<Attachment>();

            for (int n = 1; n <= Randomizer.Seed.Next(min, max + 1); n++)
            {
                attachments.Add(new Faker<Attachment>()
                    .RuleFor(a => a.Id, f => f.Lorem.Word())
                    .RuleFor(a => a.IsLost, f => f.Random.Bool(0.2F))
                    .RuleFor(a => a.IsUnknown, f => f.Random.Bool(0.2F))
                    .RuleFor(a => a.ExternalIds,
                        f => f.Random.Bool(0.2F)
                            ? new List<string>(new[] { f.Lorem.Word() })
                            : null)
                    .RuleFor(a => a.Type, f => f.PickRandom("ms", "work"))
                    .RuleFor(a => a.Name, f => f.Lorem.Word())
                    .RuleFor(a => a.Portion,
                        f => f.Random.ReplaceNumbers("#.##-#.##"))
                    .RuleFor(a => a.Note, f => f.Lorem.Sentence())
                    .Generate());
            }

            return attachments;
        }
    }
}
