namespace Checkout.Sessions.Source
{
    public class SessionIdSource : SessionSource
    {
        public string Id { get; set; }

        public SessionIdSource() : base(SessionSourceType.Id)
        {
        }
    }
}