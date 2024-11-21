// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.MatchV2.Request;
using Textkernel.Tx.Models.API.MatchV2.Response;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.MatchV2"/>
    /// </summary>
    public interface IMatchV2Client
    {

        #region Candidates

        Task<ApiResponse<object>> AddCandidate(string documentId, ParsedResume candidate, IEnumerable<string> roles, bool anonymize = false);

        /// <summary>
        /// Delete candidate documents from environment
        /// </summary>
        /// <param name="ids">The document IDs to delete</param>
        /// <returns>Document IDs that succeeded/failed to delete</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> ids);

        //Task<object> Search(object stuff);

        //Task<object> MatchDocument(object stuff);

        #endregion

        #region Vacancies

        Task<ApiResponse<object>> AddVacancy(string documentId, ParsedJob vacancy, IEnumerable<string> roles);

        /// <summary>
        /// Delete vacancy documents from environment
        /// </summary>
        /// <param name="ids">The document IDs to delete</param>
        /// <returns>Document IDs that succeeded/failed to delete</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteVacancies(IEnumerable<string> ids);

        //Task<object> Search(object stuff);

        //Task<object> MatchDocument(object stuff);

        #endregion
    }
}
