namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Destination
{
    /// <summary>
    /// Abstract destination Class
    /// The destination of the unreferenced refund.
    /// </summary>
    public abstract class AbstractDestination
    {
        public DestinationType Type;

        protected AbstractDestination(DestinationType type)
        {
            Type = type;
        }
    }
}