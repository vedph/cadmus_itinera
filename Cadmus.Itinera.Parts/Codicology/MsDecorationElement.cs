using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// An element in a <see cref="MsDecoration"/>.
    /// </summary>
    public class MsDecorationElement
    {
        /// <summary>
        /// Gets or sets the key used for this element when it represents also
        /// a parent of other elements. Its scope is limited to the part.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the parent element's key, used to group sub-elements
        /// under an element. Its scope is limited to the part.
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets the type.
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
        /// Gets or sets the ranges of locations this element spans for.
        /// </summary>
        public List<MsLocationRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the typologies assigned to this element. These are
        /// typically drawn from a thesaurus, organized in sub-sets according
        /// to the element's <see cref="Type"/>; for instance, for type
        /// "ornamentation" you would have typologies like "fregi", "cornici",
        /// "grottesche", "stemmi", etc.
        /// </summary>
        public List<string> Typologies { get; set; }

        /// <summary>
        /// Gets or sets the decoration subject, when applicable. For letters,
        /// it might be the letter itself.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the colors, usually drawn from a thesaurus.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Gets or sets the gilding type, usually drawn from a thesaurus.
        /// </summary>
        public string Gilding { get; set; }

        /// <summary>
        /// Gets or sets the technique, usually drawn from a thesaurus.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Gets or sets the tool used for the element, usually drawn from
        /// a thesaurus.
        /// </summary>
        public string Tool { get; set; }

        /// <summary>
        /// Gets or sets the position of the element relative to the page,
        /// usually drawn from a thesaurus.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the height of the element, measured in lines.
        /// </summary>
        public short LineHeight { get; set; }

        /// <summary>
        /// Gets or sets the relation of this element with the text.
        /// </summary>
        public string TextRelation { get; set; }

        /// <summary>
        /// Gets or sets the element's description. Usually this is a rich
        /// text (Markdown).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image identifier, representing the prefix for all
        /// the images of the decoration; e.g. if it is <c>ae</c>, we would
        /// expect any number of image resources named after it plus a
        /// conventional numbering, like <c>ae00001</c>, <c>ae00002</c>, etc.
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecorationElement"/>
        /// class.
        /// </summary>
        public MsDecorationElement()
        {
            Flags = new List<string>();
            Ranges = new List<MsLocationRange>();
            Typologies = new List<string>();
            Colors = new List<string>();
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

            if (!string.IsNullOrEmpty(Key))
                sb.Append('#').Append(Key);

            if (!string.IsNullOrEmpty(ParentKey))
                sb.Append("<= #").Append(ParentKey);

            sb.Append('[').Append(Type).Append("] ");

            sb.Append(Subject);

            return sb.ToString();
        }
    }
}
