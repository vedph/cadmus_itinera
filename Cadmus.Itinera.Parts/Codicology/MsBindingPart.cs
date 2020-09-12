using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Cadmus.Core;
using Cadmus.Parts.General;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's binding description.
    /// <para>Tag: <c>it.vedph.itinera.ms-binding</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-binding")]
    public sealed class MsBindingPart : PartBase
    {
        /// <summary>
        /// Gets or sets the binding's century.
        /// </summary>
        public short Century { get; set; }

        /// <summary>
        /// Gets or sets the binding's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the binding's cover material.
        /// </summary>
        public string CoverMaterial { get; set; }

        /// <summary>
        /// Gets or sets the binding's support material.
        /// </summary>
        public string SupportMaterial { get; set; }

        /// <summary>
        /// Gets or sets the binding's size.
        /// </summary>
        public PhysicalSize Size { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>century</c>, <c>cover-mat</c> (filtered,
        /// with digits), <c>support-mat</c> (filtered, with digits),
        /// <c>w</c>, <c>h</c> (both with format 00.00).</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();
            IDataPinTextFilter filter = null;

            if (Century != 0)
            {
                pins.Add(CreateDataPin("century",
                    Century.ToString(CultureInfo.InvariantCulture)));
            }

            if (!string.IsNullOrEmpty(CoverMaterial))
            {
                filter = new StandardDataPinTextFilter();
                pins.Add(CreateDataPin("cover-mat",
                    filter.Apply(CoverMaterial, true)));
            }

            if (!string.IsNullOrEmpty(SupportMaterial))
            {
                if (filter == null)
                    filter = new StandardDataPinTextFilter();
                pins.Add(CreateDataPin("support-mat",
                    filter.Apply(SupportMaterial, true)));
            }

            if (Size?.W != null)
            {
                pins.Add(CreateDataPin("w",
                    Size.W.Value.ToString("00.00", CultureInfo.InvariantCulture)));
            }

            if (Size?.H != null)
            {
                pins.Add(CreateDataPin("h",
                    Size.H.Value.ToString("00.00", CultureInfo.InvariantCulture)));
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

            sb.Append("[MsBinding]");

            if (Century != 0) sb.Append(Century).Append(" AD");

            if (!string.IsNullOrEmpty(CoverMaterial))
                sb.Append(", ").Append(CoverMaterial);

            if (!string.IsNullOrEmpty(SupportMaterial))
                sb.Append(", ").Append(SupportMaterial);

            return sb.ToString();
        }
    }
}
