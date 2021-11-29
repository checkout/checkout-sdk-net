using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payments.Response.Destination
{
    [Serializable]
    public sealed class PaymentResponseAlternativeDestination : Dictionary<string, object>,
        IPaymentResponseDestination
    {
        public PaymentResponseAlternativeDestination()
        {
        }

        private PaymentResponseAlternativeDestination(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public PaymentDestinationType? Type()
        {
            return CheckoutUtils.GetEnumFromStringMemberValue<PaymentDestinationType>(
                (string) base[CheckoutUtils.Type]);
        }
    }
}