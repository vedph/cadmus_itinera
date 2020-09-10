namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Description of any graphical sign drawn by a <see cref="MsHand"/>.
    /// </summary>
    public class MsHandSign
    {
        /// <summary>
        /// Gets or sets the sign's identifier, an arbitrary string representing
        /// a letter, a ligature, a punctuation sign, or any other relevant
        /// symbol.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the sign's type (e.g. letter, ligature, punctuation...).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the sign's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image ID. This ID represents the prefix for
        /// all the images depicting something related to this hand; e.g. if the
        /// ID is <c>ae</c>, we would expect any number of image resources
        /// named after it plus a conventional numbering, like <c>ae00001</c>,
        /// <c>ae00002</c>, etc.
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
            return $"[{Type}] {Id}";
        }
    }
}
