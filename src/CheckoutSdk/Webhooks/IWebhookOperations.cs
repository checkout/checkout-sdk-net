using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Webhooks
{
    public interface IWebhookOperations
    {
        Task<ApiResponse<IEnumerable<Webhook>>> GetAllAsync();
        Task<ApiResponse<Webhook>> GetAsync(string id);
        Task<ApiResponse<Webhook>> RegisterAsync(WebhookRequest request);
    }
}