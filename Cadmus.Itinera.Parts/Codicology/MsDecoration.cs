using Cadmus.Parts.General;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's decoration description.
    /// </summary>
    public class MsDecoration
    {
        /// <summary>
        /// Gets or sets the decoration's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a tag used to categorize or group decorations.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the decoration's subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the decoration's colors.
        /// </summary>
        public List<string> Colors { get; set; }

        /// <summary>
        /// Gets or sets the tool used for this decoration.
        /// </summary>
        public string Tool { get; set; }

        /// <summary>
        /// Gets or sets the location of the start sheet for this decoration.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the location of the end sheet for this decoration.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets the position of the decoration in the page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the decoration's 2D size.
        /// </summary>
        public PhysicalSize Size { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the relationship of this decoration with the text.
        /// </summary>
        public string TextRelation { get; set; }

        /// <summary>
        /// Gets or sets the image IDs. This ID represents the prefix for
        /// all the images depicting something related to this hand; e.g. if the
        /// ID is <c>draco</c>, we would expect any number of image resources
        /// named after it plus a conventional numbering, like <c>draco00001</c>,
        /// <c>draco00002</c>, etc.
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the guide letters.
        /// </summary>
        public List<MsGuideLetter> GuideLetters { get; set; }

        /// <summary>
        /// Gets or sets the artist for this decoration.
        /// </summary>
        public MsDecorationArtist Artist { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecoration"/> class.
        /// </summary>
        public MsDecoration()
        {
            Colors = new List<string>();
            GuideLetters = new List<MsGuideLetter>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{Type}] {Subject}: {Start} - {End}";
        }
    }
}
