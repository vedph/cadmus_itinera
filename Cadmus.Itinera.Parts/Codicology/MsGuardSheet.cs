using Fusi.Antiquity.Chronology;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's guard sheet description.
    /// </summary>
    public class MsGuardSheet
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a back (true)
        /// or front (false) guard sheet.
        /// </summary>
        public bool IsBack { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public MsLocation Location { get; set; }

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
            return $"[{(IsBack? 'B':'F')}] {Location} {Material}"
                + (Date != null? Date.ToString() : "");
        }
    }
}
