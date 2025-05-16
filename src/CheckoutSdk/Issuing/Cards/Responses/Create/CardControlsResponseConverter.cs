using Checkout.Issuing.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Issuing.Cards.Responses.Create
{
    public class CardControlsResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(AbstractCardControlsResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static AbstractCardControlsResponse Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static AbstractCardControlsResponse Create(string type)
        {
            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.VelocityLimit).Equals(type))
            {
                return new VelocityCardControlsResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.MidLimit).Equals(type))
            {
                return new MidCardControlsResponse();
            }
            
            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.MccLimit).Equals(type))
            {
                return new MccCardControlsResponse();
            }

            throw new NotImplementedException();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}