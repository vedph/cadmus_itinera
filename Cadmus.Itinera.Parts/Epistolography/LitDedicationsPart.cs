using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Literary dedications.
    /// Tag: <c>it.vedph.itinera.lit-dedications</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.lit-dedications")]
    public sealed class LitDedicationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the dedications.
        /// </summary>
        public List<LitDedication> Dedications { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitDedicationsPart"/>
        /// class.
        /// </summary>
        public LitDedicationsPart()
        {
            Dedications = new List<LitDedication>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>title</c> (filtered, including digits) and <c>date-value</c>,
        /// plus a list of participant IDs in <c>pid</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Dedications?.Count ?? 0, false);

            foreach (LitDedication dedication in Dedications)
            {
                builder.AddValue("title",
                    dedication.Title, filter: true, filterOptions: true);

                if (dedication.Date != null)
                    builder.AddValue("date-value", dedication.Date.GetSortValue());

                if (dedication.Participants?.Count > 0)
                {
                    builder.AddValues("pid",
                        dedication.Participants.Select(p => p.Id));
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
                    "The total count of dedications."),
                new DataPinDefinition(DataPinValueType.String,
                    "title",
                    "The list of dedications titles.",
                    "Mf"),
                new DataPinDefinition(DataPinValueType.Decimal,
                    "date-value",
                    "The list of dedications sortable date values.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "pid",
                    "The list of dedications person IDs.",
                    "M"),
                new DataPinDefinition(DataPinValueType.Integer,
                    "corr-count",
                    "The count of dedications by the correspondent.")
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

            sb.Append("[LitDedications]");

            if (Dedications?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var dedication in Dedications)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Dedications.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(dedication.Title);
                }
            }

            return sb.ToString();
        }
    }
}
