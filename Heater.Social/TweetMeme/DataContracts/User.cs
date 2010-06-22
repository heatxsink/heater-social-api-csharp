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

using Jayrock.Json.Conversion;

namespace Heater.Social.TweetMeme.DataContracts
{
    public class User
    {
        [JsonMemberName("id")]
        public string Id { get; set; }

        [JsonMemberName("tweeter")]
        public string Username { get; set; }

        [JsonMemberName("url")]
        public string ImageUrl { get; set; }

        [JsonMemberName("thumb_url")]
        public string ImageThumbnailUrl { get; set; }

        [JsonMemberName("location")]
        public string Location { get; set; }

        [JsonMemberName("ext_location")]
        public string LocationExtended { get; set; }

        [JsonMemberName("ext_name")]
        public string Name { get; set; }

        [JsonMemberName("ext_bio")]
        public string Bio { get; set; }

        [JsonMemberName("ext_url")] 
        public string ExternalUrl { get; set; }

        [JsonMemberName("ext_follower_count")]
        public int Followers { get; set; }

        [JsonMemberName("ext_following_count")]
        public int Following { get; set; }

        [JsonMemberName("score")]
        public double Score { get; set; }

        [JsonMemberName("kudos")]
        public double Kudos { get; set; }

        [JsonMemberName("ext_created_at")]
        public string Created { get; set; }

        [JsonMemberName("profile_updated_at")]
        public string Updated { get; set; }
    }
}
