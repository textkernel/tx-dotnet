// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Parsing;

namespace Sovren.Batches
{
    /// <summary>
    /// A single parse result from the batch parser
    /// </summary>
    public abstract class BatchResult
    {
        /// <summary>
        /// The DocumentId that was assigned for this file by you or automatically (if you did not provide your own logic to the <see cref="BatchParser"/>)
        /// </summary>
        public string DocumentId { get; protected set; }

        /// <summary>
        /// The file path for this file
        /// </summary>
        public string File { get; protected set; }

        internal BatchResult(string file, string docId)
        {
            DocumentId = docId;
            File = file;
        }
    }

    /// <summary>
    /// A result indicating the file was parsed successfully (and if specified, the file was also geocoded/indexed successfully)
    /// </summary>
    public class ResumeBatchSuccessResult : BatchResult
    {
        /// <summary>
        /// The response from the Sovren API when parsing this file
        /// </summary>
        public ParseResumeResponse Response { get; protected set; }

        internal ResumeBatchSuccessResult(string file, string docId, ParseResumeResponse response)
            : base(file, docId)
        {
            Response = response;
        }
    }

    /// <summary>
    /// A result indicating the file was parsed, but some error occurred during/after parsing
    /// </summary>
    public class ResumeBatchPartialSuccessResult : ResumeBatchSuccessResult
    {
        /// <summary>
        /// The error that occurred when parsing this file. There is probably still usable data in the <see cref="ResumeBatchSuccessResult.Response"/>
        /// </summary>
        public SovrenUsableResumeException Error { get; protected set; }

        internal ResumeBatchPartialSuccessResult(string file, string docId, SovrenUsableResumeException e)
            : base(file, docId, e.Response)
        {
            Error = e;
        }
    }

    /// <summary>
    /// A result indicating the file was parsed successfully (and if specified, the file was also geocoded/indexed successfully)
    /// </summary>
    public class JobBatchSuccessResult : BatchResult
    {
        /// <summary>
        /// The response from the Sovren API when parsing this file
        /// </summary>
        public ParseJobResponse Response { get; protected set; }

        internal JobBatchSuccessResult(string file, string docId, ParseJobResponse response)
            : base(file, docId)
        {
            Response = response;
        }
    }

    /// <summary>
    /// A result indicating the file was parsed, but some error occurred during/after parsing
    /// </summary>
    public class JobBatchPartialSuccessResult : JobBatchSuccessResult
    {
        /// <summary>
        /// The error that occurred when parsing this file. There is probably still usable data in the <see cref="JobBatchSuccessResult.Response"/>
        /// </summary>
        public SovrenUsableJobException Error { get; protected set; }

        internal JobBatchPartialSuccessResult(string file, string docId, SovrenUsableJobException e)
            : base(file, docId, e.Response)
        {
            Error = e;
        }
    }

    /// <summary>
    /// A result indicating an error occurred and particular file could not be parsed
    /// </summary>
    public class BatchErrorResult : BatchResult
    {
        /// <summary>
        /// The error that occurred when parsing this file
        /// </summary>
        public SovrenException Error { get; protected set; }

        internal BatchErrorResult(string file, string docId, SovrenException e)
            : base(file, docId)
        {
            Error = e;
        }
    }
}
