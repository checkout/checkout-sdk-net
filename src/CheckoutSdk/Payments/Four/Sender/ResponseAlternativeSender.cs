using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payments.Four.Sender
{
    [Serializable]
    public sealed class ResponseAlternativeSender : Dictionary<string, object>,
        ISender
    {
        public ResponseAlternativeSender()
        {
        }

        private ResponseAlternativeSender(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public PaymentSenderType? Type()
        {
            return CheckoutUtils.GetEnumFromStringMemberValue<PaymentSenderType>(
                (string)base[CheckoutUtils.Type]);
        }
    }
}