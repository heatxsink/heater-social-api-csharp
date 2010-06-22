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
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Jayrock.Json.Conversion;
using Heater.OAuth;
using Heater.Social.Twitter.DataContracts;

namespace Heater.Social
{
    // * 
    // * Balls a security hole.
    // * http://groups.google.com/group/twitter-development-talk/browse_thread/thread/59ed5372f7c1b623
    // * http://groups.google.com/group/twitter-development-talk/browse_thread/thread/8a598fd042e53ce0/2629fe5160fc8294
    // * 
    
    public class TwitterOAuth : AbstractApiBase
    {
        private static string API_DOMAIN = "api.twitter.com";
        
        public TwitterOAuth(string consumerKey, string consumerSecret)
        {
            ApiDomain = API_DOMAIN;
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            Timeout = DEFAULT_TIMEOUT;
            ServicePointManager.Expect100Continue = false;
        }

        public OAuthTuple GetRequestToken(string callbackUrl)
        {
            string requestTokenUrl = string.Format("http://{0}/oauth/request_token", ApiDomain);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("oauth_callback", callbackUrl));

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
                        SignatureTypes.HMACSHA1,
                        parameters);

            string requestTokenUrlFormat = "{0}?oauth_callback={1}&oauth_consumer_key={2}&oauth_nonce={3}&oauth_signature={4}&oauth_signature_method={5}&oauth_timestamp={6}&oauth_token={7}&oauth_version={8}";
            string generatedRequestTokenUrl = string.Format(
                requestTokenUrlFormat,
                requestTokenUrl,
                OAuthBase.UrlEncode(callbackUrl),
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

        public string GetAuthorizationUrl(string oAuthToken)
        {
            return string.Format("http://{0}/oauth/authorize?oauth_token={1}", ApiDomain, OAuthBase.UrlEncode(oAuthToken));
        }

        public OAuthTuple GetAccessToken(string oAuthToken, string oAuthTokenSecret, string oAuthVerifier)
        {
            OAuthBase oAuth = new OAuthBase();
            string accessTokenUrl = string.Format("http://{0}/oauth/access_token", ApiDomain);
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("oauth_verifier", oAuthVerifier));

            string signatureAccess = oAuth.GenerateSignature(
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

            string accessQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_verifier={7}&oauth_version={8}";
            string generatedAccessTokenUrl = string.Format(
                accessQueryParametersFormat,
                accessTokenUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode(oAuthVerifier),
                OAuthBase.UrlEncode("1.0"));

            string accessTokenReturn = HttpGet(generatedAccessTokenUrl);
            return Util.ParseQueryString(accessTokenReturn);
        }
        
        public Status UpdateStatus(string oAuthToken, string oAuthTokenSecret, string message)
        {
            string format = "json";
            string updateStatusUrl = string.Format("http://{0}/1/statuses/update.{1}", ApiDomain, format);

            Uri uri = new Uri(updateStatusUrl);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("status", message));

            string signature =
                oAuth.GenerateSignature(
                        uri,
                        ConsumerKey,
                        ConsumerSecret,
                        oAuthToken,
                        oAuthTokenSecret,
                        "POST",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1,
                        parameters);

            string updateStatusUrlFormat = "{0}?oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_token={6}&oauth_version={7}";

            string generatedUrl = string.Format(
                updateStatusUrlFormat,
                updateStatusUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signature),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"));

            var urlEncodedStatusData = string.Format("status={0}", OAuthBase.UrlEncode(message));
            byte[] requestData = Encoding.UTF8.GetBytes(urlEncodedStatusData);
            var response = HttpPost(generatedUrl, requestData);
            Status statusResult = JsonConvert.Import(typeof(Status), response) as Status;
            return statusResult;
        }

        public DirectMessage SendDirectMessage(string oAuthToken, string oAuthTokenSecret, string screen_name, string text)
        {
            string format = "json";
            string sendDirectMessageUrl = string.Format("http://{0}/1/direct_messages/new.{1}", ApiDomain, format);

            Uri uri = new Uri(sendDirectMessageUrl);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("text", text));
            parameters.Add(new QueryParameter("screen_name", screen_name));

            string signature =
                oAuth.GenerateSignature(
                        uri,
                        ConsumerKey,
                        ConsumerSecret,
                        oAuthToken,
                        oAuthTokenSecret,
                        "POST",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1,
                        parameters);

            string sendDirectMessageUrlFormat = "{0}?oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_token={6}&oauth_version={7}";

            string generatedUrl = string.Format(
                sendDirectMessageUrlFormat,
                sendDirectMessageUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signature),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"));

            string urlEncodedDirectMessage = string.Format("text={0}&screen_name={1}", HttpUtility.UrlEncode(text), screen_name);
            byte[] data = Encoding.UTF8.GetBytes(urlEncodedDirectMessage);
            var response = HttpPost(generatedUrl, data);
            var message = (DirectMessage)JsonConvert.Import(typeof(DirectMessage), response);
            return message;
        }

        public DirectMessage SendDirectMessage(string oAuthToken, string oAuthTokenSecret, long user_id, string text)
        {
            string format = "json";
            string sendDirectMessageUrl = string.Format("http://{0}/1/direct_messages/new.{1}", ApiDomain, format);

            Uri uri = new Uri(sendDirectMessageUrl);
            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timestamp = oAuth.GenerateTimeStamp();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("text", text));
            parameters.Add(new QueryParameter("user_id", user_id.ToString()));

            string signature =
                oAuth.GenerateSignature(
                        uri,
                        ConsumerKey,
                        ConsumerSecret,
                        oAuthToken,
                        oAuthTokenSecret,
                        "POST",
                        timestamp,
                        nonce,
                        SignatureTypes.HMACSHA1,
                        parameters);

            string sendDirectMessageUrlFormat = "{0}?oauth_consumer_key={1}&oauth_nonce={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_token={6}&oauth_version={7}";

            string generatedUrl = string.Format(
                sendDirectMessageUrlFormat,
                sendDirectMessageUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signature),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("1.0"));

            string urlEncodedDirectMessage = string.Format("text={0}&user_id={1}", HttpUtility.UrlEncode(text), user_id.ToString());
            byte[] data = Encoding.UTF8.GetBytes(urlEncodedDirectMessage);
            var response = HttpPost(generatedUrl, data);
            var message = (DirectMessage)JsonConvert.Import(typeof(DirectMessage), response);
            return message;
        }

        public User ShowUser(string oAuthToken, string oAuthTokenSecret, string screen_name)
        {
            OAuthBase oAuth = new OAuthBase();
            string showUserUrl = string.Format("http://{0}/1/users/show.{1}", ApiDomain, "json");
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            string generatedUrl = string.Empty;

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("screen_name", screen_name));

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(showUserUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1,
                parameters);

            string showUserQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}&screen_name={8}";
            generatedUrl = string.Format(
                showUserQueryParametersFormat,
                showUserUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"),
                OAuthBase.UrlEncode(screen_name));

            var response = HttpGet(generatedUrl);
            var user = (User)JsonConvert.Import(typeof(User), response);
            return user;
        }

        public User VerifyCredentials(string oAuthToken, string oAuthTokenSecret)
        {
            OAuthBase oAuth = new OAuthBase();
            string verifyCredentialsUrl = string.Format("http://{0}/1/account/verify_credentials.{1}", ApiDomain, "json");
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(verifyCredentialsUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1);

            string getFriendsQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}";
            string generatedUrl = string.Format(
                getFriendsQueryParametersFormat,
                verifyCredentialsUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"));

            var response = HttpGet(generatedUrl);
            var user = (User)JsonConvert.Import(typeof(User), response);
            return user;
        }

        public UserList GetFriends(string oAuthToken, string oAuthTokenSecret, string screen_name, long cursor)
        {
            OAuthBase oAuth = new OAuthBase();
            string getFriendsUrl = string.Format("http://{0}/1/statuses/friends.{1}", ApiDomain, "json");
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("screen_name", screen_name));
            parameters.Add(new QueryParameter("cursor", cursor.ToString()));

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(getFriendsUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1,
                parameters);

            string getFriendsQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}&cursor={8}&screen_name={9}";
            string generatedUrl = string.Format(
                getFriendsQueryParametersFormat,
                getFriendsUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"),
                OAuthBase.UrlEncode(cursor.ToString()),
                OAuthBase.UrlEncode(screen_name));

            var response = HttpGet(generatedUrl);
            var users = (UserList)JsonConvert.Import(typeof(UserList), response);
            return users;
        }

        public UserList GetFollowers(string oAuthToken, string oAuthTokenSecret, string screen_name, long cursor)
        {
            OAuthBase oAuth = new OAuthBase();
            string getFriendsUrl = string.Format("http://{0}/1/statuses/followers.{1}", ApiDomain, "json");
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter("screen_name", screen_name));
            parameters.Add(new QueryParameter("cursor", cursor.ToString()));

            string signatureAccess = oAuth.GenerateSignature(
                new Uri(getFriendsUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1,
                parameters);

            string getFriendsQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}&cursor={8}&screen_name={9}";
            string generatedUrl = string.Format(
                getFriendsQueryParametersFormat,
                getFriendsUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"),
                OAuthBase.UrlEncode(cursor.ToString()),
                OAuthBase.UrlEncode(screen_name));

            var response = HttpGet(generatedUrl);
            var users = (UserList)JsonConvert.Import(typeof(UserList), response);
            return users;
        }

        public List<long> GetFriendIds(string oAuthToken, string oAuthTokenSecret)
        {
            OAuthBase oAuth = new OAuthBase();
            string getFriendsUrl = string.Format("http://{0}/1/friends/ids.{1}", ApiDomain, "json");
            string timestamp = oAuth.GenerateTimeStamp();
            string nonce = oAuth.GenerateNonce();
            
            string signatureAccess = oAuth.GenerateSignature(
                new Uri(getFriendsUrl),
                ConsumerKey,
                ConsumerSecret,
                oAuthToken,
                oAuthTokenSecret,
                "GET",
                timestamp,
                nonce,
                SignatureTypes.HMACSHA1);

            string getFriendsQueryParametersFormat = "{0}?oauth_consumer_key={1}&oauth_token={2}&oauth_signature_method={3}&oauth_signature={4}&oauth_timestamp={5}&oauth_nonce={6}&oauth_version={7}";
            string generatedUrl = string.Format(
                getFriendsQueryParametersFormat,
                getFriendsUrl,
                OAuthBase.UrlEncode(ConsumerKey),
                OAuthBase.UrlEncode(oAuthToken),
                OAuthBase.UrlEncode("HMAC-SHA1"),
                OAuthBase.UrlEncode(signatureAccess),
                OAuthBase.UrlEncode(timestamp),
                OAuthBase.UrlEncode(nonce),
                OAuthBase.UrlEncode("1.0"));

            var response = HttpGet(generatedUrl);
            var ids = (long[])JsonConvert.Import(typeof(long[]), response);
            var list = new List<long>(ids);
            return list;
        }
    }
}
