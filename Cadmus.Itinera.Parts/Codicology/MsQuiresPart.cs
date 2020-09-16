using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;
using System.Globalization;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Mansucript's quires description.
    /// <para>Tag: <c>it.vedph.itinera.ms-quires</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-quires")]
    public sealed class MsQuiresPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<MsQuire> Quires { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsQuiresPart"/> class.
        /// </summary>
        public MsQuiresPart()
        {
            Quires = new List<MsQuire>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>sheet-count</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Quires?.Count ?? 0, false);

            if (Quires?.Count > 0)
            {
                builder.AddValues(
                    "sheet-count",
                    from q in Quires
                    select q.SheetCount.ToString(CultureInfo.InvariantCulture));
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

            sb.Append("[MsQuires]");

            if (Quires?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Quires)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Quires.Count > 3)
                    sb.Append("...(").Append(Quires.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
