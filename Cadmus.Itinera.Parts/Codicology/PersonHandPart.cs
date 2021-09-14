using Cadmus.Bricks;
using Cadmus.Core;
using Cadmus.Parts;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Person's hand description.
    /// <para>Tag: <c>it.vedph.itinera.person-hand</c></para>
    /// </summary>
    [Tag("it.vedph.itinera.person-hand")]
    public sealed class PersonHandPart : PartBase
    {
        /// <summary>
        /// Gets or sets a conventional, arbitrary identifier assigned to this
        /// hand and unique whithin the boundaries of the manuscript.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person's job. This is used to distinguish
        /// professional copysts from occasional copysts.
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// Gets or sets the other manuscripts written by this person.
        /// </summary>
        public List<DocReference> Others { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonHandPart"/> class.
        /// </summary>
        public PersonHandPart()
        {
            Others = new List<DocReference>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>person-id</c>; <c>job</c> (filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            if (!string.IsNullOrEmpty(PersonId))
                builder.AddValue("person-id", PersonId);

            if (!string.IsNullOrEmpty(Job))
                builder.AddValue("job", Job, filter: true, filterOptions: true);

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
                new DataPinDefinition(DataPinValueType.String,
                    "person-id",
                    "The person ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "job",
                    "The person's job.",
                    "f")
            });
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

            sb.Append("[PersonHand]");

            if (!string.IsNullOrEmpty(PersonId))
                sb.Append(' ').Append(PersonId);

            return sb.ToString();
        }
    }
}
