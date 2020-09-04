﻿using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cadmus.Itinera.Parts.Epistolography
{
    /// <summary>
    /// Dedications by the reference author to a correspondent, or vice-versa.
    /// Tag: <c>net.fusisoft.itinera.corr-dedications</c>.
    /// </summary>
    /// <seealso cref="PartBase" />
    [Tag("net.fusisoft.itinera.corr-dedications")]
    public sealed class CorrDedicationsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the dedications.
        /// </summary>
        public List<CorrDedication> Dedications { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrDedicationsPart"/>
        /// class.
        /// </summary>
        public CorrDedicationsPart()
        {
            Dedications = new List<CorrDedication>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: collections of unique values keyed under these
        /// IDs: <c>title</c> (filtered) and <c>date-value</c>; in addition,
        /// <c>auth-count</c>=count of dedications by author; <c>corr-count</c>
        /// =count of dedications by correspondents.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            List<DataPin> pins = new List<DataPin>();

            if (Dedications?.Count > 0)
            {
                HashSet<string> titles = new HashSet<string>();
                HashSet<double> dateValues = new HashSet<double>();
                int ac = 0, cc = 0;

                foreach (CorrDedication dedication in Dedications)
                {
                    if (!string.IsNullOrEmpty(dedication.Title))
                        titles.Add(PinTextFilter.Apply(dedication.Title, true));
                    if (dedication.Date != null)
                        dateValues.Add(dedication.Date.GetSortValue());
                    if (dedication.IsByAuthor) ac++;
                    else cc++;
                }

                foreach (string title in titles)
                    pins.Add(CreateDataPin("title", title));
                foreach (double dv in dateValues)
                {
                    pins.Add(CreateDataPin("date-value",
                        dv.ToString(CultureInfo.InvariantCulture)));
                }
                pins.Add(CreateDataPin("auth-count",
                    ac.ToString(CultureInfo.InvariantCulture)));
                pins.Add(CreateDataPin("corr-count",
                    cc.ToString(CultureInfo.InvariantCulture)));
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

            sb.Append("[CorrDedications]");

            if (Dedications?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var dedication in Dedications)
                {
                    if (++n > 5)
                    {
                        sb.Append("[...").Append(Dedications.Count).Append(']');
                        break;
                    }
                    if (n > 1) sb.Append("; ");
                    sb.Append(dedication.Title);
                }
            }

            return sb.ToString();
        }
    }
}
