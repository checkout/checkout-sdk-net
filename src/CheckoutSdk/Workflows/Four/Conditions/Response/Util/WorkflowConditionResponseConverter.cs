#if NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Checkout.Workflows.Four.Conditions.Response.Util
{
    public class WorkflowConditionResponseConverter : JsonConverter<WorkflowConditionResponse>
    {

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(WorkflowConditionResponse).IsAssignableFrom(typeToConvert);
        }

        public override WorkflowConditionResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                string str when str.Equals(nameof(EntityWorkflowConditionResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<EntityWorkflowConditionResponse>(ref reader)!,
                string str when str.Equals(nameof(ProcessingChannelWorkflowConditionResponse), StringComparison.OrdinalIgnoreCase) =>
                    System.Text.Json.JsonSerializer.Deserialize<ProcessingChannelWorkflowConditionResponse>(ref reader)!,
                _ => throw new JsonException(),
            };
        }

        public override void Write(Utf8JsonWriter writer, WorkflowConditionResponse value, JsonSerializerOptions options)
        {
            System.Text.Json.JsonSerializer.Serialize(writer, value, options);
        }
    }
}

#endif
