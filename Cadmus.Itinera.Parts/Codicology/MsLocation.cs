namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A location (sheet number, recto/verso, and optional line number) in a
    /// manuscript.
    /// </summary>
    public class MsLocation
    {
        /// <summary>
        /// Gets or sets the sheet number.
        /// </summary>
        public short N { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this location refers to the
        /// sheet's recto (false) or verso (true).
        /// </summary>
        public bool V { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        public short L { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{N}{(V? 'v':'r')}" + (L > 0? " l." + L : "");
        }
    }
}
