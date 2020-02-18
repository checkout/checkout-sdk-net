using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Disputes
{
    /// <summary>
    /// Default implementation of <see cref="IDisputesClient"/>.
    /// </summary>
    public class DisputesClient : IDisputesClient
    {
        private readonly IApiClient _apiClient;
        private readonly IApiCredentials _credentials;

        public DisputesClient(IApiClient apiClient, CheckoutConfiguration configuration)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _credentials = new SecretKeyCredentials(configuration);
        }

        public Task<GetDisputesResponse> GetDisputesAsync(
            int? limit = null,
            int? skip = null,
            string from = "",
            string to = "",
            string id = "",
            string statuses = "",
            string paymentId = "",
            string paymentReference = "",
            string paymentArn = "",
            bool? thisChannelOnly = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string path = "disputes?";
            if (limit.HasValue) path += $"limit={limit}";
            if (skip.HasValue) path += $"&skip={skip}";
            if (!string.IsNullOrEmpty(from)) path += $"&from={from}";
            if (!string.IsNullOrEmpty(to)) path += $"&to={to}";
            if (!string.IsNullOrEmpty(id)) path += $"&id={id}";
            if (!string.IsNullOrEmpty(statuses)) path += $"&statuses={statuses.Replace(" ", string.Empty)}";
            if (!string.IsNullOrEmpty(paymentId)) path += $"&payment_id={paymentId}";
            if (!string.IsNullOrEmpty(paymentReference)) path += $"&payment_reference={paymentReference}";
            if (!string.IsNullOrEmpty(paymentArn)) path += $"&payment_arn={paymentArn}";
            if (thisChannelOnly.HasValue) path += $"&this_channel_only={thisChannelOnly.ToString()}";
            return _apiClient.GetAsync<GetDisputesResponse>(path, _credentials, cancellationToken);
        }
    }
}
