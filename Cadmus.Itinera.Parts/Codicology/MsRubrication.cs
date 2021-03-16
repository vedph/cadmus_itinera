using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's rubrication description.
    /// </summary>
    public class MsRubrication
    {
        /// <summary>
        /// Gets or sets the locations ranges of the rubrication in the manuscript.
        /// </summary>
        public List<MsLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the rubrication's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the rubrication's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the rubrication's issues.
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsRubrication"/> class.
        /// </summary>
        public MsRubrication()
        {
            Ranges = new List<MsLocationRange>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Ranges?.Count > 0
                ? $"{Type}: " + string.Join(" ", Ranges)
                : Type;
        }
    }
}
