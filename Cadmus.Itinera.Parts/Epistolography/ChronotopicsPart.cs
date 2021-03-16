using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Letter's chronological and topological coordinates part.
    /// <para>Tag: <c>it.vedph.itinera.chronotopics</c>.</para>
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.chronotopics")]
    public sealed class ChronotopicsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public List<Chronotope> Chronotopes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChronotopicsPart"/>
        /// class.
        /// </summary>
        public ChronotopicsPart()
        {
            Chronotopes = new List<Chronotope>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a list of pins under these
        /// keys: <c>tag-place</c>=TAG:place (place filtered, with digits),
        /// <c>tag-date</c>=TAG:normalized date value, <c>date-value</c>,
        /// <c>tag-VALUE-count</c>.
        /// The normalized date value is a fixed-format number of type
        /// +0000.00 or -0000.00 allowing a text sort.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Chronotopes?.Count ?? 0, false);

            if (Chronotopes?.Count > 0)
            {
                foreach (Chronotope coords in Chronotopes)
                {
                    builder.Increase(coords.Tag, false, "tag-");

                    string tag = coords.Tag ?? "";
                    if (!string.IsNullOrEmpty(coords.Place))
                    {
                        builder.AddValue(
                            "tag-place",
                            builder.ApplyFilter(options: true,
                                tag + ":",
                                true,
                                coords.Place));
                    }
                    if (coords.Date != null)
                    {
                        double d = coords.Date.GetSortValue();
                        builder.AddValue("date-value", d);

                        builder.AddValue(
                            "tag-date",
                            $"{tag}:{+d:0000.00;-d:0000.00}");
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
                    "The total count of chronotopic entries."),
                new DataPinDefinition(DataPinValueType.String,
                    "tag-place",
                    "The tag + : + place.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "tag-date",
                    "The tag + : + sortable date value with format 0000.00," +
                        "prefixed by - if negative.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The list of sortable date values.",
                    "M")
            });
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Chronotopics]");

            if (Chronotopes?.Count > 0)
            {
                int n = 0;
                foreach (Chronotope coords in Chronotopes)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(coords);
                }

                if (Chronotopes.Count > 3)
                    sb.Append("...(").Append(Chronotopes.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
