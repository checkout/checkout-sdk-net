#if NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Instruments.Four.Update.Util
{

    public class UpdateInstrumentResponseConverter : JsonConverter<UpdateInstrumentResponse>
    {

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(UpdateInstrumentResponse).IsAssignableFrom(typeToConvert);
        }

        public override UpdateInstrumentResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                string str when str.Equals(nameof(UpdateBankInstrumentResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<UpdateBankInstrumentResponse>(ref reader)!,
                string str when str.Equals(nameof(UpdateCardInstrumentResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<UpdateCardInstrumentResponse>(ref reader)!,
                string str when str.Equals(nameof(UpdateInstrumentResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<UpdateInstrumentResponse>(ref reader)!,
                
            _ => throw new JsonException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, UpdateInstrumentResponse value, JsonSerializerOptions options)
        {
            System.Text.Json.JsonSerializer.Serialize(writer, value, options);
        }
    }
}

#endif
