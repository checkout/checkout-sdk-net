using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Workflows.Conditions.Response.Util
{
    public class WorkflowConditionTypeResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(WorkflowConditionResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static WorkflowConditionResponse Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static WorkflowConditionResponse Create(string type)
        {
            if (CheckoutUtils.GetEnumMemberValue(WorkflowConditionType.Entity).Equals(type))
            {
                return new EntityWorkflowConditionResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(WorkflowConditionType.Event).Equals(type))
            {
                return new EventWorkflowConditionResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(WorkflowConditionType.ProcessingChannel).Equals(type))
            {
                return new ProcessingChannelWorkflowConditionResponse();
            }

            return new WorkflowConditionResponse();
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}