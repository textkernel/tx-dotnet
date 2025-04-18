//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Textkernel.Tx.Models.API.Matching.Request;
//using Textkernel.Tx.Models.API.Matching;
//using Textkernel.Tx.Models.Matching;
//using Textkernel.Tx.Models.API.MatchV2.Request;

//namespace Textkernel.Tx.SDK.Tests.IntegrationTests
//{
//    public class MatchV2Tests : TestBase
//    {
//        private const string _documentId = "unittestci";

//        [OneTimeSetUp]
//        public async Task SetupAIMatchingIndexes()
//        {
//            // add a document to each index
//            await Client.MatchV2.AddVacancy(_documentId, TestParsedJobTech);
//            await Client.MatchV2.AddCandidate(_documentId, TestParsedResume);
//            await DelayForIndexSync(10_000);
//        }

//        [OneTimeTearDown]
//        public async Task TeardownMatchingIndexes()
//        {
//            await Client.MatchV2.DeleteVacancies([_documentId]);
//            await Client.MatchV2.DeleteCandidates([_documentId]);
//            await DelayForIndexSync(10_000);
//        }

//        [TestCase("Developer")]
//        [TestCase("VB6")]
//        public async Task TestSearch(string validSearchTerm)
//        {
//            Assert.ThrowsAsync<TxException>(async () =>
//            {
//                await Client.MatchV2.SearchCandidates(null, null);
//            });

//            SearchQuery query = new SearchQuery();
//            Assert.ThrowsAsync<TxException>(async () =>
//            {
//                await Client.MatchV2.SearchCandidates(query, null);
//            });

//            Options opts = new Options();
//            query.QueryString = validSearchTerm;
//            Assert.DoesNotThrow(() =>
//            {
//                var response = Client.MatchV2.SearchCandidates(query, opts).Result.Value;
//                Assert.NotZero(response.MatchSize);
//            });

//            query.QueryString = "ThisIsATermThatIsntInTheDocument";
//            Assert.DoesNotThrow(() =>
//            {
//                var response = Client.MatchV2.SearchCandidates(query, opts).Result.Value;
//                Assert.Zero(response.MatchSize);
//            });

//            await Task.CompletedTask;
//        }

//        [Test]
//        public async Task TestMatch()
//        {
//            Assert.ThrowsAsync<TxException>(async () =>
//            {
//                await Client.MatchV2.MatchVacancies(null, null);
//            });

//            Options opts = new Options();
//            Assert.ThrowsAsync<TxException>(async () =>
//            {
//                await Client.MatchV2.MatchVacancies(null, opts);
//            });

//            Assert.ThrowsAsync<TxException>(async () =>
//            {
//                await Client.MatchV2.MatchVacancies("fake-doc-id", opts);
//            });

//            opts.DocumentType = DocumentType.vacancy;
//            Assert.DoesNotThrow(() =>
//            {
//                var response = Client.MatchV2.MatchVacancies(_documentId, opts).Result.Value;
//                Assert.NotZero(response.MatchSize);
//            });

//            opts.DocumentType = DocumentType.vacancy;
//            Assert.DoesNotThrow(() =>
//            {
//                var response = Client.MatchV2.MatchCandidates(_documentId, opts).Result.Value;
//                Assert.NotZero(response.MatchSize);
//            });

//            await Task.CompletedTask;
//        }
//    }
//}
