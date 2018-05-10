using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Webhooks
{
    public interface IWebhookOperations
    {
        Task<WebhookResponse> GetAllAsync();
        Task<WebhookResponse> GetAsync(string id);
        Task<WebhookResponse> RegisterAsync(WebhookRequest request);
    }
}