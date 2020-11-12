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

            Assert.Throws<IOException>(() =>
            {
                new Models.Document("c:\thisisnotarealpath");
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
