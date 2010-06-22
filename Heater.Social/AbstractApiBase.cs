// The MIT License
// 
// Copyright (c) 2010 Nicholas A. Granado
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to 
// deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

namespace Heater.Social
{
    public abstract class AbstractApiBase
    {
        public int DEFAULT_TIMEOUT = 30000;

        public string ApiDomain { get; set; }
        
        public string ConsumerKey { get; set; }
        
        public string ConsumerSecret { get; set; }
        
        public NetworkCredential Credentials { get; set; }

        public int Timeout { get; set; }

        public IWebProxy Proxy { get; set; }

        private string proxy_url = string.Empty;

        public string ProxyUrl
        {
            get
            {
                return proxy_url;
            }
            set
            {
                proxy_url = value;

                WebProxy proxy = new WebProxy();
                Uri uri = new Uri(value);
                proxy.Address = uri;
                Proxy = proxy;
            }
        }

        public string Diagnostics()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("consumer key:    {0}\n", ConsumerKey);
            builder.AppendFormat("consumer secret: {0}\n", ConsumerSecret);
            builder.AppendFormat("api domain:      {0}\n", ApiDomain);
            
            if (Proxy != null)
            {
                builder.AppendFormat("proxy uri:       {0}\n", ProxyUrl);
            }

            return builder.ToString();
        }

        protected string HttpGet(string resourcePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "GET";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            var responseBody = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected string HttpGet(string resourcePath, CookieContainer cookies)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "GET";
            request.KeepAlive = true;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            request.CookieContainer = cookies;
            var response = (HttpWebResponse)request.GetResponse();
            var responseBody = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected string HttpGet(string resourcePath, NetworkCredential credentials)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Credentials = credentials;
            request.Method = "GET";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            var responseBody = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected Stream HttpGetStream(string resourcePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "GET";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        protected Stream HttpGetStream(string resourcePath, NetworkCredential credentials)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Credentials = credentials;
            request.Method = "GET";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        protected string HttpPost(string resourcePath, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            using (var receiveStream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected string HttpPost(string resourcePath, byte[] requestBody, NetworkCredential credentials)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Credentials = credentials;
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            using (var receiveStream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected string HttpPost(string resourcePath, NetworkCredential credentials, Dictionary<string, string> headers, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Credentials = credentials;
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            foreach (string key in headers.Keys)
            {
                request.Headers.Add(key, headers[key]);
            }

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            using (var receiveStream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected Stream HttpPostStream(string resourcePath, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        protected Stream HttpPostStream(string resourcePath, NetworkCredential credentials, Dictionary<string, string> headers, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "POST";
            request.Credentials = credentials;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            foreach (string key in headers.Keys)
            {
                request.Headers.Add(key, headers[key]);
            }

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        protected string HttpPut(string resourcePath, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            using (var receiveStream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected Stream HttpPutStream(string resourcePath, byte[] requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
            }
            var responseBody = string.Empty;
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        protected string HttpDelete(string resourcePath)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Method = "DELETE";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            var responseBody = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }

        protected string HttpDelete(string resourcePath, NetworkCredential credentials)
        {
            var request = (HttpWebRequest)WebRequest.Create(resourcePath);
            request.Credentials = credentials;
            request.Method = "DELETE";
            request.Timeout = this.Timeout;
            request.Proxy = this.Proxy;
            var response = (HttpWebResponse)request.GetResponse();
            var responseBody = string.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            return responseBody;
        }
    }
}
