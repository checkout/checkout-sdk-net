using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Customers.Previous
{
    public class CustomersClient : AbstractClient, ICustomersClient
    {
        private const string Customers = "customers";

        public CustomersClient(IApiClient apiClient,
            CheckoutConfiguration configuration) : base(apiClient, configuration, SdkAuthorizationType.SecretKey)
        {
        }

        public Task<CustomerDetailsResponse> Get(string customerId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("customerId", customerId);
            return ApiClient.Get<CustomerDetailsResponse>(BuildPath(Customers, customerId), SdkAuthorization(),
                cancellationToken);
        }

        public Task<IdResponse> Create(CustomerRequest customerRequest, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("customerRequest", customerRequest);
            return ApiClient.Post<IdResponse>(Customers, SdkAuthorization(), customerRequest, cancellationToken, null);
        }

        public Task<EmptyResponse> Update(string customerId, CustomerRequest customerRequest,
            CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("customerId", customerId, "customerRequest", customerRequest);
            return ApiClient.Patch<EmptyResponse>(BuildPath(Customers, customerId), SdkAuthorization(), customerRequest,
                cancellationToken);
        }

        public Task<EmptyResponse> Delete(string customerId, CancellationToken cancellationToken = default)
        {
            CheckoutUtils.ValidateParams("customerId", customerId);
            return ApiClient.Delete<EmptyResponse>(BuildPath(Customers, customerId), SdkAuthorization(), cancellationToken);
        }
    }
}