using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Pseudonyms assigned by the reference author to the correspondent,
    /// or vice-versa.
    /// Tag: <c>it.vedph.itinera.corr-pseudonyms</c>.
    /// </summary>
    [Tag("it.vedph.itinera.corr-pseudonyms")]
    public sealed class CorrPseudonymsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the pseudonyms.
        /// </summary>
        public List<CorrPseudonym> Pseudonyms { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrPseudonymsPart"/>
        /// class.
        /// </summary>
        public CorrPseudonymsPart()
        {
            Pseudonyms = new List<CorrPseudonym>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>=total count of pseudonyms;
        /// for each pseudonym, a <c>pseudonym</c> pin with value equal to
        /// <c>+</c>=author's pseudonym or <c>-</c>=non-author pseudonym,
        /// followed by the filtered pseudonym (including digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Pseudonyms?.Count ?? 0, false);

            if (Pseudonyms?.Count > 0)
            {
                builder.AddValues("pseudonym",
                    from p in Pseudonyms
                    select builder.ApplyFilter(options: true,
                        p.IsAuthor ? "+" : "-",
                        true,
                        p.Value));
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
                    "The total count of pseudonyms."),
                new DataPinDefinition(DataPinValueType.String,
                    "pseudonym",
                    "The list of pseudonyms prefixed by +=author's pseudonym" +
                        "or -=non-author pseudonym.",
                    "Mf")
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

            sb.Append("[CorrPseudonyms]");

            if (Pseudonyms?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (CorrPseudonym pseudonym in Pseudonyms)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Pseudonyms.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(pseudonym);
                }
            }

            return sb.ToString();
        }
    }
}
