using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's watermarks.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-watermarks</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-watermarks")]
    public sealed class MsWatermarksPart : PartBase
    {
        /// <summary>
        /// Gets or sets the watermarks.
        /// </summary>
        public List<MsWatermark> Watermarks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsWatermarksPart"/> class.
        /// </summary>
        public MsWatermarksPart()
        {
            Watermarks = new List<MsWatermark>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>, and a collection of pins with
        /// keys: <c>subject-X-count</c>, <c>place</c> (filtered, with digits),
        /// <c>date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Watermarks?.Count ?? 0, false);

            if (Watermarks?.Count > 0)
            {
                foreach (MsWatermark watermark in Watermarks)
                {
                    builder.Increase(watermark.Subject, false, "subject-");

                    if (!string.IsNullOrEmpty(watermark.Place))
                    {
                        builder.AddValue("place",
                            watermark.Place, filter: true, filterOptions: true);
                    }

                    if (watermark.Date != null)
                        builder.AddValue("date-value", watermark.Date.GetSortValue());
                }
            }

            return builder.Build(this);
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

            sb.Append("[MsWatermarks]");

            sb.Append(string.Join(", ", from watermark in Watermarks
                                        select watermark.Subject));

            return sb.ToString();
        }
    }
}
