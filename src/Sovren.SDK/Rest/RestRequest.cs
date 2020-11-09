// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Sovren.Rest
{
    internal enum RestMethod
    {
        GET,
        POST,
        DELETE,
        PUT,
        PATCH
    }

    internal class RestRequest
    {
        private enum RequestParamsType
        {
            QueryString,
            Body
        }

        private RequestParamsType ParamsType
        {
            get
            {
                if (Method == RestMethod.GET || Method == RestMethod.DELETE)
                {
                    return RequestParamsType.QueryString;
                }

                return RequestParamsType.Body;
            }
        }

        public string Endpoint { get; private set; }
        public RestMethod Method { get; private set; }
        private string _body;
        public Encoding Encoding { get; private set; } = Encoding.UTF8;
        public Dictionary<string, string> Headers { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        public RestRequest(string url, RestMethod method = RestMethod.GET)
        {
            Endpoint = url;
            Method = method;
        }

        public RestRequest(RestMethod method) : this("", method) { }

        public void AddHeader(string name, string value) => Headers[name] = value;

        public void AddJsonBody(string json, Encoding encoding = null)
        {
            if (ParamsType != RequestParamsType.Body)
            {
                throw new NotSupportedException($"{Method} cannot have a body");
            }
            else if (Parameters.Count > 0)
            {
                throw new NotSupportedException("Cannot have a json body and parameters");
            }

            if (encoding != null)
                Encoding = encoding;

            Headers["Content-Type"] = RestContentTypes.GetContentTypeHeader(RestContentTypes.Json, Encoding);
            _body = json;
        }

        public void AddBody(string content, string contentType, Encoding encoding = null)
        {
            if (ParamsType != RequestParamsType.Body)
            {
                throw new NotSupportedException($"{Method} cannot have a body");
            }
            else if (Parameters.Count > 0)
            {
                throw new NotSupportedException("Cannot have a body and parameters");
            }

            if (encoding != null)
                Encoding = encoding;

            Headers["Content-Type"] = RestContentTypes.GetContentTypeHeader(contentType, Encoding);
            _body = content;
        }

        public void AddParameter(string name, object value)
        {
            if (!string.IsNullOrEmpty(_body))
            {
                throw new NotSupportedException("Cannot have a body and parameters");
            }

            if (ParamsType == RequestParamsType.Body)
            {
                Headers["Content-Type"] = RestContentTypes.GetContentTypeHeader(RestContentTypes.FormUrlEncoded, Encoding);
            }

            Parameters[name] = value.ToString();
        }

        public string GetBody()
        {
            if (ParamsType == RequestParamsType.QueryString)
            {
                return "";
            }

            if (!string.IsNullOrEmpty(_body))
            {
                return _body;
            }

            if (Parameters.Count > 0)
            {
                //only x-www-form-urlencoded supported for now
                if (Headers.ContainsKey("Content-Type") && Headers["Content-Type"] != null && 
                    Headers["Content-Type"].StartsWith(RestContentTypes.FormUrlEncoded))
                {
                    return FormatParamsAsQueryString();
                }
                else
                {
                    throw new NotImplementedException($"Only {RestContentTypes.FormUrlEncoded} is supported for now");
                }
            }

            return "";
        }

        public string GetQueryString()
        {
            if (ParamsType == RequestParamsType.Body)
            {
                return "";
            }

            string query = FormatParamsAsQueryString();
            return string.IsNullOrEmpty(query) ? "" : $"?{query}";
        }

        private string FormatParamsAsQueryString()
        {
            string query = "";
            if (Parameters != null && Parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> param in Parameters)
                {
                    if (!string.IsNullOrEmpty(param.Key) && !string.IsNullOrEmpty(param.Value))
                    {
                        query += $"{HttpUtility.UrlEncode(param.Key)}={HttpUtility.UrlEncode(param.Value)}&";
                    }
                }
            }

            return query;
        }
    }
}
