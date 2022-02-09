#if NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Workflows.Four.Actions.Response.Util
{
    public class WorkflowActionResponseConverter : JsonConverter<WorkflowActionResponse>
    {

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(WorkflowActionResponse).IsAssignableFrom(typeToConvert);
        }

        public override WorkflowActionResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                string str when str.Equals(nameof(WebhookWorkflowActionResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<WebhookWorkflowActionResponse>(ref reader)!,
                string str when str.Equals(nameof(WorkflowActionResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<WorkflowActionResponse>(ref reader)!,
                _ => throw new JsonException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, WorkflowActionResponse value, JsonSerializerOptions options)
        {
            System.Text.Json.JsonSerializer.Serialize(writer, value, options);
        }
    }
}

#endif
