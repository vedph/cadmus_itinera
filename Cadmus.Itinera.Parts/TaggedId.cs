namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A general purpose tagged ID, including an identifier and an optional
    /// tag.
    /// </summary>
    public class TaggedId
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Tag)? Id : $"[{Tag}] {Id}";
        }
    }
}
