#if NET5_0_OR_GREATER
using Checkout.Instruments.Four.Get.Util;
using Checkout.Instruments.Four.Update.Util;
using Checkout.Workflows.Four.Actions.Response.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout
{
    public class JsonSerializer : ISerializer
    {

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
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,object>>(payload, _serializerSettings);
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

    }
}
#endif