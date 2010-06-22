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
using System.Text;
using Heater.Social.TweetMeme.DataContracts;
using Jayrock.Json.Conversion;

namespace Heater.Social
{
    public class TweetMemeApi : AbstractApiBase
    {
        private static string API_DOMAIN = "api.tweetmeme.com";

        public TweetMemeApi()
        {
            ApiDomain = API_DOMAIN;
            Timeout = DEFAULT_TIMEOUT;
        }

        public Tweet[] GetTweets(string url)
        {
            var page = 1;
            var uri = new Uri(url);
            var requestUrl = string.Format("http://{0}/stories/tweets.json?url={1}&page={2}", ApiDomain, uri.ToString(), page);
            Console.WriteLine(requestUrl);
            var response = HttpGet(requestUrl);
            Console.WriteLine(response);
            var data = (TweetsResult)JsonConvert.Import(typeof(TweetsResult), response);
            return data.Tweets;
        }
    }
}
