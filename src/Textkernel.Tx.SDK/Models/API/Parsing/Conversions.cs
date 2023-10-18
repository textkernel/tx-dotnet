// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;

namespace Sovren.Models.API.Parsing
{
    /// <summary>
    /// Conversions output by the document converter during a parse transaction
    /// </summary>
    public class Conversions
    {
        /// <summary>
        /// If requested by <see cref="ParseOptions.OutputPdf"/>, this is the 
        /// document converted to a PDF. This is a <see langword="byte"/>[] as a Base64-encoded string. You can use
        /// <see cref="Convert.FromBase64String(string)"/> to turn this back into a <see langword="byte"/>[]
        /// </summary>
        public string PDF { get; set; }

        /// <summary>
        /// If requested by <see cref="ParseOptions.OutputHtml"/>, this is the 
        /// document converted to HTML.
        /// </summary>
        public string HTML { get; set; }

        /// <summary>
        /// If requested by <see cref="ParseOptions.OutputRtf"/>, this is the 
        /// document converted to RTF.
        /// </summary>
        public string RTF { get; set; }

        /// <summary>
		/// If a candidate photo was extracted, it will be output here. This is a <see langword="byte"/>[] as a Base64-encoded string.
        /// You can use <see cref="Convert.FromBase64String(string)"/> to turn this back into a <see langword="byte"/>[]
		/// </summary>
		public string CandidateImage { get; set; }

        /// <summary>
        /// If a candidate photo was extracted, the appropriate file extension for the photo will be output for this field (e.g. ".png").
        /// </summary>
        public string CandidateImageExtension { get; set; }
    }
}
