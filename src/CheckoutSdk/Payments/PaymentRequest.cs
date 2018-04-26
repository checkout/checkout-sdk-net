using System;

namespace Checkout.Payments
{
    public class PaymentRequest<TSource> where TSource : class
    {
        public PaymentRequest(int? amount, string currency, TSource source)
        {
            if (amount.HasValue && amount < 0)
                throw new ArgumentNullException(nameof(amount));
            
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException(nameof(currency));
            
            Amount = amount;
            Currency = currency;
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
        
        public int? Amount { get; }
        public string Currency { get; }
        public TSource Source { get; }
    }
}