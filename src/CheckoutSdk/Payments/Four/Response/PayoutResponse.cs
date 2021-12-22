using Checkout.Common;

namespace Checkout.Payments.Four.Response
{
    public sealed class PayoutResponse : Resource
    {
        public string Id { get; set; }

        public PaymentStatus? Status { get; set; }

        public string Reference { get; set; }

        public PaymentInstructionResponse Instruction { get; set; }
             
    }
}