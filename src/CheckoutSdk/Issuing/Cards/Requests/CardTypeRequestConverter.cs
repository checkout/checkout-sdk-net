using Checkout.Issuing.Cards.Requests.Create;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Issuing.Cards.Requests
{
    public class CardTypeRequestConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(CardRequest).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static CardRequest Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static CardRequest Create(string type)
        {
            if (CheckoutUtils.GetEnumMemberValue(CardType.Physical).Equals(type))
            {
                return new CardPhysicalRequest();
            }

            if (CheckoutUtils.GetEnumMemberValue(CardType.Virtual).Equals(type))
            {
                return new CardVirtualRequest();
            }

            throw new Exception();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}