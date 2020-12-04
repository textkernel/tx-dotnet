// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job;
using Sovren.Models.Resume;
using System.Collections.Generic;

namespace Sovren.Models.API.Indexes
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
    public class IndexResumeInfo : IndexMultipleDocumentInfo
    {
        /// <summary>
        /// A resume to index
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }

    /// <inheritdoc/>
    public class IndexJobInfo : IndexMultipleDocumentInfo
    {
        /// <summary>
        /// A job to index
        /// </summary>
        public ParsedJob JobData { get; set; }
    }

    /// <summary>
    /// Information for adding a document to an index
    /// </summary>
    public class IndexSingleDocumentInfo : IndexMultipleDocumentInfo
    {
        /// <summary>
        /// The id for the index where the document should be added (case-insensitive).
        /// </summary>
        public string IndexId { get; set; }
    }

    /// <summary>
    /// Information for adding a single document to an index as part of a 'batch upload'
    /// </summary>
    public class IndexMultipleDocumentInfo
    {
        /// <summary>
        /// The id to assign to the new document. This is restricted to alphanumeric with dashes and underscores. 
        /// All values will be converted to lower-case.
        /// <br/>NOTE: If you are using the <see cref="Batches.BatchParser"/>, it will set the DocumentId for each document.
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// The user-defined tags the document should have
        /// </summary>
        public List<string> UserDefinedTags { get; set; } = new List<string>();
    }
}
