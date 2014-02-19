using System;
using System.Collections.Generic;
using System.Linq;
using PlainElastic.Net.Serialization;
using PlainElastic.Net.Utils;

namespace PlainElastic.Net
{
    public class MultiSearchBuilder
    {
        private readonly IJsonSerializer serializer;

        public MultiSearchBuilder(IJsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string Search(object data, string index = null, string type=null, string options = null, string customOptions = null)
        {
            var parameters = BuildOperationParameters(index, type, options, customOptions);
            var command = "{{ {0} }}\n".AltQuoteF(parameters);
            var dataJson = data as string ?? serializer.Serialize(data);
            return command + dataJson + "\n";
        }

        public string BuildCollection<T>(IEnumerable<T> collection, Func<MultiSearchBuilder, T, string> multiSearchOperation)
        {
            return collection.Select(element => multiSearchOperation(this, element)).Join();
        }

        public IEnumerable<string> PipelineCollection<T>(IEnumerable<T> collection, Func<MultiSearchBuilder, T, string> mulitSearchOperation)
        {
            return collection.Select(element => mulitSearchOperation(this, element));
        }

        public static string BuildOperationParameters(string index, string type, string optionsJson, string customOptions)
        {
            var parameters = new [] {
                                        index.IsNullOrEmpty() ? "" : "\"index\": " + index.Quotate(),
                                        type.IsNullOrEmpty() ? "" : "\"type\" : " + type.Quotate(),
                                        optionsJson,
                                        customOptions
                                    };
            return parameters.Where(s => !s.IsNullOrEmpty()).JoinWithComma();
        }
    }
}
