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
        /// Gets or sets the decoration's color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this decoration is golded.
        /// </summary>
        public bool IsGolden { get; set; }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        public string Layout { get; set; }

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
            return $"[{Type}] {Subject}, {Color}: {Start} - {End}";
        }
    }
}
