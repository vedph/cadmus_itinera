using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// List of works connected to a letter-related person's biography.
    /// Tag: <c>net.fusisoft.itinera.epist-bio-works</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.itinera.epist-bio-works")]
    public sealed class EpistBioWorksPart : PartBase
    {
        /// <summary>
        /// Gets or sets the works.
        /// </summary>
        public List<LitBioWork> Works { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistBioWorksPart"/>
        /// class.
        /// </summary>
        public EpistBioWorksPart()
        {
            Works = new List<LitBioWork>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>tot-count</c>, <c>title</c> (filtered, with digits),
        /// <c>language</c>, <c>date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Works?.Count ?? 0, false);

            foreach (LitBioWork work in Works)
            {
                if (!string.IsNullOrEmpty(work.Title))
                    builder.AddValue("title", PinTextFilter.Apply(work.Title, true));

                if (!string.IsNullOrEmpty(work.Language))
                    builder.AddValue("language", work.Language);

                if (work.Date != null)
                    builder.AddValue("date-value", work.Date.GetSortValue());
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

            sb.Append("[EpistBioWorks]");

            if (Works?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (LitBioWork work in Works)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Works.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(work.Title);
                    if (work.IsLost) sb.Append('*');
                }
            }

            return sb.ToString();
        }
    }
}
