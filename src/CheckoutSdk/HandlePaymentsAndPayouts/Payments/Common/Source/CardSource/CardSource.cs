using Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination.Common.AccountHolder;
using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    CardSource
{
    /// <summary>
    /// card source Class
    /// The source of the payment
    /// </summary>
    public class CardSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the CardSource class.
        /// </summary>
        public CardSource() : base(SourceType.Card)
        {
        }

        /// <summary>
        /// The expiry month
        /// [Required]
        /// [ 1 .. 2 ] characters  >= 1
        /// >= 1
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The expiry year
        /// [Required]
        /// 4 characters
        /// </summary>
        public int ExpiryYear { get; set; }

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
        /// <= 8
        /// </summary>
        public string Bin { get; set; }

        /// <summary>
        /// The payment source identifier that can be used for subsequent payments. For new sources, this will only be
        /// returned if the payment was approved
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The payment source owner's billing address
        /// [Optional]
        /// </summary>
        public BillingAddress.BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// The payment source owner's phone number
        /// [Optional]
        /// </summary>
        public Phone.Phone Phone { get; set; }

        /// <summary>
        /// The cardholder's name
        /// [Optional]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The card scheme
        /// [Optional]
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// [DEPRECATED]
        ///  Replaced by local_schemes
        /// The local co-branded card scheme.
        /// [Optional]
        /// </summary>
        [Obsolete("This property will be removed in the future, and should not be used.")]
        public string SchemeLocal { get; set; }

        /// <summary>
        /// The local co-branded card schemes.
        /// [Optional]
        /// </summary>
        public IList<string> LocalSchemes { get; set; }

        /// <summary>
        /// The card type
        /// [Optional]
        /// </summary>
        public CardType? CardType { get; set; }

        /// <summary>
        /// The card category
        /// [Optional]
        /// </summary>
        public CardCategoryType? CardCategory { get; set; }

        /// <summary>
        /// The card wallet type
        /// [Optional]
        /// </summary>
        public CardWalletType? CardWalletType { get; set; }

        /// <summary>
        /// The name of the card issuer
        /// [Optional]
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The card issuer's country (two-letter ISO code)
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string IssuerCountry { get; set; }

        /// <summary>
        /// The issuer/card scheme product identifier
        /// [Optional]
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The issuer/card scheme product type
        /// [Optional]
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// The Address Verification System check result
        /// [Optional]
        /// </summary>
        public string AvsCheck { get; set; }

        /// <summary>
        /// The card verification value (CVV) check result
        /// [Optional]
        /// </summary>
        public string CvvCheck { get; set; }

        /// <summary>
        /// A unique reference to the underlying card for network tokens (e.g., Apple Pay, Google Pay)
        /// [Optional]
        /// </summary>
        public string PaymentAccountReference { get; set; }

        /// <summary>
        /// The JWE encrypted full card number that has been updated by real-time account updater.
        /// [Optional]
        /// </summary>
        public string EncryptedCardNumber { get; set; }

        /// <summary>
        /// Specifies what card information was updated by the Real-Time Account Updater.
        /// [Optional]
        /// </summary>
        public AccountUpdateStatusType? AccountUpdateStatus { get; set; }

        /// <summary>
        /// Provides the failure code if the Real-Time Account Updater fails to update the card information.
        /// [Optional]
        /// </summary>
        public string AccountUpdateFailureCode { get; set; }

        /// <summary>
        /// Information about the account holder of the card
        /// [Optional]
        /// </summary>
        public AbstractAccountHolder AccountHolder { get; set; }
    }
}