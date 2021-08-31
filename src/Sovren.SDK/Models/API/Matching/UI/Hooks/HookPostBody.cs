// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Matching.Request;
using Sovren.Models.Resume;
using System.Collections.Generic;

namespace Sovren.Models.API.Matching.UI.Hooks
{
    /// <summary>
    /// Base class for the POST body for any server-side hook action
    /// </summary>
    /// <typeparam name="T">The type of data you passed in for <see cref="ServerSideHook.CustomInfo"/> </typeparam>
    public abstract class HookPostBody<T>
    {
        /// <summary>
        /// The user that executed this hook
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Whatever you passed in for <see cref="ServerSideHook.CustomInfo"/> when you specified the hook
        /// </summary>
        public T CustomInfo { get; set; }
    }

    /// <summary>
    /// The POST body that is sent to the specified URL when a hook is executed
    /// </summary>
    /// <typeparam name="T">The type of data you passed in for <see cref="ServerSideHook.CustomInfo"/></typeparam>
    public class UserActionHookPostBody<T> : HookPostBody<T>
    {
        /// <summary>
        /// The Id of the document for which the action/hook was executed
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// The Id of the index that contains the document for which the action/hook was executed
        /// </summary>
        public string IndexId { get; set; }
    }

    /// <summary>
    /// A document/index id combo
    /// </summary>
    public class DocumentIdentifier
    {
        /// <summary>
        /// The Id of the document for which the action/hook was executed
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// The Id of the index that contains the document for which the action/hook was executed
        /// </summary>
        public string IndexId { get; set; }
    }

    /// <summary>
    /// The POST body that is sent to the specified URL when a bulk hook is executed
    /// </summary>
    /// <typeparam name="T">The type of data you passed in for <see cref="ServerSideHook.CustomInfo"/></typeparam>
    public class BulkHookPostBody<T> : HookPostBody<T>
    {
        /// <summary>
        /// The list of documents that was selected when this hook/action was executed
        /// </summary>
        public List<DocumentIdentifier> Documents { get; set; }
    }

    /// <summary>
    /// The POST body that is sent to the specified URL when an OnUpdate hook is executed
    /// </summary>
    /// <typeparam name="T">The type of data you passed in for <see cref="ServerSideHook.CustomInfo"/></typeparam>
    public class OnUpdateHookPostBody<T> : HookPostBody<T>
    {
        /// <summary>
        /// The current filter criteria the user has specified
        /// </summary>
        public FilterCriteria FilterCriteria { get; set; }

        /// <summary>
        /// The current category weights the user has specified
        /// </summary>
        public CategoryWeights PreferredCategoryWeights { get; set; }
    }

    /// <summary>
    /// The POST body that is sent to the specified URL when a sourcing hook is executed
    /// </summary>
    /// <typeparam name="T">The type of data you passed in for <see cref="ServerSideHook.CustomInfo"/></typeparam>
    public class SourcingHookPostBody<T> : HookPostBody<T>
    {
        /// <summary>
        /// The job board where this doc came from
        /// </summary>
        public string JobBoard { get; set; }

        /// <summary>
        /// The id of the document (from the job board) that the action/hook was executed for
        /// </summary>
        public string JobBoardDocumentId { get; set; }

        /// <summary>
        /// The Sovren ParsedResume that you should store in your DB and index if you like
        /// </summary>
        public ParsedResume ResumeData { get; set; }

        /// <summary>
        /// The text of the document that the action/hook was executed for
        /// </summary>
        public string SourceDocument { get; set; }
    }
}
