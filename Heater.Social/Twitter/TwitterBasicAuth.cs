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

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using Heater.Social.Twitter.DataContracts;

namespace Heater.Social
{
    public class TwitterBasicAuth : AbstractApiBase
    {
        private static string API_DOMAIN = "api.twitter.com";
        private string Username { get; set; }
        private string Password { get; set; }
        private Dictionary<string, string> HttpHeaders { get; set; }

        public TwitterBasicAuth(string username, string password)
        {
            ApiDomain = API_DOMAIN;
            Username = username;
            Password = password;
            Timeout = DEFAULT_TIMEOUT;
            Credentials = new NetworkCredential(Username, Password);
            loadHttpHeaders();
            ServicePointManager.Expect100Continue = false;
        }

        private void loadHttpHeaders()
        {
            HttpHeaders = new Dictionary<string, string>();
            HttpHeaders.Add("X-Twitter-Client", "Heater.Social.TwitterBasicAuth");
            HttpHeaders.Add("X-Twitter-Version", "1.0");
            HttpHeaders.Add("X-Twitter-Url", "http://www.nickgranado.com");
        }

        #region Timeline Methods

        public List<Status> GetPublicTimeline()
        {
            string url = string.Format("http://{0}/1/statuses/public_timeline.{1}", ApiDomain, "json");
            var response = HttpGet(url, Credentials);
            var statues = (Status[])JsonConvert.Import(typeof(Status[]), response);
            return statues.ToList();
        }

        public List<Status> GetFriendsTimeline(int count, int page)
        {
            string url = string.Format("http://{0}/1/statuses/friends_timeline.{1}?count={2}&page={3}", ApiDomain, "json", count, page);
            var response = HttpGet(url, Credentials);
            var statues = (Status[])JsonConvert.Import(typeof(Status[]), response);
            return statues.ToList();
        }

        public List<Status> GetUserTimeline(string screen_name_or_id, int count, int page)
        {
            string url = string.Empty;
            if (string.IsNullOrEmpty(screen_name_or_id))
            {
                url = string.Format("http://{0}/1/statuses/user_timeline.{1}?count={2}&page={3}", ApiDomain, "json", count, page);
            }
            else
            {
                url = string.Format("http://{0}/1/statuses/user_timeline/{1}.{2}?count={3}&page={4}", ApiDomain, screen_name_or_id, "json", count, page);
            }
            
            var response = HttpGet(url, Credentials);
            var statues = (Status[])JsonConvert.Import(typeof(Status[]), response);
            return statues.ToList();
        }

        public List<Status> GetMentions(int count, int page)
        {
            string url = string.Format("http://{0}/1/statuses/mentions.{1}?count={2}&page={3}", ApiDomain, "json", count, page);
            var response = HttpGet(url, Credentials);
            var statues = (Status[])JsonConvert.Import(typeof(Status[]), response);
            return statues.ToList();
        }

        #endregion Timeline Methods

        #region Status Methods

        public Status ShowStatus(long id)
        {
            string url = string.Format("http://{0}/1/statuses/show/{1}.{2}", ApiDomain, id, "json");
            var response = HttpGet(url, Credentials);
            var status = (Status)JsonConvert.Import(typeof(Status), response);
            return status;
        }

        public Status UpdateStatus(string message)
        {
            string url = string.Format("http://{0}/1/statuses/update.{1}", ApiDomain, "json");
            string status = string.Format("status={0}", HttpUtility.UrlEncode(message));
            byte[] data = Encoding.UTF8.GetBytes(status);
            var response = HttpPost(url, Credentials, HttpHeaders, data);
            var statusResponse = (Status)JsonConvert.Import(typeof(Status), response);
            return statusResponse;
        }

        public Status DestroyStatus(long id)
        {
            string url = string.Format("http://{0}/1/statuses/destroy/{1}.{2}", ApiDomain, id, "json");
            var response = HttpDelete(url, Credentials);
            var statusResponse = (Status)JsonConvert.Import(typeof(Status), response);
            return statusResponse;
        }

        #endregion Status Methods

        #region User Methods

        public User ShowUser(string screen_name)
        {
            string url = string.Format("http://{0}/1/users/show.{1}?screen_name={2}", ApiDomain, "json", screen_name);

            var response = HttpGet(url, Credentials);
            var user = (User)JsonConvert.Import(typeof(User), response);
            return user;
        }

        public List<User> GetFriends(string screen_name, int page)
        {
            string url = string.Format("http://{0}/1/statuses/friends.{1}?screen_name={2}&page={3}", ApiDomain, "json", screen_name, page);
            var response = HttpGet(url, Credentials);
            var users = (User[])JsonConvert.Import(typeof(User[]), response);
            return users.ToList();
        }

        public List<User> GetFollowers(string screen_name, int page)
        {
            string url = string.Format("http://{0}/1/statuses/followers.{1}?screen_name={2}&page={3}", ApiDomain, "json", screen_name, page);
            var response = HttpGet(url, Credentials);
            var users = (User[])JsonConvert.Import(typeof(User[]), response);
            return users.ToList();
        }

        #endregion User Methods

        #region Direct Message Methods

        public List<DirectMessage> GetDirectMessages(int count, int page)
        {
            string url = string.Format("http://{0}/1/direct_messages.{1}?count={2}&page={3}", ApiDomain, "json", count, page);
            var response = HttpGet(url, Credentials);
            var messages = (DirectMessage[])JsonConvert.Import(typeof(DirectMessage[]), response);
            return messages.ToList();
        }

        public List<DirectMessage> GetSentDirectMessages(int count, int page)
        {
            string url = string.Format("http://{0}/1/direct_messages/sent.{1}?count={2}&page={3}", ApiDomain, "json", count, page);
            var response = HttpGet(url, Credentials);
            var messages = (DirectMessage[])JsonConvert.Import(typeof(DirectMessage[]), response);
            return messages.ToList();
        }

        public DirectMessage SendDirectMessage(string screen_name, string text)
        {
            string url = string.Format("http://{0}/1/direct_messages/new.{1}", ApiDomain, "json");
            string payload = string.Format("text={0}&user={1}", HttpUtility.UrlEncode(text), screen_name);
            byte[] data = Encoding.UTF8.GetBytes(payload);
            var response = HttpPost(url, Credentials, HttpHeaders, data);
            var message = (DirectMessage)JsonConvert.Import(typeof(DirectMessage), response);
            return message;
        }

        public DirectMessage DestroyDirectMessage(long id)
        {
            string url = string.Format("http://{0}/1/direct_messages/destroy/{1}.{2}", ApiDomain, id, "json");
            var response = HttpDelete(url, Credentials);
            var message = (DirectMessage)JsonConvert.Import(typeof(DirectMessage), response);
            return message;
        }

        #endregion Direct Message Methods

        #region Friendship Methods

        public User CreateFriendship(string screen_name, bool follow)
        {
            string url = string.Format("http://{0}/1/friendships/create.{1}", ApiDomain, "json");
            string payload = string.Format("screen_name={0}&follow={1}", screen_name, follow);
            byte[] data = Encoding.UTF8.GetBytes(payload);
            var response = HttpPost(url, Credentials, HttpHeaders, data);
            var user = (User)JsonConvert.Import(typeof(User), response);
            return user;
        }

        public User DestroyFriendship(long id)
        {
            string url = string.Format("http://{0}/1/friendships/destroy/{1}.{2}", ApiDomain, id, "json");
            var response = HttpDelete(url, Credentials);
            var user = (User)JsonConvert.Import(typeof(User), response);
            return user;
        }

        public Relationship ShowFriendship(long source_id, long target_id)
        {
            string url = string.Format("http://{0}/1/friendships/show.{1}?source_id={2}&target_id={3}", ApiDomain, "json", source_id, target_id);
            var response = HttpGet(url, Credentials);
            var obj = (JsonObject)JsonConvert.Import(response);
            var relationship = (Relationship)JsonConvert.Import(typeof(Relationship), obj["relationship"].ToJson());
            return relationship;
        }

        #endregion Friendship Methods
    }
}
