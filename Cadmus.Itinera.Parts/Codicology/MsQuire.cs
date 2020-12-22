using System.Globalization;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's quire.
    /// </summary>
    public class MsQuire
    {
        /// <summary>
        /// Gets or sets a generic tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the start quire number.
        /// </summary>
        public int StartNr { get; set; }

        /// <summary>
        /// Gets or sets the end quire number (inclusive).
        /// </summary>
        public int EndNr { get; set; }

        /// <summary>
        /// Gets or sets the sheets count in the quire.
        /// </summary>
        public int SheetCount { get; set; }

        /// <summary>
        /// Gets or sets the sheet(s) count difference (plus/minus N).
        /// </summary>
        public int SheetDelta { get; set; }

        /// <summary>
        /// Gets or sets an optional note about this quire.
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
            return $"{StartNr}-{EndNr}^{SheetCount}"
                + (SheetDelta != 0? SheetDelta.ToString(CultureInfo.InvariantCulture) : "")
                + (string.IsNullOrEmpty(Note)? "" : $" ({Note})");
        }
    }
}
