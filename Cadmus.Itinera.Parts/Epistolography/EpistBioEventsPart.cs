using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Biographic events connected to letters.
    /// Tag: <c>net.fusisoft.itinera.epist-bio-events</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.itinera.epist-bio-events")]
    public sealed class EpistBioEventsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public List<BioEvent> Events { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistBioEventsPart"/>
        /// class.
        /// </summary>
        public EpistBioEventsPart()
        {
            Events = new List<BioEvent>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>tot-count</c>=total events count, <c>type-TAG-count</c>,
        /// <c>date-value</c>, <c>place</c> (filtered, with digits),
        /// <c>participant</c> (filtered, with digits, prefixed by tag in
        /// <c>[]</c>).</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Events?.Count ?? 0, false);

            if (Events?.Count > 0)
            {
                foreach (BioEvent e in Events)
                {
                    if (!string.IsNullOrEmpty(e.Type))
                        builder.Increase(e.Type, false, "type-");

                    if (e.Date != null)
                        builder.AddValue("date-value", e.Date.GetSortValue());

                    if (e.Places?.Count > 0)
                    {
                        builder.AddValues("place",
                            e.Places, filter: true, filterOptions: true);
                    }

                    if (e.Participants?.Count > 0)
                    {
                        builder.AddValues("participant",
                            from p in e.Participants
                            select builder.ApplyFilter(options: true,
                                p.Tag + ":", true, p.Id));
                    }
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

            sb.Append("[EpistBioEvents]");

            if (Events?.Count > 0)
            {
                sb.Append(' ');
                Dictionary<string, int> types = new Dictionary<string, int>();
                foreach (BioEvent e in Events) types[e.Type]++;
                int n = 0;
                foreach (string key in types.Keys.OrderBy(s => s))
                {
                    if (++n > 1) sb.Append("; ");
                    sb.Append(key).Append('=').Append(types[key]);
                }
            }

            return sb.ToString();
        }
    }
}
