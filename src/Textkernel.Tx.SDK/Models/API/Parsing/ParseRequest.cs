// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <summary>
    /// The request body for a Parse request
    /// </summary>
    public class ParseRequest : ParseOptions
    {
        /// <summary>
        /// A Base64 encoded string of the document file bytes. This should use the standard 'base64' 
        /// encoding as defined in <see href="https://tools.ietf.org/html/rfc4648#section-4"> RFC 4648 Section 4</see> 
        /// (not the 'base64url' variant). 
        /// <br/>
        /// <br/>.NET users can use the <see cref="Convert.ToBase64String(byte[])"/> method. 
        /// </summary>
        public string DocumentAsBase64String { get; protected set; }

        /// <summary>
        /// <b>Mandatory</b> date - in ISO 8601 (yyyy-MM-dd) format - so that the Parser knows how to interpret dates in the document 
        /// that are expressed as "current" or "as of" or similar. It is crucial that we know the date of the resume/CV so 
        /// that we can correctly calculate date spans. For example, a resume received on January 5, 2019 should be passed 
        /// with a DocumentLastModified of "2019-01-05"; if, however, it was modified on May 7, 2019, the DocumentLastModified should be 
        /// passed as "2019-05-07". Failing to pass a DocumentLastModified, or passing DocumentLastModified that are clearly improbable, may 
        /// result in rejection of data and/or additional charges, and will utterly decimate the usefulness of AI Matching and 
        /// any generated metadata. 
        /// </summary>
        public string DocumentLastModified { get; protected set; }

        /// <summary>
        /// Create a parse request containing a document to parse and any options/settings
        /// </summary>
        /// <param name="doc">The document (resume or job) to parse</param>
        /// <param name="optionsToUse">Any non-default options to use</param>
        public ParseRequest(Document doc, ParseOptions optionsToUse = null)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            this.DocumentAsBase64String = doc.AsBase64;
            this.DocumentLastModified = doc.LastModified.ToString("yyyy-MM-dd");

            if (optionsToUse != null)
            {
                this.Configuration = optionsToUse.Configuration;
                this.GeocodeOptions = optionsToUse.GeocodeOptions;
                this.IndexingOptions = optionsToUse.IndexingOptions;
#pragma warning disable 0618
                this.NormalizerData = optionsToUse.NormalizerData;
                this.SkillsData = optionsToUse.SkillsData;
#pragma warning restore 0618
                this.OutputCandidateImage = optionsToUse.OutputCandidateImage;
                this.OutputHtml = optionsToUse.OutputHtml;
                this.OutputPdf = optionsToUse.OutputPdf;
                this.OutputRtf = optionsToUse.OutputRtf;
                this.SkillsSettings = optionsToUse.SkillsSettings;
                this.ProfessionsSettings = optionsToUse.ProfessionsSettings;
                this.UseLLMParser = optionsToUse.UseLLMParser;
                this.FlexRequests = optionsToUse.FlexRequests;
            }
        }
    }
}
