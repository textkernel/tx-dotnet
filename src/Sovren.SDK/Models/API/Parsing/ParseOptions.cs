// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using System.Collections.Generic;
using Sovren.Models.Resume.Skills;
using Sovren.Models.Job.Skills;
using Sovren.Models.API.Matching.Request;
using System;

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
        [Obsolete("You should use the V2 skills taxonomy instead.")]
        public List<string> SkillsData { get; set; }

        /// <summary>
        /// If you want to use custom normalizations during parsing, set those here. This not a recommended feature for most customers.
        /// For more information, reach out to support@sovren.com
        /// </summary>
        [Obsolete("You should use Professions Normalization and Skills Normalization instead.")]
        public string NormalizerData { get; set; }

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

    /// <sumamry>
    /// Enable skills normalization and enhanced candidate summarization, and specify the version of the skills taxonomy for this parsing transaction.
    /// </sumamry>
    public class SkillsSettings
    {
        /// <summary>
        /// When <see langword="true"/>:
        /// <br/>- Raw skills will be normalized. These will be output under <see cref="ResumeV2Skills.Normalized"/> or <see cref="JobV2Skills.Normalized"/>.
        /// <br/>- An enhanced candidate summary is generated, leveraging the taxonomy structure to relate skills with profession groups.
        /// <br/>- When <see cref="TaxonomyVersion"/> is set to (or defaults to) "V2", 
        /// <see href="https://www.sovren.com/technical-specs/latest/rest-api/overview/#transaction-cost">additional charges apply</see>.
        /// <br/>
        /// <br/>
        /// <b>This setting has no effect when <see cref="TaxonomyVersion"/> is set to (or defaults to) "V1".</b>
        /// </summary>
        public bool Normalize {get;set;}

        /// <summary>
        /// Specifies the version of the skills taxonomy to use. One of:
        /// <br/>"V1" - <b>(DEPRECATED)</b> This is the default for old accounts. Will be removed in a future release.
        /// <br/>"V2" - This is the default for new accounts, and must be explicitly set if you have access to V1 and V2.
        /// <br/>
        /// <br/>
        /// Benefits of V2 include:
        /// <br/>- 2x larger skills taxonomy, updated frequently based on real-world data.
        /// <br/>- 15-40% higher accuracy of extracted skills.
        /// <br/>- Better clustering of skill synonyms.
        /// <br/>- Distinguish skill types (IT / Professional / Soft).
        /// <br/>- Improved candidate summary.
        /// <br/>- Compatibility with the taxonomy used in Textkernel's 
        /// <see href="https://www.textkernel.com/solution/data-enrichment-apis/">Data Enrichment APIs</see> and 
        /// <see href="https://www.jobfeed.com/">Jobfeed</see>, enabling standardization of taxonomies across all of 
        /// your data and benchmarking against jobs posted online.
        /// </summary>
        public string TaxonomyVersion {get;set;}
    }
    
    /// <summary>
    /// Enable normalization of job titles using our proprietary taxonomy and international standards.
    /// </summary>
    public class ProfessionsSettings
    {
        /// <summary>
        /// When true, the most recent 3 job titles will be normalized. This includes a proprietary value from our profession 
        /// taxonomy, plus ONET and ISCO mappings. <see href="https://www.textkernel.com/professions-data-enrichment-api/">Read more</see> 
        /// about the benefits of using a professions taxonomy.
        /// <br/><br/>
        /// When enabling professions normalization, 
        /// <see href="https://www.sovren.com/technical-specs/latest/rest-api/overview/#transaction-cost">additional charges apply</see>.
        /// <br/><br/>
        /// The following languages are supported: English, Chinese (Simplified), Dutch, French, German, Italian, Polish, Portuguese, 
        /// and Spanish. For documents in other languages, no normalized values will be returned.
        /// <br/><br/>
        /// For Sovren AI Matching, normalized professions are automatically indexed and used if enabled. To also leverage profession 
        /// normalization for user-created searches, enable <see cref="SearchMatchSettings.NormalizeJobTitles"/>.
        /// <br/><br/>
        /// The profession taxonomy and the mappings are compatible with the taxonomies used in Textkernel's 
        /// <see href="https://www.textkernel.com/solution/data-enrichment-apis/">Data Enrichment APIs</see> and 
        /// <see href="https://www.jobfeed.com/">Jobfeed</see>, 
        /// enabling standardization of taxonomies across all of your data and benchmarking against jobs posted online.
        /// </summary>
        public bool Normalize {get;set;}
    }
}
