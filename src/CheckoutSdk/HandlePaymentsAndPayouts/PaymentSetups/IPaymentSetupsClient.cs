using System.Runtime.CompilerServices;
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
            PaymentSetupsRequest paymentSetupsCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a Payment Setup
        /// </summary>
        Task<PaymentSetupsResponse> UpdatePaymentSetup(
            string id,
            PaymentSetupsRequest paymentSetupsUpdateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a Payment Setup
        /// </summary>
        Task<PaymentSetupsResponse> GetPaymentSetup(
            string id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirms a Payment Setup
        /// </summary>
        Task<PaymentSetupsConfirmResponse> ConfirmPaymentSetup(
            string id, 
            string PaymentMethodOptionId,
            CancellationToken cancellationToken = default);
    }
}