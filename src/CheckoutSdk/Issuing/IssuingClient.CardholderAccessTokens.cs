using Checkout.Issuing.CardholderAccessTokens.Requests;
using Checkout.Issuing.CardholderAccessTokens.Responses;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Issuing
{
    public partial class IssuingClient
    {
        /// <summary>
        /// Request an access token
        /// OAuth endpoint to exchange your access key ID and access key secret for an access token
        /// </summary>
        public Task<CardholderAccessTokenResponse> RequestCardholderAccessToken(
            CardholderAccessTokenRequest cardholderAccessTokenRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("cardholderAccessTokenRequest", cardholderAccessTokenRequest);
            var formContent = CreateFormUrlEncodedContent(cardholderAccessTokenRequest);
            return ApiClient.Post<CardholderAccessTokenResponse>(
                BuildPath(IssuingPath, AccessTokenPath),
                SdkAuthorization(),
                formContent,
                cancellationToken
            );
        }

        private static FormUrlEncodedContent CreateFormUrlEncodedContent(CardholderAccessTokenRequest request)
        {
            var data = new List<KeyValuePair<string, string>>();
            var namingStrategy = new SnakeCaseNamingStrategy();
            
            var properties = typeof(CardholderAccessTokenRequest).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (var property in properties)
            {
                var value = property.GetValue(request);
                if (value != null)
                {
                    var fieldName = namingStrategy.GetPropertyName(property.Name, false);
                    var stringValue = value is bool boolValue ? boolValue.ToString().ToLower() : value.ToString();
                    data.Add(new KeyValuePair<string, string>(fieldName, stringValue));
                }
            }
            
            return new FormUrlEncodedContent(data);
        }
    }
}