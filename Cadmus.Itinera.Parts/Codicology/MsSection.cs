using Fusi.Antiquity.Chronology;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A section of content in a manuscript.
    /// </summary>
    public class MsSection
    {
        /// <summary>
        /// Gets or sets the optional tag, used to categorize and group sections.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the human-readable label assigned to this section.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the start location of this section.
        /// </summary>
        public MsLocation Start { get; set; }

        /// <summary>
        /// Gets or sets the end location (inclusive) of this section.
        /// </summary>
        public MsLocation End { get; set; }

        /// <summary>
        /// Gets or sets the era.
        /// </summary>
        public string Era { get; set; }

        /// <summary>
        /// Gets or sets the date for this section.
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
            return $"[{Tag} {Label} {Start}-{End}]";
        }
    }
}
