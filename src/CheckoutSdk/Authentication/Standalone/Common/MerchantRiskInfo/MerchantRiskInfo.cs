using System;

namespace Checkout.Authentication.Standalone.Common.MerchantRiskInfo
{
    /// <summary>
    /// merchant_risk_info
    /// Additional information about the cardholder's purchase.
    /// </summary>
    public class MerchantRiskInfo
    {
        /// <summary>
        /// For Electronic delivery, the email address to which the merchandise was delivered.
        /// [Optional]
        /// <= 254
        /// </summary>
        public string DeliveryEmail { get; set; }

        /// <summary>
        /// Indicates the merchandise delivery timeframe.
        /// [Optional]
        /// </summary>
        public DeliveryTimeframeType? DeliveryTimeframe { get; set; }

        /// <summary>
        /// Indicates whether the cardholder is placing an order for merchandise with a future availability or release
        /// date.
        /// [Optional]
        /// </summary>
        public bool IsPreorder { get; set; }

        /// <summary>
        /// Indicates whether the cardholder is reordering previously purchased merchandise.
        /// [Optional]
        /// </summary>
        public bool IsReorder { get; set; }

        /// <summary>
        /// Indicates the shipping method chosen for the transaction. Please choose an option that accurately describes
        /// the cardholder's specific transaction.
        /// [Optional]
        /// </summary>
        public ShippingIndicatorType? ShippingIndicator { get; set; }

        /// <summary>
        /// Specifies whether the cardholder is reordering merchandise they've previously purchased.
        /// [Optional]
        /// </summary>
        public ReorderItemsIndicatorType? ReorderItemsIndicator { get; set; }

        /// <summary>
        /// Specifies whether the cardholder is placing an order for merchandise with an availability date or release
        /// date in the future.
        /// [Optional]
        /// </summary>
        public PreOrderPurchaseIndicatorType? PreOrderPurchaseIndicator { get; set; }

        /// <summary>
        /// The UTC date the pre-ordered merchandise is expected to be available, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime PreOrderDate { get; set; }

        /// <summary>
        /// The total purchase amount, in major units. For example, the major unit amount for a gift card purchase of
        /// 135.20 USD is 135. Only applicable for prepaid or gift card purchases.
        /// [Optional]
        /// <= 15
        /// </summary>
        public string GiftCardAmount { get; set; }

        /// <summary>
        /// The currency code of the gift card,  as a three-digit ISO 4217 code. Only applicable for prepaid or gift
        /// card purchases.
        /// [Optional]
        /// 3 characters
        /// </summary>
        public string GiftCardCurrency { get; set; }

        /// <summary>
        /// The total number of individual prepaid cards, gift cards, or gift codes purchased. Only applicable for
        /// prepaid or gift card purchases.
        /// [Optional]
        /// 2 characters
        /// </summary>
        public string GiftCardCount { get; set; }
    }
}