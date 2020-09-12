using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's material description.
    /// <para>Tag: <c>it.vedph.itinera.ms-material-dsc</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-material-dsc")]
    public sealed class MsMaterialDscPart : PartBase
    {
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets an optional note about the state.
        /// </summary>
        public string StateNote { get; set; }

        /// <summary>
        /// Gets or sets the counts for some relevant properties of this
        /// manuscript, like sheets, front and back guard sheets, etc.
        /// </summary>
        public List<DecoratedCount> Counts { get; set; }

        /// <summary>
        /// Gets or sets the list of palimpsests parts in this manuscript.
        /// </summary>
        public List<MsPalimpsest> Palimpsests { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsMaterialDscPart"/>
        /// class.
        /// </summary>
        public MsMaterialDscPart()
        {
            Counts = new List<DecoratedCount>();
            Palimpsests = new List<MsPalimpsest>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>material</c> (filtered, with digits),
        /// <c>format</c> (filtered, with digits), <c>state</c> (filtered, with
        /// digits), <c>c-X-count</c> for counts, <c>palimpsest-count</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder(
                new StandardDataPinTextFilter());

            builder.Set("palimpsest", Palimpsests?.Count ?? 0, false);

            builder.AddValue("material", builder.Filter.Apply(Material, true));
            builder.AddValue("format", builder.Filter.Apply(Format, true));
            builder.AddValue("state", builder.Filter.Apply(State, true));

            if (Counts?.Count > 0)
            {
                foreach (var count in Counts)
                    builder.Set(count.Id, count.Value, false, "c-");
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

            sb.Append("MsMaterialDsc");

            if (!string.IsNullOrEmpty(Material))
                sb.Append(' ').Append(Material);

            if (!string.IsNullOrEmpty(Format))
                sb.Append(", ").Append(Format);

            if (!string.IsNullOrEmpty(State))
                sb.Append(", ").Append(State);

            return sb.ToString();
        }
    }
}
