﻿using Cadmus.Core;
using Cadmus.Core.Layers;
using System;
using System.Text.Json;

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
    }
}