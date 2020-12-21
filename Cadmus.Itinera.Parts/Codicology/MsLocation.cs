using System;
using System.Text;

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
        public int N { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MsLocation"/>
        /// is a Roman number.
        /// </summary>
        public bool R { get; set; }

        /// <summary>
        /// Gets or sets the sheet's side(s) this location refers to.
        /// </summary>
        public MsLocationSides S { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        public int L { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(N);
            if ((S & MsLocationSides.Recto) != 0) sb.Append('r');
            if ((S & MsLocationSides.Verso) != 0) sb.Append('v');
            if (L > 0) sb.Append(" l.").Append(L);
            return sb.ToString();
        }
    }

    /// <summary>
    /// Sheet sides for a <see cref="MsLocation"/>.
    /// </summary>
    [Flags]
    public enum MsLocationSides
    {
        /// <summary>Undefined</summary>
        Undefined = 0,
        /// <summary>Recto</summary>
        Recto = 0x01,
        /// <summary>Verso</summary>
        Verso = 0x02,
        /// <summary>Both recto and verso</summary>
        RectoAndVerso = Recto | Verso
    }
}
