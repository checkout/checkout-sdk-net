using Checkout.Common;
using System;

namespace Checkout.Payments.Four.Response
{
    public sealed class PaymentInstructionResponse : Resource
    {
        public DateTime? ValueDate { get; set; }
     
    }
}