﻿using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's decorations description.
    /// <para>Tag: <c>it.vedph.itinera.ms-decorations</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-decorations")]
    public sealed class MsDecorationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the decorations.
        /// </summary>
        public List<MsDecoration> Decorations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecorationsPart"/>
        /// class.
        /// </summary>
        public MsDecorationsPart()
        {
            Decorations = new List<MsDecoration>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>type-X-count</c>, <c>subject-X-count</c>,
        /// <c>color-X-count</c>, (all the keys with X are filtered, with digits),
        /// <c>artist-id</c> (filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Decorations?.Count ?? 0, false);

            if (Decorations?.Count > 0)
            {
                foreach (MsDecoration decoration in Decorations)
                {
                    builder.Increase(
                        DataPinHelper.DefaultFilter.Apply(decoration.Type, true),
                        false, "type-");

                    if (decoration.Date != null)
                        builder.AddValue("date-value", decoration.Date.GetSortValue());

                    foreach (MsDecorationElement element in decoration?.Elements)
                    {
                        builder.Increase(
                            DataPinHelper.DefaultFilter.Apply(element.Subject, true),
                            false,
                            "subject-");

                        foreach (string color in element.Colors)
                        {
                            builder.Increase(
                                DataPinHelper.DefaultFilter.Apply(color, true),
                                false,
                                "color-");
                        }
                    }

                    if (!string.IsNullOrEmpty(decoration.Artist?.Id))
                    {
                        builder.AddValue("artist-id", decoration.Artist.Id,
                            filter: true, filterOptions: true);
                    }
                }
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                    "tot-count",
                    "The total count of decorations."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "type-{TYPE}-count",
                    "The count of each decoration's type."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "subject-{SUBJECT}-count",
                    "The count of each decoration's subject."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "color-{COLOR}-count",
                    "The count of each decoration's colors."),
                new DataPinDefinition(DataPinValueType.String,
                    "artist-id",
                    "The list of decorations artists IDs.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The list of decorations sortable date values",
                    "M")
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[MsDecorations]");

            if (Decorations?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Decorations)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Decorations.Count > 3)
                    sb.Append("...(").Append(Decorations.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
