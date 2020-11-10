// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Indexes;
using Sovren.Models.Job;
using Sovren.Models.Matching;
using Sovren.Models.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <inheritdoc/>
    public class IndexService : SovrenService
    {
        /// <summary>
        /// Create a service to manage indexes and documents within those indexes
        /// </summary>
        /// <param name="client">The SovrenClient to make the low-level API calls</param>
        public IndexService(SovrenClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Get all existing indexes
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<List<Index>> GetAllIndexes()
        {
            GetAllIndexesResponse response = await Client.GetAllIndexes();
            return response.Value;
        }

        /// <summary>
        /// Create a new index
        /// </summary>
        /// <param name="type">The type of documents stored in this index. Either 'Resume' or 'Job'</param>
        /// <param name="indexId">
        /// The ID to assign to the new index. This is restricted to alphanumeric with dashes 
        /// and underscores. All values will be converted to lower-case.
        /// </param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task CreateIndex(IndexType type, string indexId)
        {
            await Client.CreateIndex(type, indexId);
        }

        /// <summary>
        /// Delete an existing index. Note that this is a destructive action and 
        /// cannot be undone. All the documents in this index will be deleted.
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task DeleteIndex(string indexId)
        {
            await Client.DeleteIndex(indexId);
        }

        /// <summary>
        /// Add a resume to an existing index
        /// </summary>
        /// <param name="resume">A resume generated by the Sovren Resume Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="customValueIds">The custom value ids that the resume should have</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task AddDocumentToIndex(ParsedResume resume, string indexId, string documentId, IEnumerable<string> customValueIds = null)
        {
            IndexSingleDocumentInfo options = new IndexSingleDocumentInfo
            {
                IndexId = indexId,
                DocumentId = documentId,
                CustomValueIds = customValueIds?.ToList()
            };
            await Client.AddDocumentToIndex(resume, options);
        }

        /// <summary>
        /// Add several resumes to an existing index
        /// </summary>
        /// <param name="resumes">The resumes generated by the Sovren Resume Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the resumes should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> AddMultipleDocumentsToIndex(IEnumerable<IndexResumeInfo> resumes, string indexId)
        {
            return await Client.AddMultipleDocumentsToIndex(resumes, indexId);
        }

        /// <summary>
        /// Add several jobs to an existing index
        /// </summary>
        /// <param name="jobs">The jobs generated by the Sovren Job Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the jobs should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> AddMultipleDocumentsToIndex(IEnumerable<IndexJobInfo> jobs, string indexId)
        {
            return await Client.AddMultipleDocumentsToIndex(jobs, indexId);
        }

        /// <summary>
        /// Add a job to an existing index
        /// </summary>
        /// <param name="job">A job generated by the Sovren Job Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="customValueIds">The custom value ids that the job should have</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task AddDocumentToIndex(ParsedJob job, string indexId, string documentId, IEnumerable<string> customValueIds = null)
        {
            IndexSingleDocumentInfo options = new IndexSingleDocumentInfo
            {
                IndexId = indexId,
                DocumentId = documentId,
                CustomValueIds = customValueIds?.ToList()
            };
            await Client.AddDocumentToIndex(job, options);
        }

        /// <summary>
        /// Delete an existing document from an index
        /// </summary>
        /// <param name="indexId">The index containing the document</param>
        /// <param name="documentId">The ID of the document to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task DeleteDocumentFromIndex(string indexId, string documentId)
        {
            await Client.DeleteDocumentFromIndex(indexId, documentId);
        }

        /// <summary>
        /// Delete a group of existing documents from an index
        /// </summary>
        /// <param name="indexId">The index containing the documents</param>
        /// <param name="documentIds">The IDs of the documents to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<DeleteMultipleDocumentsResponse> DeleteMultipleDocumentsFromIndex(string indexId, IEnumerable<string> documentIds)
        {
            return await Client.DeleteMultipleDocumentsFromIndex(indexId, documentIds);
        }

        /// <summary>
        /// Retrieve an existing resume from an index
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<ParsedResume> GetResume(string indexId, string documentId)
        {
            GetResumeResponse response = await Client.GetResumeFromIndex(indexId, documentId);
            return response.Value;
        }

        /// <summary>
        /// Retrieve an existing job from an index
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<ParsedJob> GetJob(string indexId, string documentId)
        {
            GetJobResponse response = await Client.GetJobFromIndex(indexId, documentId);
            return response.Value;
        }

        /// <summary>
        /// Updates the custom value ids for a resume
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to update</param>
        /// <param name="customValueIds">The custom value ids to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified custom value ids</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<UpdateCustomValueIdsResponse> UpdateResumeCustomValueIds(
            string indexId, 
            string documentId, 
            IEnumerable<string> customValueIds, 
            CustomValueIdsMethod method)
        {
            UpdateCustomValueIdsRequest request = new UpdateCustomValueIdsRequest
            {
                CustomValueIds = customValueIds.ToList(),
                Method = method
            };

            return await Client.UpdateResumeCustomValueIds(indexId, documentId, request);
        }

        /// <summary>
        /// Updates the custom value ids for a job
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to update</param>
        /// <param name="customValueIds">The custom value ids to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified custom value ids</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<UpdateCustomValueIdsResponse> UpdateJobCustomValueIds(
            string indexId,
            string documentId,
            IEnumerable<string> customValueIds,
            CustomValueIdsMethod method)
        {
            UpdateCustomValueIdsRequest request = new UpdateCustomValueIdsRequest
            {
                CustomValueIds = customValueIds.ToList(),
                Method = method
            };

            return await Client.UpdateJobCustomValueIds(indexId, documentId, request);
        }
    }
}
