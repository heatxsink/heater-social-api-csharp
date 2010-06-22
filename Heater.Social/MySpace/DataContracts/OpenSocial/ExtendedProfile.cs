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

namespace Heater.Social.MySpace.DataContracts.OpenSocial
{
    public class BasicProfile
    {
        [JsonMemberName("image")]
        public string Image { get; set; }

        [JsonMemberName("largeImage")]
        public string LargeImage { get; set; }

        [JsonMemberName("lastUpdatedDate")]
        public string LastUpdatedDate { get; set; }

        [JsonMemberName("name")]
        public string Name { get; set; }

        [JsonMemberName("uri")]
        public string Uri { get; set; }

        [JsonMemberName("userId")]
        public long UserId { get; set; }

        [JsonMemberName("webUri")]
        public string WebUri { get; set; }
    }

    public class FullProfile
    {
        [JsonMemberName("aboutme")]
        public string AboutMe { get; set; }

        [JsonMemberName("age")]
        public int Age { get; set; }

        [JsonMemberName("basicprofile")]
        public BasicProfile BasicProfile { get; set; }

        [JsonMemberName("city")]
        public string City { get; set; }

        [JsonMemberName("country")]
        public string Country { get; set; }

        [JsonMemberName("culture")]
        public string Culture { get; set; }

        [JsonMemberName("gender")]
        public string Gender { get; set; }

        [JsonMemberName("hometown")]
        public string Hometown { get; set; }
        
        [JsonMemberName("maritalstatus")]
        public string MaritalStatus { get; set; }

        [JsonMemberName("postalcode")]
        public string PostalCode { get; set; }

        [JsonMemberName("region")]
        public string Region { get; set; }
    }

    public class ExtendedProfile
    {
        [JsonMemberName("books")]
        public string Books { get; set; }

        [JsonMemberName("desiretomeet")]
        public string DesireToMeet { get; set; }

        [JsonMemberName("fullprofile")]
        public FullProfile FullProfile { get; set; }

        [JsonMemberName("headline")]
        public string Headline { get; set; }

        [JsonMemberName("heroes")]
        public string Heroes { get; set; }

        [JsonMemberName("interests")]
        public string Interests { get; set; }

        [JsonMemberName("mood")]
        public string Mood { get; set; }

        [JsonMemberName("movies")]
        public string Movies { get; set; }

        [JsonMemberName("music")]
        public string Music { get; set; }

        [JsonMemberName("occupation")]
        public string Occupation { get; set; }

        [JsonMemberName("status")]
        public string Status { get; set; }

        [JsonMemberName("television")]
        public string Television { get; set; }

        [JsonMemberName("type")]
        public string Type { get; set; }

        [JsonMemberName("zodiacsign")]
        public string ZodiacSign { get; set; }
    }
}