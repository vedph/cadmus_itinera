using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Essential information about a letter: language, subject, manuscript
    /// heading, and optional notes.
    /// <para>Tag: <c>it.vedph.itinera.letter-info</c>.</para>
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.letter-info")]
    public sealed class LetterInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets the language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subject. This can be Markdown.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the heading found in the manuscript text.
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets an optional note. This can be Markdown.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>language</c>, <c>subject</c> (filtered,
        /// with digits), <c>heading</c> (filtered, with digits).</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();
            IDataPinTextFilter filter = new StandardDataPinTextFilter();

            if (!string.IsNullOrEmpty(Language))
                pins.Add(CreateDataPin("language", Language));

            if (!string.IsNullOrEmpty(Subject))
                pins.Add(CreateDataPin("subject", filter.Apply(Subject, true)));

            if (!string.IsNullOrEmpty(Heading))
                pins.Add(CreateDataPin("heading", filter.Apply(Heading, true)));

            return pins;
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The letter's (main) language."),
                new DataPinDefinition(DataPinValueType.String,
                    "subject",
                    "The letter's subject.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "heading",
                    "The letter's heading.",
                    "f")
            });
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[LetterInfo]");

            if (!string.IsNullOrEmpty(Language))
                sb.Append(" [").Append(Language).Append(']');

            if (!string.IsNullOrEmpty(Subject)) sb.Append(Subject);

            return sb.ToString();
        }
    }
}
