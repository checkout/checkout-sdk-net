using Checkout.Common;

namespace Checkout.Payments.Setups
{
    /// <summary>
    /// Source information for payment setup confirm response
    /// </summary>
    public class PaymentSetupSource
    {
        /// <summary>
        /// The type of the payment source
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The unique identifier of the payment source
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The billing address associated with the payment source
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The phone number associated with the payment source
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The card scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The last four digits of the card number
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// A unique fingerprint of the underlying card number
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// The card BIN
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The card type
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// The card category
        /// </summary>
        public string CardCategory { get; set; }

        /// <summary>
        /// The name of the card issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer country ISO2 code
        /// </summary>
        public CountryCode? IssuerCountry { get; set; }

        /// <summary>
        /// The card product type
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// The Address Verification System check result
        /// </summary>
        public string AvsCheck { get; set; }

        /// <summary>
        /// The CVV check result
        /// </summary>
        public string CvvCheck { get; set; }

        /// <summary>
        /// The Payment Account Reference (PAR)
        /// </summary>
        public string PaymentAccountReference { get; set; }
    }
}