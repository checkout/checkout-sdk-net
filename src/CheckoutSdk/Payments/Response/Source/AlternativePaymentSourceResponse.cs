using Checkout.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Payments.Response.Source
{
    [Serializable]
    public sealed class AlternativePaymentSourceResponse : Dictionary<string, object>,
        IResponseSource
    {
        public AlternativePaymentSourceResponse()
        {
        }

        private AlternativePaymentSourceResponse(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }

        public PaymentSourceType? Type()
        {
            return CheckoutUtils.GetEnumFromStringMemberValue<PaymentSourceType>(
                (string)base[CheckoutUtils.Type]);
        }
    }
}