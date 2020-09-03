namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// An identification with a rank.
    /// </summary>
    public class RankedIdent
    {
        /// <summary>
        /// Gets or sets the rank. This is a numeric value used to sort
        /// identifications in their order of probability. For a single
        /// identification, just leave the rank value equal (to 0). Otherwise,
        /// use 1=highest probability, 2=lower than 1, and so on.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the identifier expressing the identification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id} [{Rank}]";
        }
    }
}
