using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's numbering(s).
    /// <para>Tag: <c>net.fusisoft.itinera.ms-numberings</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-numberings")]
    public sealed class MsNumberingsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the numberings.
        /// </summary>
       public List<MsNumbering> Numberings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsNumberingsPart"/>
        /// class.
        /// </summary>
        public MsNumberingsPart()
        {
            Numberings = new List<MsNumbering>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>, and a collection of pins
        /// with keys: <c>era-X-count</c>, <c>sys-X-count</c>,
        /// <c>tech-X-count</c>, <c>century</c>, <c>position</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Numberings?.Count ?? 0, false);

            if (Numberings?.Count > 0)
            {
                foreach (MsNumbering numbering in Numberings)
                {
                    if (!string.IsNullOrEmpty(numbering.Era))
                        builder.Increase(numbering.Era, false, "era-");

                    if (!string.IsNullOrEmpty(numbering.System))
                        builder.Increase(numbering.System, false, "sys-");

                    if (!string.IsNullOrEmpty(numbering.Technique))
                        builder.Increase(numbering.Technique, false, "tech-");

                    if (numbering.Century != 0)
                        builder.AddValue("century", numbering.Century);

                    if (!string.IsNullOrEmpty(numbering.Position))
                        builder.AddValue("position", numbering.Position);
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

            sb.Append("[MsNumberings]");

            if (Numberings?.Count > 0)
            {
                int n = 0;
                foreach (MsNumbering numbering in Numberings)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(numbering);
                }
                if (Numberings.Count > 3)
                    sb.Append("...(").Append(Numberings.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
