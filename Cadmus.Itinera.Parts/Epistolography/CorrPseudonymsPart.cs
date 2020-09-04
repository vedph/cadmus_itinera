using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Pseudonyms assigned by the reference author to the correspondent,
    /// or vice-versa.
    /// Tag: <c>net.fusisoft.itinera.corr-pseudonyms</c>.
    /// </summary>
    [Tag("net.fusisoft.itinera.corr-pseudonyms")]
    public class CorrPseudonymsPart : PartBase
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
        /// <returns>The pins: for each pseudonym, a <c>pseudonym</c> pin
        /// with value equal to <c>+</c>=author's pseudonym or <c>-</c>=non-author
        /// pseudonym, followed by the filtered pseudonym.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            foreach (CorrPseudonym pseudonym in Pseudonyms)
            {
                string value = (pseudonym.IsAuthor ? "+" : "-")
                    + PinTextFilter.Apply(pseudonym.Value);
                pins.Add(CreateDataPin("pseudonym", value));
            }

            return pins;
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
                int i = 0;
                foreach (var pseudonym in Pseudonyms)
                {
                    if (++i > 1) sb.Append(", ");
                    sb.Append(pseudonym);
                }
            }

            return sb.ToString();
        }
    }
}
