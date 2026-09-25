using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkout
{
    /// <summary>
    /// Reads a property the Checkout.com specification declares as
    /// <c>oneOf[array, object]</c> into an <see cref="IList{T}"/>, accepting either shape on the
    /// wire and normalizing a bare object into a single-element list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The first property to need this is <c>processing.airline_data[].passenger</c>.
    /// <c>AirlineData</c> (reached from <c>POST /payments</c> and from the
    /// <c>GET /payments/{id}</c> response) declares it as an array, while
    /// <c>PaymentInterfacesProcessingAirlineData</c> (payment sessions, hosted payments and
    /// payment links) declares it as <c>oneOf[array, object]</c> with the note
    /// <i>"PayPal requires a single object"</i>. Both branches resolve to the same object, so
    /// normalizing to a list loses nothing.
    /// </para>
    /// <para>
    /// <b>The outbound shape is not simply "always an array".</b> The live API does not match the
    /// specification in either direction. Verified against the sandbox on 2026-09-25 with a
    /// complete <c>airline_data</c> block:
    /// </para>
    /// <list type="table">
    /// <item><term><c>POST /payments</c></term><description>object 201, array 201</description></item>
    /// <item><term><c>POST /hosted-payments</c></term><description>object accepted, array 422 <c>processing_airline_data_0_passenger_invalid</c></description></item>
    /// <item><term><c>POST /payment-links</c></term><description>object accepted, array 422 <c>processing_airline_data_0_passenger_invalid</c></description></item>
    /// <item><term><c>POST /payment-contexts</c></term><description>object 201, array 422 <c>passenger_required</c></description></item>
    /// </list>
    /// <para>
    /// A single object is therefore accepted on every request surface, and an array only on
    /// <c>POST /payments</c>. <see cref="Checkout.Payments.ProcessingSettings"/> is shared by
    /// <c>POST /payments</c>, hosted payments and payment links, so always emitting an array
    /// would break the latter two. <see cref="WriteJson"/> emits an object for a single passenger
    /// and an array only for several, which is the only shape combination the API accepts.
    /// </para>
    /// <para>
    /// An empty array and an explicit <c>null</c> are both rejected with
    /// <c>processing_airline_data_0_passenger_invalid</c>, so the property must be omitted when
    /// there are no passengers. A converter cannot skip a property, so the owning classes do that
    /// with a <c>ShouldSerialize</c> method.
    /// </para>
    /// <para>
    /// Element deserialization is delegated to the supplied
    /// <see cref="Newtonsoft.Json.JsonSerializer"/>, so the global snake_case naming strategy and
    /// every registered converter, <see cref="ShortDateTimeConverter"/> included, still apply to
    /// <typeparamref name="T"/>. This converter never maps property names itself.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The list element type.</typeparam>
    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<T>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Null)
                return null;

            if (token.Type == JTokenType.Array)
                return token.ToObject<IList<T>>(serializer);

            return new List<T> { token.ToObject<T>(serializer) };
        }

        public override void WriteJson(JsonWriter writer, object value,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            var values = value as IList<T>;

            if (values == null || values.Count == 0)
            {
                // Unreachable when the owning class declares ShouldSerialize for the property.
                // Kept as a guard: an empty array and a null are both rejected by the API, so
                // null is the least-wrong fallback if a caller bypasses that.
                writer.WriteNull();
                return;
            }

            if (values.Count == 1)
            {
                serializer.Serialize(writer, values[0]);
                return;
            }

            serializer.Serialize(writer, values);
        }
    }
}
