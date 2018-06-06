using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Checkout
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializer(Action<JsonSerializerSettings> configureSettings = null)
        {
            _serializerSettings = CreateSerializerSettings(configureSettings);
        }

        public string Serialize(object input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return JsonConvert.SerializeObject(input, _serializerSettings);
        }

        public object Deserialize(string input, Type objectType)
        {
            return JsonConvert.DeserializeObject(input, objectType, _serializerSettings);
        }

        public static JsonSerializerSettings CreateSerializerSettings(Action<JsonSerializerSettings> configureSettings)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Converters = new[] { new StringEnumConverter() }
            };

            if (configureSettings != null)
                configureSettings(settings);

            return settings;
        }
    }
}