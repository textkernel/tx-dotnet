// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.Geocoder"/>
    /// </summary>
    public interface IGeocoderClient
    {
        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeResumeResponse> Geocode(ParsedResume resume, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeResumeResponse> Geocode(ParsedResume resume, Address address, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeJobResponse> Geocode(ParsedJob job, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeJobResponse> Geocode(ParsedJob job, Address address, GeocodeCredentials geocodeCredentials = null);

        #region GeocodeAndIndex

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexingOptionsGeneric indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);


        #endregion
    }
}
