using System;

namespace Checkout.Payments
{
    public abstract class PaymentSource
    {
        public PaymentSource(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        
        public string Type { get; }
    }
}