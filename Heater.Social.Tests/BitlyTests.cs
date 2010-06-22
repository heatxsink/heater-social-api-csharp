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

using Heater.Social.Bitly;
using Heater.Social.Bitly.DataContracts;
using NUnit.Framework;
using System;

namespace Heater.Social.Tests
{
    [TestFixture]
    public class BitlyTests
    {
        private string BITLY_LOGIN = "heatxsink";
        private string BITLY_API_KEY = "R_a1b0e0940029c19c492af4e6720602b9";

        [Test]
        public void ShortenTest()
        {
            BitlyApi api = new BitlyApi(BITLY_LOGIN, BITLY_API_KEY);
            ShortenResponse response = api.Shorten("http://www.heatxsink.com");
            Console.WriteLine(response.ToJson());
        }
    }
}
