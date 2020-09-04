using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Exchanges of attachments sent from the reference author to one or more
    /// correspondents, or vice-versa.
    /// <para>Tag: <c>net.fusisoft.itinera.corr-exchanges</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.corr-exchanges")]
    public class CorrExchangesPart : PartBase
    {
        /// <summary>
        /// Gets or sets the exchanges.
        /// </summary>
        public List<CorrExchange> Exchanges { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrExchangesPart"/> class.
        /// </summary>
        public CorrExchangesPart()
        {
            Exchanges = new List<CorrExchange>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: a collection of pins with keys:
        /// <c>count</c>=total count of exchanges in the part;
        /// <c>dubious-count</c>, <c>indirect-count</c>, <c>incoming-count</c>,
        /// <c>from-date-value</c>, <c>to-date-value</c>, <c>from-place</c>
        /// (filtered), <c>to-place</c> (filtered), <c>participant</c>
        /// (filtered and prefixed by his role between <c>[]</c>),
        /// <c>att-TYPE-count</c>=count of attachment of type TYPE (one count
        /// for each distinct type found in the part).</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            pins.Add(CreateDataPin(
                "count",
                pins.Count.ToString(CultureInfo.InvariantCulture)));

            // collect data
            if (Exchanges?.Count > 0)
            {
                int dubious = 0, indirect = 0, incoming = 0;
                HashSet<double> fromDateValues = new HashSet<double>();
                HashSet<double> toDateValues = new HashSet<double>();
                HashSet<string> fromPlaces = new HashSet<string>();
                HashSet<string> toPlaces = new HashSet<string>();
                HashSet<string> participants = new HashSet<string>();
                Dictionary<string, int> types = new Dictionary<string, int>();

                foreach (CorrExchange exchange in Exchanges)
                {
                    // flags
                    if (exchange.IsDubious) dubious++;
                    if (exchange.IsIndirect) indirect++;
                    if (exchange.IsFromParticipant) incoming++;

                    // from
                    if (exchange.From?.Date != null)
                        fromDateValues.Add(exchange.From.Date.GetSortValue());
                    if (!string.IsNullOrEmpty(exchange.From?.Place))
                        fromPlaces.Add(PinTextFilter.Apply(exchange.From.Place, true));

                    // to
                    if (exchange.To?.Date != null)
                        toDateValues.Add(exchange.To.Date.GetSortValue());
                    if (!string.IsNullOrEmpty(exchange.To?.Place))
                        toPlaces.Add(PinTextFilter.Apply(exchange.To.Place, true));

                    // participants
                    if (exchange.Participants?.Count > 0)
                    {
                        foreach (TaggedId tid in exchange.Participants)
                            participants.Add($"[{tid.Tag}]{tid.Id}");
                    }

                    // attachments
                    foreach (EpistAttachment attachment in exchange.Attachments)
                    {
                        if (!types.ContainsKey(attachment.Type))
                            types[attachment.Type] = 1;
                        else
                            types[attachment.Type]++;
                    }
                }

                // build pins
                pins.Add(CreateDataPin(
                    "dubious-count",
                    dubious.ToString(CultureInfo.InvariantCulture)));
                pins.Add(CreateDataPin(
                    "indirect-count",
                    indirect.ToString(CultureInfo.InvariantCulture)));
                pins.Add(CreateDataPin(
                    "incoming-count",
                    incoming.ToString(CultureInfo.InvariantCulture)));

                foreach (double dv in fromDateValues)
                {
                    pins.Add(CreateDataPin("from-date-value",
                        dv.ToString(CultureInfo.InvariantCulture)));
                }
                foreach (string place in fromPlaces)
                    pins.Add(CreateDataPin("from-place", place));

                foreach (double dv in toDateValues)
                {
                    pins.Add(CreateDataPin("to-date-value",
                        dv.ToString(CultureInfo.InvariantCulture)));
                }
                foreach (string place in toPlaces)
                    pins.Add(CreateDataPin("to-place", place));

                foreach (string participant in participants)
                    pins.Add(CreateDataPin("participant", participant));

                foreach (string attType in types.Keys)
                {
                    pins.Add(CreateDataPin(
                        $"att-{attType}-count",
                        types[attType].ToString(CultureInfo.InvariantCulture)));
                }
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

            sb.Append("[CorrExchanges]");

            if (Exchanges?.Count > 0)
            {
                int n = 0;
                foreach (CorrExchange exchange in Exchanges)
                {
                    if (++n > 2)
                    {
                        sb.Append("[...").Append(Exchanges.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(exchange);
                }
            }

            return sb.ToString();
        }
    }
}
