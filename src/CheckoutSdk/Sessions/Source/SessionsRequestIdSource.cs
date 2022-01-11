namespace Checkout.Sessions.Source
{
    public class SessionsRequestIdSource : SessionSource
    {
        public string Id { get; set; }

        public SessionsRequestIdSource() : base(SessionSourceType.Id)
        {
        }
    }
}