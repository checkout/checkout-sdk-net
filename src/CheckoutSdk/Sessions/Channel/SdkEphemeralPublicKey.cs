namespace Checkout.Sessions.Channel
{
    public class SdkEphemeralPublicKey
    {
        public string Kty { get; set; }
        public string Crv { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
    }
}