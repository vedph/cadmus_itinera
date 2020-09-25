namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A general-purpose geographical indication, composed by a top-level
    /// area and a variable-granularity "address", where each component is
    /// separated by commas. This is a typical input for a geocoding
    /// service. For instance, the area might be "France", and the address
    /// "Lyon, Bibliothéque Civique".
    /// </summary>
    public class GeoAddress
    {
        /// <summary>
        /// Gets or sets the geographical area; this is the top-level
        /// geographical indication in the hierarchy further specified by
        /// <see cref="Address"/>.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the optional address inside the area. This is a string
        /// including 1 or more components, in hierarchical order, like the
        /// addresses typically used in geocoding systems. Components are
        /// separated by comma.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Address)? Area : $"{Area}, {Address}";
        }
    }
}
