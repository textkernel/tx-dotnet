﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.Parser"/>
    /// </summary>
    internal class ParserService : ServiceBase, IParserService
    {
        internal ParserService(HttpClient httpClient, EnvironmentSettings settings)
            : base(httpClient, settings)
        {
        }

        /// <inheritdoc />
        public async Task<ParseResumeResponse> ParseResume(ParseRequest request)
        {
            SetEnvironment(request?.IndexingOptions);

            HttpRequestMessage apiRequest = ApiEndpoints.ParseResume();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<ParseResumeResponse>(response, apiRequest);

            if (data.Value.ParsingResponse != null && !data.Value.ParsingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.ParsingResponse, data.Info.TransactionId);
            }

            if (data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxGeocodeResumeException(response, data.Value.GeocodeResponse, data.Info.TransactionId, data);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxIndexResumeException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            if (data.Value.ProfessionNormalizationResponse != null && !data.Value.ProfessionNormalizationResponse.IsSuccess)
            {
                throw new TxProfessionNormalizationResumeException(response, data.Value.ProfessionNormalizationResponse, data.Info.TransactionId, data);
            }

            return data;
        }


        /// <inheritdoc />
        public async Task<ParseJobResponse> ParseJob(ParseRequest request)
        {
            SetEnvironment(request?.IndexingOptions);

            HttpRequestMessage apiRequest = ApiEndpoints.ParseJobOrder();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<ParseJobResponse>(response, apiRequest);

            if (data.Value.ParsingResponse != null && !data.Value.ParsingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.ParsingResponse, data.Info.TransactionId);
            }

            if (data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxGeocodeJobException(response, data.Value.GeocodeResponse, data.Info.TransactionId, data);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxIndexJobException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            if (data.Value.ProfessionNormalizationResponse != null && !data.Value.ProfessionNormalizationResponse.IsSuccess)
            {
                throw new TxProfessionNormalizationJobException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            return data;
        }
    }
}
