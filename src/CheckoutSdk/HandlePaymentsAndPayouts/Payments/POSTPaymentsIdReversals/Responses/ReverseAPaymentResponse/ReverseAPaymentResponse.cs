using Checkout.Common;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPaymentsIdReversals.Responses.ReverseAPaymentResponse
{
    /// <summary>
    /// Reverse a payment Response 200
    /// Payment is already reversed
    /// </summary>
    public class ReverseAPaymentResponse : Resource
    {

        /// <summary>
        /// The unique identifier for the previously completed payment action.
        /// [Required]
        /// ^(act)_(\w{26})$
        /// 30 characters
        /// </summary>
        public string ActionId { get; set; }
        
        /// <summary>
        /// A unique reference for the payment reversal.
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

    }
}
