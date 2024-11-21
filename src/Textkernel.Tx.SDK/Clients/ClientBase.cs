using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// A base class for all common logic in TxClient
    /// </summary>
    internal class ClientBase
    {
        internal readonly HttpClient _httpClient;

        internal ClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static async Task<T> DeserializeBody<T>(HttpResponseMessage response)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }
            catch
            {
                //non-200 responses will not have a body that we can deserialize, so swallow that error
                //but for a 200 response we should always be able to deserialize
                if (response.IsSuccessStatusCode) throw;

                return default(T);
            }
        }

        internal static async Task<string> GetBodyIfDebug(HttpRequestMessage request)
        {
            if (TxClient.ShowFullRequestBodyInExceptions)
            {
                return await request.Content?.ReadAsStringAsync();
            }

            return null;
        }

        internal static async Task<T> ProcessResponse<T>(HttpResponseMessage response, HttpRequestMessage request) where T : ITxResponse
        {
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
            {
                throw new TxException(await GetBodyIfDebug(request), response, new ApiResponseInfoLite { Code = "Error", Message = "Request body was too large." }, null);
            }

            T data = await DeserializeBody<T>(response);

            if (response != null && data == null)
            {
                //this happens when its a non-Tx 404 or a 500-level error
                string message = $"{(int)response.StatusCode} - {response.ReasonPhrase}";
                throw new TxException(await GetBodyIfDebug(request), response, new ApiResponseInfoLite { Code = "Error", Message = message }, null);
            }

            if (response == null || data == null)
            {
                //this should really never happen, but just in case...
                throw new TxException(await GetBodyIfDebug(request), response, new ApiResponseInfoLite { Code = "Error", Message = "Unknown API error." }, null);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new TxException(await GetBodyIfDebug(request), response, data.Info);
            }

            return data;
            //TODO: much more error handling here?
        }
    }

    internal static class HttpRequestExtensions
    {
        internal static void AddJsonBody<T>(this HttpRequestMessage request, T requestBody)
        {
            string json = JsonSerializer.Serialize(requestBody, TxJsonSerialization.DefaultOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
