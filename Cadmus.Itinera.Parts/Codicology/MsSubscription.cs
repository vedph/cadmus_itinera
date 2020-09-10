namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's subscription.
    /// </summary>
    public class MsSubscription
    {
        /// <summary>
        /// Gets or sets the location of the subscription in the manuscript.
        /// </summary>
        public MsLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the subscription's language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subscription's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Language}] @{Location}: " +
                (Text.Length > 60 ? Text.Substring(0, 60) + "..." : Text);
        }
    }
}
