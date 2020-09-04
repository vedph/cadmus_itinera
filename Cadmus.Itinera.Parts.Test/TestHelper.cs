using Cadmus.Core;
using Cadmus.Core.Layers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
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

        public static List<LitCitation> GetCitations(int count)
        {
            List<LitCitation> citations = new List<LitCitation>();

            for (int i = 1; i <= count; i++)
            {
                citations.Add(new LitCitation
                {
                    Author = "Hom.",
                    Work = "Il.",
                    Location = "1.23",
                    Note = $"Note {i}",
                    Tag = i % 2 == 0 ? "even" : "odd"
                });
            }
            return citations;
        }
    }
}
