using Checkout.Common;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Response.Source.Contexts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Payments.Util
{
    public class PaymentResponseSourceTypeConverter : JsonConverter
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
            
            if (CheckoutUtils.GetEnumMemberValue(PaymentSourceType.PayPal).Equals(sourceType))
            {
                return new PaymentContextsPayPalResponseSource();
            }
            
            if (CheckoutUtils.GetEnumMemberValue(PaymentSourceType.Klarna).Equals(sourceType))
            {
                return new PaymentContextsKlarnaResponseSource();
            }

            return new AlternativePaymentSourceResponse();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}