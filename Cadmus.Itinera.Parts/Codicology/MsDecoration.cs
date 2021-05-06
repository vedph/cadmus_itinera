using Cadmus.Parts;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's decoration description.
    /// </summary>
    public class MsDecoration
    {
        /// <summary>
        /// Gets or sets an identifier which can be arbitrarily assigned to this
        /// decoration.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the human-friendly name of this decoration.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the decoration's type, usually drawn from a thesaurus,
        /// like "pagina incipitaria", "pagina decorata", "illustrazione",
        /// "ornamentazione", "iniziali", etc.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the flags. These are typically drawn from a thesaurus,
        /// and represent single features of the element, which may or not be
        /// present in it, like "original", "unitary", "complete", "has tips",
        /// etc.
        /// </summary>
        public List<string> Flags { get; set; }

        /// <summary>
        /// Gets or sets the place of origin for this decoration.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the optional date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the artist for this decoration.
        /// </summary>
        public MsDecorationArtist Artist { get; set; }

        /// <summary>
        /// Gets or sets the optional references for this decoration.
        /// </summary>
        public List<DocReference> References { get; set; }

        /// <summary>
        /// Gets or sets the elements this decoration consists of.
        /// </summary>
        public List<MsDecorationElement> Elements { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecoration"/> class.
        /// </summary>
        public MsDecoration()
        {
            Flags = new List<string>();
            References = new List<DocReference>();
            Elements = new List<MsDecorationElement>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Type}] {Name} ({Elements?.Count ?? 0})";
        }
    }
}
