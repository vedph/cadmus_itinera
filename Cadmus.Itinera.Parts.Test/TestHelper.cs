﻿using Cadmus.Bricks;
using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Parts;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit;

namespace Cadmus.Itinera.Parts.Test
{
    internal sealed class TestHelper
    {
        private static readonly JsonSerializerOptions _options =
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        public static string SerializePart(IPart part)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            return JsonSerializer.Serialize(part, part.GetType(), _options);
        }

        public static T DeserializePart<T>(string json)
            where T : class, IPart, new()
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));

            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public static string SerializeFragment(ITextLayerFragment fr)
        {
            if (fr == null)
                throw new ArgumentNullException(nameof(fr));

            return JsonSerializer.Serialize(fr, fr.GetType(), _options);
        }

        public static T DeserializeFragment<T>(string json)
            where T : class, ITextLayerFragment, new()
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json));

            return JsonSerializer.Deserialize<T>(json, _options);
        }

        public static void AssertPinIds(IPart part, DataPin pin)
        {
            Assert.Equal(part.ItemId, pin.ItemId);
            Assert.Equal(part.Id, pin.PartId);
            Assert.Equal(part.RoleId, pin.RoleId);
        }

        public static List<DocReference> GetDocReferences(int count)
        {
            List<DocReference> citations = new List<DocReference>();

            for (int n = 1; n <= count; n++)
            {
                citations.Add(new DocReference
                {
                    Author = "Hom.",
                    Work = "Il.",
                    Location = "1.23",
                    Note = $"Note {n}",
                    Tag = n % 2 == 0 ? "even" : "odd"
                });
            }
            return citations;
        }

        public static List<Chronotope> GetChronotopes(int count)
        {
            List<Chronotope> chronotopes = new List<Chronotope>();

            for (int n = 1; n <= count; n++)
            {
                chronotopes.Add(new Chronotope
                {
                    Place = $"place-{n}",
                    Date = HistoricalDate.Parse((1300 + n) + " AD"),
                    IsPlaceDubious = n % 1 != 0,
                    Sources = GetDocReferences(1),
                    Tag = "tag",
                    TextDate = "text date"
                });
            }

            return chronotopes;
        }

        static public bool IsDataPinNameValid(string name) =>
            Regex.IsMatch(name, @"^[a-zA-Z0-9\-_\.]+$");

        static public void AssertValidDataPinNames(IList<DataPin> pins)
        {
            foreach (DataPin pin in pins)
            {
                Assert.True(IsDataPinNameValid(pin.Name), pin.ToString());
            }
        }
    }
}
