using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Attachments connected to an epistolographic item.
    /// <para>Tag: <c>it.vedph.itinera.epist-attachments</c>.</para>
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("it.vedph.itinera.epist-attachments")]
    public sealed class EpistAttachmentsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        public List<EpistAttachment> Attachments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistAttachmentsPart"/>
        /// class.
        /// </summary>
        public EpistAttachmentsPart()
        {
            Attachments = new List<EpistAttachment>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>att-TYPE-count</c>=count of attachment of
        /// type TYPE (one count for each distinct type found in the part),
        /// <c>tot-count</c>=total count of attachments.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Attachments?.Count ?? 0, false);

            if (Attachments.Count > 0)
            {
                builder.Increase(from a in Attachments
                                 select a.Type, false, "att-");
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[EpistAttachments]");
            var groups = from a in Attachments
                         group a by a.Type
                         into g
                         orderby g.Key
                         select new
                         {
                             Type = g.Key,
                             Count = g.Count()
                         };

            sb.Append(string.Join(" ",
                from g in groups select $"{g.Type}={g.Count}"));

            return sb.ToString();
        }
    }
}
