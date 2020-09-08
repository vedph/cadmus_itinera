using System.Collections.Generic;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// Base class for realia cited in literary sources.
    /// </summary>
    public abstract class CitedBase
    {
        /// <summary>
        /// Gets or sets the identifications proposed for the cited entity,
        /// if any.
        /// </summary>
        public List<DecoratedId> Ids { get; set; }

        /// <summary>
        /// Gets or sets the source(s) for this cited thing.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CitedBase"/> class.
        /// </summary>
        protected CitedBase()
        {
            Ids = new List<DecoratedId>();
            Sources = new List<DocReference>();
        }
    }
}
