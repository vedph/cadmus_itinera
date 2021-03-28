using Cadmus.Parts;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// An annotation in a manuscript.
    /// </summary>
    public class MsAnnotation
    {
        /// <summary>
        /// Gets or sets the annotation's language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the annotation's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the annotation's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the location of the manuscript where the annotation
        /// starts.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the location of the manuscript where the annotation
        /// ends.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets the optional sources for this annotation.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsAnnotation"/> class.
        /// </summary>
        public MsAnnotation()
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
            return $"[{Type}] {Start} - {End}: "
                + (Text?.Length > 60 ? Text.Substring(0, 60) + "..." : Text);
        }
    }
}
