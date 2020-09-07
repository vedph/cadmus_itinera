using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Epistolographic chronotopic coordinates for a letter.
    /// Used by <see cref="EpistCoordsPart"/>.
    /// </summary>
    public class EpistCoords
    {
        /// <summary>
        /// Gets or sets the tag applied to these coordinates to categorize
        /// them (e.g. writing date and place, sending date and place,
        /// reconstructed date and place, etc.).
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the date as explicitly found in the text, when present
        /// (e.g. <c>kalendis aprilis</c>). This may or not correspond to the
        /// <see cref="Date"/>'s value.
        /// </summary>
        public string TextDate { get; set; }

        /// <summary>
        /// Gets or sets the sources for these coordinates.
        /// </summary>
        public List<LitCitation> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistCoords"/> class.
        /// </summary>
        public EpistCoords()
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
            return $"[{Tag}] {Place}, {Date}";
        }
    }
}
