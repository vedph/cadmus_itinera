using System.Collections.Generic;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// Base class for realia cited in literary sources.
    /// </summary>
    public abstract class LitCitedBase
    {
        /// <summary>
        /// Gets or sets the identifications proposed for the cited entity,
        /// if any.
        /// </summary>
        public List<RankedId> Ids { get; set; }

        /// <summary>
        /// Gets or sets the source(s) for this cited person. Each source is
        /// a literary citation.
        /// </summary>
        public List<LitCitation> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitCitedBase"/> class.
        /// </summary>
        protected LitCitedBase()
        {
            Ids = new List<RankedId>();
            Sources = new List<LitCitation>();
        }
    }
}
