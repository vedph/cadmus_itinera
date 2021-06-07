using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Information about a hand in a manuscript.
    /// </summary>
    public class MsHand
    {
        /// <summary>
        /// Gets or sets a conventional, arbitrary identifier assigned to this
        /// hand and unique whithin the boundaries of the manuscript.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the hand's type(s), in order of importance.
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// Gets or sets a conventional, arbitrary identifier assigned to the
        /// person owning this hand.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the hand's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the initials description.
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Gets or sets the corrections description.
        /// </summary>
        public string Corrections { get; set; }

        /// <summary>
        /// Gets or sets the punctuation description.
        /// </summary>
        public string Punctuation { get; set; }

        /// <summary>
        /// Gets or sets the abbreviations description.
        /// </summary>
        public string Abbreviations { get; set; }

        /// <summary>
        /// Gets or sets the description of the reasons behind the hand's
        /// identification.
        /// </summary>
        public string IdReason { get; set; }

        /// <summary>
        /// Gets or sets the ranges of sheets written by this hand.
        /// </summary>
        public List<MsLocationRange> Ranges { get; set; }

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
        /// Gets or sets the signs description.
        /// </summary>
        public List<MsHandSign> Signs { get; set; }

        /// <summary>
        /// Gets or sets the images IDs. These IDs represent the prefixes for
        /// all the images depicting something related to this hand; e.g. if the
        /// ID is <c>ae</c>, we would expect any number of image resources
        /// named after it plus a conventional numbering, like <c>ae00001</c>,
        /// <c>ae00002</c>, etc.
        /// </summary>
        public List<string> ImageIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsHand"/> class.
        /// </summary>
        public MsHand()
        {
            Types = new List<string>();
            Ranges = new List<MsLocationRange>();
            Rubrications = new List<MsRubrication>();
            Subscriptions = new List<MsSubscription>();
            Signs = new List<MsHandSign>();
            ImageIds = new List<string>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id);
            if (!string.IsNullOrEmpty(PersonId))
                sb.Append(" [").Append(PersonId).Append(']');

            if (Ranges?.Count > 0)
                sb.Append(": ").Append(Ranges[0]);

            return sb.ToString();
        }
    }
}
