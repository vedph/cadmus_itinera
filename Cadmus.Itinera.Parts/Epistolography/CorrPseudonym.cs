using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A pseudonym assigned to a correspondent or to the reference author.
    /// This is used in the <see cref="CorrPseudonymsPart"/>.
    /// </summary>
    public class CorrPseudonym
    {
        /// <summary>
        /// Gets or sets the language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this pseudonym refers to
        /// the reference author or to his correspondent.
        /// </summary>
        public bool IsAuthor { get; set; }

        /// <summary>
        /// Gets or sets the source citations for this pseudonym.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrPseudonym"/> class.
        /// </summary>
        public CorrPseudonym()
        {
            Sources = new List<DocReference>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value + (IsAuthor ? "*" : "");
        }
    }
}
