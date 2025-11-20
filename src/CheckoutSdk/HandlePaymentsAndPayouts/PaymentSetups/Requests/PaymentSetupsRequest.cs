using Checkout.Common;
using Checkout.Payments.Setups.Entities;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsRequest
    {
        /// <summary>
        /// The processing channel to be used for the payment setup
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The payment amount. The exact format depends on the currency
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The three-letter ISO currency code
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment method
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// A reference you can later use to identify this payment setup
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment setup
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The payment method configuration for this setup
        /// </summary>
        public PaymentMethods PaymentMethods { get; set; }

        /// <summary>
        /// The payment setup configuration settings
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Details about the customer
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Details about the order associated with this payment setup
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Industry-specific information for specialized payment scenarios
        /// </summary>
        public Industry Industry { get; set; }
    }
}