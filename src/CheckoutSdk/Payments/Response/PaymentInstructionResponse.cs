using Checkout.Common;
using System;

namespace Checkout.Payments.Response
{
    public class PaymentInstructionResponse : Resource
    {
        public DateTime? ValueDate { get; set; }
    }
}