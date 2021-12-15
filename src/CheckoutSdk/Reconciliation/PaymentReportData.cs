using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class PaymentReportData : Resource
    {
        public string Id { get; set; }

        public Currency? ProcessingCurrency { get; set; }

        public Currency? PayoutCurrency { get; set; }

        public DateTime? RequestedOn { get; set; }

        public string ChannelName { get; set; }

        public string Reference { get; set; }

        public string PaymentMethod { get; set; }

        public CardType? CardType { get; set; }

        public CardCategory? CardCategory { get; set; }

        public CountryCode? IssuerCountry { get; set; }

        public CountryCode? MerchantCountry { get; set; }

        public string Mid { get; set; }

        public IList<Action> Actions { get; set; }
    }
}