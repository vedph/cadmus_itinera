using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's rubrication description.
    /// </summary>
    public class MsRubrication
    {
        /// <summary>
        /// Gets or sets the location of the rubrication in the manuscript.
        /// </summary>
        public MsLocation Location { get; set; }

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
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Type} @{Location}";
        }
    }
}
