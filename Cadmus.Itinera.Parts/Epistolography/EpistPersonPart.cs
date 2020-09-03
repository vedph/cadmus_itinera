using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Epistolography person part. This contains the ID and biographic data
    /// of the correspondent or any other person (the role makes the distinction
    /// explicit). It can also represent any other generic actor, e.g. a group
    /// of persons (like "Celestini").
    /// <para>Tag: <c>net.fusisoft.itinera.epist-person</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.epist-person")]
    public sealed class EpistPersonPart : PartBase
    {
        /// <summary>
        /// Gets or sets the person identifier, an internal human-readable ID
        /// for the correspondent, e.g. `barbato`, `flavio-2`, etc. It can used
        /// as a facility for referring to the correspondent across parts,
        /// without recurring to the machine-related item ID. Thus, it must be
        /// unique in the database.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the optional external IDs, from external resources
        /// which can be mapped to this correspondent.
        /// </summary>
        public List<string> ExternalIds { get; set; }

        /// <summary>
        /// Gets or sets the name(s).
        /// </summary>
        public List<PersonName> Names { get; set; }

        /// <summary>
        /// Gets or sets the sex: <c>M</c>=male, <c>F</c>=female, else
        /// unknown.
        /// </summary>
        public char Sex { get; set; }

        /// <summary>
        /// Gets or sets the optional birth date.
        /// </summary>
        public HistoricalDate BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the optional birth place.
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// Gets or sets the optional death date.
        /// </summary>
        public HistoricalDate DeathDate { get; set; }

        /// <summary>
        /// Gets or sets the optional death place.
        /// </summary>
        public string DeathPlace { get; set; }

        /// <summary>
        /// Gets or sets the person's bio. Usually this is a rich text
        /// like Markdown.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistPersonPart"/> class.
        /// </summary>
        public EpistPersonPart()
        {
            ExternalIds = new List<string>();
            Names = new List<PersonName>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>person-id</c> (when <see cref="PersonId"/>
        /// is specified); 0 or more <c>ext-id</c>'s; 0 or more <c>name</c>'s,
        /// filtered; <c>sex</c>, if specified; <c>birth-date-value</c> if
        /// specified; <c>death-date-value</c> if specified; filtered
        /// <c>birth-place</c> and <c>death-place</c> if specified; count of
        /// characters in <see cref="Bio"/> in <c>bio-length</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(PersonId))
                pins.Add(CreateDataPin("person-id", PersonId));

            if (ExternalIds?.Count > 0)
            {
                foreach (string id in ExternalIds)
                    pins.Add(CreateDataPin("ext-id", id));
            }

            if (Names?.Count > 0)
            {
                foreach (PersonName name in Names)
                {
                    pins.Add(CreateDataPin(
                        "name",
                        PinTextFilter.Apply(name.GetFullName())));
                }
            }

            if (Sex != '\0')
                pins.Add(CreateDataPin("sex", new string(Sex, 1)));

            if (BirthDate != null)
            {
                pins.Add(CreateDataPin("birth-date-value",
                    BirthDate.GetSortValue()
                    .ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(BirthPlace))
            {
                pins.Add(CreateDataPin("birth-place",
                    PinTextFilter.Apply(BirthPlace)));
            }

            if (DeathDate != null)
            {
                pins.Add(CreateDataPin("death-date-value",
                    DeathDate.GetSortValue().ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(DeathPlace))
            {
                pins.Add(CreateDataPin("death-place",
                    PinTextFilter.Apply(DeathPlace)));
            }

            int cc = Bio?.Length ?? 0;
            pins.Add(CreateDataPin("bio-length",
                cc.ToString(CultureInfo.InvariantCulture)));

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

            sb.Append("[EpistPerson]");

            if (!string.IsNullOrEmpty(PersonId))
                sb.Append(' ').Append(PersonId);

            if (Names?.Count > 0)
                sb.Append(' ').Append(Names[0].GetFullName());

            return sb.ToString();
        }
    }
}
