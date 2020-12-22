using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Information about a poetic text related to some letters.
    /// <para>Tag: <c>it.vedph.itinera.poetic-text-info</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.poetic-text-info")]
    public sealed class PoeticTextInfoPart : PartBase
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
        /// Gets or sets the (prevalent) metre, if any specified.
        /// </summary>
        public string Metre { get; set; }

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
        /// Gets or sets the IDs of the document(s) this text is
        /// replying to.
        /// </summary>
        public List<DecoratedId> ReplyingTo { get; set; }

        /// <summary>
        /// Gets or sets the author(s) this text is attributed to.
        /// </summary>
        public List<CitedPerson> Authors { get; set; }

        /// <summary>
        /// Gets or sets the references to related text passages.
        /// </summary>
        public List<DocReference> Related { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoeticTextInfoPart"/>
        /// class.
        /// </summary>
        public PoeticTextInfoPart()
        {
            Headings = new List<string>();
            Recipients = new List<DecoratedId>();
            ReplyingTo = new List<DecoratedId>();
            Authors = new List<CitedPerson>();
            Related = new List<DocReference>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>language</c>, <c>subject</c> (filtered, with
        /// digits), <c>metre</c>; also, for each author: <c>author-name</c>
        /// (filtered, with digits), <c>author-id</c> (prefixed by rank and :);
        /// finally, for each recipient/text this text is replying to:
        /// <c>recipient</c>, <c>reply-to</c> (both filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.AddValue("id", TextId);
            builder.AddValue("language", Language);
            builder.AddValue("subject", Subject, filter: true, filterOptions: true);
            builder.AddValue("metre", Metre);

            if (Authors?.Count > 0)
            {
                builder.AddValues("author-name",
                    from a in Authors select a.Name.GetFullName(), filter: true);

                foreach (CitedPerson author in Authors)
                {
                    builder.AddValues("author-id",
                        from a in author.Ids
                        select $"{a.Rank}:{a.Id}");
                }
            }

            if (Headings?.Count > 0)
            {
                foreach (string heading in Headings)
                {
                    builder.AddValue("heading", heading,
                        filter: true, filterOptions: true);
                }
            }

            if (Recipients?.Count > 0)
            {
                foreach (DecoratedId id in Recipients)
                {
                    builder.AddValue("recipient", id.Id,
                        filter: true, filterOptions: true);
                }
            }

            if (ReplyingTo?.Count > 0)
            {
                foreach (DecoratedId id in ReplyingTo)
                {
                    builder.AddValue("reply-to", id.Id,
                        filter: true, filterOptions: true);
                }
            }

            return builder.Build(this);
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
                    "The subject.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "metre",
                    "The text's (main) metre."),
                new DataPinDefinition(DataPinValueType.String,
                    "author-name",
                    "The author's name.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "metre",
                    "The author ID, prefixed by his rank plus :."),
                new DataPinDefinition(DataPinValueType.String,
                    "heading",
                    "The text's heading.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "recipient",
                    "The text's recipient ID.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "reply-to",
                    "The ID of the document the text is replying to.",
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

            sb.Append("[PoeticTextInfo]");

            if (!string.IsNullOrEmpty(TextId))
                sb.Append(TextId).Append(": ");

            sb.Append('[').Append(Language).Append("] ")
                .Append(Subject)
                .Append(" (").Append(Authors?.Count ?? 0).Append(')');

            return sb.ToString();
        }
    }
}
