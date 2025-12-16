using Checkout.Accounts.Payout.Response.Util;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.Util;
using Checkout.Instruments.Create.Util;
using Checkout.Instruments.Get.Util;
using Checkout.Instruments.Update.Util;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Controls.Responses;
using Checkout.Workflows.Actions.Response.Util;
using Checkout.Workflows.Conditions.Response.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using CardTypeResponseConverter = Checkout.Issuing.Common.Responses.CardTypeResponseConverter;

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
                    new CreateInstrumentResponseTypeConverter(), new GetInstrumentResponseTypeConverter(),
                    new UpdateInstrumentResponseTypeConverter(),
                    // Workflows CS2
                    new WorkflowActionTypeResponseConverter(), new WorkflowConditionTypeResponseConverter(),
                    // Short date format converter (must come before IsoDateTimeConverter)
                    new ShortDateTimeConverter(),
                    GetConverterDateTimeToIso(),
                    // Accounts Payout Schedules
                    new GetScheduleResponseTypeConverter(), new ScheduleResponseTypeConverter(),
                    // Items Response
                    new ItemsResponseConverter(),
                    // Issuing
                    new CardTypeResponseConverter(), new CardControlsResponseConverter(),
                    new CardCreateResponseConverter(), new CardControlTypeResponseConverter(),
                    // HandlePaymentsAndPayouts Sources
                    new RequestAPaymentOrPayoutResponseCreatedSourceTypeConverter(),
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

        private class ShortDateTimeConverter : JsonConverter
        {
            private const string DateTimeFormat = "yyyy-MM-dd";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
            }

            public override bool CanRead => true;
            public override bool CanWrite => false;

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    if (objectType == typeof(DateTime?))
                        return null;
                    else
                        throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
                }

                var dateString = reader.Value?.ToString();
                if (dateString == null)
                    return reader.Value; // Already parsed by another converter

                // Only handle short date format (yyyy-MM-dd)
                if (dateString.Length == 10 && dateString.Count(c => c == '-') == 2)
                {
                    if (DateTime.TryParseExact(dateString, DateTimeFormat, null, System.Globalization.DateTimeStyles.None, out var result))
                        return result;
                }

                // Let the default converter handle other formats
                return JsonConvert.DeserializeObject($"\"{dateString}\"", objectType);
            }

            public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                throw new NotSupportedException("ShortDateTimeConverter should not handle writing.");
            }
        }
    }
}