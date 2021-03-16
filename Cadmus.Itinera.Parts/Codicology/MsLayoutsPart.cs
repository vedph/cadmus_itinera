using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Cadmus.Parts.General;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's physical layouts part.
    /// <para>Tag: <c>it.vedph.itinera.ms-layouts</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-layouts")]
    public sealed class MsLayoutsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the layouts.
        /// </summary>
        public List<MsLayout> Layouts { get;set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsLayoutsPart"/>
        /// class.
        /// </summary>
        public MsLayoutsPart()
        {
            Layouts = new List<MsLayout>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: list with these keys: <c>cols</c>=columns count;
        /// <c>ruling</c>=ruling technique; <c>derolez</c>=Derolez classification;
        /// <c>pricking</c>=pricking; <c>d.{TAG}</c>=dimensions.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            if (Layouts?.Count > 0)
            {
                foreach (MsLayout layout in Layouts)
                {
                    builder.AddValue("cols", layout.ColumnCount);

                    if (!string.IsNullOrEmpty(layout.RulingTechnique))
                        builder.AddValue("ruling", layout.RulingTechnique);

                    if (!string.IsNullOrEmpty(layout.Derolez))
                        builder.AddValue("derolez", layout.Derolez);

                    if (!string.IsNullOrEmpty(layout.Pricking))
                        builder.AddValue("pricking", layout.Pricking);

                    if (layout.Dimensions?.Count > 0)
                    {
                        foreach (PhysicalDimension dim in layout.Dimensions)
                        {
                            builder.AddValue($"d.{dim.Tag ?? "-"}",
                                dim.Value.ToString("00.0", CultureInfo.InvariantCulture));
                        }
                    }
                }
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions: <c>cols</c>, <c>ruling</c>,
        /// <c>d.{TAG}</c> with format 00.0 (for no tag, <c>-</c> is used).
        /// </returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                    "cols",
                    "The number of columns.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "ruling",
                    "The ruling technique.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "derolez",
                    "The Derolez classification.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "pricking",
                    "The pricking type.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "d.{TAG}",
                    "The list of dimensions grouped by their tag, " +
                    "with format 00.0.",
                    "M"),
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

            sb.Append("[MsLayouts]");

            if (Layouts?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsLayout layout in Layouts)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(layout);
                }
                if (Layouts.Count > 3)
                    sb.Append("...(").Append(Layouts.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
