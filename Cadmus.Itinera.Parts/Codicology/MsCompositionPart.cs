using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Tag: <c>net.fusisoft.itinera.ms-composition</c>.
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-composition")]
    public sealed class MsCompositionPart : PartBase
    {
        public int SheetCount { get; set; }
        public int GuardSheetCount { get; set; }
        public List<MsSection> Sections { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
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
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[MsComposition]");

            // TODO: append summary data...

            return sb.ToString();
        }
    }
}
