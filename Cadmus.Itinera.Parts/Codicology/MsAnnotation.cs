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
        /// Gets or sets the ranges of locations where the annotation is found.
        /// </summary>
        public List<MsLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the optional sources for this annotation.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsAnnotation"/> class.
        /// </summary>
        public MsAnnotation()
        {
            Ranges = new List<MsLocationRange>();
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
            return $"[{Type}]: "
                + (Text?.Length > 60 ? Text.Substring(0, 60) + "..." : Text);
        }
    }
}
