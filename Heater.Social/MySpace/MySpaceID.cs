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

using System;
using System.Net;
using System.Web;
using System.Text;
using System.Collections.Generic;
using Heater.OAuth;

namespace Heater.Social
{
    public class MySpaceID : AbstractApiBase
    {
        public string MDP_DOMAIN = "api.myspace.com";
        public string FIELDS = "aboutMe,age,books,currentLocation,dateOfBirth,emails,gender,hasApp,heroes,id,interests,movies,music,name,familyName,givenName,networkPresence,profileUrl,smoker,status,thumbnailUrl,tvShows";

        public MySpaceID()
        {
            ApiDomain = MDP_DOMAIN;
            Timeout = DEFAULT_TIMEOUT;
        }
        
        public MySpaceID(string consumerKey, string consumerSecret)
        {
            ApiDomain = MDP_DOMAIN;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = DEFAULT_TIMEOUT;
        }

        public MySpaceID(string apiDomain, string consumerKey, string consumerSecret)
        {
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = DEFAULT_TIMEOUT;
        }

        public MySpaceID(string apiDomain, string consumerKey, string consumerSecret, int timeout)
        { 
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = timeout;
        }

        public MySpaceID(string apiDomain, string consumerKey, string consumerSecret, int timeout, IWebProxy proxy)
        {
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = timeout;
            Proxy = proxy;
        }

        public string GetUserInfo(string oAuthToken, string oAuthTokenSecret, string format)
        {
            return GetUserInfo(oAuthToken, oAuthTokenSecret, format, FIELDS);
        }

        public string GetUserInfo(string oAuthToken, string oAuthTokenSecret, string format, string fields)
        {
            OAuthBase oAuth = new OAuthBase();
            string accessTokenUrl = string.Format("http://{0}/v2/people/@me/@self", ApiDomain);
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();
            
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("fields", fields));
            parameters.Add(new QueryParameter("format", format));
            
            string signature = oAuth.GenerateSignature(
                new Uri(accessTokenUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1,
                parameters);

            string accessQueryParametersFormat = "{0}?fields={1}&format={2}&oauth_consumer_key={3}&oauth_nonce={4}&oauth_signature={5}&oauth_signature_method={6}&oauth_timestamp={7}&oauth_token={8}&oauth_version={9}";
            string generatedAccessTokenUrl = string.Format(
                accessQueryParametersFormat, 
                accessTokenUrl, 
                OAuthBase.UrlEncode(fields),
                OAuthBase.UrlEncode(format),
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode(signature),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"));
            
            return HttpGet(generatedAccessTokenUrl);
        }
    }
}
