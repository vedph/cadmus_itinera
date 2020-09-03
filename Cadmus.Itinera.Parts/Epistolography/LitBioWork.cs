using Fusi.Antiquity.Chronology;
using System.Collections.Generic;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// A literary work cited in the context of a biography.
    /// </summary>
    public class LitBioWork
    {
        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the language. Usually this is an ISO639-3 code.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the attribution rank (e.g. 0=author, 1=dubious,
        /// 2=spurious).
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this work is lost.
        /// </summary>
        public bool IsLost { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Gets or sets the external IDs connected to this work.
        /// </summary>
        public List<string> ExternalIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LitBioWork"/> class.
        /// </summary>
        public LitBioWork()
        {
            ExternalIds = new List<string>();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Title}{(IsLost? "*":"")} [{Genre}]";
        }
    }
}
