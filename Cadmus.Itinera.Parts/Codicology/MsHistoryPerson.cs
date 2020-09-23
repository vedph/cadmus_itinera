using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A person cited in a <see cref="MsHistoryPart"/>.
    /// </summary>
    public class MsHistoryPerson
    {
        /// <summary>
        /// Gets or sets the person's role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public PersonName Name { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets a note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the optional external IDs which can refer to this
        /// person in external resources.
        /// </summary>
        public List<string> ExternalIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsHistoryPerson"/>
        /// class.
        /// </summary>
        public MsHistoryPerson()
        {
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
            return $"[{Role}] {Name} {Date}";
        }
    }
}
