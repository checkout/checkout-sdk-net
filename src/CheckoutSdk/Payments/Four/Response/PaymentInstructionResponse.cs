using Checkout.Common;
using System;

namespace Checkout.Payments.Four.Response
{
    public class PaymentInstructionResponse : Resource
    {
        public DateTime? ValueDate { get; set; }
    }
}