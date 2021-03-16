using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's place of origin.
    /// <para>Tag: <c>it.vedph.itinera.ms-place</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-place")]
    public sealed class MsPlacePart : PartBase
    {
        /// <summary>
        /// Gets or sets the geographical area. This is the top-level geographical
        /// indication in the hierarchy further specified by <see cref="Address"/>.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the optional address inside the area. This is a
        /// string including 1 or more components, in hierarchical order,
        /// like the addresses typically used in geocoding systems.
        /// Components are separated by comma. For instance, the area might
        /// be "France", and the address "Lyon, Bibliothéque Civique").
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the site inside the city.
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the subscriber.
        /// </summary>
        public string Subscriber { get; set; }

        /// <summary>
        /// Gets or sets the location of the subscription in the manuscript.
        /// </summary>
        public MsLocation SubscriptionLoc { get; set; }

        /// <summary>
        /// Gets or sets the bibliographic sources.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>area</c>, <c>address</c>, <c>city</c>,
        /// <c>subscriber</c>, and each component of the address named after
        /// the pattern <c>address-N</c> where N is the ordinal of each component;
        /// all filtered, with digits.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(Area))
            {
                pins.Add(CreateDataPin("area",
                    DataPinHelper.DefaultFilter.Apply(Area, true)));
            }

            if (!string.IsNullOrEmpty(City))
            {
                pins.Add(CreateDataPin("city",
                    DataPinHelper.DefaultFilter.Apply(City, true)));
            }

            if (!string.IsNullOrEmpty(Address))
            {
                pins.Add(CreateDataPin("address",
                    DataPinHelper.DefaultFilter.Apply(Address, true)));
                int n = 0;
                foreach (string c in Address.Split(','))
                {
                    pins.Add(CreateDataPin($"address-{++n}",
                        DataPinHelper.DefaultFilter.Apply(c.Trim(), true)));
                }
            }

            if (!string.IsNullOrEmpty(Subscriber))
            {
                pins.Add(CreateDataPin("subscriber",
                    DataPinHelper.DefaultFilter.Apply(Subscriber, true)));
            }

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
                    "area",
                    "The manuscript's area, if any.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "city",
                    "The manuscript's city, if any.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "address",
                    "The manuscript's address, if any.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "address-{N}",
                    "The list of manuscript's address components, "+
                    "each numbered, if any.",
                    "f"),
                new DataPinDefinition(DataPinValueType.String,
                    "subscriber",
                    "The manuscript's subscriber, if any.",
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

            sb.Append("[MsPlace]");

            if (!string.IsNullOrEmpty(Area)) sb.Append(' ').Append(Area);

            if (!string.IsNullOrEmpty(Address))
            {
                if (sb[sb.Length - 1] != ' ') sb.Append(", ");
                sb.Append(Address);
            }

            return sb.ToString();
        }
    }
}
