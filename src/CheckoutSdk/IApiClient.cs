using System.Threading.Tasks;
using Checkout.Payments;
using Checkout.Webhooks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse> PostAsync<TRequest>(string path, TRequest request);        
        IPaymentOperations Payments {get;}
        IWebhookOperations Webhooks {get;}
    }
}