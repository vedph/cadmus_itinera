using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's rubrication description.
    /// </summary>
    public class MsRubrication
    {
        /// <summary>
        /// Gets or sets the location(s) of the rubrication in the manuscript.
        /// </summary>
        public List<MsLocation> Locations { get; set; }

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
            Locations = new List<MsLocation>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Locations?.Count > 0
                ? $"{Type}: " + string.Join(" ", Locations)
                : Type;
        }
    }
}
