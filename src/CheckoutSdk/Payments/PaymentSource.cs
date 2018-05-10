using System;

namespace Checkout.Payments
{
    public abstract class PaymentSource
    {
        public PaymentSource(string type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        
        public int Id { get; }
        public string Type { get; }
    }
}