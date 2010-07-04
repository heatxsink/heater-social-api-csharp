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
using System.Net;
using System.Web;
using Jayrock.Json;
using Heater.OAuth;

namespace Heater.Social
{
    public class Util
    {
        public static IWebProxy CreateWebProxy(string address)
        {
            WebProxy proxy = new WebProxy();
            Uri uri = new Uri(address);
            proxy.Address = uri;
            return proxy;
        }

        public static string HandleWebException(WebException ex)
        {
            StringBuilder builder = new StringBuilder();

            try
            {
                builder.AppendLine(ex.Message);
                if (ex.Response != null)
                {
                    builder.AppendLine();
                    
                    using (System.IO.Stream receiveStream = ex.Response.GetResponseStream())
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8))
                    {
                        builder.AppendLine(sr.ReadToEnd());
                    }
                    
                    builder.AppendLine();
                    builder.AppendFormat("Status Code: {0}", ((ex.Response) as HttpWebResponse).StatusCode);
                    builder.AppendLine();
                    builder.AppendFormat("Status Description: {0}", ((ex.Response) as HttpWebResponse).StatusDescription);
                    builder.AppendLine();
                }
            }
            catch (Exception exception)
            {
                builder.AppendLine(exception.ToString());
            }

            return builder.ToString();
        }

        public static string ListToJson(IList<string> list)
        {
            //StringBuilder builder = new StringBuilder();
            //using (JsonTextWriter w = new JsonTextWriter())
            //{
            //    w.WriteStartArray();
            //    foreach(string item in list)
            //    {
            //        w.WriteString(item);
            //    }
            //    w.WriteEndArray();
            //    builder.Append(w.ToString());
            //}
            //return builder.ToString();

            string mediaItemsValue = string.Empty;
            if (list != null)
            {
                StringBuilder mediaItemList = new StringBuilder();
                mediaItemList.Append('{');
                foreach (string mediaItem in list)
                {
                    mediaItemList.Append('"');
                    mediaItemList.Append(mediaItem);
                    mediaItemList.Append('"');
                    mediaItemList.Append(',');
                }
                mediaItemList.Replace(',', '}', mediaItemList.Length - 1, 1);
                mediaItemsValue = mediaItemList.ToString();
            }
            return mediaItemsValue;
        }
        
        public static string DictionaryToJson(IDictionary<string, string> dictionary)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");

            List<string> keys = dictionary.Keys.ToList<string>();
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                string value = dictionary[key];
                builder.AppendFormat("\"{0}\":\"{1}\"", key, value);
                if (i < keys.Count - 1)
                {
                    builder.Append(",");
                }
            }
            builder.Append("}");
            return builder.ToString();
        }
        
        public static OAuthTuple ParseQueryString(string queryString)
        {
            string[] tokens = queryString.Split('&');

            OAuthTuple tuple = new OAuthTuple();

            foreach (string token in tokens)
            {
                string[] parameters = token.Split('=');

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i > 0)
                    {
                        if (parameters[0] == "oauth_token")
                        {
                            tuple.token += HttpUtility.UrlDecode(parameters[i]);
                        }

                        if (parameters[0] == "oauth_token_secret")
                        {
                            tuple.tokenSecret += HttpUtility.UrlDecode(parameters[i]);
                        }
                    }
                }
            }
            return tuple;
        }

        public static Cookie GetMySpaceUserCookie(string email, string password, string domain)
        {
            var loginUrl = string.Format("https://secure.{0}/index.cfm?fuseaction=login.process", domain);

            List<PostParameter> parameters = new List<PostParameter>();
            parameters.Add(new PostParameter("__VIEWSTATE", "/wEPDwUJNTU4OTgyNDc1ZBgBBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WAQUnY3RsMDAkY3BNYWluJExvZ2luQm94JFJlbWVtYmVyX0NoZWNrYm94"));
            parameters.Add(new PostParameter("NextPage", ""));
            parameters.Add(new PostParameter("ctl00$cpMain$LoginBox$Email_Textbox", email));
            parameters.Add(new PostParameter("ctl00$cpMain$LoginBox$Password_Textbox", password));
            parameters.Add(new PostParameter("ctl00_cpMain_LoginBox_Remember_Checkbox", "on"));
            parameters.Add(new PostParameter("dlb", "Log In"));

            StringBuilder builder = new StringBuilder();

            foreach (PostParameter parameter in parameters)
            {
                builder.AppendFormat("{0}={1}&", parameter.Name, parameter.Value);
            }

            var data = builder.ToString().TrimEnd('&');

            var requestBody = Encoding.UTF8.GetBytes(data);

            var request = (HttpWebRequest)WebRequest.Create(loginUrl);
            request.Method = "POST";
            request.KeepAlive = false;
            request.UserAgent = "Mozilla/5.0";
            CookieContainer jar = new CookieContainer();
            request.CookieContainer = jar;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestBody.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Flush();
            }
			
			Cookie userCookie = new Cookie();
            foreach (Cookie cookie in jar.GetCookies(new Uri(string.Format("https://www.{0}", domain))))
            {
                if (cookie.Name.Equals("USER"))
                {
                    userCookie = cookie;
                }
            }
			
            return userCookie;
        }
    }

    public enum PostParameterType
    {
        File,
        Parameter
    };

    public class PostParameter
    {
        public string Name = null;
        public object Value = null;
        public PostParameterType Type = PostParameterType.Parameter;
        public string Filename = null;

        public PostParameter(string name, object value)
        {
            this.Name = name;
            this.Value = value;
            this.Type = PostParameterType.Parameter;
        }

        public PostParameter(string name, string filename, object value, PostParameterType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Filename = filename;
        }

        public static string GetOnlyFileName(string filename)
        {
            return System.IO.Path.GetFileName(filename);
        }
    }

}
