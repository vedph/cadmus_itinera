using System.Collections.Generic;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// An identifier decorated with a tag, a rank and a list of sources
    /// for that identification.
    /// </summary>
    public class DecoratedId
    {
        /// <summary>
        /// Gets or sets the identifier expressing the identification.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the rank. This is a numeric value used to sort
        /// identifications in their order of probability. For a single
        /// identification, just leave the rank value equal (to 0). Otherwise,
        /// use 1=highest probability, 2=lower than 1, and so on.
        /// </summary>
        public short Rank { get; set; }

        /// <summary>
        /// Gets or sets the tag, used to categorize this ID in any way.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the sources.
        /// </summary>
        public List<DocReference> Sources { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratedId"/> class.
        /// </summary>
        public DecoratedId()
        {
            Sources = new List<DocReference>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id} [{Tag}: {Rank}] ({Sources?.Count ?? 0})";
        }
    }
}
