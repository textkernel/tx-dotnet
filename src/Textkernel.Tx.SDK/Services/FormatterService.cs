// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Formatter;

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.Formatter"/>
    /// </summary>
    internal class FormatterService : ServiceBase, IFormatterService
    {
        internal FormatterService(HttpClient httpClient, EnvironmentSettings settings)
            : base(httpClient, settings)
        {
        }

        /// <inheritdoc />
        public async Task<FormatResumeResponse> FormatResume(FormatResumeRequest request)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.FormatResume();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<FormatResumeResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<FormatResumeResponse> FormatResumeWithTemplate(FormatResumeWithTemplateRequest request)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.FormatResumeWithTemplate();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<FormatResumeResponse>(response, apiRequest);
        }
    }
}
