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
    public class Address
    {
        [JsonMemberName("country")]
        public string Country { get; set; }

        [JsonMemberName("extendedAddress")]
        public string ExtendedAddress { get; set; }

        [JsonMemberName("latitude")]
        public string Latitude { get; set; }

        [JsonMemberName("longitude")]
        public string Longitude { get; set; }

        [JsonMemberName("locality")]
        public string Locality { get; set; }

        [JsonMemberName("poBox")]
        public string PoBox { get; set; }

        [JsonMemberName("postalCode")]
        public string PostalCode { get; set; }

        [JsonMemberName("primary")]
        public string Primary { get; set; }

        [JsonMemberName("region")]
        public string Region { get; set; }

        [JsonMemberName("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonMemberName("type")]
        public string Type { get; set; }

        [JsonMemberName("formatted")]
        public string Formatted { get; set; }
    }
}
