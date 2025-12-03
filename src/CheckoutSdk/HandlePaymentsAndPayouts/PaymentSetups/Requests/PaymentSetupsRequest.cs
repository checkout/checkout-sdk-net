using Checkout.Common;
using Checkout.Payments.Setups.Entities;

namespace Checkout.Payments.Setups
{
    /// <summary>
    /// Creates a Payment Setup.
    /// To maximize the amount of information the payment setup can use, we recommend that you create a payment setup as
    /// early as possible in the customer's journey. For example, the first time they land on the basket page.
    /// [Beta]
    /// </summary>
    public class PaymentSetupsRequest
    {
        /// <summary>
        /// The processing channel to use for the payment.
        /// [Required]
        /// ^(pc)_(\w{26})$
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary>
        /// The payment amount, in the minor currency unit.
        /// [Required]
        /// </summary>
        public long? Amount { get; set; }

        /// <summary>
        /// The currency of the payment, as a three-letter ISO currency code
        /// [Required]
        /// </summary>
        public Currency? Currency { get; set; }
        
        /// <summary>
        /// The type of payment.
        /// You must provide this field for card payments in which the cardholder is not present. For example, if the
        /// transaction is a recurring payment, or a mail order/telephone order (MOTO) payment.
        /// Enum: "Regular" "Recurring" "MOTO" "Installment" "Unscheduled"
        /// [Optional]
        /// </summary>
        public PaymentType? PaymentType { get; set; } = Payments.PaymentType.Regular;

        /// <summary>
        /// A reference you can use to identify the payment. For example, an order number
        /// &lt;= 80 characters
        /// [Optional]
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// A description of the payment.
        /// &lt;= 100 characters
        /// [Optional]
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The payment methods that are enabled on your account and available for use
        /// [Optional]
        /// </summary>
        public PaymentMethods PaymentMethods { get; set; }

        /// <summary>
        /// Settings for the Payment Setup
        /// [Optional]
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// The customer's details
        /// [Optional]
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The customer's order details
        /// [Optional]
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Details for specific industries, including airline and accommodation industries
        /// [Optional]
        /// </summary>
        public Industry Industry { get; set; }
    }
}