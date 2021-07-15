// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.IO;

namespace Sovren.Models
{
    /// <summary>
    /// Represents an unparsed document (a file on the filesystem, byte[] from a database, etc)
    /// </summary>
    public class Document
    {
        /// <summary>
        /// The base-64 encoded byte[] for this file. This is what is sent over HTTPS to Sovren's API
        /// </summary>
        public string AsBase64 { get; protected set; }

        /// <summary>
        /// The last revised/modified date for this file.
        /// </summary>
        public DateTime LastModified { get; protected set; }

        private static readonly DateTime _epoch = new DateTime(1970, 1, 1);

        /// <summary>
        /// Create a <see cref="Document"/> from a file byte[]
        /// </summary>
        /// <param name="fileBytes">The file byte array</param>
        /// <param name="lastModified">
        /// The last-revised date for this file.
        /// <br/>Per our AUP (<see href="https://sovren.com/policies-and-agreements/acceptable-use-policy/"/>), you
        /// MUST pass a good-faith last-revised date for every parse transaction.
        /// <br/>This is extremely important so that the Parser knows how to interpret dates in the document that 
        /// are expressed as "current" or "as of" (or similar) to correctly calculate date spans
        /// </param>
        public Document(byte[] fileBytes, DateTime lastModified)
        {
            if (fileBytes == null) throw new ArgumentNullException(nameof(fileBytes));
            if (fileBytes.Length == 0) throw new ArgumentException("byte[] cannot be empty", nameof(fileBytes));
            AsBase64 = Convert.ToBase64String(fileBytes);
            LastModified = lastModified;

            if (LastModified == DateTime.MinValue || LastModified == DateTime.MaxValue || LastModified == _epoch)
            {
                throw new ArgumentException("You must provide a valid last-modified date so that parser metadata is accurate", nameof(lastModified));
            }
            //TODO: no more than 10 yrs back and 45? days forward, if so, this should be done in the API, not SDK
        }

        /// <summary>
        /// Create a <see cref="Document"/> from a file on the filesystem.
        /// <br/>NOTE: this will automatically set the <see cref="LastModified"/> using <see cref="File.GetLastWriteTime(string)"/>.
        /// <br/><b>If your files do not have an accurate 'LastWrite' or 'LastModified' time, you must use a different constructor</b>
        /// </summary>
        public Document(string path)
            : this(File.ReadAllBytes(path), File.GetLastWriteTime(path))
        {
        }
    }
}
