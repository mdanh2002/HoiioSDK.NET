using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Perform basic HTTP POST to Hoiio server and parse the response. 
    /// Base class for most API calls.
    /// </summary>
    public class HttpService
    {
        public static String PARAM_APP_ID = "app_id";
	    public static String PARAM_ACCESS_TOKEN = "access_token";
	    public static String API_BASE_URL = "http://secure.hoiio.com/open/";
	    public static String INTERNAL_SERVER_EXCEPTION = "internal_server_exception";	    

        /// <summary>
        /// Send the actual HTTP POST to Hoiio servers.
        /// </summary>
        /// <param name="urlString">The Hoiio API service name to be called</param>
        /// <param name="fields">The fields to be posted.</param>
        public static Dictionary<String, Object> doHttpPost(string serviceString, System.Collections.Generic.Dictionary<string, string> fields)
        {
            WebRequest webRequest = WebRequest.Create(API_BASE_URL + serviceString);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            // parameters: name1=value1&name2=value2	
            StringBuilder sb = new StringBuilder();            
            foreach (KeyValuePair<String, String> field in fields)
            {
                sb.Append("&" + field.Key + "=" + System.Web.HttpUtility.UrlEncode(field.Value));
            }
            String postBody = sb.ToString().StartsWith("&") ? sb.ToString().Remove(0, 1) : sb.ToString();

            // send the Post
            byte[] bytes = Encoding.ASCII.GetBytes(postBody);
            Stream os = null;            
            webRequest.ContentLength = bytes.Length;   
            os = webRequest.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();

            // deserialize the response
            WebResponse webResponse = webRequest.GetResponse();            
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            String response = sr.ReadToEnd().Trim();
            Dictionary<String, Object> deserializedResponse = JsonConvert.DeserializeObject<Dictionary<String, Object>>(response);

            return deserializedResponse;
        }
 
        /// <summary>
        /// Return a dictionary with the provided appID and access token, ready to be posted to Hoiio API
        /// </summary>
        protected static Dictionary<String, String> initParam(String appId, String accessToken)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add(PARAM_APP_ID, appId);
            map.Add(PARAM_ACCESS_TOKEN, accessToken);

            return map;
        }
    }
}
