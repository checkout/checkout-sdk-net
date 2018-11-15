using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Payments
{
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;

    /// <summary>
    /// JSON converter for handling different payment response source types.
    /// </summary>
    public class ResponseSourceConverter : JsonConverter
    {
        private const string TypeField = "type";

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IResponseSource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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
        
        protected IResponseSource Create(JObject jObject)
        {
            if (jObject == null)
                throw new ArgumentNullException(nameof(jObject));

            var sourceType = GetSourceType(jObject);

            return CreateRequest(sourceType);
        }

        private static IResponseSource CreateRequest(string sourceType)
        {
            switch (sourceType)
            {
                case CardSource.TypeName:
                    return new CardSourceResponse();
                default:
                    return new AlternativePaymentSourceResponse();
            }
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(TypeField)?.Value<string>().ToLowerInvariant();
        }
    }
}