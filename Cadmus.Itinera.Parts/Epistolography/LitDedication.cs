using System.Linq;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A literary dedication, used in <see cref="LitDedicationsPart"/>.
    /// </summary>
    public class LitDedication
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
        /// Gets or sets the persons involded in the dedication.
        /// </summary>
        public List<DecoratedId> Participants { get; set; }

        /// <summary>
        /// Gets or sets the source citations related to this dedication.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitDedication"/> class.
        /// </summary>
        public LitDedication()
        {
            Sources = new List<DocReference>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Title}" +
                (Participants?.Count > 0
                    ? string.Join(", ", from p in Participants select p.Id)
                    : "") +
                (Date != null? ": " + Date.ToString() : "");
        }
    }
}
