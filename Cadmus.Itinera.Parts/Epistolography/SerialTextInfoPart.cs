using Cadmus.Core;
using Cadmus.Parts;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Information about a serial text, like a letter or some other kind of
    /// text belonging to a series of exchanges.
    /// <para>Tag: <c>it.vedph.itinera.serial-text-info</c></para>.
    /// </summary>
    [Tag("it.vedph.itinera.serial-text-info")]
    public sealed class SerialTextInfoPart : PartBase
    {
        /// <summary>
        /// Gets or sets a human-readable, arbitrarily-defined identifier
        /// for this text.
        /// </summary>
        public string TextId { get; set; }

        /// <summary>
        /// Gets or sets the (prevalent) language (ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the verse.
        /// </summary>
        public string Verse { get; set; }

        /// <summary>
        /// Gets or sets the rhyme.
        /// </summary>
        public string Rhyme { get; set; }

        /// <summary>
        /// Gets or sets the heading(s) found in the manuscript text,
        /// sorted by their relevance (the most relevant first).
        /// </summary>
        public List<string> Headings { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        public List<CitedPerson> Authors { get; set; }

        /// <summary>
        /// Gets or sets the recipient(s) IDs. At least 1 recipient
        /// should be defined.
        /// </summary>
        public List<DecoratedId> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the IDs of the document(s) this text is
        /// replying to.
        /// </summary>
        public List<DecoratedId> ReplyingTo { get; set; }

        /// <summary>
        /// Gets or sets the references to related text passages.
        /// </summary>
        public List<DocReference> Related { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this text has been received.
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialTextInfoPart"/>
        /// class.
        /// </summary>
        public SerialTextInfoPart()
        {
            Headings = new List<string>();
            Authors = new List<CitedPerson>();
            Recipients = new List<DecoratedId>();
            ReplyingTo = new List<DecoratedId>();
            Related = new List<DocReference>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>id</c>, <c>language</c>, <c>subject</c>
        /// (filtered, with digits), <c>author</c> (list, filtered, with digits),
        /// <c>heading</c> (list, filtered, with digits), <c>recipient</c>
        /// (filtered, with digits), <c>reply-to</c> (filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(TextId))
                pins.Add(CreateDataPin("id", TextId));

            if (!string.IsNullOrEmpty(Language))
                pins.Add(CreateDataPin("language", Language));

            if (!string.IsNullOrEmpty(Subject))
            {
                pins.Add(CreateDataPin("subject",
                    DataPinHelper.DefaultFilter.Apply(Subject, true)));
            }

            if (Authors?.Count > 0)
            {
                foreach (CitedPerson author in Authors)
                {
                    pins.Add(CreateDataPin("author",
                        DataPinHelper.DefaultFilter.Apply(
                            author.Name.GetFullName(), true)));
                }
            }

            if (!string.IsNullOrEmpty(Genre))
            {
                pins.Add(CreateDataPin("genre",
                    DataPinHelper.DefaultFilter.Apply(Genre, true)));
            }

            if (Headings?.Count > 0)
            {
                foreach (string heading in Headings)
                {
                    pins.Add(CreateDataPin("heading",
                        DataPinHelper.DefaultFilter.Apply(heading, true)));
                }
            }

            if (Recipients?.Count > 0)
            {
                foreach (DecoratedId id in Recipients)
                {
                    pins.Add(CreateDataPin("recipient",
                        DataPinHelper.DefaultFilter.Apply(id.Id, true)));
                }
            }

            if (ReplyingTo?.Count > 0)
            {
                foreach (DecoratedId id in ReplyingTo)
                {
                    pins.Add(CreateDataPin("reply-to",
                        DataPinHelper.DefaultFilter.Apply(id.Id, true)));
                }
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
                    "The text's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The text's (main) language."),
                new DataPinDefinition(DataPinValueType.String,
                    "subject",
                    "The text's subject.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "author",
                    "The text's author(s) names.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "genre",
                    "The text's genre.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "heading",
                    "The text's heading(s).",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "recipient",
                    "The text's recipient ID(s).",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "reply-to",
                    "The ID(s) of the text(s) this one is replying to.",
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

            sb.Append("[SerialTextInfo]");

            if (!string.IsNullOrEmpty(TextId))
                sb.Append(TextId).Append(": ");

            if (!string.IsNullOrEmpty(Language))
                sb.Append(" [").Append(Language).Append(']');

            if (!string.IsNullOrEmpty(Subject)) sb.Append(Subject);

            return sb.ToString();
        }
    }
}
