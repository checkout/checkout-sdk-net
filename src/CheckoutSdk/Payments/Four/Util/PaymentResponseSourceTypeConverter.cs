using Checkout.Common;
using Checkout.Payments.Four.Response.Source;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Payments.Four.Util
{
    public sealed class PaymentResponseSourceTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IResponseSource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static IResponseSource Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return CreateResponse(sourceType);
        }

        private static IResponseSource CreateResponse(string sourceType)
        {
            if (CheckoutUtils.GetEnumMemberValue(PaymentSourceType.Card).Equals(sourceType))
            {
                return new CardResponseSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(PaymentSourceType.CurrencyAccount).Equals(sourceType))
            {
                return new CurrencyAccountResponseSource();
            }

            return new AlternativePaymentSourceResponse();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}