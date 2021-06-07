using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's subscription.
    /// </summary>
    public class MsSubscription
    {
        /// <summary>
        /// Gets or sets the locations ranges of the rubrication in the manuscript.
        /// </summary>
        public List<MsLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the subscription's language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subscription's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSubscription"/> class.
        /// </summary>
        public MsSubscription()
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
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Language))
                sb.Append('[').Append(Language).Append(']');

            if (Ranges?.Count > 0)
                sb.Append('@').Append(string.Join(" ", Ranges));

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(": ")
                  .Append(Text.Length > 60 ? Text.Substring(0, 60) + "..." : Text);
            }

            return sb.ToString();
        }
    }
}
