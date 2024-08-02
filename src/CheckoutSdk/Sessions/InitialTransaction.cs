namespace Checkout.Sessions
{
    public class InitialTransaction
    {
        public string AcsTransactionId { get; set; }
        
        public string AuthenticationMethod { get; set; }
        
        public string AuthenticationTimestamp { get; set; }
        
        public string AuthenticationData { get; set; }
        
        public string InitialSessionId { get; set; }
    }
}