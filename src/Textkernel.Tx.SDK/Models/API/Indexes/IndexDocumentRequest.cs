// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using System.Collections.Generic;
using Textkernel.Tx.Clients;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Textkernel.Tx.Models.API.Indexes
{
    /// <summary>
    /// Base class for 'index resume' or 'index job' request body
    /// </summary>
    public class IndexDocumentRequest
    {
        /// <summary>
        /// The user-defined tags you want the document to have
        /// </summary>
        public List<string> UserDefinedTags { get; set; }
    }

    /// <summary>
    /// Request body for an 'index resume' request
    /// </summary>
    public class IndexResumeRequest : IndexDocumentRequest
    {
        /// <summary>
        /// The resume to index
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }

    /// <summary>
    /// Request body for an 'index job' request
    /// </summary>
    public class IndexJobRequest : IndexDocumentRequest
    {
        /// <summary>
        /// The job to index
        /// </summary>
        public ParsedJob JobData { get; set; }
    }

    /// <summary>
    /// Request body for an 'index multiple resumes' request
    /// </summary>
    public class IndexMultipleResumesRequest
    {
        /// <summary>
        /// The resumes to add into the index
        /// </summary>
        public List<IndexResumeInfo> Resumes { get; set; }
    }

    /// <summary>
    /// Request body for an 'index multiple jobs' request
    /// </summary>
    public class IndexMultipleJobsRequest
    {
        /// <summary>
        /// The jobs to add into the index
        /// </summary>
        public List<IndexJobInfo> Jobs { get; set; }
    }

    /// <inheritdoc/>
    public class IndexResumeInfo : IndexingOptionsGeneric
    {
        /// <summary>
        /// A resume to index
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }

    /// <inheritdoc/>
    public class IndexJobInfo : IndexingOptionsGeneric
    {
        /// <summary>
        /// A job to index
        /// </summary>
        public ParsedJob JobData { get; set; }
    }

    /// <summary>
    /// The Search &amp; Match Version to use for indexing.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SearchAndMatchVersion
    {
        /// <summary>
        /// V1, see <see cref="ISearchMatchClient"/>
        /// </summary>
        V1,
        /// <summary>
        /// V2, see <see cref="IMatchV2Client"/>
        /// </summary>
        V2
    }

    /// <summary>
    /// Generic options that have properties for both V1 and V2.
    /// </summary>
    public class IndexingOptionsGeneric
    {
        /// <summary>
        /// <strong>Be sure to set all the correct properties depending if you are using Match V1 or V2 if you use this constructor</strong>
        /// </summary>
        internal IndexingOptionsGeneric() { }

        /// <summary>
        /// Create options to index a document with Match V1
        /// </summary>
        /// <param name="documentId">The id to assign to the new document. This is restricted to alphanumeric with dashes and underscores. All values will be converted to lower-case.</param>
        /// <param name="indexId">The id for the index where the document should be added (case-insensitive).</param>
        /// <param name="userDefinedTags">The user-defined tags the document should have</param>
        public IndexingOptionsGeneric(string documentId, string indexId, List<string> userDefinedTags = null)
        {
            SearchAndMatchVersion = SearchAndMatchVersion.V1;
            UserDefinedTags = userDefinedTags ?? new List<string>();
            IndexId = indexId;
            DocumentId = documentId;
        }

        /// <summary>
        /// Create options to upload a document with Match V2
        /// </summary>
        /// <param name="env">The target environment where the document will be uploaded</param>
        /// <param name="documentId">The id to assign to the new document. This is restricted to alphanumeric with dashes and underscores.</param>
        /// <param name="roles">The list of roles that are allowed to retrieve the document. If not set, <c>["all"]</c> will be used.</param>
        /// <param name="customFields">A key-value collection of custom fields.</param>
        public IndexingOptionsGeneric(
            MatchV2Environment env,
            string documentId,
            List<string> roles = null,
            Dictionary<string, string> customFields = null)
        {
            SearchAndMatchVersion = SearchAndMatchVersion.V2;
            Roles = roles;
            CustomFields = customFields;
            DocumentId = documentId;
            SearchAndMatchEnvironment = env;
        }

        /// <summary>
        /// The id to assign to the new document. This is restricted to alphanumeric with dashes and underscores. 
        /// All values will be converted to lower-case.
        /// <br/>NOTE: If you are using the <see cref="Batches.BatchParser"/>, it will set the DocumentId for each document.
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// The Search &amp; Match Version to use for indexing.
        /// </summary>
        public SearchAndMatchVersion SearchAndMatchVersion { get; set; }

        /// <summary>
        /// The id for the index where the document should be added (case-insensitive).
        /// </summary>
        /// <remarks>
        /// Only use when <see cref="SearchAndMatchVersion"/> = <see cref="SearchAndMatchVersion.V1"/>
        /// </remarks>
        public string IndexId { get; set; }

        /// <summary>
        /// The user-defined tags the document should have
        /// </summary>
        /// <remarks>
        /// Only use when <see cref="SearchAndMatchVersion"/> = <see cref="SearchAndMatchVersion.V1"/>
        /// </remarks>
        public List<string> UserDefinedTags { get; set; }

        /// <summary>
        /// The target environment where the document will be uploaded
        /// </summary>
        /// <remarks>
        /// Only use when <see cref="SearchAndMatchVersion"/> = <see cref="SearchAndMatchVersion.V2"/>
        /// </remarks>
        public MatchV2Environment SearchAndMatchEnvironment { get; set; }

        /// <summary>
        /// The list of roles that are allowed to retrieve the document. If not set, <c>["all"]</c> will be used.
        /// </summary>
        /// <remarks>
        /// Only use when <see cref="SearchAndMatchVersion"/> = <see cref="SearchAndMatchVersion.V2"/>
        /// </remarks>
        public List<string> Roles { get; set; }

        /// <summary>
        /// A key-value collection of custom fields.
        /// </summary>
        /// <remarks>
        /// Only use when <see cref="SearchAndMatchVersion"/> = <see cref="SearchAndMatchVersion.V2"/>
        /// </remarks>
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
