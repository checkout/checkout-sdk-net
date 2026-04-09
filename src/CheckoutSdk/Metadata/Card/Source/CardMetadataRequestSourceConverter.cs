using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Checkout.Metadata.Card.Source
{
    public class CardMetadataRequestSourceConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(CardMetadataRequestSource).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var sourceType = jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();

            CardMetadataRequestSource target;
            if (string.Equals(sourceType, "card"))
                target = new CardMetadataCardSource();
            else if (string.Equals(sourceType, "bin"))
                target = new CardMetadataBinSource();
            else if (string.Equals(sourceType, "token"))
                target = new CardMetadataTokenSource();
            else if (string.Equals(sourceType, "id"))
                target = new CardMetadataIdSource();
            else
                target = null;

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
    }
}
