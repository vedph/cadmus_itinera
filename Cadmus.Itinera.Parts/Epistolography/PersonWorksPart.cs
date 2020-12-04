using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Person's works.
    /// <para>Tag: <c>it.vedph.itinera.person-works</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.person-works")]
    public sealed class PersonWorksPart : PartBase
    {
        /// <summary>
        /// Gets or sets the works.
        /// </summary>
        public List<PersonWork> Works { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonWorksPart"/> class.
        /// </summary>
        public PersonWorksPart()
        {
            Works = new List<PersonWork>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>, <c>dubious-count</c>,
        /// <c>lost-count</c>, and a list with <c>title</c> (filtered, with
        /// digits), <c>genre</c>, <c>language</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            // tot-count
            builder.Set("tot", Works?.Count ?? 0, false);
            // dubious-count
            builder.Set("dubious", Works?.Count(w => w.IsDubious) ?? 0, false);
            // lost-count
            builder.Set("lost", Works?.Count(w => w.IsLost) ?? 0, false);

            if (Works?.Count > 0)
            {
                foreach (PersonWork work in Works)
                {
                    // title
                    builder.AddValues("title", work.Titles,
                        filter: true, filterOptions: true);
                    // genre
                    builder.AddValue("genre", work.Genre);
                    // language
                    builder.AddValue("language", work.Language);
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
                    "The total count of works."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "dubious-count",
                    "The total count of dubious works."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "lost-count",
                    "The total count of lost works."),
                new DataPinDefinition(DataPinValueType.String,
                    "title",
                    "The work's title.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "genre",
                    "The work's genre.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The work's language.",
                    "M"),
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

            sb.Append("[PersonWorks]");

            if (Works?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var work in Works)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(work);
                }
                if (Works.Count > 3)
                    sb.Append("...(").Append(Works.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
