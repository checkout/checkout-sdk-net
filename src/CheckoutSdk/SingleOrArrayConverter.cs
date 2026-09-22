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
    /// <b><see cref="CanWrite"/> is deliberately <c>false</c>.</b> Newtonsoft only routes writes
    /// through <see cref="WriteJson"/> when a converter reports that it can write, so leaving it
    /// false hands serialization back to the default list serializer and the SDK always emits an
    /// array. That is the only valid outbound shape for <c>AirlineData</c> and a valid one for
    /// <c>PaymentInterfacesProcessingAirlineData</c>. Setting it to <c>true</c> would make every
    /// request carrying airline data throw.
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

        public override bool CanWrite => false;

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
            throw new NotSupportedException(
                $"{nameof(SingleOrArrayConverter<T>)} is read-only. CanWrite is false so that " +
                "serialization falls back to the default list serializer, which always emits an array.");
        }
    }
}
