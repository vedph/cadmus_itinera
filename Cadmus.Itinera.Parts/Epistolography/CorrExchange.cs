﻿using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// An exchange of some objects between one correspondent and any number
    /// of other participants, whatever their role (e.g. bearer, recipient,
    /// etc.).
    /// </summary>
    public class CorrExchange
    {
        /// <summary>
        /// Gets or sets a value indicating whether this exchange is dubious,
        /// e.g. because other sources report it but we cannot find any
        /// confirmation.
        /// </summary>
        public bool IsDubious { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this exchange is indirect.
        /// </summary>
        public bool IsIndirect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the direction of the exchange:
        /// when this is true, the object is from the participant(s) to the
        /// reference person; when this is false, the object is from the reference
        /// person to the participant(s) (or at least to those of them whose
        /// role is specified as recipients).
        /// </summary>
        public bool IsFromParticipant { get; set; }

        /// <summary>
        /// Gets or sets the date and place of origin.
        /// </summary>
        public EpistDatePlace From { get; set; }

        /// <summary>
        /// Gets or sets the date and place of destination.
        /// </summary>
        public EpistDatePlace To { get; set; }

        /// <summary>
        /// Gets or sets the participants involved in this exchange, at the
        /// other side of the relationship with the reference author.
        /// </summary>
        public List<TaggedId> Participants { get; set; }

        /// <summary>
        /// Gets or sets the literary sources for this exchange.
        /// </summary>
        public List<LitCitation> Sources { get; set; }

        /// <summary>
        /// Gets or sets the attachments in this exchange.
        /// </summary>
        public List<EpistAttachment> Attachments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrExchange"/> class.
        /// </summary>
        public CorrExchange()
        {
            Participants = new List<TaggedId>();
            Sources = new List<LitCitation>();
            Attachments = new List<EpistAttachment>();
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

            char arrow = IsIndirect ? '-' : '=';
            sb.Append(IsFromParticipant? $"[<{arrow}]" : $"[{arrow}>]");
            if (Participants?.Count > 0)
                sb.Append(string.Join("; ", Participants));

            if (From != null) sb.Append(' ').Append(From);
            if (To != null) sb.Append(" - ").Append(To);

            sb.Append(": ").Append(Attachments?.Count ?? 0);

            return sb.ToString();
        }
    }
}