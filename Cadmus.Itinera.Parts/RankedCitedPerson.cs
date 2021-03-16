namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A <see cref="CitedPerson"/> with a rank. The rank is used to express
    /// the level of confidence of the attribution to this cited person of the
    /// object containing it.
    /// </summary>
    /// <seealso cref="CitedPerson" />
    public class RankedCitedPerson : CitedPerson
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public short Rank { get; set; }
    }
}
