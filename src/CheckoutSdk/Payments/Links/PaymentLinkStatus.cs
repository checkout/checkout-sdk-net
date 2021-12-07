using System.Runtime.Serialization;

namespace Checkout.Payments.Links
{
    public enum PaymentLinkStatus
	{
		[EnumMember(Value = "Active")]
		ACTIVE,

		[EnumMember(Value = "Payment Received")]
		PAYMENT_RECEIVED,

		[EnumMember(Value = "Expired")]
		EXPIRED
	}

}