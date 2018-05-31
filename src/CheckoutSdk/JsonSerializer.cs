using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Checkout
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializer(JsonSerializerSettings serializerSettings = null)
        {
            _serializerSettings = serializerSettings ?? CreateSerializerSettings();
        }

        public string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input, _serializerSettings);
        }

        public T Deserialize<T>(string input)
        {
            return (T)JsonConvert.DeserializeObject(input, typeof(T), _serializerSettings);
        }

        public static JsonSerializerSettings CreateSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Converters = new[] { new StringEnumConverter() }
            };
        }

        public object Deserialize(string input, Type objectType)
        {
            return JsonConvert.DeserializeObject(input, objectType, _serializerSettings);
        }
    }
}