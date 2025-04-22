// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Formatter;

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.Formatter"/>
    /// </summary>
    public interface IFormatterService
    {
        /// <summary>
        /// Format a parsed resume into a standardized/templated resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The formatted resume document</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<FormatResumeResponse> FormatResume(FormatResumeRequest request);

        /// <summary>
        /// Format a parsed resume into a template that you provide
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The formatted resume document</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<FormatResumeResponse> FormatResumeWithTemplate(FormatResumeWithTemplateRequest request);
    }
}
