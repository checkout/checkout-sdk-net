namespace Checkout.Sessions
{
    public class SessionResponse
    {
        public CreateSessionAcceptedResponse Accepted { get; set; }
        public CreateSessionOkResponse Created { get; set; }

        public SessionResponse(CreateSessionAcceptedResponse accepted)
        {
            Accepted = accepted;
        }

        public SessionResponse(CreateSessionOkResponse created)
        {
            Created = created;
        }
    }
}