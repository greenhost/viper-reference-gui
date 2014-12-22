using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ViperClient
{
    class Api
    {
        private readonly Uri baseRequestUri = new Uri("http://localhost:8088");

        protected string GetResponseBody(HttpWebResponse response)
        {
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();
                return responseText;
            }
        }

        protected HttpWebResponse makeRequest(string uri, string method, string body)
        {
            Uri dest = new Uri(baseRequestUri, uri);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create( dest );
            req.ContentType = "text/json";
            req.Method = method;

            try
            {
                // if we have anything to submit in the body of the request
                if (string.Empty != body)
                {
                    using (var sw = new StreamWriter(req.GetRequestStream()))
                    {
                        sw.Write((string)body);
                    }
                }
            } catch (WebException ex) {
                return null;
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
        } // makeRequest

        protected HttpWebResponse makeRequest(string uri, string method)
        {
            return this.makeRequest(uri, method, string.Empty);
         } // makeRequest

        public bool OpenTunnel(string cfgfile, string logfile)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"config", cfgfile},
                {"log", logfile},
            };

            string body = JsonConvert.SerializeObject(data, Formatting.Indented);
            HttpWebResponse resp = this.makeRequest("/tunnel/open", "POST", body);

            if (HttpStatusCode.OK == resp.StatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CloseTunnel()
        {
            HttpWebResponse resp = this.makeRequest("/tunnel/close", "POST", null);

            if (HttpStatusCode.OK == resp.StatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    } // class

} // ns

/*
public static bool SendAnSMSMessage(string message)
{
    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.pennysms.com/jsonrpc");
    httpWebRequest.ContentType = "text/json";
    httpWebRequest.Method = "POST";

    var serializer = new Newtonsoft.Json.JsonSerializer();
    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    {
        using (var tw = new Newtonsoft.Json.JsonTextWriter(sw))
        {
             serializer.Serialize(tw, 
                 new {method= "send",
                      @params = new string[]{
                          "IPutAGuidHere", 
                          "msg@MyCompany.com",
                          "MyTenDigitNumberWasHere",
                          message
                      }});
        }
    }
    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    {
        var responseText = streamReader.ReadToEnd();
        //Now you have your response.
        //or false depending on information in the response
        return true;        
    }
}
*/
