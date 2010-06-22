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
using Heater.Social.Tests;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Heater.Social.MySpace.DataContracts.OpenSocial;

namespace Heater.Social.Tests
{
    [TestFixture]
    public class MySpaceMdpTests
	{
        [Test]
        public void MdpGetMySpaceInfo()
        {
            MDP mdp = new MDP(TestConstants.API_MS_CONSUMER_KEY, TestConstants.API_MS_CONSUMER_SECRET);
            PersonInfo person = mdp.GetMySpaceInfo(TestConstants.API_ACCESS_TOKEN, TestConstants.API_ACCESS_TOKEN_SECRET);
            Console.WriteLine(person.ToJson());
        }

        [Test]
        public void MySpaceIdGetMySpaceInfo()
        {
            MDP mdp = new MDP(TestConstants.API_MS_CONSUMER_KEY, TestConstants.API_MS_CONSUMER_SECRET);
            PersonInfo person = mdp.GetMySpaceInfo(TestConstants.API_ACCESS_TOKEN, TestConstants.API_ACCESS_TOKEN_SECRET);
            Console.WriteLine(person.ToJson());
        }

        [Test]
        public void MdpPostActivityFeed()
        {
            Dictionary<string, MySpaceUser> users = TestConstants.GetMySpaceUsers();
           	MdpApplication application = TestConstants.GetApplication();
            
            MDP mdp = new MDP(application.key, application.secret);
            
            string oAuthToken = TestConstants.API_ACCESS_TOKEN;
            long myspaceId = users["tom"].id;
            
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("eventTitle", HttpUtility.UrlEncode("Hello"));
            parameters.Add("eventDate", HttpUtility.UrlEncode(DateTime.Now.ToString("MMMM d")));
            parameters.Add("eventLoc", HttpUtility.UrlEncode("My House"));
            parameters.Add("eventId", HttpUtility.UrlEncode("12"));

            try
            {
                PostActivityFeedResult result = mdp.PostActivityFeed(oAuthToken, "CreateEvent", parameters, "en-US", "json");
                Console.WriteLine("Consumer Key: {0}", application.key);
                Console.WriteLine("OAuthToken: {0}", oAuthToken);
                Console.WriteLine("Result: {0} ", result.PostActivityId);
            }
            catch (WebException ex)
            {
                Console.Error.WriteLine(Util.HandleWebException(ex));
                Assert.Fail();
            }
        }
    }
}