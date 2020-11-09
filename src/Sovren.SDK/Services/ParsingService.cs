// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using Sovren.Models.API.Parsing;
using System;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <inheritdoc/>
    public class ParsingService : SovrenService
    {
        /// <summary>
        /// Settings for all parses
        /// </summary>
        public ParseOptions Options { get; protected set; }

        /// <summary>
        /// Create a service to parse resumes/jobs
        /// </summary>
        /// <param name="client">The SovrenClient that will make the low-level API calls</param>
        /// <param name="options">Settings for all parses. This can be changed on-the-fly via <see cref="Options"/></param>
        public ParsingService(SovrenClient client, ParseOptions options = null)
            : base(client)
        {
            Options = options ?? new ParseOptions();
        }

        /// <summary>
        /// Parse a resume
        /// </summary>
        /// <param name="doc">The document to parse</param>
        /// <param name="documentId">
        /// If you are indexing the document while parsing, you must provide the document id to use here.
        /// <br/>
        /// If specified, this value overwrites the value in the <see cref="Options"/>
        /// </param>
        /// <returns>A <see cref="ParseResumeResponseValue"/> containing the parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeResumeException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexResumeException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        public async Task<ParseResumeResponseValue> ParseResume(Document doc, string documentId = null)
        {
            if (documentId != null && Options?.IndexingOptions != null)
            {
                Options.IndexingOptions.DocumentId = documentId;
            }

            ParseRequest request = new ParseRequest(doc, Options);
            ParseResumeResponse response = await Client.ParseResume(request);
            return response?.Value;
        }

        /// <summary>
        /// Parse a job
        /// </summary>
        /// <param name="doc">The document to parse</param>
        /// <param name="documentId">
        /// If you are indexing the document while parsing, you must provide the document id to use here.
        /// <br/>
        /// If specified, this value overwrites the value in the <see cref="Options"/>
        /// </param>
        /// <returns>A <see cref="ParseJobResponseValue"/> containing the parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeJobException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexJobException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        public async Task<ParseJobResponseValue> ParseJob(Document doc, string documentId = null)
        {
            if (documentId != null && Options?.IndexingOptions != null)
            {
                Options.IndexingOptions.DocumentId = documentId;
            }

            ParseRequest request = new ParseRequest(doc, Options);
            ParseJobResponse response = await Client.ParseJob(request);
            return response?.Value;
        }
    }
}
