// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sovren.SDK.Tests.UnitTests
{
    public class DocumentTests
    {
        [Test]
        public void TestDocumentConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Models.Document(null);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document("");
            });

            Assert.Throws<FileNotFoundException>(() =>
            {
                new Models.Document("notarealfile.docx");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Models.Document(null, DateTime.Now);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document(new byte[0], DateTime.MinValue);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document(new byte[0], new DateTime(1970, 1, 1));
            });
        }
    }
}
