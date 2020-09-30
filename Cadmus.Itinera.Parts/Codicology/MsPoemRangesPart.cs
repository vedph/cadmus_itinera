using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's poems ranges. This part defines the sequence of poems or
    /// other types of numbered compositions, as reported in a specific
    /// manuscript.
    /// <para>Tag: <c>it.vedph.itinera.ms-poem-ranges</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-poem-ranges")]
    public sealed class MsPoemRangesPart : PartBase
    {
        /// <summary>
        /// Gets or sets the optional tag, used to categorize this set of ranges.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the ranges.
        /// </summary>
        public List<AlnumRange> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the optional note linked to this set of ranges.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsPoemRangesPart"/> class.
        /// </summary>
        public MsPoemRangesPart()
        {
            Ranges = new List<AlnumRange>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            throw new NotImplementedException();

            // TODO: implement indexing logic...
            // sample:
            // return Tag != null
            //    ? new[]
            //    {
            //        CreateDataPin("tag", Tag)
            //    }
            //    : Enumerable.Empty<DataPin>();
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            List<DataPinDefinition> pins = new List<DataPinDefinition>();

            // TODO

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

            sb.Append("[MsPoemRanges]");

            // TODO: append summary data...

            return sb.ToString();
        }
    }
}
