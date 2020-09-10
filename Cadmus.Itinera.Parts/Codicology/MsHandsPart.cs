using System.Linq;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's hands description.
    /// <para>Tag: <c>net.fusisoft.itinera.ms-hands</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-hands")]
    public class MsHandsPart : PartBase
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
        /// these keys: <c>id</c>, <c>type-X-count</c>, <c>sign-X-count</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Hands?.Count ?? 0, false);

            if (Hands?.Count > 0)
            {
                foreach (var hand in Hands)
                {
                    builder.AddValue("id", hand.Id);
                    builder.Increase(hand.Type, false, "type-");

                    if (hand.Signs?.Count > 0)
                    {
                        builder.Update(from s in hand.Signs
                                       select s.Id, false, "sign-");
                    }
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
