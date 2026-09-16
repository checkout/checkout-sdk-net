using System;
using Newtonsoft.Json;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsCustomerSummary
    {
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? RegistrationDate { get; set; }
        
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? FirstTransactionDate { get; set; }
        
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? LastPaymentDate { get; set; }
        
        public long? TotalOrderCount { get; set; }
        
        public double? LastPaymentAmount { get; set; }
    }
}