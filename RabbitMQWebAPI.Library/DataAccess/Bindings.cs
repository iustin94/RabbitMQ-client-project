﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQWebAPI.Library.DataAccess.DataFactory;
using RabbitMQWebAPI.Library.Models.BaseModel;
using RabbitMQWebAPI.Library.Models.Binding;

namespace RabbitMQWebAPI.Library.DataAccess
{
    public class Bindings
    {
        private DataFactory<Binding> dataFactory;
        private BindingSentinel sentinel;

        public Bindings() { }
        public Bindings(HttpClient client)
        {
            //TODO validate client has necessary credentials
            dataFactory = new DataFactory<Binding>(client);
            sentinel = new BindingSentinel();
        }

        /// <summary>
        /// A list of all bindings.
        /// </summary>
        /// <returns></returns>
      

        ///// <summary>
        ///// A list of all bindings in a given virtual host.
        ///// </summary>
        ///// <param name="vhost"></param>
        ///// <returns></returns>
        //public static async Task<IEnumerable<BindingInfo>> GetBindingInfosForVhost(string vhost)
        //{
        //    return await GetBindingInfosForVhostInternal(vhost);
        //}

        ///// <summary>
        ///// A list of all bindings between an exchange and a queue. Remember, an exchange and a queue can be bound together many times! To create a new binding, POST to this URI. You will need a body looking something like this:
        ///// </summary>
        ///// <param name="exchange"></param>
        ///// <param name="queue"></param>
        ///// <param name="vhost"></param>
        ///// <returns></returns>
        //public static async Task<IEnumerable<BindingInfo>> GetBindingInfosBetweenExchangeAndQueueOnVhost(string exchange,
        //    string queue, string vhost)
        //{
        //    return await GetBindingInfosBetweenExchangeAndQueueOnVhostInternal(exchange, queue, vhost);
        //}

        ///// <summary>
        ///// A list of all bindings between two exchanges. Similar to the list of all bindings between an exchange and a queue, above.
        ///// </summary>
        ///// <param name="vhost"></param>
        ///// <param name="sourceExchange"></param>
        ///// <param name="destinationExchange"></param>
        ///// <returns></returns>
        //public static async Task<IEnumerable<BindingInfo>> GetBindingInfosBetweenTwoExchangesOnVhost(string vhost,
        //    string sourceExchange, string destinationExchange)
        //{
        //    return await GetBindingInfosBetweenTwoExchangesOnVhostInternal(vhost, sourceExchange, destinationExchange);
        //}

        ///// <summary>
        ///// A list of all bindings in which a given exchange is the source.
        ///// </summary>
        ///// <returns></returns>
        //public static async Task<IEnumerable<BindingInfo>> GetBindingInfosForExchangeOnVhost(string vhost, string name)
        //{
        //    return await GetBindingInfosForExchangeOnVhostInternal(vhost, name);
        //}

        public async Task<IEnumerable<Binding>> GetBindingInfosToExchangeOnVhost(string vhost, string name)
        {
            vhost = WebUtility.UrlEncode(vhost);
            name = WebUtility.UrlEncode(name);

          return
                await dataFactory.BuildModels(String.Format("api/exchanges/{0}/{1}/bindings/destination", vhost, name), this.sentinel);
        }

        // /api/bindings
        public async Task<IEnumerable<Binding>> GetBindingInfos()
        {
            List<Binding> bindings = await dataFactory.BuildModels("api/bindings", this.sentinel);

            return bindings;
        }


        ///*  /api/bindings/vhost 
        // * A list of all bindings in a given virtual host.
        // * */

        //private static async Task<IEnumerable<BindingInfo>> GetBindingInfosForVhostInternal(string vhost)
        //{
        //    vhost = WebUtility.UrlEncode(vhost);
        //    string result = await RMApiProvider.GetJson(String.Format("bindings/{1}", vhost));

        //    List<BindingInfo> bindings = new List<BindingInfo>();

        //    JArray info = JsonConvert.DeserializeObject<JArray>(result);

        //    foreach (JObject bindingData in info)
        //    {
        //        BindingInfoSentinel sentinel = new BindingInfoSentinel();
        //        BindingInfo binding =
        //            sentinel.CreateModel(
        //                JsonConvert.DeserializeObject<Dictionary<string, object>>(bindingData.ToString()));
        //        bindings.Add(binding);
        //    }

        //    return bindings;
        //}

        ///*  /api/bindings/vhost/e/exchange/q/queue
        // * A list of all bindings between an exchange and a queue. Remember, an exchange and a queue can be bound together many times! 
        // * To create a new binding, POST to this URI. You will need a body looking something like this:
        // * 
        // * EX: {"routing_key":"my_routing_key","arguments":{}}
        // *
        // * All keys are optional. The response will contain a Location header telling you the URI of your new binding.
        // */

        //private static async Task<IEnumerable<BindingInfo>> GetBindingInfosBetweenExchangeAndQueueOnVhostInternal(
        //    string exchange, string queue, string vhost)
        //{
        //    vhost = WebUtility.UrlEncode(vhost);
        //    exchange = WebUtility.UrlEncode(exchange);
        //    queue = WebUtility.UrlEncode(queue);

        //    string result =
        //        await RMApiProvider.GetJson(String.Format("bindings/{0}/e/{1}/q/{2}", vhost, exchange, queue));

        //    List<BindingInfo> bindings = new List<BindingInfo>();

        //    JArray info = JsonConvert.DeserializeObject<JArray>(result);

        //    foreach (JObject bindingData in info)
        //    {
        //        BindingInfoSentinel sentinel = new BindingInfoSentinel();
        //        BindingInfo binding =
        //            sentinel.CreateModel(
        //                JsonConvert.DeserializeObject<Dictionary<string, object>>(bindingData.ToString()));

        //        bindings.Add(binding);
        //    }

        //    return bindings;
        //}



        ///* /api/bindings/vhost/e/source/e/destination	
        // * A list of all bindings between two exchanges. Similar to the list of all bindings between an exchange and a queue, above.
        // */
        //private static async Task<IEnumerable<BindingInfo>> GetBindingInfosBetweenTwoExchangesOnVhostInternal(string vhost,
        //    string sourceExchange, string destinationExchange)
        //{
        //    vhost = WebUtility.UrlEncode(vhost);
        //    sourceExchange = WebUtility.UrlEncode(sourceExchange);
        //    destinationExchange = WebUtility.UrlEncode(destinationExchange);

        //    string result =
        //        await
        //            RMApiProvider.GetJson(String.Format("bindings/{0}/e/{1}/e/{2}	", vhost, sourceExchange,
        //                destinationExchange));

        //    List<BindingInfo> bindings = new List<BindingInfo>();

        //    JArray info = JsonConvert.DeserializeObject<JArray>(result);

        //    foreach (JObject bindingData in info)
        //    {
        //        BindingInfoSentinel sentinel = new BindingInfoSentinel();
        //        BindingInfo binding =
        //            sentinel.CreateModel(
        //                JsonConvert.DeserializeObject<Dictionary<string, object>>(bindingData.ToString()));

        //        bindings.Add(binding);
        //    }

        //    return bindings;
        //}

        //private static async Task<IEnumerable<BindingInfo>> GetBindingInfosForExchangeOnVhostInternal(string vhost,
        //    string name)
        //{
        //    vhost = WebUtility.UrlEncode(vhost);
        //    name = WebUtility.UrlEncode(name);

        //    string result =
        //        await RMApiProvider.GetJson(String.Format("/api/exchanges/{0}/{1}/bindings/source", vhost, name));

        //    List<BindingInfo> bindings = new List<BindingInfo>();

        //    JArray info = JsonConvert.DeserializeObject<JArray>(result);

        //    foreach (JObject bindingData in info)
        //    {
        //        BindingInfoSentinel sentinel = new BindingInfoSentinel();
        //        BindingInfo binding =
        //            sentinel.CreateModel(
        //                JsonConvert.DeserializeObject<Dictionary<string, object>>(bindingData.ToString()));

        //        bindings.Add(binding);
        //    }

        //    return bindings;
        //}

        ///* TODO /api/bindings/vhost/e/source/e/destination/props	
        // * An individual binding between two exchanges. Similar to the individual binding between an exchange and a queue, above.
        // */

        ///* TODO /api/bindings/vhost/e/exchange/q/queue/props	
        // * 
        // * An individual binding between an exchange and a queue. The props part of the URI is a "name" for the binding composed of its routing key and a hash of its arguments.
        // */

    }
}
