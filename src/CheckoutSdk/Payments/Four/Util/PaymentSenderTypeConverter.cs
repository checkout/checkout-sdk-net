using Checkout.Payments.Four.Sender;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Payments.Four.Util
{
    public sealed class PaymentSenderTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(PaymentSender).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(jObject);
            if (target != null)
            {
                serializer.Populate(jObject.CreateReader(), target);
            }

            return target;
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static PaymentSender Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return CreateRequest(sourceType);
        }

        private static PaymentSender CreateRequest(string senderType)
        {
            if (CheckoutUtils.GetEnumMemberValue(PaymentSenderType.Individual).Equals(senderType))
            {
                return new PaymentIndividualSender();
            }

            if (CheckoutUtils.GetEnumMemberValue(PaymentSenderType.Corporate).Equals(senderType))
            {
                return new PaymentCorporateSender();
            }

            if (CheckoutUtils.GetEnumMemberValue(PaymentSenderType.Instrument).Equals(senderType))
            {
                return new PaymentInstrumentSender();
            }

            return new PaymentSender(
                CheckoutUtils.GetEnumFromStringMemberValue<PaymentSenderType>(senderType));
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}