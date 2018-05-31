namespace Checkout.Extensions
{
    internal class CheckoutOptions
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public bool Sandbox { get; set; }
        public string Uri { get; set; }
    }
}