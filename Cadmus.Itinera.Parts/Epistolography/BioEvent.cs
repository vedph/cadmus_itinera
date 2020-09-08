﻿using Fusi.Antiquity.Chronology;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A biographic event found in literary sources. This is used in
    /// <see cref="EpistBioEventsPart"/>.
    /// </summary>
    public class BioEvent
    {
        /// <summary>
        /// Gets or sets the event's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the event's date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets zero or more places connected to this event.
        /// </summary>
        public List<string> Places { get; set; }

        /// <summary>
        /// Gets or sets the event description, usually with a rich text
        /// like Markdown.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the literary sources for this event.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Gets or sets all the persons cited as involved in this event.
        /// </summary>
        public List<DecoratedId> Participants { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BioEvent"/> class.
        /// </summary>
        public BioEvent()
        {
            Places = new List<string>();
            Sources = new List<DocReference>();
            Participants = new List<DecoratedId>();
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

            if (!string.IsNullOrEmpty(Type))
                sb.Append('[').Append(Type).Append(']');

            if (Date != null) sb.Append(' ').Append(Date);

            if (!string.IsNullOrEmpty(Description))
            {
                sb.Append(Description.Length > 80 ?
                    Description.Substring(0, 80) + "..." : Description);
            }

            return sb.ToString();
        }
    }
}