using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's numberings.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-numberings</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-numberings")]
    public sealed class MsNumberingsPart : PartBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is the main numbering
        /// in the manuscript.
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Gets or sets the era (e.g. "coeva", "antica", "moderna", "recente").
        /// </summary>
        public string Era { get; set; }

        /// <summary>
        /// Gets or sets the numeric system.
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the technique.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Gets or sets the century number.
        /// </summary>
        public short Century { get; set; }

        /// <summary>
        /// Gets or sets the position of numbers in the page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets a description of eventual numbering issues.
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>era</c>, <c>system</c>, <c>technique</c>,
        /// <c>century</c>, <c>position</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(Era))
                pins.Add(CreateDataPin("era", Era));

            if (!string.IsNullOrEmpty(System))
                pins.Add(CreateDataPin("system", System));

            if (!string.IsNullOrEmpty(Technique))
                pins.Add(CreateDataPin("technique", Technique));

            if (Century != 0)
            {
                pins.Add(CreateDataPin("century",
                    Century.ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(Position))
                pins.Add(CreateDataPin("position", Position));

            return pins;
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

            sb.Append("[MsNumberings]");

            if (!string.IsNullOrEmpty(Era))
                sb.Append(" (").Append(Era).Append(')');

            if (!string.IsNullOrEmpty(System))
                sb.Append(' ').Append(System);

            if (!string.IsNullOrEmpty(Technique))
                sb.Append(": ").Append(Technique);

            if (IsMain) sb.Append('*');

            return sb.ToString();
        }
    }
}
