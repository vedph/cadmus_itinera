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
        /// (filtered, with digits), <c>author-id</c> (prefixed by rank and :).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

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
                    "The author ID, prefixed by his rank plus :.")
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

            sb.Append('[').Append(Language).Append("] ")
                .Append(Subject)
                .Append(" (").Append(Authors?.Count ?? 0).Append(')');

            return sb.ToString();
        }
    }
}
