namespace Checkout.Webhooks
{
    /// <summary>
    /// Defines a <see cref="WebhookRequest"/>.
    /// </summary>
    public class WebhookRequest<TWebhook> : Webhook where TWebhook : IWebhook
    {
        /// <summary>
        /// Creates a new <see cref="WebhookRequest"/> instance.
        /// </summary>
        /// <param name="webhook">The webhook to be used in the request.</param>
        public WebhookRequest(TWebhook webhook)
        {
            Url = webhook.Url;
            Active = webhook.Active;
            Headers = webhook.Headers;
            EventTypes = webhook.EventTypes;
        }
    }
}
