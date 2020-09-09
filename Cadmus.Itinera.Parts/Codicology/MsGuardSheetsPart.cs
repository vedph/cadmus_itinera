using Cadmus.Core;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's guard sheets description.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-guard-sheets</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-guard-sheets")]
    public sealed class MsGuardSheetsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the guard sheets.
        /// </summary>
        public List<MsGuardSheet> GuardSheets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsGuardSheetsPart"/>
        /// class.
        /// </summary>
        public MsGuardSheetsPart()
        {
            GuardSheets = new List<MsGuardSheet>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: a collection of pins with these keys:
        /// <c>tot-count</c>, <c>back-count</c>, <c>front-count</c>,
        /// <c>material-X-count</c>, <c>date-value</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", GuardSheets?.Count ?? 0, false);

            if (GuardSheets?.Count > 0)
            {
                foreach (MsGuardSheet sheet in GuardSheets)
                {
                    builder.Increase(sheet.IsBack ? "back" : "front", false);
                    builder.Increase(sheet.Material, false, "material-");
                    if (sheet.Date != null)
                        builder.AddValue("date-value", sheet.Date.GetSortValue());
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

            sb.Append("[MsGuardSheets]");

            if (GuardSheets?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (MsGuardSheet sheet in GuardSheets)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(sheet);
                }
            }

            return sb.ToString();
        }
    }
}
