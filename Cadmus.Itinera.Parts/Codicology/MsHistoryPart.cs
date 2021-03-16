using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's history.
    /// <para>Tag: <c>it.vedph.itinera.ms-history</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-history")]
    public sealed class MsHistoryPart : PartBase
    {
        /// <summary>
        /// Gets or sets the provenance(s) in their chronological order.
        /// </summary>
        /// <remarks>The provenance is kept distinct from origin, which is
        /// defined by <see cref="MsPlacePart"/>. The origin is where the
        /// manuscript was materially crafted; the provenance is "the last
        /// place where the manuscript was preserved before reaching the current
        /// one" (Petrucci, La descrizione del manoscritto. Storia, problemi,
        /// modelli, Roma 2001 p.66).</remarks>
        public List<GeoAddress> Provenances { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        public string History { get; set; }

        /// <summary>
        /// Gets or sets the persons cited in the manuscript's history.
        /// </summary>
        public List<MsHistoryPerson> Persons { get; set; }

        /// <summary>
        /// Gets or sets the manuscript's annotations.
        /// </summary>
        public List<MsAnnotation> Annotations { get; set; }

        /// <summary>
        /// Gets or sets the manuscript's restorations.
        /// </summary>
        public List<MsRestoration> Restorations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsHistoryPart"/> class.
        /// </summary>
        public MsHistoryPart()
        {
            Provenances = new List<GeoAddress>();
            Persons = new List<MsHistoryPerson>();
            Annotations = new List<MsAnnotation>();
            Restorations = new List<MsRestoration>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>area</c> (filtered, with digits),
        /// <c>pers-count</c>, <c>ann-count</c>, <c>rest-count</c>,
        /// <c>pers-name</c> (full name; filtered, with digits),
        /// <c>pers-date-value</c>, <c>ann-X-count</c>, <c>rest-X-count</c>,
        /// <c>rest-date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder =
                new DataPinBuilder(DataPinHelper.DefaultFilter);

            if (Provenances?.Count > 0)
            {
                builder.AddValues("area", Provenances.Select(p => p.Area),
                    filter: true, filterOptions: true);
            }

            builder.Set("pers", Persons?.Count ?? 0, false);
            builder.Set("ann", Annotations?.Count ?? 0, false);
            builder.Set("rest", Restorations?.Count ?? 0, false);

            if (Persons?.Count > 0)
            {
                builder.AddValues("pers-name", from p in Persons
                                               where p.Name != null
                                               select p.Name.GetFullName(),
                                               filter: true, filterOptions: true);

                foreach (var person in Persons.Where(p => p.Date != null))
                {
                    builder.AddValue("pers-date-value",
                        person.Date.GetSortValue());
                }
            }

            if (Annotations?.Count > 0)
            {
                foreach (var annotation in Annotations)
                    builder.Increase(annotation.Type, false, "ann-");
            }

            if (Restorations?.Count > 0)
            {
                foreach (var restoration in Restorations)
                {
                    builder.Increase(restoration.Type, false, "rest-");

                    if (restoration.Date != null)
                    {
                        builder.AddValue("rest-date-value",
                            restoration.Date.GetSortValue());
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
                new DataPinDefinition(DataPinValueType.String,
                    "area",
                    "The manuscript's provenance area(s).",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "pers-count",
                    "The count of persons related to the manuscript's history."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "ann-count",
                    "The count of annotations in the manuscript."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "rest-count",
                    "The count of restorations in the manuscript."),
                new DataPinDefinition(DataPinValueType.String,
                    "pers-name",
                    "The list of the names of persons related to the " +
                    "manuscript's history, with format \"last, first\".",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "pers-date-value",
                    "The list of sortable date values connected to the persons" +
                    "related to the manuscript's history.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "ann-{TYPE}-count",
                    "The count of each type of annotation in the manuscript."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "rest-{TYPE}-count",
                    "The count of each type of restoration in the manuscript."),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "rest-date-value",
                    "The list of sortable date values for dated restorations " +
                    "in the manuscript.",
                    "M")
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

            sb.Append("[MsHistory]");

            if (Provenances?.Count > 0)
                sb.Append(Provenances[0].Area).Append(": ");

            sb.Append("P=").Append(Persons?.Count ?? 0).Append(", ");
            sb.Append("A=").Append(Annotations?.Count ?? 0).Append(", ");
            sb.Append("R=").Append(Restorations?.Count ?? 0).Append(", ");

            return sb.ToString();
        }
    }
}
