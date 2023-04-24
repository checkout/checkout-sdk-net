using Checkout.Issuing.Controls.Requests.Create;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Issuing.Controls.Requests
{
    public class CardControlTypeRequestConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(CardControlRequest).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static CardControlRequest Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static CardControlRequest Create(string controlType)
        {
            if (CheckoutUtils.GetEnumMemberValue(ControlType.VelocityLimit).Equals(controlType))
            {
                return new VelocityCardControlRequest();
            }

            if (CheckoutUtils.GetEnumMemberValue(ControlType.MccLimit).Equals(controlType))
            {
                return new MccCardControlRequest();
            }

            throw new NotImplementedException();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.ControlType)?.Value<string>()?.ToLowerInvariant();
        }
    }
}