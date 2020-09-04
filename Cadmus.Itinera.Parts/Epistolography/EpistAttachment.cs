using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// An attachment to a letter.
    /// </summary>
    public class EpistAttachment
    {
        /// <summary>
        /// Gets or sets the type of the attachment (e.g. manuscript, work...).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the attachment's name. If a work, this is its title.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional specification of the portion of
        /// the attached object, used when the object is not attached in full.
        /// For instance, when attaching a part of a literary work this would
        /// contain its location, like <c>2.12-3.45</c>.
        /// </summary>
        public string Portion { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append('[').Append(Type ?? "").Append(']');

            if (!string.IsNullOrEmpty(Name))
                sb.Append(' ').Append(Name);

            if (!string.IsNullOrEmpty(Portion))
                sb.Append(" (").Append(Portion).Append(')');

            return sb.ToString();
        }
    }
}
