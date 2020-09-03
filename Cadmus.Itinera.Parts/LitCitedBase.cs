using System.Collections.Generic;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// Base class for realia cited in literary sources.
    /// </summary>
    public abstract class LitCitedBase
    {
        /// <summary>
        /// Gets or sets the identifications proposed for this person, if any.
        /// </summary>
        public List<RankedIdent> Idents { get; set; }

        /// <summary>
        /// Gets or sets the sources for this cited person. Each source is
        /// a literary citation.
        /// </summary>
        public List<LitCitation> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitCitedBase"/> class.
        /// </summary>
        protected LitCitedBase()
        {
            Idents = new List<RankedIdent>();
            Sources = new List<LitCitation>();
        }
    }
}
