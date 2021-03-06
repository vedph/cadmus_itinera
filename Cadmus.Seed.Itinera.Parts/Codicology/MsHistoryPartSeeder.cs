﻿using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's history part seeder.
    /// Tag: <c>seed.it.vedph.itinera.ms-history</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.ms-history")]
    public sealed class MsHistoryPartSeeder : PartSeederBase
    {
        private static List<GeoAddress> GetProvenances(int count)
        {
            List<GeoAddress> provenances = new List<GeoAddress>();

            for (int n = 1; n <= count; n++)
            {
                provenances.Add(new Faker<GeoAddress>()
                    .RuleFor(a => a.Area,
                        f => f.PickRandom("France", "Germany", "Italy"))
                    .RuleFor(a => a.Address,
                        f => $"{f.Lorem.Word()}, {f.Lorem.Word()}")
                    .Generate());
            }

            return provenances;
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

            MsHistoryPart part = new Faker<MsHistoryPart>()
                .RuleFor(p => p.Provenances, GetProvenances(2))
                .RuleFor(p => p.History, f => f.Lorem.Sentence())
                .Generate();
            SetPartMetadata(part, roleId, item);

            // persons
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Persons.Add(new Faker<MsHistoryPerson>()
                    .RuleFor(p => p.Role,
                        f => f.PickRandom("scribe", "corrector", "owner"))
                    .RuleFor(p => p.Name, new Faker<PersonName>()
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
                        .Generate())
                    .RuleFor(p => p.Date, HistoricalDate.Parse($"{1200 + n} AD"))
                    .RuleFor(p => p.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .RuleFor(p => p.ExternalIds, f => new List<string>(new[]
                        {
                            $"www.someurl.org/entities/{f.Random.Number()}"
                        }))
                    .RuleFor(p => p.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            // annotations
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Annotations.Add(new Faker<MsAnnotation>()
                    .RuleFor(a => a.Language, f => f.PickRandom("lat", "ita"))
                    .RuleFor(a => a.Type, f => f.Lorem.Word())
                    .RuleFor(a => a.Text, f => f.Lorem.Sentence())
                    .RuleFor(a => a.PersonId, f => f.Person.FirstName)
                    .RuleFor(r => r.Ranges, new List<MsLocationRange>(new[]
                    {
                        new MsLocationRange
                        {
                            Start = new MsLocation
                            {
                                N = n,
                                S = n % 2 == 0 ? "v" : "r",
                                L = n * 5
                            },
                            End = new MsLocation
                            {
                                N = n + 1,
                                S = n % 2 == 0 ? "r" : "v",
                                L = n * 5
                            }
                        }
                    }))
                    .RuleFor(f => f.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            // restorations
            for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
            {
                part.Restorations.Add(new Faker<MsRestoration>()
                    .RuleFor(r => r.Type, f => f.Lorem.Word())
                    .RuleFor(r => r.Place, f => f.Lorem.Word())
                    .RuleFor(r => r.Date, HistoricalDate.Parse($"{1200 + n} AD"))
                    .RuleFor(r => r.PersonId, f => f.Person.FirstName)
                    .RuleFor(r => r.Note, f => f.PickRandom(null, f.Lorem.Sentence()))
                    .RuleFor(r => r.Sources, SeederHelper.GetDocReferences(1, 3))
                    .Generate());
            }

            return part;
        }
    }
}
