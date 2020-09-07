using Fusi.Antiquity.Chronology;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A union of a date and a place, used to label a letter.
    /// </summary>
    public class EpistDatePlace
    {
        /// <summary>
        /// Gets or sets an optional tag used to group this union with others.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        public string Place { get; set; }

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
