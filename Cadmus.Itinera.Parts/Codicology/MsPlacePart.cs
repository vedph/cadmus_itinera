using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's place of origin.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-place</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-place")]
    public sealed class MsPlacePart : PartBase
    {
        /// <summary>
        /// Gets or sets the geographical area.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the city inside the area.
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
        /// <returns>The pins: <c>area</c>, <c>city</c>, <c>site</c>,
        /// <c>subscriber</c>: all filtered, with digits.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(Area))
                pins.Add(CreateDataPin("area", PinTextFilter.Apply(Area, true)));

            if (!string.IsNullOrEmpty(City))
                pins.Add(CreateDataPin("city", PinTextFilter.Apply(City, true)));

            if (!string.IsNullOrEmpty(Site))
                pins.Add(CreateDataPin("site", PinTextFilter.Apply(Site, true)));

            if (!string.IsNullOrEmpty(Subscriber))
            {
                pins.Add(CreateDataPin("subscriber",
                    PinTextFilter.Apply(Subscriber, true)));
            }

            return pins;
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

            if (!string.IsNullOrEmpty(City))
            {
                if (sb[sb.Length - 1] != ' ') sb.Append(", ");
                sb.Append(City);
            }

            if (!string.IsNullOrEmpty(Subscriber))
            {
                if (sb[sb.Length - 1] != ' ') sb.Append(": ");
                sb.Append(Subscriber);
            }

            return sb.ToString();
        }
    }
}
