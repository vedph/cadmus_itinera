using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's numbering.
    /// </summary>
    public class MsNumbering
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is the main numbering
        /// in the manuscript.
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Gets or sets the era (e.g. "coeva", "antica", "moderna", "recente").
        /// </summary>
        public string Era { get; set; }

        /// <summary>
        /// Gets or sets the numeric system.
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the technique.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Gets or sets the century number.
        /// </summary>
        public int Century { get; set; }

        /// <summary>
        /// Gets or sets the position of numbers in the page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets a description of eventual numbering issues.
        /// </summary>
        public string Issues { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Era))
                sb.Append('[').Append(Era).Append(']');

            if (!string.IsNullOrEmpty(System))
                sb.Append(' ').Append(System);

            if (!string.IsNullOrEmpty(Technique))
                sb.Append(": ").Append(Technique);

            if (IsMain) sb.Append('*');

            return sb.ToString();
        }
    }
}
