using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's physical measurements part.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-measurements</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-measurements")]
    public sealed class MsMeasurementsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the sample location, i.e. the location of the sheet
        /// used as the sample for taking measurements.
        /// </summary>
        public MsLocation Sample { get; set; }

        /// <summary>
        /// Gets or sets the measurements.
        /// </summary>
        public List<PhysicalMeasurement> Measurements { get;set; }

        /// <summary>
        /// Gets or sets the counts and/or description about any desired
        /// property, with different levels of precision: for instance, you
        /// might have rowMinCount, rowMaxCount, lineCount, approxLineCount,
        /// lineMinCount, lineMaxCount, prickCount, etc.; for descriptions,
        /// you might have columns, direction, blanks, ruling, execution,
        /// etc., eventually with a count (which might represent an average,
        /// or the most frequent value, etc.).</summary>
        public List<DecoratedCount> Counts { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsMeasurementsPart"/>
        /// class.
        /// </summary>
        public MsMeasurementsPart()
        {
            Measurements = new List<PhysicalMeasurement>();
            Counts = new List<DecoratedCount>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of measurements with keys <c>m-ID</c>,
        /// and <c>count-id</c> with collections of counts/descriptions IDs
        /// values.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (Measurements?.Count > 0)
            {
                foreach (PhysicalMeasurement m in Measurements)
                {
                    pins.Add(CreateDataPin("m-" + m.Id,
                        m.Value.ToString(CultureInfo.InvariantCulture)));
                }
            }

            if (Counts?.Count > 0)
            {
                foreach (DecoratedCount c in Counts)
                    pins.Add(CreateDataPin("count-id", c.Id));
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

            sb.Append("[MsMeasurements]");

            if (Measurements?.Count > 0)
            {
                sb.Append("M: ");
                sb.Append(string.Join(", ", from m in Measurements
                                            select m.Id));
            }
            if (Counts?.Count > 0)
            {
                sb.Append("C: ");
                sb.Append(string.Join(", ", from c in Counts
                                            select c.Id));
            }

            return sb.ToString();
        }
    }
}
