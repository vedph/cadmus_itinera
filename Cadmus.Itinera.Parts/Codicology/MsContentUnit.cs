namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A unit in a <see cref="MsContent"/>.
    /// </summary>
    public class MsContentUnit
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the text of the optional incipit.
        /// </summary>
        public string Incipit { get; set; }

        /// <summary>
        /// Gets or sets the text of the optional explicit.
        /// </summary>
        public string Explicit { get; set; }

        /// <summary>
        /// Gets or sets the start sheet.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the end sheet.
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
            return $"{Label}: {Start}-{End}";
        }
    }
}
