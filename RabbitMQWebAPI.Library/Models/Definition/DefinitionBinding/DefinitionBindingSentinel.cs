﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQWebAPI.Library.Models.BaseModel;
using RabbitMQWebAPI.Library.Models.Sentinel;

namespace RabbitMQWebAPI.Library.Models.Definition.DefinitionBinding
{
    public class DefinitionBindingSentinel : Sentinel<DefinitionBinding>
    {
        public override IModel ParseDictionaryToParameters(IDictionary<String, Object> parametersDictionary)
        {
            DefinitionBinding parameters = new DefinitionBinding();;
            parameters.name = parametersDictionary["name"].ToString();
            parameters.vhost = parametersDictionary["vhost"].ToString();
            parameters.type = parametersDictionary["type"].ToString();
            parameters.durable = Boolean.Parse(parametersDictionary["durable"].ToString());
            parameters.auto_delete = Boolean.Parse(parametersDictionary["auto_delete"].ToString());
            parameters._internal = Boolean.Parse(parametersDictionary["internal"].ToString());
            parameters.arguments =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(parametersDictionary["arguments"].ToString());

            return parameters;
        }

    }
}
