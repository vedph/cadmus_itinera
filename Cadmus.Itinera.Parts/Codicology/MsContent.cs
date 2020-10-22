using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's content.
    /// </summary>
    public class MsContent
    {
        /// <summary>
        /// Gets or sets the author if any.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the claimed author if any.
        /// </summary>
        public string ClaimedAuthor { get; set; }

        /// <summary>
        /// Gets or sets the work.
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// Gets or sets the optional start location.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the optional end location.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets the optional state, usually from a thesaurus.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the optional units this section is built of.
        /// </summary>
        public List<MsContentUnit> Units { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsContent"/> class.
        /// </summary>
        public MsContent()
        {
            Units = new List<MsContentUnit>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Author)) sb.Append(Author).Append(", ");
            sb.Append(Work);
            if (Start != null)
            {
                sb.Append(Start);
                if (End != null) sb.Append('-').Append(End);
            }
            return sb.ToString();
        }
    }

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
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Label;
        }
    }
}
