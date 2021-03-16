using Cadmus.Parts.General;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A manuscript's layout.
    /// </summary>
    public class MsLayout
    {
        /// <summary>
        /// Gets or sets the sample sheet location.
        /// </summary>
        public MsLocation Sample { get; set; }

        /// <summary>
        /// Gets or sets the dimensions.
        /// </summary>
        public List<PhysicalDimension> Dimensions { get; set; }

        /// <summary>
        /// Gets or sets the ruling technique.
        /// </summary>
        public string RulingTechnique { get; set; }

        /// <summary>
        /// Gets or sets the Derolez classification.
        /// </summary>
        public string Derolez { get; set; }

        /// <summary>
        /// Gets or sets the pricking type.
        /// </summary>
        public string Pricking { get; set; }

        /// <summary>
        /// Gets or sets the columns count.
        /// </summary>
        public int ColumnCount { get; set; }

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
        /// Initializes a new instance of the <see cref="MsLayout"/> class.
        /// </summary>
        public MsLayout()
        {
            Dimensions = new List<PhysicalDimension>();
            Counts = new List<DecoratedCount>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Sample}: {ColumnCount}";
        }
    }
}
