using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataRequestSourceConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(CardMetadataRequestSource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static CardMetadataRequestSource Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return CreateSource(sourceType);
        }

        private static CardMetadataRequestSource CreateSource(string sourceType)
        {
            if (CheckoutUtils.GetEnumMemberValue(CardMetadataSourceType.Card).Equals(sourceType))
            {
                return new CardMetadataCardSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(CardMetadataSourceType.Bin).Equals(sourceType))
            {
                return new CardMetadataBinSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(CardMetadataSourceType.Token).Equals(sourceType))
            {
                return new CardMetadataTokenSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(CardMetadataSourceType.Id).Equals(sourceType))
            {
                return new CardMetadataIdSource();
            }

            return null;
        }

        private static string GetSourceType(JToken jToken)
        {
            return jToken.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}
