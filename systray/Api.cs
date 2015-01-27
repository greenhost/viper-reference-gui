using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ViperClient
{
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class TunnelStatus
    {
        [JsonProperty("state")]
        public string state;
        [JsonProperty("policies")]
        public List<string> policies;
    }

    public class ServiceNotRunning : ApiException
    {
        public ServiceNotRunning()
        {
        }

        public ServiceNotRunning(string message)
            : base(message)
        {
        }

        public ServiceNotRunning(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

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
            req.ContentType = "application/json";
            req.Method = method;

            // if we have anything to submit in the body of the request
            if ( (string.Empty != body) && (null != body) )
            {
                using (var sw = new StreamWriter(req.GetRequestStream()))
                {
                    sw.Write((string)body);
                }
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

            try
            {
                // create request
                string body = JsonConvert.SerializeObject(data, Formatting.None);
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
            catch (WebException ex)
            {
                HttpWebResponse error = ex.Response as HttpWebResponse;
                if ( (null != error) && (error.StatusCode == HttpStatusCode.ServiceUnavailable) )
                {
                    return false;
                }
                else
                {
                    throw new ServiceNotRunning("Viper service doesn't seem to respond to requests", ex);
                }
            }
        }

        private bool PolicyRequest(string uri, string policyName)
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"name", policyName},
            };

            try
            {
                // create request
                string body = JsonConvert.SerializeObject(data, Formatting.None);
                HttpWebResponse resp = this.makeRequest(uri, "POST", body);

                if (HttpStatusCode.OK == resp.StatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse error = ex.Response as HttpWebResponse;
                if ((null != error) && (error.StatusCode == HttpStatusCode.ServiceUnavailable))
                {
                    return false;
                }
                else
                {
                    throw new ServiceNotRunning("Viper service doesn't seem to respond to requests", ex);
                }
            }
        }

        public bool EnablePolicy(string pname)
        {
            return PolicyRequest("/policy/enable", pname);
        }

        public bool DisablePolicy(string pname)
        {
            return PolicyRequest("/policy/disable", pname);
        }

        public TunnelStatus TunnelStatus()
        {
            try
            {
                HttpWebResponse resp = this.makeRequest("/tunnel/status", "GET", null);
                if (HttpStatusCode.OK == resp.StatusCode)
                {
                    using (var reader = new StreamReader(resp.GetResponseStream()))
                    {
                        var txt = reader.ReadToEnd();
                        TunnelStatus obj = (TunnelStatus)JsonConvert.DeserializeObject<TunnelStatus>(txt);
                        return obj;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse error = ex.Response as HttpWebResponse;
                if ((null != error) && (error.StatusCode == HttpStatusCode.ServiceUnavailable))
                {
                    return null;
                }
                else
                {
                    throw new ServiceNotRunning("Viper service doesn't seem to respond to requests", ex);
                }
            }
        } // TunnelStatus()

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
