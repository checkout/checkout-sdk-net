using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payments.Setups
{
    public interface IPaymentSetupsClient
    {
        /// <summary>
        /// Creates a Payment Setup.
        /// To maximize the amount of information the payment setup can use, we recommend that you create a payment
        /// setup as early as possible in the customer's journey. For example, the first time they land on the basket
        /// page
        /// [Beta]
        /// </summary>
        Task<PaymentSetupsResponse> CreatePaymentSetup(
            PaymentSetupsRequest paymentSetupsCreateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a Payment Setup
        /// You should update the payment setup whenever there are significant changes in the data relevant to the
        /// customer's transaction. For example, when the customer makes a change that impacts the total payment amount
        /// [Beta]
        /// </summary>
        Task<PaymentSetupsResponse> UpdatePaymentSetup(
            string id,
            PaymentSetupsRequest paymentSetupsUpdateRequest,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a Payment Setup
        /// [Beta]
        /// </summary>
        Task<PaymentSetupsResponse> GetPaymentSetup(
            string id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Confirm a Payment Setup to begin processing the payment request with your chosen payment method option
        /// [Beta]
        /// </summary>
        Task<PaymentSetupsConfirmResponse> ConfirmPaymentSetup(
            string id, 
            string paymentMethodOptionId,
            CancellationToken cancellationToken = default);
    }
}