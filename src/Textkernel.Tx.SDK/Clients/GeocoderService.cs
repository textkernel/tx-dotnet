// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models;


namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.Geocoder"/>
    /// </summary>
    internal class GeocoderService : ServiceBase, IGeocoderService
    {
        internal GeocoderService(HttpClient httpClient) : base(httpClient) { }

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

        #region GeocodeAndIndex


        private async Task<GeocodeAndIndexResumeResponse> InternalGeocodeAndIndex(ParsedResume resume, GeocodeCredentials geocodeCredentials, IndexingOptionsGeneric indexingOptions, bool indexIfGeocodeFails, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeAndIndexResumeRequest requestBody = new GeocodeAndIndexResumeRequest
            {
                ResumeData = resume,
                GeocodeOptions = new GeocodeOptionsBase
                {
                    Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                    ProviderKey = geocodeCredentials?.ProviderKey,
                    PostalAddress = address,
                    GeoCoordinates = coordinates
                },
                IndexingOptions = indexingOptions,
                IndexIfGeocodeFails = indexIfGeocodeFails
            };

            HttpRequestMessage apiRequest = ApiEndpoints.GeocodeAndIndexResume();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<GeocodeAndIndexResumeResponse>(response, apiRequest);

            if (!requestBody.IndexIfGeocodeFails && data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.GeocodeResponse, data.Info.TransactionId);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.IndexingResponse, data.Info.TransactionId);
            }

            return data;
        }

        private async Task<GeocodeAndIndexJobResponse> InternalGeocodeAndIndex(ParsedJob job, GeocodeCredentials geocodeCredentials, IndexingOptionsGeneric indexingOptions, bool indexIfGeocodeFails, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeAndIndexJobRequest requestBody = new GeocodeAndIndexJobRequest
            {
                JobData = job,
                GeocodeOptions = new GeocodeOptionsBase
                {
                    Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                    ProviderKey = geocodeCredentials?.ProviderKey,
                    PostalAddress = address,
                    GeoCoordinates = coordinates
                },
                IndexingOptions = indexingOptions,
                IndexIfGeocodeFails = indexIfGeocodeFails
            };

            HttpRequestMessage apiRequest = ApiEndpoints.GeocodeAndIndexJob();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<GeocodeAndIndexJobResponse>(response, apiRequest);

            if (!requestBody.IndexIfGeocodeFails && data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.GeocodeResponse, data.Info.TransactionId);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.IndexingResponse, data.Info.TransactionId);
            }

            return data;
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed resume.
        /// The coordinates will be inserted into your parsed resume, and the address included in the 
        /// parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates AND a known address and want to add/override them in your parsed resume.
        /// The coordinates will be inserted into your parsed resume, and the address in the 
        /// parsed resume will not be set/modified with what you specify.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="address">The address to set/override in the parsed resume prior to indexing</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates, address: address);
        }

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed job.
        /// The coordinates will be inserted into your parsed job, and the address included in the 
        /// parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates AND a known address and want to add/override them in your parsed job.
        /// The coordinates will be inserted into your parsed job, and the address in the 
        /// parsed job will not be set/modified with what you specify.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="address">The address to set/override in the parsed job prior to indexing</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates, address: address);
        }

        #endregion
    }
}
