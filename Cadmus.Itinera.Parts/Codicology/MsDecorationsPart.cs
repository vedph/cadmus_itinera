using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's decorations description.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-decorations</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-decorations")]
    public sealed class MsDecorationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the decorations.
        /// </summary>
        public List<MsDecoration> Decorations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsDecorationsPart"/>
        /// class.
        /// </summary>
        public MsDecorationsPart()
        {
            Decorations = new List<MsDecoration>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>type-X-count</c>, <c>subject-X-count</c>,
        /// <c>color-X-count</c>, <c>golden-count</c>, <c>tag-X-count</c>
        /// (all the keys with X are filtered, with digits).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("tot", Decorations?.Count ?? 0, false);

            if (Decorations?.Count > 0)
            {
                foreach (var decoration in Decorations)
                {
                    if (decoration.IsGolden) builder.Increase("golden", false);

                    builder.Increase(decoration.Type, false, "type-");
                    builder.Increase(decoration.Subject, false, "subject-");
                    builder.Increase(decoration.Color, false, "color-");
                    builder.Increase(decoration.Tag, false, "tag-");
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

            sb.Append("[MsDecorations]");

            if (Decorations?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Decorations)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Decorations.Count > 3)
                    sb.Append("...(").Append(Decorations.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
