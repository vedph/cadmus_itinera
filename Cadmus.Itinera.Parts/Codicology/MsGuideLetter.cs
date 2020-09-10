using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's guide letter description.
    /// </summary>
    public class MsGuideLetter
    {
        /// <summary>
        /// Gets or sets the position in the manuscript's page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the morphology of the guide letter.
        /// </summary>
        public string Morphology { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Position}: {Morphology}";
        }
    }
}
