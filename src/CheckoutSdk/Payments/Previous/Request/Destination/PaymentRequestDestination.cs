namespace Checkout.Payments.Previous.Request.Destination
{
    public abstract class PaymentRequestDestination
    {
        public PaymentDestinationType? Type { get; }

        protected PaymentRequestDestination(PaymentDestinationType type)
        {
            Type = type;
        }
    }
}