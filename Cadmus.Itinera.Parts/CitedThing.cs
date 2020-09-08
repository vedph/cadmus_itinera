using System.Text;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A named thing (e.g. a place) cited in a literary source.
    /// </summary>
    public class CitedThing : CitedBase
    {
        /// <summary>
        /// Gets or sets the thing's name, as found in the citation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Name != null) sb.Append(Name);
            if (Ids?.Count > 0)
                sb.Append(" = ").Append(string.Join("; ", Ids));
            if (Sources?.Count > 0)
                sb.Append(" (").Append(Sources.Count).Append(')');
            return sb.ToString();
        }
    }
}
