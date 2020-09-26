using Cadmus.Itinera.Parts.Epistolography;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// Chronotopic coordinates. Used by <see cref="ChronotopicsPart"/>.
    /// </summary>
    public class Chronotope
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
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chronotope"/> class.
        /// </summary>
        public Chronotope()
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
            return $"[{Tag}] {Place}, {Date}";
        }
    }
}
