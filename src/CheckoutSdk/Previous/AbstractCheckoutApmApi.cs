using Checkout.Apm.Ideal;
using Checkout.Apm.Previous.Klarna;
using Checkout.Apm.Previous.Sepa;

namespace Checkout.Previous
{
    public abstract class AbstractCheckoutApmApi
    {
        private readonly IIdealClient _idealClient;
        private readonly IKlarnaClient _klarnaClient;
        private readonly ISepaClient _sepaClient;

        protected AbstractCheckoutApmApi(CheckoutConfiguration configuration)
        {
            var apiClient = new ApiClient(configuration.HttpClientFactory,
                configuration.Environment.GetAttribute<EnvironmentAttribute>().ApiUri);
            _idealClient = new IdealClient(apiClient, configuration);
            _klarnaClient = new KlarnaClient(apiClient, configuration);
            _sepaClient = new SepaClient(apiClient, configuration);
        }

        public IIdealClient IdealClient()
        {
            return _idealClient;
        }

        public IKlarnaClient KlarnaClient()
        {
            return _klarnaClient;
        }

        public ISepaClient SepaClient()
        {
            return _sepaClient;
        }
    }
}