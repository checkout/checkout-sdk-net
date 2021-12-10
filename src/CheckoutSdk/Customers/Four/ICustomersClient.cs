using Checkout.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Customers.Four
{
    public interface ICustomersClient
    {
        Task<CustomerDetailsResponse> Get(string customerId, CancellationToken cancellationToken = default);

        Task<IdResponse> Create(CustomerRequest customerRequest, CancellationToken cancellationToken = default);

        Task<object> Update(string customerId, CustomerRequest customerRequest,
            CancellationToken cancellationToken = default);

        Task<object> Delete(string customerId, CancellationToken cancellationToken = default);
    }
}