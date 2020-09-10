using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's recalls description.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-recalls</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-recalls")]
    public sealed class MsRecallsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the recalls.
        /// </summary>
        public List<MsRecall> Recalls { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsRecallsPart"/> class.
        /// </summary>
        public MsRecallsPart()
        {
            Recalls = new List<MsRecall>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// keys: <c>pos-X-count</c>, <c>vrt-X-count</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Recalls?.Count ?? 0, false);

            if (Recalls?.Count > 0)
            {
                foreach (MsRecall recall in Recalls)
                {
                    builder.Increase(recall.Position, false, "pos-");
                    builder.Increase(recall.IsVertical, false, "vrt-");
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

            sb.Append("[MsRecalls]");

            if (Recalls?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsRecall recall in Recalls)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(recall);
                }
                if (Recalls.Count > 3)
                    sb.Append("...(").Append(Recalls.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
