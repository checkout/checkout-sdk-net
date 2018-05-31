using System;
using System.Threading.Tasks;

namespace Checkout.Tokens
{
    public class TokensClient : ITokensClient
    {
        private readonly IApiClient _apiClient;
        private readonly CheckoutConfiguration _configuration;

        public TokensClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public Task<ApiResponse<CardTokenResponse>> RequestAsync(CardTokenRequest cardTokenRequest)
        {
            ValidatePublicKey();
            return _apiClient.PostAsync<CardTokenRequest, CardTokenResponse>("tokens", cardTokenRequest);
        }

        public Task<ApiResponse<TokenResponse>> RequestAsync(WalletTokenRequest walletTokenRequest)
        {
            ValidatePublicKey();
            return _apiClient.PostAsync<WalletTokenRequest, TokenResponse>("tokens", walletTokenRequest);
        }

        private void ValidatePublicKey()
        {
            if (string.IsNullOrEmpty(_configuration.PublicKey))
                throw new InvalidOperationException("Public key required to request tokens");
        }
    }
}