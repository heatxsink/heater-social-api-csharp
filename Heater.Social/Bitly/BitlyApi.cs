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

using System.Web;
using System.Net;
using Heater.Social.Bitly.DataContracts;
using Jayrock.Json.Conversion;

namespace Heater.Social
{
    public class BitlyApi : AbstractApiBase
    {
        private static string API_DOMAIN = "api.bit.ly";

        public string ApiKey
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        public BitlyApi(string login, string apiKey)
        {
            Login = login;
            ApiKey = apiKey;
            ApiDomain = API_DOMAIN;
            Timeout = DEFAULT_TIMEOUT;
        }
        
        public ShortenResponse Shorten(string uri)
        {
            string shortenUrlFormat = "http://api.bit.ly/v3/shorten?login={0}&apiKey={1}&uri={2}&format={3}";
            string shortenUrl = string.Format(shortenUrlFormat, Login, ApiKey, HttpUtility.UrlEncode(uri), "json");
            var response = HttpGet(shortenUrl);
            var data = (ShortenResponse)JsonConvert.Import(typeof(ShortenResponse), response);
            return data;
        }
    }
}
