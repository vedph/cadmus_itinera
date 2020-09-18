using System.Linq;
using Cadmus.Core;
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
        /// Gets or sets the hand's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a note about the hand's extent.
        /// </summary>
        public string ExtentNote { get; set; }

        /// <summary>
        /// Gets or sets the hand's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the initials description.
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Gets or sets the corrections description.
        /// </summary>
        public string Corrections { get; set; }

        /// <summary>
        /// Gets or sets the punctuation description.
        /// </summary>
        public string Punctuation { get; set; }

        /// <summary>
        /// Gets or sets the abbreviations description.
        /// </summary>
        public string Abbreviations { get; set; }

        /// <summary>
        /// Gets or sets the rubrications description.
        /// </summary>
        public List<MsRubrication> Rubrications { get; set; }

        /// <summary>
        /// Gets or sets the subscriptions description.
        /// </summary>
        public List<MsSubscription> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the images IDs. These IDs represent the prefixes for
        /// all the images depicting something related to this hand; e.g. if the
        /// ID is <c>ae</c>, we would expect any number of image resources
        /// named after it plus a conventional numbering, like <c>ae00001</c>,
        /// <c>ae00002</c>, etc.
        /// </summary>
        public List<string> ImageIds { get; set; }

        /// <summary>
        /// Gets or sets the signs description.
        /// </summary>
        public List<MsHandSign> Signs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonHandPart"/> class.
        /// </summary>
        public PersonHandPart()
        {
            Rubrications = new List<MsRubrication>();
            Subscriptions = new List<MsSubscription>();
            ImageIds = new List<string>();
            Signs = new List<MsHandSign>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>id</c>, <c>job</c>
        /// (filtered, with digits), <c>type</c> (filtered, with digits),
        /// <c>img-count</c>, <c>sign-tot-count</c>, <c>sign-X-count</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            if (!string.IsNullOrEmpty(PersonId))
                builder.AddValue("id", PersonId);

            if (!string.IsNullOrEmpty(Job))
                builder.AddValue("job", Job, filter: true, filterOptions: true);

            if (!string.IsNullOrEmpty(Type))
                builder.AddValue("type", Type, filter: true, filterOptions: true);

            builder.Set("img-count", ImageIds?.Count ?? 0, false);

            if (Signs?.Count > 0)
            {
                builder.Increase(from s in Signs select s.Id, true, "sign-");
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
                new DataPinDefinition(DataPinValueType.String,
                    "id",
                    "The hand's ID."),
                new DataPinDefinition(DataPinValueType.String,
                    "job",
                    "The person's job.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "type",
                    "The hand's type.",
                    "f"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "img-count",
                    "The images count."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "sign-{SIGN}-count",
                    "The count of each type of described sign.")
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

            if (!string.IsNullOrEmpty(Type))
                sb.Append(" [").Append(Type).Append(']');

            return sb.ToString();
        }
    }
}
