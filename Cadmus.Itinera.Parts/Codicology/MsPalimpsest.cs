using Fusi.Antiquity.Chronology;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's palimpsest part.
    /// </summary>
    public class MsPalimpsest
    {
        /// <summary>
        /// Gets or sets the range of locations of this palimpsest in the
        /// manuscript.
        /// </summary>
        public MsLocationRange Range { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Range}" + (Date != null? Date.ToString() : "");
        }
    }
}
