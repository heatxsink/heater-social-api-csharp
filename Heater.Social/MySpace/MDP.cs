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
using System.Collections.Generic;
using System.Text;
using System.Web;
using Jayrock.Json.Conversion;
using Heater.OAuth;
using Heater.Social.MySpace.DataContracts.OpenSocial;
using System.Net;

namespace Heater.Social
{
    public class MDP : AbstractApiBase
    {
        private static string MDP_DOMAIN = "api.myspace.com";
        
        public MDP()
        {
            ApiDomain = MDP_DOMAIN;
            Timeout = DEFAULT_TIMEOUT;
        }
        
        public MDP(string consumerKey, string consumerSecret)
        {
            ApiDomain = MDP_DOMAIN;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = DEFAULT_TIMEOUT;
        }

        public MDP(string apiDomain, string consumerKey, string consumerSecret)
        {
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = DEFAULT_TIMEOUT;
        }
        
        public MDP(string apiDomain, string consumerKey, string consumerSecret, int timeout)
        { 
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = Timeout;
        }

        public MDP(string apiDomain, string consumerKey, string consumerSecret, int timeout, IWebProxy proxy)
        {
            ApiDomain = apiDomain;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = timeout;
            Proxy = proxy;
        }


        public static string GenerateMySpaceEventUrl(long appId, string path)
        {
            string jsonObject = string.Format("{{\"rt\":\"{0}\"}}", path);
            return string.Format("http://apps.myspace.com/{0}?appParams={1}", appId, HttpUtility.UrlEncode(jsonObject));
        }

        [Obsolete("GenerateMySpaceEventUrl(string appId, string eventId) is depreicated, please use GenerateMySpaceEventUrl(long appId, string path).", true)]
        public static string GenerateMySpaceEventUrl(string appId, string eventId)
        {
            string url = string.Format("http://apps.myspace.com/{0}?appParams={1}", appId, HttpUtility.UrlEncode("{\"rt\":\"/Event/View/" + eventId + "\"}"));
            return url;
        }
        
        private string GenerateTemplateActivityBody(string templateId, string templateParameters)
        {
            string templateFormat = "templateId={0}&templateParameters={1}";
            return string.Format(templateFormat, templateId, templateParameters);
        }

        private string GenerateTemplateActivityBody(string templateId, string templateParameters, string mediaItems)
        {
            string templateFormat = "templateId={0}&templateParameters={1}&mediaItems={2}";
            return string.Format(templateFormat, templateId, templateParameters, mediaItems);
        }

        #region OAuth
        
        public OAuthTuple GetRequestToken()
        {
            string requestTokenUrl = string.Format("http://{0}/request_token", ApiDomain);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();
            
            string signature =
                oAuth.GenerateSignature(
                        new Uri(requestTokenUrl),
                        ConsumerKey,
                        ConsumerSecret,
                        "",
                        "",
                        "GET",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1);

            string requestTokenUrlFormat = "{0}?oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature={3}&oauth_signature_method={4}&oauth_timestamp={5}&oauth_token={6}&oauth_version={7}";
            string generatedRequestTokenUrl = string.Format(
                requestTokenUrlFormat, 
                requestTokenUrl,
                OAuthBase.UrlEncode(ConsumerKey), 
                OAuthBase.UrlEncode(nonce), 
                OAuthBase.UrlEncode(signature), 
                OAuthBase.UrlEncode("HMAC-SHA1"), 
                OAuthBase.UrlEncode(timestamp), 
                OAuthBase.UrlEncode(""), 
                OAuthBase.UrlEncode("1.0"));

            string requestToken = HttpGet(generatedRequestTokenUrl);
            return Util.ParseQueryString(requestToken);
        }

        public OAuthTuple GetAccessToken(string oAuthToken, string oAuthTokenSecret)
        {
            OAuthBase oAuth = new OAuthBase();
            string accessTokenUrl = string.Format("http://{0}/access_token", ApiDomain);
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(accessTokenUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1);

            string accessQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}";
            string generatedAccessTokenUrl = string.Format(
                accessQueryParametersFormat, 
                accessTokenUrl,
                OAuthBase.UrlEncode(ConsumerKey), 
                OAuthBase.UrlEncode(oAuthToken), 
                OAuthBase.UrlEncode("HMAC-SHA1"), 
                OAuthBase.UrlEncode(signatureAccess), 
                OAuthBase.UrlEncode(timestamp), 
                OAuthBase.UrlEncode(nonce), 
                OAuthBase.UrlEncode("1.0"));
            
            string accessTokenReturn = HttpGet(generatedAccessTokenUrl);
            return Util.ParseQueryString(accessTokenReturn);
        }

        public string GetAuthorizationUrl(string callbackUrl, string oAuthToken)
        {
            return string.Format("http://{0}/authorize?oauth_callback={1}&oauth_token={2}", ApiDomain, OAuthBase.UrlEncode(callbackUrl), OAuthBase.UrlEncode(oAuthToken));
        }

        #endregion OAuth

        public PersonInfo GetMySpaceInfo(string oAuthToken)
        {
            return GetMySpaceInfo(oAuthToken, string.Empty);
        }

        public PersonInfo GetMySpaceInfo(string oAuthToken, string oAuthTokenSecret)
        {
            OAuthBase oAuth = new OAuthBase();
            string url = string.Format("http://{0}/v1/user.json", ApiDomain);
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(url),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1);

            string queryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}";

            string generatedUrl = string.Format(
                queryParametersFormat,
                url,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"));

            var response = HttpGet(generatedUrl);
            var personInfoResult = (PersonInfo)JsonConvert.Import(typeof(PersonInfo), response);
            return personInfoResult;
        }

        public PostActivityFeedResult PostActivityFeed(string oAuthToken, string templateId, IDictionary<string, string> templateParameters, string culture, string format)
        {
            string templateParametersString = Util.DictionaryToJson(templateParameters);
            string body = GenerateTemplateActivityBody(templateId, templateParametersString);
            string requestTokenUrl = string.Format("http://{0}/v2/activities/@me/@self", ApiDomain);

            Uri uri = new Uri(requestTokenUrl);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("culture", culture));
            parameters.Add(new QueryParameter("format", format));
            parameters.Add(new QueryParameter("templateId", templateId));
            parameters.Add(new QueryParameter("templateParameters", templateParametersString));

            string signature =
                oAuth.GenerateSignature(
                        uri,
                        ConsumerKey,
                        ConsumerSecret,
                        oAuthToken,
                        "",
                        "POST",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1,
                        parameters);
            
            string requestTokenUrlFormat = "http://{0}/v2/activities/@me/@self?culture={1}&format={2}&oauth_consumer_key={3}&oauth_nonce={4}&oauth_signature_method={5}&oauth_timestamp={6}&oauth_token={7}&oauth_version={8}&oauth_signature={9}";
            
            string generatedUrl = string.Format(
                requestTokenUrlFormat,
                ApiDomain,
                culture,
                format,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"),
                OAuthBase.UrlEncode(signature));
            
            string message = GenerateTemplateActivityBody(templateId, OAuthBase.UrlEncode(templateParametersString));
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] requestData = encoding.GetBytes(message);
            
            var response = HttpPost(generatedUrl, requestData);
            Console.WriteLine(response);
            var postActivityFeedResult = (PostActivityFeedResult)JsonConvert.Import(typeof(PostActivityFeedResult), response);
            return postActivityFeedResult;
        }

        public PostActivityFeedResult PostActivityFeed(string oAuthToken, string templateId, IDictionary<string, string> templateParameters, IList<string> mediaItems, string culture, string format)
        {
            string templateParametersString = Util.DictionaryToJson(templateParameters);
            string mediaItemsString = Util.ListToJson(mediaItems);
            string body = GenerateTemplateActivityBody(templateId, templateParametersString, mediaItemsString);
            string requestTokenUrl = string.Format("http://{0}/v2/activities/@me/@self", ApiDomain);

            Uri uri = new Uri(requestTokenUrl);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("culture", culture));
            parameters.Add(new QueryParameter("format", format));
            parameters.Add(new QueryParameter("templateId", templateId));
            parameters.Add(new QueryParameter("templateParameters", templateParametersString));
            parameters.Add(new QueryParameter("mediaItems", mediaItemsString));

            string signature =
                oAuth.GenerateSignature(
                        uri,
                        ConsumerKey,
                        ConsumerSecret,
                        oAuthToken,
                        "",
                        "POST",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1,
                        parameters);

            string requestTokenUrlFormat = "http://{0}/v2/activities/@me/@self?culture={1}&format={2}&oauth_consumer_key={3}&oauth_nonce={4}&oauth_signature_method={5}&oauth_timestamp={6}&oauth_token={7}&oauth_version={8}&oauth_signature={9}";

            string generatedUrl = string.Format(
                requestTokenUrlFormat,
                ApiDomain,
                culture,
                format,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"),
                OAuthBase.UrlEncode(signature));

            string message = GenerateTemplateActivityBody(templateId, OAuthBase.UrlEncode(templateParametersString), OAuthBase.UrlEncode(mediaItemsString));
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] requestData = encoding.GetBytes(message);

            var response = HttpPost(generatedUrl, requestData);
            Console.WriteLine(response);
            var postActivityFeedResult = (PostActivityFeedResult)JsonConvert.Import(typeof(PostActivityFeedResult), response);
            return postActivityFeedResult;
        }

        public ExtendedProfile GetMySpaceExtendedProfile(string oAuthToken, string oAuthTokenSecret, long userId)
        {
            OAuthBase oAuth = new OAuthBase();
            string url = string.Format("http://{0}/v1/users/{1}/profile.json", ApiDomain, userId);
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("detailtype", "extended"));

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(url),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1,
                parameters);

            string queryParametersFormat = "{0}?detailtype={1}&oauth_consumer_key={2}&oauth_token={3}&oauth_signature_method={4}&oauth_signature={5}&oauth_timestamp={6}&oauth_nonce={7}&oauth_version={8}";

            string generatedUrl = string.Format(
                queryParametersFormat,
                url,
                OAuthBase.UrlEncode("extended"),
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"));

            var response = HttpGet(generatedUrl);
            var extendedProfile = (ExtendedProfile)JsonConvert.Import(typeof(ExtendedProfile), response);
            return extendedProfile;
        }


        //public string PostNotifications(string oAuthToken, long applicationId, IDictionary<string, string> parameters)
        //{
        //    string templateParametersString = Util.DictionaryToJson(templateParameters);
        //    string body = GenerateTemplateActivityBody(templateId, templateParametersString);
        //    string requestTokenUrl = string.Format("http://{0}/v1/applications/{1}/notifications", ApiDomain, applicationId);

        //    Uri uri = new Uri(requestTokenUrl);
        //    OAuthBase oAuth = new OAuthBase();
        //    string nonce = oAuth.GenerateNonce();
        //    string timestamp = oAuth.GenerateTimeStamp();
        //    List<QueryParameter> parameters = new List<QueryParameter>();
        //    parameters.Add(new QueryParameter("culture", culture));
        //    parameters.Add(new QueryParameter("format", format));
        //    parameters.Add(new QueryParameter("templateId", templateId));
        //    parameters.Add(new QueryParameter("templateParameters", templateParametersString));

        //    string signature =
        //        oAuth.GenerateSignature(
        //                uri,
        //                ConsumerKey,
        //                ConsumerSecret,
        //                oAuthToken,
        //                "",
        //                "POST",
        //                timestamp,
        //                nonce,
        //                SignatureTypes.HMACSHA1,
        //                parameters);
        //    string requestTokenUrlFormat = "http://{0}/v2/activities/@me/@self?culture={1}&format={2}&oauth_consumer_key={3}&oauth_nonce={4}&oauth_signature_method={5}&oauth_timestamp={6}&oauth_token={7}&oauth_version={8}&oauth_signature={9}";

        //    string generatedUrl = string.Format(
        //        requestTokenUrlFormat,
        //        ApiDomain,
        //        culture,
        //        format,
        //        OAuthBase.UrlEncode(ConsumerKey),
        //        OAuthBase.UrlEncode(nonce),
        //        OAuthBase.UrlEncode("HMAC-SHA1"),
        //        OAuthBase.UrlEncode(timestamp),
        //        OAuthBase.UrlEncode(oAuthToken),
        //        OAuthBase.UrlEncode("1.0"),
        //        OAuthBase.UrlEncode(signature));

        //    string message = GenerateTemplateActivityBody(templateId, OAuthBase.UrlEncode(templateParametersString));
        //    UTF8Encoding encoding = new UTF8Encoding();
        //    byte[] requestData = encoding.GetBytes(message);

        //    Stream stream = HttpPostStream(generatedUrl, requestData);
        //    DataContractJsonSerializer postActivityFeedResultSerializer = new DataContractJsonSerializer(typeof(PostActivityFeedResult));
        //    PostActivityFeedResult postActivityFeedResult = postActivityFeedResultSerializer.ReadObject(stream) as PostActivityFeedResult;
        //    return postActivityFeedResult;
        //}

    }
}
