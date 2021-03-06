﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQWebAPI.Library.DataAccess.DataFactory;

namespace RabbitMQWebAPI.Library.DataAccess
{
    public class Miscellaneous
    {
        private HttpClient _client;
        public Miscellaneous(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        ///  Various random bits of information that describe the whole system.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> GetOverview()
        {
            var result = await GetJsonString("/api/overview");

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(result.ToString());
        }

        /// <summary>
        /// Name identifying this RabbitMQ cluster.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetClusterName()
        {
            var result = await GetJsonString("api/cluster-name");

            return result.ToString();
        }

        /// <summary>
        /// Details of the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> WhoAmI()
        {
            var result = await GetJsonString("api/whoami");
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ToString());
        }

        /// <summary>
        /// Declares a test queue, then publishes and consumes a message. Intended for use by monitoring tools. If everything is working correctly, 
        /// will return HTTP status 200 with body:	
        /// </summary>
        /// <param name="vhost"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> AlivenessTest(string vhost)
        {
            vhost = WebUtility.UrlEncode(vhost);

            var result = await GetJsonString(String.Format("/api/aliveness-test/{0}", vhost));

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ToString());

        }


        /// <summary>
        /// Runs basic healthchecks in the current node. Checks that the rabbit application is running, channels and queues can be listed successfully, 
        /// and that no alarms are in effect.If everything is working correctly, will return HTTP status 200 with body:{"status":"ok"
        /// If something fails, will return HTTP status 200 with the body of
        /// {"status":"failed","reason":"string"}
        /// </summary>
        /// <returns></returns>

        public async Task<Dictionary<string, string>> HealthCheckCurrentNode()
        {
            var result =await GetJsonString("/api/healthchecks/node");

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ToString());
        }

        /// <summary>
        ///  Runs basic healthchecks in the given node. Checks that the rabbit application is running, list_channels and list_queues return, and that no alarms are raised. If everything is working correctly, will return HTTP status 200 with body:
        /// ';*{"status":"ok"
        /// If something fails, will return HTTP status 200 with the body of
        /// {"status":"failed","reason":"string"}
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> HealthcheckNode(string nodeName)
        {
            var result = await GetJsonString(String.Format("/api/healthchecks/node/{0}", nodeName));

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ToString());
        }


        private async Task<string> GetJsonString(string path)
        {
            string result;

            using (_client)
            {
                using (
                    var response =
                        await _client.GetAsync(path, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }
    }
}
