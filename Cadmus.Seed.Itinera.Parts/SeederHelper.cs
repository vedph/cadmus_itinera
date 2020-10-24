using Bogus;
using Cadmus.Itinera.Parts;
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
                    .Generate());
            }

            return names;
        }
    }
}
