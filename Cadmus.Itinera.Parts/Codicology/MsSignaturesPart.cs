﻿using Cadmus.Core;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Text;

namespace Cadmus.Itinera.Parts.Codicology
{
    /// <summary>
    /// Manuscript's signature(s).
    /// <para>Tag: <c>net.fusisoft.itinera.ms-signatures</c>.</para>
    /// </summary>
    [Tag("net.fusisoft.itinera.ms-signatures")]
    public sealed class MsSignaturesPart : PartBase
    {
        /// <summary>
        /// Gets or sets the signatures.
        /// </summary>
        public List<MsSignature> Signatures { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSignaturesPart"/>
        /// class.
        /// </summary>
        public MsSignaturesPart()
        {
            Signatures = new List<MsSignature>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c>, and lists of pins with keys:
        /// <c>tag-VALUE-count</c>, <c>library</c> (filtered, with digits),
        /// <c>city</c> (filtered).
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.Set("tot", Signatures?.Count ?? 0, false);

            if (Signatures?.Count > 0)
            {
                foreach (MsSignature signature in Signatures)
                {
                    builder.Increase(signature.Tag, false, "tag-");

                    if (!string.IsNullOrEmpty(signature.Library))
                    {
                        builder.AddValue("library",
                            PinTextFilter.Apply(signature.Library, true));
                    }

                    if (!string.IsNullOrEmpty(signature.City))
                    {
                        builder.AddValue("city",
                            PinTextFilter.Apply(signature.City));
                    }
                }
            }

            return builder.Build(this);
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

            sb.Append("[MsSignatures]");

            if (Signatures?.Count > 0)
            {
                int n = 0;
                foreach (MsSignature signature in Signatures)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(signature);
                }
            }

            return sb.ToString();
        }
    }
}
