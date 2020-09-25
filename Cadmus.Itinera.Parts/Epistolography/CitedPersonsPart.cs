using Cadmus.Core;
using Fusi.Tools.Config;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// List of persons cited in a letter's text.
    /// <para>Tag: <c>it.vedph.itinera.cited-persons</c>.</para>
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.cited-persons")]
    public sealed class CitedPersonsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the persons.
        /// </summary>
        public List<CitedPerson> Persons { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CitedPersonsPart"/>
        /// class.
        /// </summary>
        public CitedPersonsPart()
        {
            Persons = new List<CitedPerson>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and lists of pins under keys:
        /// <c>name</c>=full name (filtered), <c>id</c>=identification.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Persons?.Count ?? 0, false);

            if (Persons?.Count > 0)
            {
                foreach (CitedPerson person in Persons)
                {
                    builder.AddValue("name",
                        person.Name.GetFullName(), filter: true);

                    if (person.Ids != null)
                    {
                        builder.AddValues("id", from ident in person.Ids
                                                select ident.Id);
                    }
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
                    "The total count of cited persons."),
                new DataPinDefinition(DataPinValueType.String,
                    "name",
                    "The list of persons names.",
                    "MF"),
                new DataPinDefinition(DataPinValueType.String,
                    "name",
                    "The list of persons IDs.",
                    "M")
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

            sb.Append("[EpistCitedPersons]");

            var names = (from p in Persons
                         select p.Name.GetFullName()).Take(5);

            sb.Append(string.Join("; ", names));
            if (Persons.Count > 5)
                sb.Append("... (").Append(Persons.Count).Append(')');

            return sb.ToString();
        }
    }
}
