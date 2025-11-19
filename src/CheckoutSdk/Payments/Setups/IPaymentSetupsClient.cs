using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Setups
{
    public interface IPaymentSetupsClient
    {
        /// <summary>
        /// Creates a Payment Setup
        /// </summary>
        Task<PaymentSetupsResponse> CreatePaymentSetup(
            PaymentSetupsCreatePaymentSetupRequest paymentSetupsCreatePaymentSetupRequest,
            CancellationToken cancellationToken = default);
    }
}