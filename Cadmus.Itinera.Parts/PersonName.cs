using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cadmus.Itinera.Parts
{
    /// <summary>
    /// A person's name.
    /// </summary>
    public class PersonName
    {
        /// <summary>
        /// Gets or sets the language. Usually this is an ISO639-3 identifier.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the optional tag, used to group several person names;
        /// this can be useful when a person has several names.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the parts of this name, in their conventional order.
        /// Note that parts types are not unique in a name: for instance, you
        /// might have a person with 2 first names.
        /// </summary>
        public List<PersonNamePart> Parts { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonName"/> class.
        /// </summary>
        public PersonName()
        {
            Parts = new List<PersonNamePart>();
        }

        /// <summary>
        /// Gets the full name, built by concatenating all of its parts values.
        /// </summary>
        /// <returns>The full name, eventually empty if no parts.</returns>
        public string GetFullName() =>
            Parts?.Count > 0? string.Join(" ", from p in Parts select p.Value) : "";

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (Parts == null || Parts.Count == 0) return base.ToString();

            StringBuilder sb = new StringBuilder(string.Join(" ", Parts));
            if (!string.IsNullOrEmpty(Tag))
                sb.Append(" [").Append(Tag).Append(']');
            if (!string.IsNullOrEmpty(Language))
                sb.Append(" <").Append(Language).Append('>');
            return sb.ToString();
        }
    }

    /// <summary>
    /// A part of a <see cref="PersonName"/>.
    /// </summary>
    public class PersonNamePart
    {
        /// <summary>
        /// Gets or sets the name part's type, like first name, last name,
        /// patronymic, epithet, etc.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name part's value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Type) ?
                Value : $"{Value} [{Type}]";
        }
    }
}
