// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sovren.Models;
using Sovren.Models.API.Parsing;
using System.Threading.Tasks;

namespace Sovren.Batches
{
    /// <summary>
    /// Thrown when the files found do not meet the criteria for a valid batch. See <see cref="BatchParsingRules"/>
    /// </summary>
    public class SovrenInvalidBatchException : Exception
    {
        internal SovrenInvalidBatchException(string message) : base(message) { }
    }

    /// <summary>
    /// A utility to parse batches of files.
    /// </summary>
    public class BatchParser
    {
        /// <summary>
        /// Parses a batch of resumes
        /// </summary>
        /// <param name="apiClient">The API client to use to parse the files</param>
        /// <param name="parseOptions">Any parsing/indexing options</param>
        /// <param name="rules">
        /// The rules that should be applied to whatever files are found prior to parsing.
        /// This is important to reduce the number of invalid parse API calls and reduce parsing costs.
        /// </param>
        /// <param name="directory">The directory containing the files to be parsed</param>
        /// <param name="searchOption"></param>
        /// <param name="successCallback">A callback for when a file is parsed successfully</param>
        /// <param name="partialSuccessCallback">A callback for when some error happened during/after parsing, but there is still usable data in the response</param>
        /// <param name="errorCallback">A callback for when an error occurred when parsing the file, and there is no usable data</param>
        /// <param name="generateDocumentIdFn">A callback so you can specify a DocumentId for each file that is parsed</param>
        /// <exception cref="SovrenInvalidBatchException">Thrown when the directory provided does not meet the <see cref="BatchParsingRules"/></exception>
        public static async Task ParseResumes(
            SovrenClient apiClient,
            ParseOptions parseOptions,
            BatchParsingRules rules,
            string directory,
            SearchOption searchOption,
            Func<ResumeBatchSuccessResult, Task> successCallback,
            Func<ResumeBatchPartialSuccessResult, Task> partialSuccessCallback,
            Func<BatchErrorResult, Task> errorCallback,
            Func<string, string> generateDocumentIdFn = null)
        {
            if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));

            IEnumerable<string> files = GetFiles(rules, directory, searchOption);

            //process the batch serially, since multi-threading could cause the customer to violate the AUP accidentally
            foreach (string file in files)
            {
                Document doc = new Document(file);
                string docId = generateDocumentIdFn == null ? Guid.NewGuid().ToString() : generateDocumentIdFn(file);

                try
                {
                    //set document id if we plan to index these documents
                    if (parseOptions?.IndexingOptions != null)
                        parseOptions.IndexingOptions.DocumentId = docId;

                    ParseRequest request = new ParseRequest(doc, parseOptions);
                    ParseResumeResponse response = await apiClient.ParseResume(request);
                    if (successCallback != null)
                    {
                        await successCallback(new ResumeBatchSuccessResult(file, docId, response));
                    }
                }
                catch (SovrenUsableResumeException e)
                {
                    //this happens when something wasn't 100% successful, but there still might be usable data
                    if (partialSuccessCallback != null)
                    {
                        await partialSuccessCallback(new ResumeBatchPartialSuccessResult(file, docId, e));
                    }
                }
                catch (SovrenException e)
                {
                    //this happens where there is no usable data
                    if (errorCallback != null)
                    {
                        await errorCallback(new BatchErrorResult(file, docId, e));
                    }
                }
            }
        }

        /// <summary>
        /// Parses a batch of jobs
        /// </summary>
        /// <param name="apiClient">The API client to use to parse the files</param>
        /// <param name="parseOptions">Any parsing/indexing options</param>
        /// <param name="rules">
        /// The rules that should be applied to whatever files are found prior to parsing.
        /// This is important to reduce the number of invalid parse API calls and reduce parsing costs.
        /// </param>
        /// <param name="directory">The directory containing the files to be parsed</param>
        /// <param name="searchOption"></param>
        /// <param name="successCallback">A callback for when a file is parsed successfully</param>
        /// <param name="partialSuccessCallback">A callback for when some error happened during/after parsing, but there is still usable data in the response</param>
        /// <param name="errorCallback">A callback for when an error occurred when parsing the file, and there is no usable data</param>
        /// <param name="generateDocumentIdFn">A callback so you can specify a DocumentId for each file that is parsed</param>
        /// <exception cref="SovrenInvalidBatchException">Thrown when the directory provided does not meet the <see cref="BatchParsingRules"/></exception>
        public static async Task ParseJobs(
            SovrenClient apiClient,
            ParseOptions parseOptions,
            BatchParsingRules rules,
            string directory,
            SearchOption searchOption,
            Func<JobBatchSuccessResult, Task> successCallback,
            Func<JobBatchPartialSuccessResult, Task> partialSuccessCallback,
            Func<BatchErrorResult, Task> errorCallback,
            Func<string, string> generateDocumentIdFn = null)
        {
            if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));

            IEnumerable<string> files = GetFiles(rules, directory, searchOption);

            //process the batch serially, since multi-threading could cause the customer to violate the AUP accidentally
            foreach (string file in files)
            {
                Document doc = new Document(file);
                string docId = generateDocumentIdFn == null ? Guid.NewGuid().ToString() : generateDocumentIdFn(file);

                try
                {
                    //set document id if we plan to index these documents
                    if (parseOptions?.IndexingOptions != null)
                        parseOptions.IndexingOptions.DocumentId = docId;

                    ParseRequest request = new ParseRequest(doc, parseOptions);
                    ParseJobResponse response = await apiClient.ParseJob(request);
                    if (successCallback != null)
                    {
                        await successCallback(new JobBatchSuccessResult(file, docId, response));
                    }
                }
                catch (SovrenUsableJobException e)
                {
                    //this happens when something wasn't 100% successful, but there still might be usable data
                    if (partialSuccessCallback != null)
                    {
                        await partialSuccessCallback(new JobBatchPartialSuccessResult(file, docId, e));
                    }
                }
                catch (SovrenException e)
                {
                    if (errorCallback != null)
                    {
                        //this happens where there is no usable data
                        await errorCallback(new BatchErrorResult(file, docId, e));
                    }
                }
            }
        }

        private static IEnumerable<string> GetFiles(BatchParsingRules rules, string directory, SearchOption searchOption)
        {
            if (string.IsNullOrWhiteSpace(directory)) throw new ArgumentNullException(nameof(directory));
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            //use the rules to determine if this batch of files is valid (and which files)
            IEnumerable<string> files = Directory.EnumerateFiles(directory, "*", searchOption)
                .Where(f => rules.FileIsAllowed(f));

            if (files == null || files.Count() == 0) throw new SovrenInvalidBatchException("No files found in given directory");

            if (files.Count() > rules.MaxBatchSize)
            {
                throw new SovrenInvalidBatchException("This batch is too large to process.");
            }

            return files;
        }
    }
}
