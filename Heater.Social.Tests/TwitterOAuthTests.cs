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
using NUnit.Framework;
using System.Net;
using Heater.Social.Twitter.DataContracts;
using Heater.Social.Tests;

namespace Heater.Social.Tests
{
    [TestFixture]
    public class TwitterOAuthTests
    {
        [Test]
        public void OAuthUpdateStatus()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);
            try
            {
                var message = string.Format("Hello World, this is a test {0}.", DateTime.Now.Ticks.ToString());
                var response = tweet.UpdateStatus(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, message);
                Assert.AreEqual(message, response.Text);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthSendDirectMessageWithId()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var text = "Hi, this is a direct message.";
                var user = tweet.ShowUser(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, TestConstants.TWITTER_USERNAME_READ);
                var response = tweet.SendDirectMessage(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, user.Id, text);
                Assert.AreEqual(text, response.Text);
                Console.WriteLine("{0}", response.Text);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthSendDirectMessageWithScreeName()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var text = "Hi, this is a direct message.";
                var response = tweet.SendDirectMessage(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, TestConstants.TWITTER_USERNAME_READ, text);
                Assert.AreEqual(text, response.Text);
                Console.WriteLine("{0}", response.Text);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthShowUserLargeFriendCount()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.ShowUser(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, "heatxsink");
                Assert.AreEqual("heatxsink", response.ScreenName);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
            }
        }

        [Test]
        public void OAuthShowUser()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.ShowUser(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, TestConstants.TWITTER_USERNAME_READ);
                Assert.AreEqual(TestConstants.TWITTER_USERNAME_READ, response.ScreenName);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }
        
        [Test]
        public void OAuthGetFriendsBlankUserField()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.GetFriends(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, "", 0);
                Assert.AreEqual(0, response.Users.Length);
                foreach (User user in response.Users)
                {
                    Console.WriteLine("{0}", user.ScreenName);
                    Console.WriteLine(user.ToJson());
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthGetFriendsForLargeUserAccount()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.GetFriends(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, "heatxsink", -1);
                // Assert.AreEqual(2, response.Count);
                Console.WriteLine("TOTAL:{0}", response.Users.Length);
                foreach (User user in response.Users)
                {
                    Console.WriteLine("{0}", user.ScreenName);
                    Console.WriteLine(user.ToJson());
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthGetFriends()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.GetFriends(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, TestConstants.TWITTER_USERNAME_READ, -1);
                Assert.AreEqual(2, response.Users.Length);
                Console.WriteLine("TOTAL: {0}", response.Users.Length);
                foreach (User user in response.Users)
                {
                    Console.WriteLine("{0}", user.ScreenName);
                    Console.WriteLine(user.ToJson());
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthGetFollowers()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var response = tweet.GetFollowers(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET, "heatxsink", -1);
                Assert.GreaterOrEqual(response.Users.Length, 2);
                Console.WriteLine("TOTAL:{0}", response.Users.Length);
                foreach (User user in response.Users)
                {
                    Console.WriteLine("{0}", user.ScreenName);
                    Console.WriteLine("\t{0}", user.Following);
                    Console.WriteLine(user.ToJson());
                }

                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthGetFriendIds()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var ids = tweet.GetFriendIds(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET);

                Console.WriteLine(ids.ToJson());
                Console.WriteLine();
                foreach (long id in ids)
                {
                    Console.WriteLine(id);
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void OAuthVerifyCredentials()
        {
            TwitterOAuth tweet = new TwitterOAuth(TestConstants.TWITTER_CONSUMER_KEY, TestConstants.TWITTER_CONSUMER_SECRET);

            try
            {
                var user = tweet.VerifyCredentials(TestConstants.TWITTER_ACCESS_TOKEN, TestConstants.TWITTER_ACCESS_SECRET);
                Console.WriteLine(user.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }
    }
}