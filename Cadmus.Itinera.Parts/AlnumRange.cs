namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A range of two alphanumeric values, i.e. strings which always start
    /// by a digit, and may continue with digits only, or include non-digit
    /// characters.
    /// </summary>
    public class AlnumRange
    {
        /// <summary>
        /// Gets or sets the start value in a range, or the unique value.
        /// </summary>
        public string A { get; set; }

        /// <summary>
        /// Gets or sets the end value in a range, or null for a unique value.
        /// </summary>
        public string B { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(B)? A : $"{A}-{B}";
        }
    }
}
