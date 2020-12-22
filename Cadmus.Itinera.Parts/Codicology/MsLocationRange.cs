namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A range of <see cref="MsLocation"/>'s.
    /// </summary>
    public class MsLocationRange
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Start}-{End}";
        }
    }
}
