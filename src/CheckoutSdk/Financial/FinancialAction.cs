using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Financial
{
    public class FinancialAction
    {
        public string PaymentId { get; set; }

        public string ActionId { get; set; }

        public string ActionType { get; set; }

        public string EntityId { get; set; }

        public string SubEntityId { get; set; }

        public string CurrencyAccountId { get; set; }

        public string PaymentMethod { get; set; }

        public string ProcessingChannelId { get; set; }

        public string Reference { get; set; }

        public string Mid { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public Region? Region { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public string MerchantCategoryCode { get; set; }

        public string FxTradeId { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public DateTime? RequestedOn { get; set; }

        public IList<ActionBreakdown> Breakdown { get; set; }
    }
}
