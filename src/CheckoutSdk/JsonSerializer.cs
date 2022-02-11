using Checkout.Instruments.Four.Get.Util;
using Checkout.Instruments.Four.Update.Util;
using Checkout.Workflows.Four.Actions.Response.Util;
using Checkout.Workflows.Four.Conditions.Response.Util;
using System;
using System.Collections.Generic;

#if NET5_0_OR_GREATER
using System.Text.Json;
using System.Text.Json.Serialization;

#else 
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
#endif

namespace Checkout
{
    public class JsonSerializer : ISerializer
    {
#if NET5_0_OR_GREATER

        private readonly JsonSerializerOptions _serializerSettings;

        public JsonSerializer(Action<JsonSerializerOptions> configureSettings = null)
        {
            _serializerSettings = CreateSerializerSettings(configureSettings);
        }


        public object Deserialize(string payload, Type objectType)
        {
            CheckoutUtils.ValidateParams("payload", payload);
            return System.Text.Json.JsonSerializer.Deserialize(payload, objectType, _serializerSettings);
        }

        public IDictionary<string, object> Deserialize(string payload)
        {
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload, _serializerSettings);
        }

        public string Serialize(object payload)
        {
            CheckoutUtils.ValidateParams("payload", payload);
            return System.Text.Json.JsonSerializer.Serialize(payload, payload.GetType(), _serializerSettings);
        }

        private static JsonSerializerOptions CreateSerializerSettings(Action<JsonSerializerOptions> configureSettings)
        {
            var settings = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            settings.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));

            settings.Converters.Add(new GetInstrumentResponseConverter());
            settings.Converters.Add(new UpdateInstrumentResponseConverter());
            settings.Converters.Add(new WorkflowActionResponseConverter());
            settings.Converters.Add(new WorkflowActionResponseConverter());


            configureSettings?.Invoke(settings);

            return settings;
        }


#else
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializer(Action<JsonSerializerSettings> configureSettings = null)
        {
            _serializerSettings = CreateSerializerSettings(configureSettings);
        }

        public string Serialize(object payload)
        {
            CheckoutUtils.ValidateParams("payload", payload);
            return JsonConvert.SerializeObject(payload, _serializerSettings);
        }

        public object Deserialize(string payload, Type objectType)
        {
            CheckoutUtils.ValidateParams("payload", payload);
            return JsonConvert.DeserializeObject(payload, objectType, _serializerSettings);
        }

        public IDictionary<string, object> Deserialize(string payload)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
        }

        private static JsonSerializerSettings CreateSerializerSettings(Action<JsonSerializerSettings> configureSettings)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                Converters = new JsonConverter[]
                {
                    new StringEnumConverter(),
                    // Instruments CS2
                    new GetInstrumentResponseTypeConverter(), new UpdateInstrumentResponseTypeConverter(),
                    // Workflows CS2
                    new WorkflowActionTypeResponseConverter(), new WorkflowConditionTypeResponseConverter(),
                    GetConverterDateTimeToIso()
                },
            };

            configureSettings?.Invoke(settings);

            return settings;
        }

        private static IsoDateTimeConverter GetConverterDateTimeToIso()
        {
            IsoDateTimeConverter converter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK"
            };

            return converter;
        }    
#endif
    }
}