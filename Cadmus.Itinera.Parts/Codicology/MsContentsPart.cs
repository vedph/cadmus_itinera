using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript contents list part.
    /// <para>Tag: <c>it.vedph.itinera.ms-contents</c></para>.
    /// </summary>
    [Tag("it.vedph.itinera.ms-contents")]
    public sealed class MsContentsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        public List<MsContent> Contents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsContentsPart"/> class.
        /// </summary>
        public MsContentsPart()
        {
            Contents = new List<MsContent>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>; a list of unique <c>author</c>
        /// <c>claimed-author</c>'s, and <c>work</c>'s (all filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder =
                new DataPinBuilder(new StandardDataPinTextFilter());

            builder.Set("tot", Contents?.Count ?? 0, false);

            if (Contents?.Count > 0)
            {
                foreach (MsContent content in Contents)
                {
                    builder.AddValue("author", content.Author,
                        filter: true, filterOptions: true);

                    if (!string.IsNullOrEmpty(content.ClaimedAuthor))
                    {
                        builder.AddValue("claimed-author", content.ClaimedAuthor,
                            filter: true, filterOptions: true);
                    }

                    builder.AddValue("work", content.Work,
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
                new DataPinDefinition(DataPinValueType.Integer,
                    "tot-count",
                    "The count of content sections."),
                new DataPinDefinition(DataPinValueType.String,
                    "author",
                    "The author of a section.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "claimed-author",
                    "The claimed author of a section.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "work",
                    "The work of a section.",
                    "Mf")
            });
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

            sb.Append("[MsContents]");

            if (Contents?.Count > 0)
            {
                sb.Append(string.Join("; ", Contents.Select(c => c.ToString())));
            }

            return sb.ToString();
        }
    }
}
