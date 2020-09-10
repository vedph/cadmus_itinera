﻿namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's recalls (richiami).
    /// </summary>
    public class MsRecall
    {
        /// <summary>
        /// Gets or sets the position in the page.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this recall is vertical
        /// instead of horizontal.
        /// </summary>
        public bool IsVertical { get; set; }

        /// <summary>
        /// Gets or sets the decoration description.
        /// </summary>
        public string Decoration { get; set; }

        /// <summary>
        /// Gets or sets the description about registered numberings/signatures
        /// (numerazioni o segnature a registro).
        /// </summary>
        public string Register { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{(IsVertical? 'V':'H')}] {Position}";
        }
    }
}
