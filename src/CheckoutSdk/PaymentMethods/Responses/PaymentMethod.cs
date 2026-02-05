namespace Checkout.PaymentMethods.Responses
{
    public class PaymentMethod
    {
        /// <summary>
        /// The type of the payment method
        /// </summary>
        /// [Required]
        public PaymentMethodType? Type { get; set; }

        /// <summary>
        /// The unique identifier for the merchant, provided by the partner
        /// </summary>
        public string PartnerMerchantId { get; set; }
    }
}