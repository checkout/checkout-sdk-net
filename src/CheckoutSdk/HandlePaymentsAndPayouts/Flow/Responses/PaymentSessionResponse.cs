using Checkout.Common;


namespace Checkout.HandlePaymentsAndPayouts.Flow.Responses
{
    public class PaymentSessionResponse : HttpMetadata
    {
        /// <summary>
        /// The Payment Sessions unique identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A unique token representing the payment session, which you must provide when you initialize Flow.
        /// Do not log or store this value.
        /// </summary>
        public string PaymentSessionToken { get; set; }

        /// <summary>
        /// The secret used by Flow to authenticate payment session requests.
        /// Do not log or store this value.
        /// </summary>
        public string PaymentSessionSecret { get; set; }
    }
}