// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace Sovren.Rest
{
    internal class RestClient
    {
        public string BaseUrl { get; private set; }
        public Dictionary<string, string> Headers { get; private set; } = new Dictionary<string, string>();
        public CookieContainer CookieContainer { get; set; }//leave as null by default
        public IWebProxy Proxy { get; set; }//leave as null by default

        private static readonly string _sdkVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public RestClient(string baseAddr)
        {
            BaseUrl = baseAddr;
        }

        private static void SetHeader(HttpWebRequest webRequest, string headerName, string headerValue)
        {
            if (WebHeaderCollection.IsRestricted(headerName))
            {
                switch (headerName.ToLower())
                {
                    case "accept":
                        webRequest.Accept = headerValue; break;
                    case "connection":
                        webRequest.Connection = headerValue; break;
                    case "content-length":
                        webRequest.ContentLength = long.Parse(headerValue); break;
                    case "content-type":
                        webRequest.ContentType = headerValue; break;
                    //case "date":
                    //    webRequest.Date = DateTime.Parse(headerValue); break;
                    case "expect":
                        webRequest.Expect = headerValue; break;
                    case "host":
                        webRequest.Host = headerValue; break;
                    //case "if-modified-since":
                    //    webRequest.IfModifiedSince = DateTime.Parse(headerValue); break;
                    //case "range":
                    //    webRequest.AddRange(int.Parse(headerValue)); break;
                    case "referer":
                        webRequest.Referer = headerValue; break;
                    case "transfer-encoding":
                        webRequest.TransferEncoding = headerValue; break;
                    case "user-agent":
                        webRequest.UserAgent = headerValue; break;
                    //case "proxy-connection":
                    //    webRequest.Proxy = //headerValue; break;
                    default:
                        throw new NotImplementedException("Header is restricted but the handler is not implemented.");
                }
            }
            else
            {
                webRequest.Headers[headerName] = headerValue;
            }
        }

        private HttpWebRequest CreateWebRequest(RestRequest request)
        {
            string fullUrl = BaseUrl + request.Endpoint + request.GetQueryString();

            if (!string.IsNullOrEmpty(BaseUrl) && 
                !string.IsNullOrEmpty(request.Endpoint) &&
                fullUrl[BaseUrl.Length - 1] != '/' &&
                fullUrl[BaseUrl.Length] != '/')
            {
                //auto-add the dividing /
                fullUrl = fullUrl.Insert(BaseUrl.Length, "/");
            }

            //create a system level request with the correct method/headers
            HttpWebRequest webRequest = HttpWebRequest.CreateHttp(fullUrl);
            webRequest.Method = request.Method.ToString();
            webRequest.Proxy = this.Proxy;

            foreach (string headerName in request.Headers.Keys)
            {
                SetHeader(webRequest, headerName, request.Headers[headerName]);
            }

            if (CookieContainer != null)
            {
                //as long as you pass this with each request, it will keep track of any cookies in the response
                webRequest.CookieContainer = CookieContainer;
            }

            //override/set any permanent headers from this client
            foreach (string headerName in Headers.Keys)
            {
                if (!string.IsNullOrEmpty(Headers[headerName]))
                {
                    SetHeader(webRequest, headerName, Headers[headerName]);
                }
            }

            webRequest.UserAgent = $"sovren-dotnet-{_sdkVersion}";

            //add the body in the requested encoding
            string body = request.GetBody();
            byte[] bodyBytes = request.Encoding.GetBytes(body);
            webRequest.ContentLength = bodyBytes.Length;

            if (!string.IsNullOrEmpty(body))
            {
                using (Stream s = webRequest.GetRequestStream())
                {
                    s.Write(bodyBytes, 0, bodyBytes.Length);
                    s.Flush();
                    s.Close();
                }
            }

            return webRequest;
        }

        public RestResponse Execute(RestRequest request)
        {
            HttpWebRequest webRequest = CreateWebRequest(request);
            return GetResponse(webRequest);
        }

        public RestResponse<T> Execute<T>(RestRequest request)
        {
            HttpWebRequest webRequest = CreateWebRequest(request);
            RestResponse response = GetResponse(webRequest);
            return new RestResponse<T>(response);
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            HttpWebRequest webRequest = CreateWebRequest(request);
            return await GetResponseAsync(webRequest);
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            HttpWebRequest webRequest = CreateWebRequest(request);
            RestResponse response = await GetResponseAsync(webRequest);
            return new RestResponse<T>(response);
        }

        private static RestResponse GetResponse(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse webResponse = GetWebResponse(request))
                {
                    return CreateNormalResponse(webResponse);
                }
            }
            catch (Exception e)
            {
                return CreateErrorResponse(e);
            }
        }

        private static async Task<RestResponse> GetResponseAsync(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse webResponse = await GetWebResponseAsync(request))
                {
                    return CreateNormalResponse(webResponse);
                }
            }
            catch (Exception e)
            {
                return CreateErrorResponse(e);
            }
        }

        private static HttpWebResponse GetWebResponse(HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                //handle stuff like 400 or 500 level errors where there could be an actual response body
                if (e.Response is HttpWebResponse)
                {
                    return e.Response as HttpWebResponse;
                }

                throw;
            }
        }

        private static async Task<HttpWebResponse> GetWebResponseAsync(HttpWebRequest request)
        {
            try
            {

                //convert from APM (asynchronous programming model) to TAP (task-based asynchronous pattern)
                //http://msdn.microsoft.com/en-us/library/hh873178%28v=vs.110%29.aspx#ApmToTap
                Task<WebResponse> asyncTask = Task.Factory.FromAsync(
                    (asyncCallback, state) => request.BeginGetResponse(asyncCallback, state),
                    (asyncResult) => request.EndGetResponse(asyncResult), null);

                return (HttpWebResponse)(await asyncTask);
            }
            catch (WebException e)
            {
                //handle stuff like 400 or 500 level errors where there could be an actual response body
                if (e.Response is HttpWebResponse)
                {
                    return e.Response as HttpWebResponse;
                }

                throw;
            }
        }

        private static RestResponse CreateNormalResponse(HttpWebResponse webResponse)
        {
            string content;
            Encoding encodingToUse = Encoding.UTF8;

            if (!string.IsNullOrEmpty(webResponse.ContentEncoding))
            {
                encodingToUse = Encoding.GetEncoding(webResponse.ContentEncoding);
            }
            else if (!string.IsNullOrEmpty(webResponse.Headers[HttpResponseHeader.ContentType]))
            {
                encodingToUse = RestContentTypes.ParseContentTypeHeader(webResponse.Headers[HttpResponseHeader.ContentType]).Encoding;
            }


            using (Stream s = webResponse.GetResponseStream())
            using (StreamReader sr = new StreamReader(s, encodingToUse))
            {
                content = sr.ReadToEnd();
            }

            RestResponse response = new RestResponse(
                webResponse.StatusCode,
                webResponse.StatusDescription,
                webResponse.Headers,
                content
            );

            webResponse.Close();
            return response;
        }

        private static RestResponse CreateErrorResponse(Exception e)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string description = e.Message;

            if (e is WebException exception && exception.Status == WebExceptionStatus.Timeout)
            {
                code = HttpStatusCode.RequestTimeout;
                description = exception.Message;
            }

            return new RestResponse(code, description);
        }
    }
}
