using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Accounts.Payout.Response.Util
{
    public class ScheduleResponseTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(ScheduleResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = RetrieveScheduleResponseType(jObject);
            if (target != null)
            {
                serializer.Populate(jObject.CreateReader(), target);
            }

            return target;
        }

        private static ScheduleResponse RetrieveScheduleResponseType(JToken jToken)
        {
            string sourceType = jToken.SelectToken(CheckoutUtils.Frequency)?.Value<string>()?.ToLowerInvariant();
            if (CheckoutUtils.GetEnumMemberValue(ScheduleFrequency.Daily).Equals(sourceType))
            {
                return new ScheduleFrequencyDailyResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(ScheduleFrequency.Weekly).Equals(sourceType))
            {
                return new ScheduleFrequencyWeeklyResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(ScheduleFrequency.Monthly).Equals(sourceType))
            {
                return new ScheduleFrequencyMonthlyResponse();
            }

            return new ScheduleResponse(CheckoutUtils.GetEnumFromStringMemberValue<ScheduleFrequency>(sourceType));
        }
    }
}