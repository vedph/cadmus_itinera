using Cadmus.Core;
using Fusi.Tools;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
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

        private int GetPoemCount()
        {
            if (Ranges?.Count == 0) return 0;

            int n = 0;

            foreach (AlnumRange range in Ranges)
            {
                if (range.A == null) continue;  // defensive

                if (range.B != null)
                {
                    Alphanumeric start = Alphanumeric.Parse(range.A);
                    Alphanumeric end = Alphanumeric.Parse(range.B);
                    if (!start.IsNull() && !end.IsNull()
                        && start.Alpha == null && end.Alpha == null)
                    {
                        n += (int)end.Number - (int)start.Number;
                    }
                }
                else n++;
            }
            return n;
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tag</c>=tag (if any); <c>poem-count</c>=
        /// count of poems calculated from their ranges.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (!string.IsNullOrEmpty(Tag))
                pins.Add(CreateDataPin("tag", Tag));

            int n = GetPoemCount();
            pins.Add(CreateDataPin("poem-count",
                n.ToString(CultureInfo.InvariantCulture)));

            return pins;
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.String,
                    "tag",
                    "The tag if any."),
                new DataPinDefinition(DataPinValueType.Integer,
                    "poem-count",
                    "The counts of the poems calculated from their ranges."),
            });
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

            sb.Append("[MsPoemRanges]: ").Append(Ranges?.Count ?? 0);

            if (!string.IsNullOrEmpty(Tag)) sb.Append(' ').Append(Tag);

            return sb.ToString();
        }
    }
}
