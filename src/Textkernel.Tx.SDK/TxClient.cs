// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Clients;
using Textkernel.Tx.Models.API.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        /// <summary>
        /// Should certification skills be included when using the Skills Intelligence APIs
        /// </summary>
        public bool SkillsIntelligenceIncludeCertifications { get; set; } = true;

        /// <summary>
        /// The environment to target for any MatchV2 API calls
        /// </summary>
        public MatchV2Environment MatchV2Environment { get; set; } = MatchV2Environment.ACC;
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
        {
            if (string.IsNullOrEmpty(settings?.AccountId))
                throw new ArgumentNullException(nameof(settings.AccountId));

            if (string.IsNullOrEmpty(settings?.ServiceKey))
                throw new ArgumentNullException(nameof(settings.ServiceKey));

            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            if (settings?.DataCenter == null)
                throw new ArgumentNullException(nameof(settings.DataCenter));

            _httpClient = httpClient ?? new HttpClient();

            //do not validate credentials here, as this could lead to calling GetAccount for every parse call, an AUP violation
            _httpClient.BaseAddress = new Uri(settings.DataCenter.Url + (settings.DataCenter.Url.EndsWith("/") ? "" : "/"));
            _httpClient.DefaultRequestHeaders.Add("Tx-AccountId", settings.AccountId);
            _httpClient.DefaultRequestHeaders.Add("Tx-ServiceKey", settings.ServiceKey);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"tx-dotnet-{_sdkVersion}");

            if (settings.TrackingTags?.Any() ?? false)
            {
                string tagsHeaderValue = string.Join(", ", settings.TrackingTags);
                if (tagsHeaderValue.Length >= 75)//API allows 100, but just to be safe, this should be way more than enough
                {
                    throw new ArgumentException("Too many values or values are too long", nameof(settings.TrackingTags));
                }

                _httpClient.DefaultRequestHeaders.Add("Tx-TrackingTag", tagsHeaderValue);
            }

            Formatter = new FormatterClient(_httpClient);
            Parser = new ParserClient(_httpClient);
            Geocoder = new GeocoderClient(_httpClient);
            SearchMatch = new SearchMatchClient(_httpClient);
            SkillsIntelligence = new SkillsIntelligenceClient(_httpClient, settings.SkillsIntelligenceIncludeCertifications);
            MatchV2 = new MatchV2Client(_httpClient, settings.MatchV2Environment);
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
