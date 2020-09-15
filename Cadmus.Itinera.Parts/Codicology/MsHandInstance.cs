using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Information about a hand's instance in a manuscript.
    /// </summary>
    public class MsHandInstance
    {
        /// <summary>
        /// Gets or sets a conventional, arbitrary identifier assigned to this
        /// hand and unique whithin the boundaries of the manuscript.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the reasons behind the hand's
        /// identification.
        /// </summary>
        public string IdReason { get; set; }

        /// <summary>
        /// Gets or sets the start location of this hand in the manuscript.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the end location (inclusive) of this hand in the
        /// manuscript.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets a note about the hand's extent.
        /// </summary>
        public string ExtentNote { get; set; }

        /// <summary>
        /// Gets or sets the rubrications description.
        /// </summary>
        public List<MsRubrication> Rubrications { get; set; }

        /// <summary>
        /// Gets or sets the subscriptions description.
        /// </summary>
        public List<MsSubscription> Subscriptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsHandInstance"/> class.
        /// </summary>
        public MsHandInstance()
        {
            Rubrications = new List<MsRubrication>();
            Subscriptions = new List<MsSubscription>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id} {Start}-{End}";
        }
    }
}
