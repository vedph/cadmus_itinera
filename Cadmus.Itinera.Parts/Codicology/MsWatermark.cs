using Fusi.Antiquity.Chronology;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A watermark.
    /// </summary>
    public class MsWatermark
    {
        /// <summary>
        /// Gets or sets the watermark's subject. In most cases this will
        /// be some internal ID, variously mapped to other repositories.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the similarity rank, expressing the level of similarity
        /// of the watermark described here to the paradigmatic watermark model;
        /// the higher the number, the less similar is the watermark.
        /// </summary>
        public int SimilarityRank { get; set; }

        /// <summary>
        /// Gets or sets the description of this watermark instance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public HistoricalDate Date { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Subject).Append('=').Append(SimilarityRank);

            if (!string.IsNullOrEmpty(Place)) sb.Append(' ').Append(Place);
            if (Date != null) sb.Append(' ').Append(Date);

            return sb.ToString();
        }
    }
}
