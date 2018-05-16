using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout
{
    public class ApiClient : IApiClient
    {
        private readonly CheckoutConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClient(CheckoutConfiguration configuration)
            : this(configuration, new DefaultHttpClientFactory())
        {
            
        }

        public ApiClient(CheckoutConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        
        public Task<ApiResponse<TResult>> PostAsync<TRequest, TResult>(string uri, TRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}