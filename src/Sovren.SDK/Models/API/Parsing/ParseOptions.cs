// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using System.Collections.Generic;

namespace Sovren.Models.API.Parsing
{
    /// <summary>
    /// Options for parsing
    /// </summary>
    public class BasicParseOptions
    {
        //********************************
        //IF YOU ADD ANY PARAMS HERE BE SURE TO ADD THEM IN THE DEEP COPY INSIDE ParseRequest.ctor() !!
        //********************************

        /// <summary>
        /// The configuration settings to use during parsing. See <see href="https://sovren.com/technical-specs/latest/rest-api/resume-parser/overview/configuration/#config"/>.
        /// <br/>NOTE: leaving this <see langword="null"/>/empty will use the default parsing settings which is recommended in most cases.
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// If you want to use custom skills lists during parsing, set those here. This not a recommended feature for most customers.
        /// For more information, reach out to support@sovren.com
        /// </summary>
        public List<string> SkillsData { get; set; }

        /// <summary>
        /// If you want to use custom normalizations during parsing, set those here. This not a recommended feature for most customers.
        /// For more information, reach out to support@sovren.com
        /// </summary>
        public string NormalizerData { get; set; }

        //********************************
        //IF YOU ADD ANY PARAMS HERE BE SURE TO ADD THEM IN THE DEEP COPY INSIDE ParseRequest.ctor() !!
        //********************************
    }

    /// <inheritdoc/>
    public class ParseOptions : BasicParseOptions
    {
        //********************************
        //IF YOU ADD ANY PARAMS HERE BE SURE TO ADD THEM IN THE DEEP COPY INSIDE ParseRequest.ctor() !!
        //********************************

        /// <summary>
        /// <see langword="true"/> to output the document converted to HTML
        /// </summary>
        public bool OutputHtml { get; set; }

        /// <summary>
        /// <see langword="true"/> to output the document converted to RTF
        /// </summary>
        public bool OutputRtf { get; set; }

        /// <summary>
        /// <see langword="true"/> to output the document converted to PDF
        /// </summary>
        public bool OutputPdf { get; set; }

        /// <summary>
        /// Only used for resumes. <see langword="true"/> to extract/output a candidate's image if it is present in the resume.
        /// </summary>
        public bool OutputCandidateImage { get; set; }

        /// <summary>
        /// Use this property to also include geocoding in this parse request.
        /// The document will be parsed and then geocoded.
        /// </summary>
        public GeocodeOptions GeocodeOptions { get; set; }

        /// <summary>
        /// If you are using Sovren AI Matching, use this property to also index the document after it is parsed/geocoded.
        /// This means you only need to send the document to our API once instead of twice for parsing+indexing.
        /// </summary>
        public IndexSingleDocumentInfo IndexingOptions { get; set; }

        //********************************
        //IF YOU ADD ANY PARAMS HERE BE SURE TO ADD THEM IN THE DEEP COPY INSIDE ParseRequest.ctor() !!
        //********************************
    }
}
