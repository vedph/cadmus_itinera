using Cadmus.Bricks;
using Cadmus.Parts;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A person's literary work.
    /// </summary>
    public class PersonWork
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this work is dubious.
        /// </summary>
        public bool IsDubious { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this work is lost.
        /// </summary>
        public bool IsLost { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the titles. The first is the default title; other
        /// titles can be added as variants.
        /// </summary>
        public List<string> Titles { get; set; }

        /// <summary>
        /// Gets or sets the place and time of composition, in order of
        /// preference.
        /// </summary>
        public List<Chronotope> Chronotopes { get; set; }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        public List<DocReference> References { get; set; }

        /// <summary>
        /// Gets or sets an optional note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonWorksPart"/> class.
        /// </summary>
        public PersonWork()
        {
            Titles = new List<string>();
            Chronotopes = new List<Chronotope>();
            References = new List<DocReference>();
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

            if (!string.IsNullOrEmpty(Language))
                sb.Append('[').Append(Language).Append(']');

            if (Titles?.Count > 0)
                sb.Append(' ').Append(string.Join("; ", Titles));

            if (IsDubious) sb.Append(" (?)");
            if (IsLost) sb.Append(" (*)");

            return sb.ToString();
        }
    }
}
