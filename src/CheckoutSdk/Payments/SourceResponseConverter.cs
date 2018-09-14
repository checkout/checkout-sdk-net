using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Checkout.Sdk.Payments
{
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class SourceResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => true;

        protected IResponsePaymentSource Create(JObject jObject)
        {
            if (jObject == null)
                throw new ArgumentNullException(nameof(jObject));

            var sourceType = GetSourceType(jObject);

            return CreateRequest(sourceType);
        }

        private IResponsePaymentSource CreateRequest(string sourceType)
        {
            switch (sourceType)
            {
                case CardSource.TypeName:
                    return new CardSourceResponse();
                default:
                    throw new NotImplementedException();
            }
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken("type")?.Value<string>().ToLowerInvariant();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            var target = Create(jObject);

            if (target != null)
                serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }
}