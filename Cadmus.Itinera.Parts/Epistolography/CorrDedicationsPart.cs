using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Dedications by the reference author to a correspondent, or vice-versa.
    /// Tag: <c>it.vedph.itinera.corr-dedications</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.corr-dedications")]
    public sealed class CorrDedicationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the dedications.
        /// </summary>
        public List<CorrDedication> Dedications { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrDedicationsPart"/>
        /// class.
        /// </summary>
        public CorrDedicationsPart()
        {
            Dedications = new List<CorrDedication>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>title</c> (filtered, including digits) and <c>date-value</c>;
        /// in addition, <c>auth-count</c>=count of dedications by author;
        /// <c>corr-count</c>=count of dedications by correspondents;
        /// <c>tot-count</c>=total count of dedications (including 0).</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            foreach (CorrDedication dedication in Dedications)
            {
                builder.AddValue("title",
                    dedication.Title, filter: true, filterOptions: true);

                if (dedication.Date != null)
                    builder.AddValue("date-value", dedication.Date.GetSortValue());

                builder.Increase(dedication.IsByAuthor? "auth" : "corr");
            }

            return builder.Build(this, "tot");
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

            sb.Append("[CorrDedications]");

            if (Dedications?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var dedication in Dedications)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Dedications.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(dedication.Title);
                }
            }

            return sb.ToString();
        }
    }
}
