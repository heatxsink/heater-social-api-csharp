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
		public static readonly string BITLY_LOGIN = "heatxsink";
        public static readonly string BITLY_API_KEY = "R_a1b0e0940029c19c492af4e6720602b9";
        
		public static readonly string TWITTER_ACCESS_TOKEN = "62468094-cL9fcmxniVm03MFAtFpdWeTHdyy36VqiGyxwxwkU";
        public static readonly string TWITTER_ACCESS_SECRET = "5dOVprzdGQZhHpYRFDiAZTCW4GEtK1UYd72THW7xkc";

        public static readonly string TWITTER_CONSUMER_KEY = "udOtwbB709Ya0lil02dhQ";
        public static readonly string TWITTER_CONSUMER_SECRET = "lZS22Xw4oBbRRywDAkjRfv0IKDu1Tm1LIK3HEAMEm4";
        
        public static readonly string TWITTER_USERNAME_READ = "myspaceevents";
		
        public static readonly string API_MS_CONSUMER_KEY = "217ef0b423ed48b0bdacc83db0617ea8";
        public static readonly string API_MS_CONSUMER_SECRET = "cb62fb90374c44a7a179eef4009bf0de";
        public static readonly string API_ACCESS_TOKEN = "2MESAGRG+p89AAM0RCDaMEofpmfS6ppWFvtSkLY5g/oAKQ8nLSUPAWEa/+ZFHISvYgRx8DwHwc4/+Ct5FHkHrX/y/ENyEvTLf0Tj+yE2OyA=";
        public static readonly string API_ACCESS_TOKEN_SECRET = "cf1abcae72d944d1b6e1f2c7cfda858a";
        public static readonly string MYSPACE_TEST_EMAIL = "christian@slingshotlabs.com";
        public static readonly string MYSPACE_TEST_PASSWORD = "myspace1";

        public static Dictionary<string, MySpaceUser> GetMySpaceUsers()
        {
            Dictionary<string, MySpaceUser> users = new Dictionary<string, MySpaceUser>();

            MySpaceUser user = new MySpaceUser();

            user.name = "donny";
            user.id = 20599042;
            users[user.name] = user;

            user.name = "nick";
            user.id = 25563666;
            users[user.name] = user;

            user.name = "stanleythedog";
            user.id = 16901774;
            users[user.name] = user;

            user.name = "tomatwork";
            user.id = 222553756;
            users[user.name] = user;

            user.name = "tomtom";
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
