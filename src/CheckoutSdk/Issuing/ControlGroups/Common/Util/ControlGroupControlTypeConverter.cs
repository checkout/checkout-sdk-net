using System;
using System.Reflection;
using Checkout;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkout.Issuing.ControlGroups.Common.Util
{
    /// <summary>
    /// JSON converter for deserializing ControlGroupControl objects based on their control type properties.
    /// </summary>
    public class ControlGroupControlTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(ControlGroupControl).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static ControlGroupControl Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            
            // Check for MCC limit control
            if (jToken["mcc_limit"] != null)
            {
                return new MccControlGroupControl();
            }
            
            // Check for MID limit control
            if (jToken["mid_limit"] != null)
            {
                return new MidControlGroupControl();
            }
            
            // Check for velocity limit control
            if (jToken["velocity_limit"] != null)
            {
                return new VelocityControlGroupControl();
            }
            
            throw new JsonSerializationException(
                $"Unable to determine ControlGroupControl type from JSON: {jToken}");
        }
    }
}