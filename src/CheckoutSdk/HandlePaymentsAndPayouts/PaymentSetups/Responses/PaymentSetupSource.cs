using System.Collections.Generic;
using Checkout.Common;
using CardSource = Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource;
using AccountHolder = Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.CardSource.AccountHolder;

namespace Checkout.Payments.Setups
{
    /// <summary>
    /// Source information for payment setup confirm response
    /// </summary>
    public class PaymentSetupSource
    {
        /// <summary>
        /// The payment source type
        /// [Required]
        /// </summary>
        public PaymentSourceType? Type { get; set; }

        /// <summary>
        /// The expiry month
        /// [Required]
        /// </summary>
        public int? ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year
        /// [Required]
        /// </summary>
        public int? ExpiryYear { get; set; }

        /// <summary>
        /// The last four digits of the card number
        /// [Required]
        /// </summary>
        public string Last4 { get; set; }

        /// <summary>
        /// Uniquely identifies this particular card number. You can use this to compare cards across customers.
        /// [Required]
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// The card issuer's Bank Identification Number (BIN)
        /// [Required]
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The payment source identifier that can be used for subsequent payments. For new sources, this will only be returned if the payment was approved
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The payment source owner's billing address
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        /// The payment source owner's phone number
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// The cardholder's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The card scheme
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Deprecated Replaced by local_schemes
        /// The local co-branded card scheme.
        /// [Deprecated]
        /// </summary>
        public string SchemeLocal { get; set; }

        /// <summary>
        /// The local co-branded card schemes.
        /// </summary>
        public IList<string> LocalSchemes { get; set; }

        /// <summary>
        /// The card type
        /// </summary>
        public CardSource.CardType? CardType { get; set; }

        /// <summary>
        /// The card category
        /// </summary>
        public CardSource.CardCategoryType? CardCategory { get; set; }

        /// <summary>
        /// The card wallet type
        /// </summary>
        public CardSource.CardWalletType? CardWalletType { get; set; }

        /// <summary>
        /// The name of the card issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer's country (two-letter ISO code)
        /// </summary>
        public string IssuerCountry { get; set; }

        /// <summary>
        /// The issuer/card scheme product identifier
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The issuer/card scheme product type
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// The Address Verification System check result
        /// </summary>
        public string AvsCheck { get; set; }

        /// <summary>
        /// The card verification value (CVV) check result
        /// </summary>
        public string CvvCheck { get; set; }

        /// <summary>
        /// A unique reference to the underlying card for network tokens (e.g., Apple Pay, Google Pay)
        /// </summary>
        public string PaymentAccountReference { get; set; }

        /// <summary>
        /// The JWE encrypted full card number that has been updated by real-time account updater.
        /// </summary>
        public string EncryptedCardNumber { get; set; }

        /// <summary>
        /// Specifies what card information was updated by the Real-Time Account Updater.
        /// </summary>
        public CardSource.AccountUpdateStatusType? AccountUpdateStatus { get; set; }

        /// <summary>
        /// Provides the failure code if the Real-Time Account Updater fails to update the card information.
        /// </summary>
        public string AccountUpdateFailureCode { get; set; }

        /// <summary>
        /// Information about the account holder of the card
        /// </summary>
        public AccountHolder.AbstractAccountHolder AccountHolder { get; set; }
    }
}