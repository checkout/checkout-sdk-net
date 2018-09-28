using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Checkout
{
    /// <summary>
    /// Serializer that provides serialization to and from JSON.
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Creates a new <see cref="JsonSerializer"/> that allows customization of the underlying
        /// JSON.NET serializer settings.
        /// </summary>
        /// <param name="configureSettings">An action to be run against the JSON.NET serializer settings.</param>
        public JsonSerializer(Action<JsonSerializerSettings> configureSettings = null)
        {
            _serializerSettings = CreateSerializerSettings(configureSettings);
        }

        /// <summary>
        /// Serializes the provided <paramref="input"/> to JSON.
        /// </summary>
        /// <param name="input">The input to serialize.</param>
        /// <returns>The input serialized as JSON.</returns>
        public string Serialize(object input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return JsonConvert.SerializeObject(input, _serializerSettings);
        }

        /// <summary>
        /// Deserializes the provided JSON <paramref="input"/> to the specified <paramref="objectType"/>.
        /// </summary>
        /// <param name="input">The JSON input to deserialize.</param>
        /// <param name="objectType">The object type to deserialize to.</param>
        /// <returns>The deserialized object.</returns>
        public object Deserialize(string input, Type objectType)
        {
            return JsonConvert.DeserializeObject(input, objectType, _serializerSettings);
        }

        private static JsonSerializerSettings CreateSerializerSettings(Action<JsonSerializerSettings> configureSettings)
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