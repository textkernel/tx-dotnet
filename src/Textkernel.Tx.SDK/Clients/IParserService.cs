// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.Parser"/>
    /// </summary>
    public interface IParserService
    {
        /// <summary>
        /// Parse a resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="TxException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="TxGeocodeResumeException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="TxIndexResumeException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        /// <exception cref="TxProfessionNormalizationResumeException">Thrown when parsing was successful, but an error occurred during profession normalization</exception>
        Task<ParseResumeResponse> ParseResume(ParseRequest request);


        /// <summary>
        /// Parse a job
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="TxException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="TxGeocodeJobException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="TxIndexJobException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        /// <exception cref="TxProfessionNormalizationJobException">Thrown when parsing was successful, but an error occurred during profession normalization</exception>
        Task<ParseJobResponse> ParseJob(ParseRequest request);
    }
}
