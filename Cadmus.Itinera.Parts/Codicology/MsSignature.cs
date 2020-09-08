namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's signature.
    /// </summary>
    public class MsSignature
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the library.
        /// </summary>
        public string Library { get; set; }

        /// <summary>
        /// Gets or sets the library's fund.
        /// </summary>
        public string Fund { get; set; }

        /// <summary>
        /// Gets or sets the location in the library.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{City}, {Library}, {Fund}, {Location}";
        }
    }
}
