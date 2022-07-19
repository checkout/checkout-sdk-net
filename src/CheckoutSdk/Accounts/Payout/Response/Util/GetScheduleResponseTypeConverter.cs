using Checkout.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Checkout.Accounts.Payout.Response.Util
{
    public class GetScheduleResponseTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(GetScheduleResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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
            GetScheduleResponse response = new GetScheduleResponse();
            foreach (var node in jObject)
            {
                if (node.Value != null && Enum.TryParse(node.Key, out Currency currency))
                {
                    CurrencySchedule currencySchedule = new CurrencySchedule();
                    serializer.Populate(node.Value.CreateReader(), currencySchedule);
                    response.Currency =
                        new Dictionary<Currency, CurrencySchedule> {{currency, currencySchedule}};
                }

                if (node.Key.Equals("_links") && node.Value != null)
                {
                    var resource = new Dictionary<string, Link>();
                    serializer.Populate(node.Value.CreateReader(), resource);
                    response.Links = resource;
                }
            }

            return response;
        }
    }
}