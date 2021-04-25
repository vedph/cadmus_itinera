using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Person part. This contains the ID and biographic data of the
    /// correspondent or any other person (the role makes the distinction
    /// explicit). It can also represent any other generic actor, e.g. a group
    /// of persons (like "Celestini").
    /// <para>Tag: <c>it.vedph.itinera.person</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.person")]
    public sealed class PersonPart : PartBase
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
        /// Gets or sets a list of date/place pairs, typically used for
        /// birth and death.
        /// </summary>
        public List<Chronotope> Chronotopes { get; set; }

        /// <summary>
        /// Gets or sets the person's bio. Usually this is a rich text
        /// like Markdown.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonPart"/> class.
        /// </summary>
        public PersonPart()
        {
            ExternalIds = new List<string>();
            Names = new List<PersonName>();
            Chronotopes = new List<Chronotope>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>person-id</c> (when <see cref="PersonId"/>
        /// is specified); 0 or more <c>ext-id</c>'s; 0 or more <c>name</c>'s,
        /// filtered, with digits; <c>sex</c>, if specified; count of characters
        /// in <see cref="Bio"/> in <c>bio-length</c>; and a list of
        /// <c>{TAG}-date-value</c> and <c>{TAG}-place</c> (filtered, with
        /// digits).
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
                        DataPinHelper.DefaultFilter.Apply(name.GetFullName(), true)));
                }
            }

            if (Sex != '\0')
                pins.Add(CreateDataPin("sex", new string(Sex, 1)));

            if (Chronotopes?.Count > 0)
            {
                foreach (Chronotope c in Chronotopes)
                {
                    string tag = c.Tag?.ToLowerInvariant() ?? "";

                    if (c.Date != null)
                    {
                        pins.Add(CreateDataPin("date-value." + tag,
                        c.Date.GetSortValue().ToString(CultureInfo.InvariantCulture)));
                    }

                    if (!string.IsNullOrEmpty(c.Place))
                    {
                        pins.Add(CreateDataPin(
                            "place." + tag,
                            DataPinHelper.DefaultFilter.Apply(c.Place, true)));
                    }
                }
            }

            int cc = Bio?.Length ?? 0;
            pins.Add(CreateDataPin("bio-length",
                cc.ToString(CultureInfo.InvariantCulture)));

            return pins;
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.String,
                    "person-id",
                    "The person ID, if any."),
                new DataPinDefinition(DataPinValueType.String,
                    "ext-id",
                    "The list of external IDs, if any.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "name",
                    "The list name(s) for this person, if any.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.String,
                    "sex",
                    "The person's sex, if any."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value.{TAG}",
                    "The list of the exchange's sortable date values.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "place.{TAG}",
                    "The list of the exchange's places of origin.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "bio-length",
                    "The length (in characters) of the bio resume.")
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

            sb.Append("[Person]");

            if (!string.IsNullOrEmpty(PersonId))
                sb.Append(' ').Append(PersonId);

            if (Names?.Count > 0)
                sb.Append(' ').Append(Names[0].GetFullName());

            return sb.ToString();
        }
    }
}
