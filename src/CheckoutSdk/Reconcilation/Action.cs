using Checkout.Common;
using Checkout.Common.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
	public sealed class Action : BaseEquatable<Action>
	{
		public string Id { get; set; }
		public string Type { get; set; }

		public DateTime ProcessedOn { get; set; }

		public string ResponseCode { get; set; }

		public string ResponseDescription { get; set; }

		public IList<Breakdown> Breakdown { get; set; }

		public override bool EqualExp(Action other)
			=> Id.EqualsNull(other.Id);

		public override int GetHashCode()
			=> HashCode.Combine(Id);
    }

}