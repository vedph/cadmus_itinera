using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript composition part.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-composition</c></para>.
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-composition")]
    public sealed class MsCompositionPart : PartBase
    {
        /// <summary>
        /// Gets or sets the manuscript's sheets count.
        /// </summary>
        public int SheetCount { get; set; }

        /// <summary>
        /// Gets or sets the manuscript's guard sheets count.
        /// </summary>
        public int GuardSheetCount { get; set; }

        /// <summary>
        /// Gets or sets the manuscript's content sections.
        /// </summary>
        public List<MsSection> Sections { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsCompositionPart"/>
        /// class.
        /// </summary>
        public MsCompositionPart()
        {
            Sections = new List<MsSection>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>sheet-count</c>, <c>guard-sheet-count</c>,
        /// <c>section-count</c>, and a list with keys: <c>section-TAG-count</c>,
        /// <c>section-label</c> (filtered, with digits), <c>section-date-value</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("sheet", SheetCount, false);
            builder.Set("guard-sheet", GuardSheetCount, false);
            builder.Set("section", Sections?.Count ?? 0, false);

            if (Sections?.Count > 0)
            {
                foreach (MsSection section in Sections)
                {
                    builder.Increase(section.Tag, false, "section-");

                    if (!string.IsNullOrEmpty(section.Label))
                    {
                        builder.AddValue("section-label",
                            PinTextFilter.Apply(section.Label, true));
                    }

                    if (section.Date != null)
                        builder.AddValue("section-date-value", section.Date.GetSortValue());
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

            sb.Append("[MsComposition]");

            sb.Append("s=").Append(SheetCount);
            sb.Append(" gs=").Append(GuardSheetCount);
            if (Sections?.Count > 0)
                sb.Append(" sct=").Append(Sections.Count);

            return sb.ToString();
        }
    }
}
