using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Letter's chronological and topological coordinates part.
    /// <para>Tag: <c>net.fusisoft.itinera.epist-coords</c>.</para>
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.itinera.epist-coords")]
    public sealed class EpistCoordsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        public List<EpistCoords> Coordinates { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistCoordsPart"/>
        /// class.
        /// </summary>
        public EpistCoordsPart()
        {
            Coordinates = new List<EpistCoords>();
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
                new StandardDataPinTextFilter());

            builder.Set("tot", Coordinates?.Count ?? 0, false);

            if (Coordinates?.Count > 0)
            {
                foreach (EpistCoords coords in Coordinates)
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
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[EpistCoords]");

            if (Coordinates?.Count > 0)
            {
                int n = 0;
                foreach (EpistCoords coords in Coordinates)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(coords);
                }

                if (Coordinates.Count > 3)
                    sb.Append("...(").Append(Coordinates.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
