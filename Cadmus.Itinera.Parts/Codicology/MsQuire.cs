using System.Globalization;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's quire.
    /// </summary>
    public class MsQuire
    {
        /// <summary>
        /// Gets or sets a value indicating whether this quire represents the
        /// "normal" pattern in the collation.
        /// </summary>
        public bool IsNormal { get; set; }

        /// <summary>
        /// Gets or sets the start quire number.
        /// </summary>
        public short StartNr { get; set; }

        /// <summary>
        /// Gets or sets the end quire number (inclusive).
        /// </summary>
        public short EndNr { get; set; }

        /// <summary>
        /// Gets or sets the sheets count in the quire.
        /// </summary>
        public short SheetCount { get; set; }

        /// <summary>
        /// Gets or sets the sheet(s) count difference (plus/minus N).
        /// </summary>
        public short SheetDelta { get; set; }

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
