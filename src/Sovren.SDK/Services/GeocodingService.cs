// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using Sovren.Models.API.Geocoding;
using Sovren.Models.Job;
using Sovren.Models.Resume;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <inheritdoc/>
    public class GeocodingService : SovrenService
    {
        /// <summary>
        /// The credentials used for geocoding
        /// </summary>
        public GeocodeCredentials GeocodingCredentials { get; set; }

        /// <summary>
        /// Create a service to add geocoordinates into <see cref="ParsedResume"/> or <see cref="ParsedJob"/> objects.
        /// These coordinates are necessary for distance/location/radius filtering in the AI Searching/Matching engine.
        /// </summary>
        /// <param name="client">The SovrenClient that will make the low-level API calls</param>
        /// <param name="geocodeCreds">The credentials used for geocoding, these can be changed on-the-fly via <see cref="GeocodingCredentials"/></param>
        public GeocodingService(SovrenClient client, GeocodeCredentials geocodeCreds)
            : base(client)
        {
            GeocodingCredentials = geocodeCreds ?? new GeocodeCredentials { Provider = GeocodeProvider.Google };
        }

        private async Task<ParsedResume> InternalGeocode(ParsedResume resume, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeResumeRequest request = new GeocodeResumeRequest
            {
                ResumeData = resume,
                Provider = GeocodingCredentials.Provider,
                ProviderKey = GeocodingCredentials.ProviderKey,
                PostalAddress = address,
                GeoCoordinates = coordinates
            };

            GeocodeResumeResponse response = await Client.Geocode(request);
            return response.Value?.ResumeData;
        }

        private async Task<ParsedJob> InternalGeocode(ParsedJob job, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeJobRequest request = new GeocodeJobRequest
            {
                JobData = job,
                Provider = GeocodingCredentials.Provider,
                ProviderKey = GeocodingCredentials.ProviderKey,
                PostalAddress = address,
                GeoCoordinates = coordinates
            };

            GeocodeJobResponse response = await Client.Geocode(request);
            return response.Value?.JobData;
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <returns>The <see cref="ParsedResume"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedResume> Geocode(ParsedResume resume)
        {
            return await InternalGeocode(resume);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <returns>The <see cref="ParsedResume"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedResume> Geocode(ParsedResume resume, Address address)
        {
            return await InternalGeocode(resume, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed resume.
        /// The coordinates will be inserted into your parsed resume, and the address included in the 
        /// parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates into</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <returns>The <see cref="ParsedResume"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedResume> Geocode(ParsedResume resume, GeoCoordinates coordinates)
        {
            return await InternalGeocode(resume, coordinates: coordinates);
        }

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <returns>The <see cref="ParsedJob"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedJob> Geocode(ParsedJob job)
        {
            return await InternalGeocode(job);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <returns>The <see cref="ParsedJob"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedJob> Geocode(ParsedJob job, Address address)
        {
            return await InternalGeocode(job, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed job.
        /// The coordinates will be inserted into your parsed job, and the address included in the 
        /// parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates into</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <returns>The <see cref="ParsedJob"/> with geocoordinates added</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<ParsedJob> Geocode(ParsedJob job, GeoCoordinates coordinates)
        {
            return await InternalGeocode(job, coordinates: coordinates);
        }
    }
}
