using Checkout.Accounts.Payout.Response.Util;
using Checkout.Instruments.Create.Util;
using Checkout.Instruments.Get.Util;
using Checkout.Instruments.Update.Util;
using Checkout.Issuing.Cards.Requests;
using Checkout.Issuing.Cards.Responses;
using Checkout.Issuing.Controls.Requests;
using Checkout.Issuing.Controls.Responses;
using Checkout.Workflows.Actions.Response.Util;
using Checkout.Workflows.Conditions.Response.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Checkout
{
    public class JsonSerializer : ISerializer
    {
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
                    new CreateInstrumentResponseTypeConverter(),
                    new GetInstrumentResponseTypeConverter(),
                    new UpdateInstrumentResponseTypeConverter(),
                    // Workflows CS2
                    new WorkflowActionTypeResponseConverter(),
                    new WorkflowConditionTypeResponseConverter(),
                    GetConverterDateTimeToIso(),
                    // Accounts Payout Schedules
                    new GetScheduleResponseTypeConverter(),
                    new ScheduleResponseTypeConverter(),
                    // Items Response
                    new ItemsResponseConverter(),
                    // Issuing
                    new CardTypeRequestConverter(),
                    new CardTypeResponseConverter(),
                    new CardControlTypeRequestConverter(),
                    new CardControlTypeResponseConverter(),
                }
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
    }
}