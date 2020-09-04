using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A dedication by the reference author to the correspondent, or vice-versa.
    /// Used in <see cref="CorrDedicationsPart"/>.
    /// </summary>
    public class CorrDedication
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the send date.
        /// </summary>
        public HistoricalDate DateSent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this dedication is by
        /// the author to the correspondent, or vice-versa. Given that the
        /// focus is on the author, the dedication by the author to the
        /// correspondent can be the marked case of the binary opposition.
        /// </summary>
        public bool IsByAuthor { get; set; }

        /// <summary>
        /// Gets or sets the source citations related to this dedication.
        /// </summary>
        public List<LitCitation> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrDedication"/> class.
        /// </summary>
        public CorrDedication()
        {
            Sources = new List<LitCitation>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Title}" + (IsByAuthor? "*":"") +
                (Date != null? ": " + Date.ToString() : "");
        }
    }
}
