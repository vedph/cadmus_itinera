using System.Text;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A place cited in a literary source.
    /// </summary>
    /// <remarks>Notice that this place just corresponds to a conventional name
    /// (e.g. <c>Napoli</c>). This is not a full-fledged geographic model,
    /// but just a name; including data like coordinates here would not be an
    /// option, because this would imply duplicating (i.e. re-entering and
    /// re-storing) them wherever the same name occurs.</remarks>
    public class LitCitedPlace : LitCitedBase
    {
        /// <summary>
        /// Gets or sets the place's name.
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
            if (Idents?.Count > 0)
                sb.Append(" = ").Append(string.Join("; ", Idents));
            if (Sources?.Count > 0)
                sb.Append(" (").Append(Sources.Count).Append(')');
            return sb.ToString();
        }
    }
}
