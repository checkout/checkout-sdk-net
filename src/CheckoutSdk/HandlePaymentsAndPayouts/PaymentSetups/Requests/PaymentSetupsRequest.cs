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
        /// The payment amount, in the minor currency unit. The exact format depends on the currency
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The currency of the payment, as a three-letter ISO currency code
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// The type of payment.
        //// You must provide this field for card payments in which the cardholder is not present. For example, if the transaction is a recurring payment, or a mail order/telephone order (MOTO) payment.
        /// Enum: "Regular" "Recurring" "MOTO" "Installment" "Unscheduled"
        /// </summary>
        public PaymentType? PaymentType { get; set; }

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment setup
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The payment methods that are enabled on your account and available for use
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
        /// Details for specific industries, including airline and accommodation industries
        /// </summary>
        public Industry Industry { get; set; }
    }
}