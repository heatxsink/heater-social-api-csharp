﻿// The MIT License
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
    public class DirectMessage
    {
        [JsonMemberName("id")]
        public long Id { get; set; }

        [JsonMemberName("sender_id")]
        public long SenderId { get; set; }

        [JsonMemberName("text")]
        public string Text { get; set; }

        [JsonMemberName("recipient_id")]
        public long RecipientId { get; set; }

        [JsonMemberName("created_at")]
        public string CreatedAt { get; set; }

        [JsonMemberName("sender_screen_name")]
        public string SenderScreenName { get; set; }

        [JsonMemberName("recipient_screen_name")]
        public string RecipientScreenName { get; set; }

        [JsonMemberName("sender")]
        public User Sender { get; set; }

        [JsonMemberName("recipient")]
        public User Recipient { get; set; }
    }
}
