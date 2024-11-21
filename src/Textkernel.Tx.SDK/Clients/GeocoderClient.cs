using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;


namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.Geocoder"/>
    /// </summary>
    internal class GeocoderClient : ClientBase, IGeocoderClient
    {
        internal GeocoderClient(HttpClient httpClient) : base(httpClient) { }

        private async Task<GeocodeResumeResponse> InternalGeocode(ParsedResume resume, GeocodeCredentials geocodeCredentials, Address address = null)
        {
            GeocodeResumeRequest requestBody = new GeocodeResumeRequest
            {
                ResumeData = resume,
                Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                ProviderKey = geocodeCredentials?.ProviderKey,
                PostalAddress = address
            };

            HttpRequestMessage apiRequest = ApiEndpoints.GeocodeResume();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GeocodeResumeResponse>(response, apiRequest);
        }

        private async Task<GeocodeJobResponse> InternalGeocode(ParsedJob job, GeocodeCredentials geocodeCredentials, Address address = null)
        {
            GeocodeJobRequest requestBody = new GeocodeJobRequest
            {
                JobData = job,
                Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                ProviderKey = geocodeCredentials?.ProviderKey,
                PostalAddress = address
            };

            HttpRequestMessage apiRequest = ApiEndpoints.GeocodeJob();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GeocodeJobResponse>(response, apiRequest);
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeResumeResponse> Geocode(ParsedResume resume, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(resume, geocodeCredentials);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeResumeResponse> Geocode(ParsedResume resume, Address address, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(resume, geocodeCredentials, address: address);
        }

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeJobResponse> Geocode(ParsedJob job, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(job, geocodeCredentials);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeJobResponse> Geocode(ParsedJob job, Address address, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(job, geocodeCredentials, address: address);
        }

    }
}
