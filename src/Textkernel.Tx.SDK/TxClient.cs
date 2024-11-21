// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Clients;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Account;
using Textkernel.Tx.Models.API.DataEnrichment;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.JobDescription;

namespace Textkernel.Tx
{
    /// <summary>
    /// Settings for a TxClient (used when configuring the TxClient with dependency injection)
    /// </summary>
    public class TxClientSettings
    {
        /// <summary>
        /// The Account ID for your account
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// The Service Key for your account
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// The Data Center for your account. Either <see cref="DataCenter.US"/>, <see cref="DataCenter.EU"/>, or <see cref="DataCenter.AU"/>
        /// </summary>
        public DataCenter DataCenter { get; set; }
        /// <summary>
        /// Optional tags to use to track API usage for your account
        /// </summary>
        public IEnumerable<string> TrackingTags { get; set; }
    }

    //public static class TxClientExtensions
    //{
    //    public static IServiceCollection AddTxClient(this IServiceCollection services, Action<TxClientSettings> setupAction)
    //    {
    //        services.AddOptions<TxClientSettings>().Configure(setupAction);
    //        services.AddHttpClient<ITxClient, TxClient>();
    //        return services;
    //    }
    //}

    /// <summary>
    /// The SDK client to perform Tx API calls.
    /// </summary>
    public class TxClient : ITxClient, IDisposable
    {
        /// <summary>
        /// Disposes this object and the contained HttpClient
        /// </summary>
        public void Dispose() => _httpClient?.Dispose();

        internal readonly HttpClient _httpClient;

        /// <summary>
        /// Set to <see langword="true"/> for debugging API errors. It will show the full JSON request body in <see cref="TxException.RequestBody"/>
        /// <br/><b>NOTE: do not set this to <see langword="true"/> in your production system, as it increases the memory footprint</b>
        /// </summary>
        public static bool ShowFullRequestBodyInExceptions { get; set; }

        /// <summary>
        /// Contains all endpoints/methods for the Resume Formatter
        /// </summary>
        public IFormatterClient Formatter { get; private set; }

        /// <summary>
        /// Contains all endpoints/methods for the Resume &amp; Job Parsers
        /// </summary>
        public IParserClient Parser { get; private set; }

        /// <summary>
        /// Contains all endpoints/methods for the Geocoder
        /// </summary>
        public IGeocoderClient Geocoder { get; private set; }

        /// <summary>
        /// Contains all endpoints/methods for Search &amp; Match
        /// </summary>
        public ISearchMatchClient SearchMatch { get; private set; }

        /// <summary>
        /// Contains all endpoints/methods for Skills Intelligence
        /// </summary>
        public ISkillsIntelligenceClient SkillsIntelligence { get; private set; }

        /// <summary>
        /// Contains all endpoints/methods for Match V2
        /// </summary>
        public IMatchV2Client MatchV2 { get; set; }

        private static readonly string _sdkVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <param name="accountId">The account id for your account</param>
        /// <param name="serviceKey">The service key for your account</param>
        /// <param name="dataCenter">The Data Center for your account. Either <see cref="DataCenter.US"/>, <see cref="DataCenter.EU"/>, or <see cref="DataCenter.AU"/></param>
        /// <param name="trackingTags">Optional tags to use to track API usage for your account</param>
        /// <remarks>
        /// IMPORTANT: if you are using DI or would like to pass in your own HttpClient, use <see cref="TxClient(HttpClient, TxClientSettings)"/>
        /// </remarks>
        public TxClient(string accountId, string serviceKey, DataCenter dataCenter, IEnumerable<string> trackingTags = null)
            : this(accountId, serviceKey, dataCenter, trackingTags, null)
        { }

        /// <summary>
        /// This constructor allows the user to specify the HttpClient to use. For best practices,
        /// see <see href="https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines">here</see>.
        /// <br/>Here is an example of how to inject a TxClient with DI:
        /// <code>
        /// var builder = WebApplication.CreateBuilder(args);
        /// builder.Services.AddSingleton(_ => new TxClientSettings
        /// {
        ///     AccountId = "12345678",
        ///     ...
        /// });
        /// //requires Microsoft.Extensions.Http package
        /// builder.Services.AddHttpClient&lt;ITxClient, TxClient&gt;();
        /// 
        /// //now you can retrieve your injected client from the service provider
        /// TxClient client = builder.Services.GetRequiredService&lt;TxClient&gt;();
        /// </code>
        /// </summary>
        /// <param name="httpClient">The HttpClient to use</param>
        /// <param name="settings">The settings for this client</param>
        public TxClient(HttpClient httpClient, TxClientSettings settings)
            : this(settings?.AccountId, settings?.ServiceKey, settings?.DataCenter, settings?.TrackingTags, httpClient)
        { }

        private TxClient(string accountId, string serviceKey, DataCenter dataCenter, IEnumerable<string> trackingTags, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            if (string.IsNullOrEmpty(serviceKey))
                throw new ArgumentNullException(nameof(serviceKey));

            _httpClient = httpClient ?? new HttpClient();

            //do not validate credentials here, as this could lead to calling GetAccount for every parse call, an AUP violation
            _httpClient.BaseAddress = new Uri(dataCenter.Url + (dataCenter.Url.EndsWith("/") ? "" : "/"));
            _httpClient.DefaultRequestHeaders.Add("Tx-AccountId", accountId);
            _httpClient.DefaultRequestHeaders.Add("Tx-ServiceKey", serviceKey);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"tx-dotnet-{_sdkVersion}");

            if (trackingTags?.Any() ?? false)
            {
                string tagsHeaderValue = string.Join(", ", trackingTags);
                if (tagsHeaderValue.Length >= 75)//API allows 100, but just to be safe, this should be way more than enough
                {
                    throw new ArgumentException("Too many values or values are too long", nameof(trackingTags));
                }

                _httpClient.DefaultRequestHeaders.Add("Tx-TrackingTag", tagsHeaderValue);
            }

            Formatter = new FormatterClient(_httpClient);
            Parser = new ParserClient(_httpClient);
            Geocoder = new GeocoderClient(_httpClient);
            SearchMatch = new SearchMatchClient(_httpClient);
            SkillsIntelligence = new SkillsIntelligenceClient(_httpClient);
            MatchV2 = new MatchV2Client(_httpClient);
        }

        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<GetAccountInfoResponse> GetAccountInfo()
        {
            HttpRequestMessage apiRequest = ApiEndpoints.GetAccountInfo();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ClientBase.ProcessResponse<GetAccountInfoResponse>(response, apiRequest);
        }
    }
}
