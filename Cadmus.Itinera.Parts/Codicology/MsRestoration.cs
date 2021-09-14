using Cadmus.Bricks;
using Cadmus.Parts;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A restoration in a manuscript.
    /// </summary>
    public class MsRestoration
    {
        /// <summary>
        /// Gets or sets the restoration's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place where the restoration was made.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the date when the restoration was made.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the optional person identifier.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets a note about the restoration.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the sources for this restoration.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsRestoration"/> class.
        /// </summary>
        public MsRestoration()
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
            return $"[{Type}] {Place}, {Date}";
        }
    }
}
