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
using System;
using System.Text;

namespace Heater.Social.Tests
{
    public class MySpaceUser
    {
        public string name;
        public long id;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("**** MYSPACE USER INFORMATION ****\n");
            builder.AppendFormat("NAME:       {0}\n", name);
            builder.AppendFormat("ID:         {0}\n", id);
            builder.AppendFormat("**********************************\n");
            return builder.ToString();
        }
    }

    public class MdpApplication
    {
        public long myspaceId;
        public long id;
        public string key;
        public string secret;
        public string name;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("**** APPLICATION INFORMATION ****\n");
            builder.AppendFormat("NAME:       {0}\n", name);
            builder.AppendFormat("ID:         {0}\n", id);
            builder.AppendFormat("MYSPACE ID: {0}\n", myspaceId);
            builder.AppendFormat("KEY:        {0}\n", key);
            builder.AppendFormat("SECRET:     {0}\n", secret);
            builder.AppendFormat("*********************************\n");
            return builder.ToString();
        }
    }

    public static class TestConstants
    {
		public static readonly string BITLY_LOGIN = "<your bitly login here>";
        public static readonly string BITLY_API_KEY = "<your bitly api key here>";
        
		public static readonly string TWITTER_ACCESS_TOKEN = "<twitter access token>";
        public static readonly string TWITTER_ACCESS_SECRET = "<twitter access secret>";

        public static readonly string TWITTER_CONSUMER_KEY = "<your twitter application consumer key>";
        public static readonly string TWITTER_CONSUMER_SECRET = "<your twitter application consumer secret>";
        
        public static readonly string TWITTER_USERNAME_READ = "<enter a twitter username to read from here>";
		
        public static readonly string API_MS_CONSUMER_KEY = "<your myspace application consumer key>";
        public static readonly string API_MS_CONSUMER_SECRET = "<your myspace application consumer secret>";
		
        public static readonly string API_ACCESS_TOKEN = "<myspace access token>";
        public static readonly string API_ACCESS_TOKEN_SECRET = "<myspace access secret>";
        
		public static readonly string MYSPACE_TEST_EMAIL = "<your email address here>";
        public static readonly string MYSPACE_TEST_PASSWORD = "<your password here>";

        public static Dictionary<string, MySpaceUser> GetMySpaceUsers()
        {
            Dictionary<string, MySpaceUser> users = new Dictionary<string, MySpaceUser>();

            MySpaceUser user = new MySpaceUser();
			
			user.name = "tomatwork";
            user.id = 222553756;
            users[user.name] = user;

            user.name = "tom";
            user.id = 6221;
            users[user.name] = user;

            return users;
        }

        public static MdpApplication GetApplication()
        {
            MdpApplication app = new MdpApplication();

            app.key = "a94b8e4544394d54bd04c03d0a9c77f6";
            app.secret = "77b6cac8889c41dd840dd80bb04f1b0e1322cda7927d4b8ab3e24e5138f28586";
            app.name = "Heater.Social Unit Test Account";

            return app;
        }
    }
}
