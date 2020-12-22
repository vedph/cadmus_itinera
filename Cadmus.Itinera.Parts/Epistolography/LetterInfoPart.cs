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
        /// Gets or sets a human-readable, arbitrarily-defined identifier
        /// for this letter.
        /// </summary>
        public string LetterId { get; set; }

        /// <summary>
        /// Gets or sets the language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subject. This can be Markdown.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the arbitrarily defined author identifier.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the heading(s) found in the manuscript text,
        /// sorted by their relevance (the most relevant first).
        /// </summary>
        public List<string> Headings { get; set; }

        /// <summary>
        /// Gets or sets the recipient(s) IDs. At least 1 recipient
        /// should be defined.
        /// </summary>
        public List<DecoratedId> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the IDs of the document(s) this letter is
        /// replying to.
        /// </summary>
        public List<DecoratedId> ReplyingTo { get; set; }

        /// <summary>
        /// Gets or sets an optional note. This can be Markdown.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterInfoPart"/> class.
        /// </summary>
        public LetterInfoPart()
        {
            Headings = new List<string>();
            Recipients = new List<DecoratedId>();
            ReplyingTo = new List<DecoratedId>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>language</c>, <c>subject</c> (filtered,
        /// with digits), <c>heading</c> (filtered, with digits), <c>recipient</c>
        /// (filtered, with digits), <c>reply-to</c> (filtered, with digits),
        /// <c>author</c> (filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();
            IDataPinTextFilter filter = new StandardDataPinTextFilter();

            if (!string.IsNullOrEmpty(LetterId))
                pins.Add(CreateDataPin("id", LetterId));

            if (!string.IsNullOrEmpty(Language))
                pins.Add(CreateDataPin("language", Language));

            if (!string.IsNullOrEmpty(Subject))
                pins.Add(CreateDataPin("subject", filter.Apply(Subject, true)));

            if (!string.IsNullOrEmpty(AuthorId))
                pins.Add(CreateDataPin("author", filter.Apply(AuthorId, true)));

            if (Headings?.Count > 0)
            {
                foreach (string heading in Headings)
                    pins.Add(CreateDataPin("heading", filter.Apply(heading, true)));
            }

            if (Recipients?.Count > 0)
            {
                foreach (DecoratedId id in Recipients)
                    pins.Add(CreateDataPin("recipient", filter.Apply(id.Id, true)));
            }

            if (ReplyingTo?.Count > 0)
            {
                foreach (DecoratedId id in ReplyingTo)
                    pins.Add(CreateDataPin("reply-to", filter.Apply(id.Id, true)));
            }

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
                    "id",
                    "The letter's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The letter's (main) language."),
                new DataPinDefinition(DataPinValueType.String,
                    "author",
                    "The letter's author ID.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "subject",
                    "The letter's subject.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "heading",
                    "The letter's heading.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "recipient",
                    "The letter's recipient ID.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "reply-to",
                    "The ID of the document the letter is replying to.",
                    "Mf")
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

            if (!string.IsNullOrEmpty(LetterId))
                sb.Append(LetterId).Append(": ");

            if (!string.IsNullOrEmpty(Language))
                sb.Append(" [").Append(Language).Append(']');

            if (!string.IsNullOrEmpty(Subject)) sb.Append(Subject);

            return sb.ToString();
        }
    }
}
