using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's catchwords description.
    /// <para>Tag: <c>it.vedph.itinera.ms-catchwords</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-catchwords")]
    public sealed class MsCatchwordsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the catchwords.
        /// </summary>
        public List<MsCatchword> Catchwords { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsCatchwordsPart"/> class.
        /// </summary>
        public MsCatchwordsPart()
        {
            Catchwords = new List<MsCatchword>();
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

            builder.Set("tot", Catchwords?.Count ?? 0, false);

            if (Catchwords?.Count > 0)
            {
                foreach (MsCatchword catchword in Catchwords)
                {
                    builder.Increase(catchword.Position, false, "pos-");
                    builder.Increase(catchword.IsVertical, false, "vrt-");
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

            sb.Append("[MsCatchwords]");

            if (Catchwords?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsCatchword recall in Catchwords)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(recall);
                }
                if (Catchwords.Count > 3)
                    sb.Append("...(").Append(Catchwords.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
