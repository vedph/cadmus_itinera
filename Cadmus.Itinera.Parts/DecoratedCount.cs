namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A count value decorated with an ID and an optional note.
    /// </summary>
    public class DecoratedCount
    {
        /// <summary>
        /// Gets or sets the count's identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the count's value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id}={Value}";
        }
    }
}
