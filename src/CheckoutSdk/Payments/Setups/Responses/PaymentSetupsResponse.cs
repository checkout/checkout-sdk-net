using Checkout.Common;
using Checkout.Payments.Setups.Entities;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsResponse : Resource
    {
        public string ProcessingChannelId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public PaymentSetupsPaymentMethods PaymentMethods { get; set; }

        public PaymentSetupsSettings Settings { get; set; }

        public PaymentSetupsCustomer Customer { get; set; }

        public PaymentSetupsOrder Order { get; set; }

        public PaymentSetupsIndustry Industry { get; set; }
    }
}