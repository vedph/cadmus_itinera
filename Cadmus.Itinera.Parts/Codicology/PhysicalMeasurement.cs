namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// A physical measurement.
    /// </summary>
    public class PhysicalMeasurement
    {
        /// <summary>
        /// Gets or sets the identifier of the measured property.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the size value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the measurement unit.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this measurement is only
        /// roughly approximate.
        /// </summary>
        public bool IsApproximate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this measurement refers
        /// to an incomplete object.
        /// </summary>
        public bool IsIncomplete { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Id}={(IsApproximate ? "c." : "")}{Value} {Unit}"
                + (IsIncomplete ? "*" : "");
        }
    }
}
