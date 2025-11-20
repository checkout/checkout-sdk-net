using Checkout.Common;
using Checkout.Payments.Setups.Entities;

namespace Checkout.Payments.Setups
{
    public class PaymentSetupsCreateRequest
    {
        public string ProcessingChannelId { get; set; }

        public long? Amount { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public PaymentMethods PaymentMethods { get; set; }

        public Settings Settings { get; set; }

        public Customer Customer { get; set; }

        public Order Order { get; set; }

        public Industry Industry { get; set; }
    }
}