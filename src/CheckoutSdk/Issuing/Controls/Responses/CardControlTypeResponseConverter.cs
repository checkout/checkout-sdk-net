using Checkout.Issuing.Common;
using Checkout.Issuing.Controls.Responses.Create;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Issuing.Controls.Responses
{
    public class CardControlTypeResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(AbstractCardControlResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static AbstractCardControlResponse Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static AbstractCardControlResponse Create(string controlType)
        {
            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.VelocityLimit).Equals(controlType))
            {
                return new VelocityCardControlResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.MccLimit).Equals(controlType))
            {
                return new MccCardControlResponse();
            }
            
            if (CheckoutUtils.GetEnumMemberValue(IssuingControlType.MidLimit).Equals(controlType))
            {
                return new MidCardControlResponse();
            }

            throw new Exception();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.ControlType)?.Value<string>()?.ToLowerInvariant();
        }
    }
}