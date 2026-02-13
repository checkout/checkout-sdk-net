using System;

namespace Checkout.Issuing.Disputes
{
    public class DisputeArbitration
    {
        public DateTime? SubmittedOn { get; set; }

        public DisputeAmount Amount { get; set; }

        public string Justification { get; set; }

        public DateTime? DecidedOn { get; set; }
    }
}