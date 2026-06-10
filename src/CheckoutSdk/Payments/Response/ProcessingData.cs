using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Response
{
    public class ProcessingData
    {
        public PreferredSchema? PreferredScheme { get; set; }
        
        public string AppId { get; set; }
        
        public string PartnerCustomerId { get; set; }
        
        public string PartnerPaymentId { get; set; }
        
        public long? TaxAmount { get; set; }
        
        public CountryCode? PurchaseCountry { get; set; }
        
        public string Locale { get; set; }
        
        public string RetrievalReferenceNumber { get; set; }
        
        public string PartnerOrderId { get; set; }
        
        public string PartnerStatus { get; set; }
        
        public string PartnerTransactionId { get; set; }
        
        public IList<string> PartnerErrorCodes { get; set; }
        
        public string PartnerErrorMessage { get; set; }
        
        public string PartnerAuthorizationCode { get; set; }
        
        public string PartnerAuthorizationResponseCode { get; set; }
        
        public string FraudStatus { get; set; }
        
        public ProviderAuthorizedPaymentMethod ProviderAuthorizedPaymentMethod { get; set; }
        
        public IList<string> CustomPaymentMethodIds { get; set; }

        public bool? Aft { get; set; }
        
        public string MerchantCategoryCode { get; set; }
        
        public string SchemeMerchantId { get; set; }
        
        public PanProcessedType? PanTypeProcessed { get; set; }
        
        public bool? CkoNetworkTokenAvailable { get; set; }

        /// <summary>
        /// Indicates whether the fallback_source field was used for the payment.
        /// </summary>
        public bool? FallbackSourceUsed { get; set; }

        /// <summary>
        /// A high-level failure category returned by the payment provider when a payment is declined or fails.
        /// Not all payment methods return this field.
        /// [Optional]
        /// </summary>
        public string FailureCode { get; set; }

        /// <summary>
        /// The 6-digit partner code returned by the payment provider. Returned when source.type is blik.
        /// [Optional]
        /// Pattern: ^\d{6}$
        /// 6 characters
        /// </summary>
        public string PartnerCode { get; set; }

        /// <summary>
        /// The raw response code returned by the payment provider when a payment is declined or fails.
        /// Not all payment methods return this field.
        /// [Optional]
        /// </summary>
        public string PartnerResponseCode { get; set; }

        /// <summary>
        /// The scheme transaction link identifier. Returned for Mastercard transactions when the scheme
        /// provides a link identifier that ties together related transactions on the network
        /// (see Mastercard Transaction Link Identifier documentation).
        /// [Optional]
        /// </summary>
        public string SchemeTransactionLinkId { get; set; }
    }
}