// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Text.Json;
using Sovren.Rest;

//use this namespace specifically so that we dont make the intellisense confusing for integrators
namespace Sovren
{
    /// <summary>
    /// A raw REST response from an API request
    /// </summary>
    public class RestResponse
    {
        /// <summary>
        /// The body of the HTTP response
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The HTTP status code returned by the server
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// A short message about the <see cref="StatusCode"/>
        /// </summary>
        public string StatusDescription { get; private set; }

        /// <summary>
        /// HTTP headers in the response
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        internal bool IsSuccessful => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        internal RestResponse(HttpStatusCode code, string description = null, WebHeaderCollection headers = null, string content = null)
        {
            StatusCode = code;
            StatusDescription = description;
            Headers = headers ?? new WebHeaderCollection();
            Content = content;
        }
    }

    internal class RestResponse<T> : RestResponse
    {
        /// <summary>
        /// Be sure to null-check this value, as it will be default(T) any time you get a non-200 response (and possibly other scenarios)
        /// </summary>
        public T Data { get; private set; }

        internal RestResponse(RestResponse copyFrom)
            : base(copyFrom.StatusCode, copyFrom.StatusDescription, copyFrom.Headers, copyFrom.Content)
        {
            if (!string.IsNullOrEmpty(Headers[HttpResponseHeader.ContentType]))
            {
                (string ContentType, Encoding Encoding) typeAndEncoding = RestContentTypes.ParseContentTypeHeader(Headers[HttpResponseHeader.ContentType]);

                if (typeAndEncoding.ContentType == RestContentTypes.Json)
                {
                    try
                    {
                        Data = JsonSerializer.Deserialize<T>(Content);
                    }
                    catch
                    {
                        if (StatusCode == HttpStatusCode.OK)
                        {
                            throw;//rethrow, since this is a json deserialization issue
                        }
                        //otherwise, eat the exception since a non-200 response will not have the expected response body
                    }
                }
                else if (typeAndEncoding.ContentType == RestContentTypes.Xml)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        using (StringReader sr = new StringReader(Content))
                        {
                            Data = (T)serializer.Deserialize(sr);
                        }
                    }
                    catch 
                    {
                        if (StatusCode == HttpStatusCode.OK)
                        {
                            throw;//rethrow, since this is a json deserialization issue
                        }
                        //otherwise, eat the exception since a non-200 response will not have the expected response body
                    }
                }
            }
        }
    }
}
