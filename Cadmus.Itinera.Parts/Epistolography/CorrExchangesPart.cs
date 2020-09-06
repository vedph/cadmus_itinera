using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
        /// <c>tot-count</c>=total count of exchanges in the part;
        /// <c>dubious-count</c>, <c>indirect-count</c>, <c>incoming-count</c>,
        /// <c>from-date-value</c>, <c>to-date-value</c>, <c>from-place</c>
        /// (filtered, including digits), <c>to-place</c> (filtered, including
        /// digits), <c>participant</c> (filtered including digits, and prefixed
        /// by his role between <c>[]</c>), <c>att-TYPE-count</c>=count of
        /// attachment of type TYPE (one count for each distinct type found
        /// in the part), <c>att-tot-count</c>=total count of attachments.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();
            builder.Set("tot", Exchanges.Count, false);

            foreach (CorrExchange exchange in Exchanges)
            {
                if (exchange.IsDubious) builder.Increase("dubious", false);
                if (exchange.IsIndirect) builder.Increase("indirect", false);
                if (exchange.IsFromParticipant) builder.Increase("incoming", false);

                if (exchange.From?.Date != null)
                {
                    builder.AddValue("from-date-value",
                        exchange.From.Date.GetSortValue());
                }

                if (!string.IsNullOrEmpty(exchange.From?.Place))
                {
                    builder.AddValue("from-place",
                        PinTextFilter.Apply(exchange.From.Place, true));
                }

                if (exchange.To?.Date != null)
                {
                    builder.AddValue("to-date-value",
                        exchange.To.Date.GetSortValue());
                }

                if (!string.IsNullOrEmpty(exchange.To?.Place))
                {
                    builder.AddValue("to-place",
                        PinTextFilter.Apply(exchange.To.Place, true));
                }

                if (exchange.Participants?.Count > 0)
                {
                    builder.AddValues("participants",
                        from p in exchange.Participants
                        select $"[{p.Tag}]{PinTextFilter.Apply(p.Id, true)}");
                }

                if (exchange?.Attachments.Count > 0)
                {
                    builder.Update(from a in exchange.Attachments
                                   select a.Type, false, "type-");
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
