using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Instruments.Update.Util
{
    public sealed class UpdateInstrumentResponseTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(UpdateInstrumentResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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

        private static UpdateInstrumentResponse Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return Create(sourceType);
        }

        private static UpdateInstrumentResponse Create(string type)
        {
            if (CheckoutUtils.GetEnumMemberValue(InstrumentType.BankAccount).Equals(type))
            {
                return new UpdateBankInstrumentResponse();
            }

            if (CheckoutUtils.GetEnumMemberValue(InstrumentType.Card).Equals(type))
            {
                return new UpdateCardInstrumentResponse();
            }

            return new UpdateInstrumentResponse(
                CheckoutUtils.GetEnumFromStringMemberValue<InstrumentType>(type));
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }
    }
}