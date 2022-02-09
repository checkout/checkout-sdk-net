#if NET5_0_OR_GREATER
using Checkout.Payments.Four.Response.Destination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Payments.Util
{
    public class PaymentResponseDestinationConverter : JsonConverter<IPaymentResponseDestination>
    {

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IPaymentResponseDestination).IsAssignableFrom(typeToConvert);
        }

        public override IPaymentResponseDestination Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string typeName = null;
            while (readerClone.Read())
            {
                if (readerClone.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = readerClone.GetString();
                    if (CheckoutUtils.Type.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        readerClone.Read();
                        typeName = readerClone.GetString();
                        break;
                    }
                }
            }

            if (typeName is null)
            {
                throw new JsonException();
            }

            return typeName switch
            {
                string str when str.Equals(nameof(PaymentResponseAlternativeDestination), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<PaymentResponseAlternativeDestination>(ref reader)!,
                string str when str.Equals(nameof(PaymentResponseBankAccountDestination), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<PaymentResponseBankAccountDestination>(ref reader)!,
                _ => throw new JsonException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, IPaymentResponseDestination value, JsonSerializerOptions options)
        {
            System.Text.Json.JsonSerializer.Serialize(writer, value, options);
        }
    }
}

#endif