using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.Matching;
using Textkernel.Tx.Models.API.MatchV2.Request;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class MatchV2Tests : TestBase
    {
        private const string _documentId = "unittestci";

        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // add a document to each index
            await Client.SearchMatchV2.AddJob(_documentId, TestParsedJobTech);
            await Client.SearchMatchV2.AddCandidate(_documentId, TestParsedResume);
            await DelayForIndexSync(10_000);
        }

        [OneTimeTearDown]
        public async Task TeardownMatchingIndexes()
        {
            await Client.SearchMatchV2.DeleteJobs([_documentId]);
            await Client.SearchMatchV2.DeleteCandidates([_documentId]);
            await DelayForIndexSync(10_000);
        }

        [TestCase("Developer")]
        [TestCase("VB6")]
        public async Task TestSearch(string validSearchTerm)
        {
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatchV2.SearchCandidates(null, null);
            });

            SearchQuery query = new SearchQuery();
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatchV2.SearchCandidates(query, null);
            });

            Options opts = new Options();
            query.QueryString = validSearchTerm;
            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.SearchCandidates(query, opts).Result.Value;
                Assert.IsNotEmpty(response.ResultItems);
            });

            query.QueryString = "ThisIsATermThatIsntInTheDocument";
            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.SearchCandidates(query, opts).Result.Value;
                Assert.IsNotEmpty(response.ResultItems);
            });

            await Task.CompletedTask;
        }

        //[Test]
        public async Task TestMatch()
        {
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatchV2.MatchJobs(null, null);
            });

            Options opts = new Options();
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatchV2.MatchJobs(null, opts);
            });

            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatchV2.MatchJobs("fake-doc-id", opts);
            });

            opts.DocumentType = DocumentType.vacancy;
            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.MatchJobs(_documentId, opts).Result.Value;
                Assert.IsNotEmpty(response.ResultItems);
            });

            opts.DocumentType = DocumentType.vacancy;
            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.MatchCandidates(_documentId, opts).Result.Value;
                Assert.IsNotEmpty(response.ResultItems);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestAutocomplete()
        {
            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.AutocompleteCandidates(AutocompleteCandidatesField.FullText, "Softwa").Result;
                Assert.IsNotEmpty(response.Value.Return);
            });

            Assert.DoesNotThrow(() =>
            {
                var response = Client.SearchMatchV2.AutocompleteJobs(AutocompleteJobsField.JobTitle, "Softwa").Result;
                Assert.IsNotEmpty(response.Value.Return);
            });

            await Task.CompletedTask;
        }
    }
}
