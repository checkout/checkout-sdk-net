using Checkout.Events.Previous;
using Checkout.Payments.Previous;
using Checkout.Webhooks.Previous;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Checkout
{
    public class ItemsResponseConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType && typeof(ItemsResponse<>).GetGenericTypeDefinition()
                .IsAssignableFrom(objectType.GetGenericTypeDefinition());
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
            Type genericTypeDefinition = objectType.GetGenericTypeDefinition();
            Type genericArgument = objectType.GetGenericArguments()[0];
            Type constructed = genericTypeDefinition.MakeGenericType(genericArgument);

            var wrapper = Activator.CreateInstance(constructed);

            Type instanceType = typeof(List<>).MakeGenericType(genericArgument);
            var targetItems = Activator.CreateInstance(instanceType);
            JContainer jObject = JArray.Load(reader);

            if (targetItems != null)
            {
                serializer.Populate(jObject.CreateReader(), targetItems);
            }

            CheckAndSetItemsList(genericArgument, wrapper, targetItems);

            return wrapper;
        }

        private static void CheckAndSetItemsList(Type genericArgument, object wrapper, object target)
        {
            if (genericArgument == typeof(WebhookResponse))
            {
                ((ItemsResponse<WebhookResponse>)wrapper).Items = (List<WebhookResponse>)target;
            }
            else if (genericArgument == typeof(EventTypesResponse))
            {
                ((ItemsResponse<EventTypesResponse>)wrapper).Items = (List<EventTypesResponse>)target;
            }
            else if (genericArgument == typeof(Workflows.Events.EventTypesResponse))
            {
                ((ItemsResponse<Workflows.Events.EventTypesResponse>)wrapper).Items =
                    (List<Workflows.Events.EventTypesResponse>)target;
            }
            else if (genericArgument == typeof(PaymentAction))
            {
                ((ItemsResponse<PaymentAction>)wrapper).Items =
                    (List<PaymentAction>)target;
            }
            else if (genericArgument == typeof(Payments.PaymentAction))
            {
                ((ItemsResponse<Payments.PaymentAction>)wrapper).Items =
                    (List<Payments.PaymentAction>)target;
            }
        }
    }
}