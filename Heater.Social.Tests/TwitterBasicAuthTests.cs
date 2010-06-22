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
using Heater.Social.Tests;
using Heater.Social.Twitter.DataContracts;
using NUnit.Framework;

namespace Heater.Social.Tests
{
    [TestFixture]
    public class TwitterBasicAuthTests
    {
        [Test]
        public void BasicPublicTimeline()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME_READ, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetPublicTimeline();
                Assert.AreEqual(20, response.Count);
                
                foreach (Status status in response)
                {
                    Console.WriteLine(status.User.ScreenName);
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicFriendsTimeline()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME_READ, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetFriendsTimeline(20, 0);
                //Assert.AreEqual(19, response.Count);

                foreach (Status status in response)
                {
                    Console.WriteLine("{0}\t{1}", status.User.ScreenName, status.Text);
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicUserTimeline()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetUserTimeline("heatxsink", 20, 1);
                Assert.AreEqual(20, response.Count);

                foreach (Status status in response)
                {
                    Console.WriteLine("{0}\t{1}", status.User.ScreenName, status.Text);
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicMentions()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME_READ, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetMentions(20, 1);
                foreach (Status status in response)
                {
                    Console.WriteLine("{0}\t{1}", status.User.ScreenName, status.Text);
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicShowStatus()
        {
            long status_id = 3100982020;
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME_READ, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.ShowStatus(status_id);
                Assert.AreEqual(status_id, response.Id);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicUpdateStatus()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var message = string.Format("hello world {0}.", DateTime.Now.Ticks.ToString());
                var response = basic.UpdateStatus(message);
                Console.WriteLine(response.Text);
                Assert.AreEqual(message, response.Text);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicDestroyStatus()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var message = "hello world this is a test.";
                var response_setup = basic.UpdateStatus(message);
                var response = basic.DestroyStatus(response_setup.Id);
                Assert.AreEqual(response_setup.Id, response.Id);
                Console.WriteLine(response.Text);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicShowUser()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.ShowUser(TwitterConstants.USERNAME_READ);
                Assert.AreEqual(TwitterConstants.USERNAME_READ, response.ScreenName);
                Console.WriteLine(response.ScreenName);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicGetFriends()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetFriends(TwitterConstants.USERNAME , 0);
                Assert.AreEqual(3, response.Count);
                foreach(User user in response)
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
        public void BasicGetFollowers()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetFollowers(TwitterConstants.USERNAME, 0);
                Assert.AreEqual(2, response.Count);
                foreach (User user in response)
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
        public void BasicGetDirectMessages()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetDirectMessages(20, 0);
                foreach (DirectMessage message in response)
                {
                    Console.WriteLine("{0}", message.Text);
                    Console.WriteLine(message.ToJson());
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicGetSentDirectMessages()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response = basic.GetSentDirectMessages(20, 0);
                foreach (DirectMessage message in response)
                {
                    Console.WriteLine("{0}", message.Text);
                    Console.WriteLine(message.ToJson());
                }
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicSendDirectMessage()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var text = "Hi, this is a direct message.";
                var response = basic.SendDirectMessage(TwitterConstants.USERNAME_READ, text);
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
        public void BasicDestroyDirectMessages()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var text = "Hi, this is a direct message, but will be deleted soon.";
                var response_setup = basic.SendDirectMessage(TwitterConstants.USERNAME_READ, text);
                var response = basic.DestroyDirectMessage(response_setup.Id);
                Assert.AreEqual(response_setup.Id, response.Id);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
            }
        }

        [Test]
        public void BasicCreateFriendship()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response_setup = basic.GetPublicTimeline();
                var response = basic.CreateFriendship(response_setup[0].User.ScreenName, false);
                var response_teardown = basic.DestroyFriendship(response.Id);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

        [Test]
        public void BasicDestroyFriendship()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                var response_setup = basic.GetPublicTimeline();
                var response_friend = basic.CreateFriendship(response_setup[0].User.ScreenName, false);
                var response = basic.DestroyFriendship(response_friend.Id);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
            }
        }

        [Test]
        public void BasicShowFriendship()
        {
            TwitterBasicAuth basic = new TwitterBasicAuth(TwitterConstants.USERNAME, TwitterConstants.PASSWORD);

            try
            {
                long source_id = 62468094;
                long target_id = 44074184;
                var response = basic.ShowFriendship(source_id, target_id);
                Console.WriteLine(response.ToJson());
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }

    }
}
