using System;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public class TokensClient : ITokensClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;

        public TokensClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new PublicKeyCredentials(configuration);
        }

        public Task<ApiResponse<CardTokenResponse>> RequestAsync(CardTokenRequest cardTokenRequest)
        {
            if (cardTokenRequest == null)
                throw new ArgumentNullException(nameof(cardTokenRequest));

            return _apiClient.PostAsync<CardTokenResponse>("tokens", _credentials, cardTokenRequest);
        }

        public Task<ApiResponse<TokenResponse>> RequestAsync(WalletTokenRequest walletTokenRequest)
        {
            if (walletTokenRequest == null)
                throw new ArgumentNullException(nameof(walletTokenRequest));

            return _apiClient.PostAsync<TokenResponse>("tokens", _credentials, walletTokenRequest);
        }
    }
}