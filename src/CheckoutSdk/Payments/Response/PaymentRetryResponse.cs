using System;

namespace Checkout.Payments.Response
{
    public class PaymentRetryResponse
    {
        public int? AttemptsMade { get; set; }
        
        public int? MaxAttempts { get; set; }

        public DateTime EndsOn { get; set; }

        public DateTime NextAttemptOn { get; set; }
    }
}