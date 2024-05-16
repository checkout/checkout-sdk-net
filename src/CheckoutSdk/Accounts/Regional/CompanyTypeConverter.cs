using Checkout.Accounts.Regional.US;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Checkout.Accounts.Regional
{
    public class CompanyTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;
        
        public override bool CanConvert(Type objectType)
        {
            return typeof(OnboardEntityDetailsResponse).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
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
            
            if (reader.TokenType == JsonToken.Null)
                return null;
            
            var jObject = JToken.Load(reader);
            if (jObject.Type == JTokenType.Object)
            {
                var businessTypeToken = jObject["company"]?["business_type"];
                if (businessTypeToken != null && businessTypeToken.Type == JTokenType.String )
                {
                    var enumString = businessTypeToken.Value<string>();
                    
                    var businessType = CheckoutUtils.GetEnumFromStringMemberValue<BusinessType>(enumString);
                    if (businessType != null)
                    {
                        var target = new OnboardEntityDetailsResponse();
                        serializer.Populate(jObject.CreateReader(), target);
                        return target;
                    }
                    
                    var usBusinessType = CheckoutUtils.GetEnumFromStringMemberValue<USBusinessType>(enumString);
                    if (usBusinessType != null)
                    {
                        var target = new OnboardEntityDetailsUSCompanyResponse();
                        serializer.Populate(jObject.CreateReader(), target);
                        return target;
                    }
                }
            }
            
            throw new JsonSerializationException($"Unexpected token or value when parsing enum. Token: {reader.TokenType}, Value: {jObject}");
        }
    }
}