using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Formatter;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.Formatter"/>
    /// </summary>
    internal class FormatterClient : ClientBase, IFormatterClient
    {
        internal FormatterClient(HttpClient httpClient) : base(httpClient) { }

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
