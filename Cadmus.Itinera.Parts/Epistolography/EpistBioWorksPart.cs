using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// List of works connected to a letter-related person's biography.
    /// Tag: <c>net.fusisoft.itinera.epist-bio-works</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.itinera.epist-bio-works")]
    public sealed class EpistBioWorksPart : PartBase
    {
        /// <summary>
        /// Gets or sets the works.
        /// </summary>
        public List<LitBioWork> Works { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpistBioWorksPart"/>
        /// class.
        /// </summary>
        public EpistBioWorksPart()
        {
            Works = new List<LitBioWork>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>title</c>, <c>language</c>, <c>date-value</c>.
        /// The <c>title</c> collection has its value filtered.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (Works?.Count > 0)
            {
                HashSet<string> titles = new HashSet<string>();
                HashSet<string> languages = new HashSet<string>();
                HashSet<double> dateValues = new HashSet<double>();

                foreach (LitBioWork work in Works)
                {
                    if (!string.IsNullOrEmpty(work.Title))
                        titles.Add(work.Title);
                    if (!string.IsNullOrEmpty(work.Language))
                        languages.Add(work.Language);
                    if (work.Date != null)
                        dateValues.Add(work.Date.GetSortValue());
                }

                foreach (string title in titles)
                    pins.Add(CreateDataPin("title", PinTextFilter.Apply(title)));
                foreach (string lang in languages)
                    pins.Add(CreateDataPin("language", lang));
                foreach (double dv in dateValues)
                {
                    pins.Add(CreateDataPin("date-value",
                        PinTextFilter.Apply(dv.ToString(CultureInfo.InvariantCulture))));
                }
            }

            return pins;
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

            sb.Append("[EpistBioWorks]");

            if (Works?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (LitBioWork work in Works)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Works.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(work.Title);
                    if (work.IsLost) sb.Append('*');
                }
            }

            return sb.ToString();
        }
    }
}
