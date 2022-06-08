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

        /// <summary>
        /// Enable skills normalization and enhanced candidate summarization, and specify the version of the skills taxonomy for this parsing transaction.
        /// </summary> 
        public SkillsSettings SkillsSettings { get; set; }

        /// <summary>
        /// Enable normalization of job titles using our proprietary taxonomy and international standards.
        /// </summary>
        public ProfessionsSettings ProfessionsSettings { get; set; }

        //********************************
        //IF YOU ADD ANY PARAMS HERE BE SURE TO ADD THEM IN THE DEEP COPY INSIDE ParseRequest.ctor() !!
        //********************************
    }

    /// <sumamry>
    /// Enable skills normalization and enhanced candidate summarization, and specify the version of the skills taxonomy for this parsing transaction.
    /// </sumamry>
    public class SkillsSettings
    {
        /// <summary>
        /// When true:
        /// <ul>
        ///     <li>raw skills will be normalized. These will be output under Value.ResumeData.Skills.Normalized.</li> <a href="https://www.textkernel.com/skills/">Read more</a> about the benefits of using a skills taxonomy.
        ///     <li>An enhanced candidate summary is generated, leveraging the taxonomy structure to relate skills to profession groups.</li>
        /// </ul>
        /// When using TaxonomyVersion V2 (see below), additional <a href="https://www.sovren.com/technical-specs/latest/rest-api/overview/#transaction-cost">charges apply</a>. when normalization is enabled.
        /// <br/><br/>
        /// When you have access to TaxonomyVersion V1, and did not set the taxonomy to V2 explicitly (see below), normalization is enabled by default and the candidate summary is generated using the V1 taxonomy structure.
        /// </summary>
        public bool Normalize {get;set;}

        /// <summary>
        /// Specifies the version of the skills taxonomy to use. Defaults to V2, unless your account has access to V1. If you have access to V1, use v2 as the value for this property to explicitly set V2.
        /// <br/><br/>
        /// <strong>V1 is deprecated</strong> and will be removed in a future release.
        /// <br/><br/>
        /// Benefits of V2 include:
        /// <ul>
        ///     <li>2x larger skills taxonomy, updated frequently based on real-world data</li>
        ///     <li>15-40% higher accuracy of extracted skills</li>
        ///     <li>Better clustering of skill synonyms</li>
        ///     <li>Distinguish skill types (IT / Professional / Soft)</li>
        ///     <li>Improved candidate summary</li>
        ///     <li>Compatibility with the taxonomy used in Textkernel's <a href="https://www.textkernel.com/solution/data-enrichment-apis/">Data Enrichment APIs</a> and <a href="https://www.jobfeed.com/">Jobfeed</a>, enabling standardization of taxonomies across all of your data and benchmarking against jobs posted online.</li>
        /// </ul>
        /// </summary>
        public string TaxonomyVersion {get;set;}
    }
    
    /// <summary>
    /// Enable normalization of job titles using our proprietary taxonomy and international standards.
    /// </summary>
    public class ProfessionsSettings
    {
        /// <summary>
        /// When true, the most recent 3 job titles will be normalized. This includes a proprietary value from our profession taxonomy, plus ONET and ISCO mappings. <a href="https://www.textkernel.com/professions-data-enrichment-api/">Read more</a> about the benefits of using a professions taxonomy.
        /// <br/><br/>
        /// When enabling professions normalization, additional <a href="https://www.sovren.com/technical-specs/latest/rest-api/overview/#transaction-cost">charges apply</a>.
        /// <br/><br/>
        /// The following languages are supported: English, Chinese (Simplified), Dutch, French, German, Italian, Polish, Portuguese, and Spanish. For documents in other languages, no normalized values will be returned.
        /// <br/><br/>
        /// For Sovren AI Matching, normalized professions are automatically indexed and used when profession normalization is enabled during parsing 
        /// (through <a href="https://www.sovren.com/technical-specs/latest/rest-api/resume-parser/api/">IndexingOptions</a>). To leverage profession normalization for user-created searches, 
        /// enable <a href="https://www.sovren.com/technical-specs/latest/rest-api/ai-matching/querying-api/search/?h=Settings.NormalizeJobTitles">profession normalization at query time</a>.
        /// <br/><br/>
        /// The profession taxonomy and the mappings are compatible with the taxonomies used in Textkernel's <a href="https://www.textkernel.com/solution/data-enrichment-apis/">Data Enrichment APIs</a> and <a href="https://www.jobfeed.com/">Jobfeed</a>, 
        /// enabling standardization of taxonomies across all of your data and benchmarking against jobs posted online.
        /// </summary>
        public bool Normalize {get;set;}
    }
}
