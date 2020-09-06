using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
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
        public List<LitBioEvent> Events { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistBioEventsPart"/>
        /// class.
        /// </summary>
        public EpistBioEventsPart()
        {
            Events = new List<LitBioEvent>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>count</c>, <c>type-TAG-count</c>, <c>date-value</c>,
        /// <c>place</c>, <c>participant</c>.
        /// The last 2 collections have their value filtered.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>
            {
                CreateDataPin("count",
                    Events.Count.ToString(CultureInfo.InvariantCulture))
            };

            if (Events?.Count > 0)
            {
                Dictionary<string, int> types = new Dictionary<string, int>();
                HashSet<double> dateValues = new HashSet<double>();
                HashSet<string> places = new HashSet<string>();
                HashSet<string> participants = new HashSet<string>();

                foreach (LitBioEvent e in Events)
                {
                    if (!types.ContainsKey(e.Type))
                        types[e.Type] = 1;
                    else
                        types[e.Type]++;

                    if (e.Date != null) dateValues.Add(e.Date.GetSortValue());
                    if (e.Places != null)
                    {
                        foreach (LitCitedPlace place in e.Places)
                            places.Add(PinTextFilter.Apply(place.Name, true));
                    }
                    if (e.Participants != null)
                    {
                        foreach (LitCitedPerson person in e.Participants)
                        {
                            participants.Add(
                                PinTextFilter.Apply(person.Name.GetFullName(), true));
                        }
                    }
                }

                foreach (string type in types.Keys)
                {
                    pins.Add(CreateDataPin(
                        $"type-{type}",
                        types[type].ToString(CultureInfo.InvariantCulture)));
                }

                foreach (double dv in dateValues)
                {
                    pins.Add(CreateDataPin("date-value",
                        dv.ToString(CultureInfo.InvariantCulture)));
                }
                foreach (string place in places)
                    pins.Add(CreateDataPin("place", place));
                foreach (string participant in participants)
                    pins.Add(CreateDataPin("participant", participant));
            }

            return pins;
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
                foreach (LitBioEvent e in Events) types[e.Type]++;
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
