namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A literary passage citation: author, work, location, plus some optional
    /// metadata.
    /// </summary>
    public class LitCitation
    {
        /// <summary>
        /// Any classification tag meaningful in the data context.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The author ID (e.g. <c>Hom.</c>).
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The work ID (e.g. <c>Il.</c>).
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// The work's location (e.g. <c>12.34</c>).
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// A generic annotation.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return $"{Author}, {Work}, {Location}" +
                (string.IsNullOrEmpty(Tag)? "" : $" [{Tag}]");
        }
    }
}
