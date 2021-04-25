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
    /// <para>Tag: <c>it.vedph.itinera.corr-exchanges</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.corr-exchanges")]
    public sealed class CorrExchangesPart : PartBase
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
        /// <c>{TAG}-date-value</c>, <c>{TAG}-place</c> (filtered, including
        /// digits), <c>participant.ROLE</c> (filtered including digits),
        /// <c>att-TYPE-count</c>=count of attachment of type TYPE (one count
        /// for each distinct type found in the part), <c>att-tot-count</c>=
        /// total count of attachments.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder =
                new DataPinBuilder(DataPinHelper.DefaultFilter);

            builder.Set("tot", Exchanges?.Count ?? 0, false);

            foreach (CorrExchange exchange in Exchanges)
            {
                if (exchange.IsDubious) builder.Increase("dubious", false);
                if (exchange.IsIndirect) builder.Increase("indirect", false);
                if (exchange.IsFromParticipant) builder.Increase("incoming", false);

                if (exchange.Chronotopes?.Count > 0)
                {
                    foreach (Chronotope c in exchange.Chronotopes)
                    {
                        string tag = c.Tag?.ToLowerInvariant() ?? "";

                        if (c.Date != null)
                            builder.AddValue("date-value." + tag, c.Date.GetSortValue());

                        if (!string.IsNullOrEmpty(c.Place))
                        {
                            builder.AddValue("place." + tag,
                                c.Place, filter: true, filterOptions: true);
                        }
                    }
                }

                if (exchange.Participants?.Count > 0)
                {
                    foreach (var p in exchange.Participants)
                    {
                        builder.AddValue($"participant.{p.Tag ?? ""}", p.Id,
                            filter: true, filterOptions: true);
                    }
                }

                if (exchange.Attachments?.Count > 0)
                {
                    builder.Increase(from a in exchange.Attachments
                                     select a.Type, true, "att-");
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
                    "The total count of exchanges."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "dubious-count",
                    "The count of dubious exchanges."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "indirect-count",
                    "The count of indirect exchanges."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "incoming-count",
                    "The count of exchanges originated from participants."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value.{TAG}",
                    "The list of the exchange's sortable date values.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "place.{TAG}",
                    "The list of the exchange's places of origin.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "participant.{TAG}",
                    "The list of participants grouped by their role (tag).",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "att-{TYPE}-count",
                    "The counts of each type of attachment."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "att-tot-count",
                    "The total count of attachments.")
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
