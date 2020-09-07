using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A set of literary citations.
    /// Tag: <c>net.fusisoft.itinera.lit-citations</c>.
    /// </summary>
    /// <remarks>This is used for (a) for correspondent items: all the text
    /// passages referencing the correspondent or the reference author,
    /// (the part's role distinguishes these two usages); (b) for letter items:
    /// works cited in the letter's text.</remarks>
    [Tag("net.fusisoft.itinera.lit-citations")]
    public sealed class LitCitationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the citations.
        /// </summary>
        public List<LitCitation> Citations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitCitationsPart"/> class.
        /// </summary>
        public LitCitationsPart()
        {
            Citations = new List<LitCitation>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collections of pins with
        /// keys <c>author</c>, <c>work</c>, <c>tag</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Citations?.Count ?? 0, false);

            if (Citations?.Count > 0)
            {
                foreach (LitCitation citation in Citations)
                {
                    builder.AddValue("author", citation.Author);
                    builder.AddValue("work", citation.Work);
                    builder.AddValue("tag", citation.Tag);
                }
            }

            return builder.Build(this);
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

            sb.Append("[LitCitations]");

            if (Citations?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (LitCitation citation in Citations)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Citations.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(citation);
                }
            }

            return sb.ToString();
        }
    }
}
