using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Payments
{
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class SourceResponseConverter : JsonConverter
    {
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

        protected IResponsePaymentSource Create(JObject jObject)
        {
            if (jObject == null)
                throw new ArgumentNullException(nameof(jObject));

            var sourceType = GetSourceType(jObject);

            return CreateRequest(sourceType);
        }

        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType)
        {
            return typeof(IResponsePaymentSource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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