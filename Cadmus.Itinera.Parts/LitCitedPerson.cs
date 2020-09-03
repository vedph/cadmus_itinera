using System.Text;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A person cited in a literary source.
    /// </summary>
    public class LitCitedPerson : LitCitedBase
    {
        /// <summary>
        /// Gets or sets the cited person's name.
        /// </summary>
        public PersonName Name { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Name != null) sb.Append(Name.ToString());
            if (Idents?.Count > 0)
                sb.Append(" = ").Append(string.Join("; ", Idents));
            if (Sources?.Count > 0)
                sb.Append(" (").Append(Sources.Count).Append(')');
            return sb.ToString();
        }
    }
}
