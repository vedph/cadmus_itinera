﻿using Bogus;
using Cadmus.Core;
using Cadmus.Itinera.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Itinera.Parts.Codicology
{
    /// <summary>
    /// Person's hand part seeder.
    /// Tag: <c>seed.it.vedph.itinera.person-hand</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.itinera.person-hand")]
    public sealed class PersonHandPartSeeder : PartSeederBase
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

            PersonHandPart part = new Faker<PersonHandPart>()
                .RuleFor(p => p.PersonId, f => f.Lorem.Word())
                .RuleFor(p => p.Job, f => f.PickRandom("copyst", "writer", "poet"))
                .RuleFor(p => p.Others, SeederHelper.GetDocReferences(0, 3))
                .Generate();

            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
