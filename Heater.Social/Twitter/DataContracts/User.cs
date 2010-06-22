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

namespace Heater.Social.Twitter.DataContracts
{
    public class User
    {
        [JsonMemberName("friends_count")]
        public long FriendsCount { get; set; }

        [JsonMemberName("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonMemberName("description")]
        public string Description { get; set; }

        [JsonMemberName("utc_offset")]
        public int UtcOffset { get; set; }

        [JsonMemberName("statuses_count")]
        public long StatusesCount { get; set; }

        [JsonMemberName("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonMemberName("screen_name")]
        public string ScreenName { get; set; }

        [JsonMemberName("notifications")]
        public bool Notifications { get; set; }

        [JsonMemberName("profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }
        
        [JsonMemberName("favourites_count")]
        public long FavouritesCount { get; set; }

        [JsonMemberName("time_zone")]
        public string TimeZone { get; set; }

        [JsonMemberName("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonMemberName("url")]
        public string Url { get; set; }

        [JsonMemberName("name")]
        public string Name { get; set; }

        [JsonMemberName("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }
        
        [JsonMemberName("created_at")]
        public string CreatedAt { get; set; }

        [JsonMemberName("protected")]
        public bool Protected { get; set; }

        [JsonMemberName("verified")]
        public bool Verified { get; set; }

        [JsonMemberName("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonMemberName("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonMemberName("following")]
        public bool Following { get; set; }

        [JsonMemberName("followers_count")]
        public long FollowersCount { get; set; }

        [JsonMemberName("location")]
        public string Location { get; set; }

        [JsonMemberName("id")]
        public long Id { get; set; }

        [JsonMemberName("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonMemberName("status")]
        public Status Status { get; set; }
    }
}
