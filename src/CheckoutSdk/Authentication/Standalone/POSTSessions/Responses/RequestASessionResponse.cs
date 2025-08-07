namespace Checkout.Authentication.Standalone.POSTSessions.Responses
{
    public class RequestASessionResponse
    {
        public RequestASessionResponseAccepted.RequestASessionResponseAccepted Accepted { get; set; }
        public RequestASessionResponseCreated.RequestASessionResponseCreated Created { get; set; }

        public RequestASessionResponse(RequestASessionResponseAccepted.RequestASessionResponseAccepted accepted)
        {
            Accepted = accepted;
        }

        public RequestASessionResponse(RequestASessionResponseCreated.RequestASessionResponseCreated created)
        {
            Created = created;
        }
    }
}