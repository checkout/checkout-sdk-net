using Checkout.Accounts.Payout.Response.Util;
using Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.Util;
using Checkout.Instruments.Create.Util;
using Checkout.Instruments.Get.Util;
using Checkout.Instruments.Update.Util;
using Checkout.Issuing.Cards.Responses.Create;
using Checkout.Issuing.Controls.Responses;
using Checkout.Issuing.ControlGroups.Common.Util;
using Checkout.Metadata.Card.Source;
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
                    new ToleratingStringEnumConverter(),
                    // Instruments CS2
                    new CreateInstrumentResponseTypeConverter(), 
                    new GetInstrumentResponseTypeConverter(),
                    new UpdateInstrumentResponseTypeConverter(),
                    // Workflows CS2
                    new WorkflowActionTypeResponseConverter(),
                    new WorkflowConditionTypeResponseConverter(),
                    // Short date format converter (must come before IsoDateTimeConverter).
                    // Read-only here on purpose: it matches every DateTime, so writing
                    // yyyy-MM-dd globally would truncate the format: date-time properties
                    // too. Date-only properties opt in with
                    // [JsonConverter(typeof(ShortDateTimeConverter))].
                    new ShortDateTimeConverter(canWrite: false),
                    GetConverterDateTimeToIso(),
                    // Accounts Payout Schedules
                    new GetScheduleResponseTypeConverter(),
                    new ScheduleResponseTypeConverter(),
                    // Items Response
                    new ItemsResponseConverter(),
                    // Issuing
                    new CardTypeResponseConverter(), 
                    new CardControlsResponseConverter(),
                    new CardCreateResponseConverter(),
                    new CardControlTypeResponseConverter(),
                    new ControlGroupControlTypeConverter(),
                    // HandlePaymentsAndPayouts Sources
                    new RequestAPaymentOrPayoutResponseCreatedSourceTypeConverter(),
                    // Metadata Card Sources
                    new CardMetadataRequestSourceConverter(),
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

        private class ToleratingStringEnumConverter : StringEnumConverter
        {
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                Newtonsoft.Json.JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                    return null;

                try
                {
                    return base.ReadJson(reader, objectType, existingValue, serializer);
                }
                catch (JsonSerializationException)
                {
                    // Unknown enum value from the API — return null for nullable types rather than crashing.
                    return Nullable.GetUnderlyingType(objectType) != null ? null : existingValue;
                }
            }
        }
    }

    /// <summary>
    /// Handles the properties the Checkout.com specification declares as
    /// <c>"type": "string", "format": "date"</c> (yyyy-MM-dd) rather than
    /// <c>"format": "date-time"</c> (RFC 3339).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Used in two modes, because reading and writing have different blast radii:
    /// </para>
    /// <list type="bullet">
    /// <item><b>Globally, read-only</b> (<c>new ShortDateTimeConverter(canWrite: false)</c> in
    /// <see cref="JsonSerializer"/>). This is load-bearing: <c>IsoDateTimeConverter</c> with the
    /// SDK's fixed <c>yyyy-MM-ddTHH:mm:ssK</c> format throws on a date-only string, so without
    /// this every date-only response value would fail to deserialize.</item>
    /// <item><b>Per property, read and write</b>
    /// (<c>[JsonConverter(typeof(ShortDateTimeConverter))]</c>). Writing must be opt-in because
    /// <see cref="CanConvert"/> matches every <see cref="DateTime"/>: enabling it globally would
    /// also truncate the genuine <c>format: date-time</c> properties.</item>
    /// </list>
    /// <para>
    /// Before the write mode existed, every <see cref="DateTime"/> serialized through
    /// <c>IsoDateTimeConverter</c>, so a date-only property emitted <c>2026-10-01T00:00:00</c>.
    /// Tamara rejects that on <c>processing.accommodation_data.check_in_date</c> and the merchant
    /// sees a gateway 500.
    /// </para>
    /// </remarks>
    public class ShortDateTimeConverter : JsonConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd";

        private readonly bool _canWrite;

        /// <summary>
        /// Creates a converter that both reads and writes yyyy-MM-dd. This is the constructor
        /// <c>[JsonConverter(typeof(ShortDateTimeConverter))]</c> uses.
        /// </summary>
        public ShortDateTimeConverter() : this(true)
        {
        }

        /// <summary>
        /// Creates a converter with writing explicitly enabled or disabled. The global
        /// registration must pass <c>false</c>; see the remarks on the class.
        /// </summary>
        public ShortDateTimeConverter(bool canWrite)
        {
            _canWrite = canWrite;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override bool CanRead => true;
        public override bool CanWrite => _canWrite;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType == typeof(DateTime?))
                    return null;
                else
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}.");
            }

            var dateString = reader.Value as string;
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
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(((DateTime)value).ToString(
                DateTimeFormat, System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}