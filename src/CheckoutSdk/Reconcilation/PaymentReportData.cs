using Checkout.Common;
using Checkout.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
	public sealed class PaymentReportData : EquatableResource<PaymentReportData>
	{
		public string Id { get; set; }

		public double ProcessingCurrency { get; set; }

		public double PayoutCurrency{ get; set; }

		public DateTime RequestedOn{ get; set; }

		public string ChannelName{ get; set; }

		public string Reference{ get; set; }

		public string PaymentMethod{ get; set; }

		public string CardType{ get; set; }

		public string CardCategory{ get; set; }

		public CountryCode IssuerCountry{ get; set; }

		public CountryCode MerchantCountry{ get; set; }

		public string Mid{ get; set; }

		public IList<Action> Actions{ get; set; }

		public override bool EqualExp(PaymentReportData other)
			=> Id.EqualsNull(other.Id);

		public override int GetHashCode()
			 => HashCode.Combine(Id);
	}
}