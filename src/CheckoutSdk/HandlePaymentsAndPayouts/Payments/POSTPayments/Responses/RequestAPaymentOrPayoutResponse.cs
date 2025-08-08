namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Responses
{
    public class RequestAPaymentOrPayoutResponse
    {
        public RequestAPaymentOrPayoutResponseAccepted.RequestAPaymentOrPayoutResponseAccepted Accepted { get; set; }
        public RequestAPaymentOrPayoutResponseCreated.RequestAPaymentOrPayoutResponseCreated Created { get; set; }

        public RequestAPaymentOrPayoutResponse(RequestAPaymentOrPayoutResponseAccepted.RequestAPaymentOrPayoutResponseAccepted accepted)
        {
            Accepted = accepted;
        }

        public RequestAPaymentOrPayoutResponse(RequestAPaymentOrPayoutResponseCreated.RequestAPaymentOrPayoutResponseCreated created)
        {
            Created = created;
        }
    }
}