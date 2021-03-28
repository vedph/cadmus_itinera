using Cadmus.Parts;
using Fusi.Antiquity.Chronology;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A biographic event found in literary sources. This is used in
    /// <see cref="PersonEventsPart"/>.
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
        /// Gets or sets the work, whatever it is (literary, artistic, etc.),
        /// connected to this event. For instance, for a literary work this
        /// should contain the work's title.
        /// </summary>
        public string Work { get; set; }

        /// <summary>
        /// Gets or sets the confindence rank: 0=not specified, 1=sure,
        /// 2=dubious, etc. This is often used in connection with <see cref="Work"/>
        /// when the work's attribution is not sure.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Work"/> is dubious.
        /// </summary>
        public bool IsWorkDubious { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Work"/> is lost.
        /// </summary>
        public bool IsWorkLost { get; set; }

        /// <summary>
        /// Gets or sets the external IDs connected to this event or its work.
        /// </summary>
        public List<string> ExternalIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BioEvent"/> class.
        /// </summary>
        public BioEvent()
        {
            Places = new List<string>();
            Sources = new List<DocReference>();
            Participants = new List<DecoratedId>();
            ExternalIds = new List<string>();
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
