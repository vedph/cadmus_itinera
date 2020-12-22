namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Locus criticus in a manuscript.
    /// </summary>
    public class MsContentLocus
    {
        /// <summary>
        /// Gets or sets the locus citation.
        /// </summary>
        public string Citation { get; set; }

        /// <summary>
        /// Gets or sets the locus text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the reference sheet for this locus.
        /// </summary>
        public MsLocation RefSheet { get; set; }

        /// <summary>
        /// Gets or sets the image identifier for this locus.
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Citation}: {Text}";
        }
    }
}
