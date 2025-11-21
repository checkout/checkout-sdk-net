namespace Checkout.AgenticCommerce.Common
{
    /// <summary>
    /// The payment source to enroll
    /// </summary>
    public class Source
    {
        /// <summary>
        /// The full card number, without separators
        /// [Required]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Card expiry month
        /// [ 13 .. 19 ] characters
        /// [Required]
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Card expiry year
        /// [Required]
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// Payment source type
        /// [Required]
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// The card's 3 or 4 digit card verification value (CVV) or security code
        /// [Optional]
        /// </summary>
        public string Cvv { get; set; }
    }
}