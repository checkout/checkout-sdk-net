using System;
using System.Reflection;
using Checkout.Payments.Four.Response.Destination;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkout.Payments.Four.Util
{
    public sealed class PaymentResponseDestinationTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IPaymentResponseDestination).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static IPaymentResponseDestination Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return CreateRequest(sourceType);
        }

        private static IPaymentResponseDestination CreateRequest(string destinationType)
        {
            if (CheckoutUtils.GetEnumMemberValue(PaymentDestinationType.BankAccount).Equals(destinationType))
            {
                return new PaymentResponseBankAccountDestination();
            }

            return new PaymentResponseAlternativeDestination();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}