using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's loci critici part.
    /// <para>Tag: <c>it.vedph.itinera.ms-content-loci</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-content-loci")]
    public sealed class MsContentLociPart : PartBase
    {
        /// <summary>
        /// Gets or sets the loci.
        /// </summary>
        public List<MsContentLocus> Loci { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsContentLociPart"/> class.
        /// </summary>
        public MsContentLociPart()
        {
            Loci = new List<MsContentLocus>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a list of unique
        /// <c>citation</c>'s.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (Loci?.Count > 0)
            {
                pins.Add(CreateDataPin("tot-count",
                    Loci.Count.ToString(CultureInfo.InvariantCulture)));

                HashSet<string> citations = new HashSet<string>(
                    Loci.Select(l => l.Citation));
                foreach (string citation in citations)
                    pins.Add(CreateDataPin("citation", citation));
            }
            else pins.Add(CreateDataPin("tot-count", "0"));

            return pins;
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
                    "The total count of loci."),
                new DataPinDefinition(DataPinValueType.String,
                    "citation",
                    "The locus citation.",
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

            sb.Append("[MsContentLoci]");

            if (Loci?.Count > 0)
            {
                sb.Append(' ')
                  .Append(string.Join("; ", Loci.Select(l => l.Citation)));
            }

            return sb.ToString();
        }
    }
}
