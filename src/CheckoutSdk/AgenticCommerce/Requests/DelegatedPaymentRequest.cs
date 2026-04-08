using System.Collections.Generic;

namespace Checkout.AgenticCommerce.Requests
{
    /// <summary>
    /// Request to create a delegated payment token for agentic commerce transactions.
    /// </summary>
    public class DelegatedPaymentRequest
    {
        /// <summary>
        /// The card payment method details.
        /// [Required]
        /// </summary>
        public DelegatedPaymentMethodCard PaymentMethod { get; set; }

        /// <summary>
        /// The spending constraints for the delegated payment token.
        /// [Required]
        /// </summary>
        public DelegatedPaymentAllowance Allowance { get; set; }

        /// <summary>
        /// The customer billing address.
        /// [Optional]
        /// </summary>
        public DelegatedPaymentBillingAddress BillingAddress { get; set; }

        /// <summary>
        /// An array of risk assessment signals provided by the platform.
        /// [Required]
        /// </summary>
        public IList<DelegatedPaymentRiskSignal> RiskSignals { get; set; }

        /// <summary>
        /// A set of key-value pairs to attach to the delegated payment request.
        /// The metadata object only supports string values.
        /// [Required]
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
