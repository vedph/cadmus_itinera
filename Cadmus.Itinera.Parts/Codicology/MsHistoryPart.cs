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
        /// Gets or sets the provenance.
        /// </summary>
        public string Provenance { get; set; }

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
        /// <returns>The pins: <c>provenance</c> (filtered, with digits),
        /// <c>pers-count</c>, <c>ann-count</c>, <c>rest-count</c>,
        /// <c>pers-name</c> (last, first; filtered, with digits),
        /// <c>pers-date-value</c>, <c>ann-X-count</c>, <c>rest-X-count</c>,
        /// <c>rest-date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder =
                new DataPinBuilder(new StandardDataPinTextFilter());

            builder.AddValue("provenance", Provenance,
                filter: true, filterOptions: true);

            builder.Set("pers", Persons?.Count ?? 0, false);
            builder.Set("ann", Annotations?.Count ?? 0, false);
            builder.Set("rest", Restorations?.Count ?? 0, false);

            if (Persons?.Count > 0)
            {
                builder.AddValues("pers-name", from p in Persons
                                               select builder.ApplyFilter(true,
                                                 true, p.LastName,
                                                 false, ", ",
                                                 true, p.FirstName));

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
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[MsHistory]");

            if (!string.IsNullOrEmpty(Provenance))
                sb.Append(Provenance).Append(": ");

            sb.Append("P=").Append(Persons?.Count ?? 0).Append(", ");
            sb.Append("A=").Append(Annotations?.Count ?? 0).Append(", ");
            sb.Append("R=").Append(Restorations?.Count ?? 0).Append(", ");

            return sb.ToString();
        }
    }
}
