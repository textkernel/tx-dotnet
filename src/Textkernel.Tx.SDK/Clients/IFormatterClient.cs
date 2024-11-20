using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Formatter;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.Formatter"/>
    /// </summary>
    public interface IFormatterClient
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
