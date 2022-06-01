using Checkout.Events;
using Checkout.Payments;
using Checkout.Webhooks;
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
            else if (genericArgument == typeof(Workflows.Four.Events.EventTypesResponse))
            {
                ((ItemsResponse<Workflows.Four.Events.EventTypesResponse>)wrapper).Items =
                    (List<Workflows.Four.Events.EventTypesResponse>)target;
            }
            else if (genericArgument == typeof(PaymentAction))
            {
                ((ItemsResponse<PaymentAction>)wrapper).Items =
                    (List<PaymentAction>)target;
            }
            else if (genericArgument == typeof(Payments.Four.PaymentAction))
            {
                ((ItemsResponse<Payments.Four.PaymentAction>)wrapper).Items =
                    (List<Payments.Four.PaymentAction>)target;
            }
        }
    }
}