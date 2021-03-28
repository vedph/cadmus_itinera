using Cadmus.Parts;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// An artist of a <see cref="MsDecoration"/>.
    /// </summary>
    public class MsDecorationArtist
    {
        /// <summary>
        /// Gets or sets the artist's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the arbitrary identifier internally used for this
        /// artist.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the artist's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the sources for this artist.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecorationArtist"/>
        /// class.
        /// </summary>
        public MsDecorationArtist()
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
            return $"[{Type}] {Id} {Name}";
        }
    }
}
