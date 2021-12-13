using Checkout.Common;
using System;

namespace Checkout.Payments.Hosted
{

	public sealed class HostedPaymentResponse : Resource
	{
		public string Reference { get; set; }
    }
}