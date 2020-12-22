using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's hands description.
    /// <para>Tag: <c>it.vedph.itinera.ms-hands</c>.</para>
    /// </summary>
    [Tag("it.vedph.itinera.ms-hands")]
    public sealed class MsHandsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the hands.
        /// </summary>
        public List<MsHand> Hands { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsHandsPart"/> class.
        /// </summary>
        public MsHandsPart()
        {
            Hands = new List<MsHand>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>id</c>.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Hands?.Count ?? 0, false);

            if (Hands?.Count > 0)
            {
                foreach (MsHand hand in Hands)
                {
                    if (!string.IsNullOrEmpty(hand.Id))
                        builder.AddValue("id", hand.Id);

                    if (!string.IsNullOrEmpty(hand.PersonId))
                        builder.AddValue("person-id", hand.PersonId);

                    if (hand.Types?.Count > 0)
                    {
                        foreach (string type in hand.Types)
                            builder.AddValue("type", type);
                    }
                }
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions: <c>tot-count</c> and a list of
        /// pins with keys: <c>id</c>, <c>person-id</c>, <c>type</c>.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                    "tot-count",
                    "The total count of hands."),
                new DataPinDefinition(DataPinValueType.String,
                    "id",
                    "The list of hands IDs.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "person-id",
                    "The list of person IDs.",
                    "M"),
                new DataPinDefinition(DataPinValueType.String,
                    "type",
                    "The list of script types.",
                    "M")
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

            sb.Append("[MsHands]");

            if (Hands?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Hands)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Hands.Count > 3)
                    sb.Append("...(").Append(Hands.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
