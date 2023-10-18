// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Sovren.Batches
{
    /// <summary>
    /// Rules to limit invalid parse transactions (and reduce parsing costs).
    /// </summary>
    public class BatchParsingRules
    {
        /// <summary>
        /// The default file types that will result in invalid parse transactions (and cost unnecessary credits)
        /// </summary>
        public static ReadOnlyCollection<string> DefaultDisallowedFileTypes = new ReadOnlyCollection<string>(new List<string>()
        {
            //images
            "png",
            "jpg",
            "jpeg",
            "bmp",
            "tiff",
            "gif",
            "webp",
            "psd",
            "raw",
            //binaries
            "exe",
            "dll",
            "deb",
            "app",
            //archives
            "zip",
            "tar",
            "gz",
            "7z",
            //structured
            "json",
            "xml",
            "csv"
        });

        /// <summary>
        /// The maximum amount of files allowed in a batch parse. If a directory contains more valid files, an error is thrown.
        /// This is important to keep users from unknowingly consuming large numbers of parsing credits.
        /// </summary>
        public int MaxBatchSize { get; set; }

        /// <summary>
        /// File types to skip.
        /// </summary>
        public IEnumerable<string> DisallowedFileTypes { get; protected set; }

        /// <summary>
        /// ANY value in here will mean the 'DisallowedFileTypes' property is ignored and only types in this list are allowed
        /// </summary>
        public IEnumerable<string> AllowedFileTypes { get; protected set; }

        /// <summary>
        /// A custom function to decide whether or not a file should be parsed. It should return <see langword="true"/> to parse the file.
        /// </summary>
        public Func<string, bool> ShouldProcessFile { get; protected set; }

        /// <summary>
        /// Create a set of rules to limit invalid parse transactions (and reduce parsing costs).
        /// </summary>
        /// <param name="maxBatchSize">
        /// The maximum amount of files allowed in a batch parse. If a directory contains more valid files, an error is thrown.
        /// This is important to keep users from unknowingly consuming large numbers of parsing credits.
        /// </param>
        /// <param name="disallowedFileTypes">
        /// File types to skip. Use the <see cref="DefaultDisallowedFileTypes"/> unless you have a specific use case.
        /// </param>
        /// <param name="allowedFileTypes">
        /// File types to exclusively allow. ANY value in here will mean the <see cref="DisallowedFileTypes"/>
        /// property is ignored and only types in this list are allowed.
        /// </param>
        /// <param name="shouldProcessFn">
        /// A custom function to decide whether or not a file should be parsed. It should return <see langword="true"/> to parse the file.
        /// This could be used, for example, to check if you have already parsed a particular 
        /// file in your system before spending credits to parse it again.
        /// NOTE: If defined, this will be called only AFTER a file passes the other 'file type' checks.
        /// </param>
        public BatchParsingRules(int maxBatchSize,
            IEnumerable<string> disallowedFileTypes = null,
            IEnumerable<string> allowedFileTypes = null,
            Func<string, bool> shouldProcessFn = null)
        {
            MaxBatchSize = maxBatchSize;
            DisallowedFileTypes = disallowedFileTypes ?? DefaultDisallowedFileTypes;
            ShouldProcessFile = shouldProcessFn;
            AllowedFileTypes = allowedFileTypes ?? new List<string>();

            //remove any leading periods
            DisallowedFileTypes = DisallowedFileTypes.Select(s => s.TrimStart('.'));
            AllowedFileTypes = AllowedFileTypes.Select(s => s.TrimStart('.'));
        }

        internal bool FileIsAllowed(string file)
        {
            string fileExt = Path.GetExtension(file);

            if (AllowedFileTypes.Any() && !AllowedFileTypes.Contains(fileExt, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            if (DisallowedFileTypes.Any() && DisallowedFileTypes.Contains(fileExt, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            if (ShouldProcessFile != null)
            {
                return ShouldProcessFile(file);
            }

            return true;
        }
    }
}
