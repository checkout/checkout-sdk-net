using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Extensions for <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Converts a provided <paramref name="httpResponseMessage"/> into a <see cref="CheckoutHttpResponseMessage{dynamic}"/>.
        /// </summary>
        /// <param name="httpResponseMessage">The <see cref="CheckoutHttpResponseMessage{dynamic}"/>.</param>
        /// <param name="resultType">The <see cref="Type"/> of the result.</param>
        /// <returns>The <see cref="CheckoutHttpResponseMessage{dynamic}"/> representation of the <see cref="HttpResponseMessage"/>.</returns>
        public static async Task<CheckoutHttpResponseMessage<dynamic>> ConvertToChekoutHttpResponseMessage(this HttpResponseMessage httpResponseMessage, Type resultType)
        {
            ISerializer _serializer = new JsonSerializer();
            var statusCode = httpResponseMessage.StatusCode;
            var headers = httpResponseMessage.Headers;
            var content = httpResponseMessage.Content;
            
            if(content == null)
                return new CheckoutHttpResponseMessage<dynamic>(statusCode, headers, content);

            var json = await content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(json))
                return new CheckoutHttpResponseMessage<dynamic>(statusCode, headers, _serializer.Deserialize(json, resultType));

            return new CheckoutHttpResponseMessage<dynamic>(statusCode, headers, json);
        }
    }
}
