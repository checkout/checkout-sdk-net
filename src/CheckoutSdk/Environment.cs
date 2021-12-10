namespace Checkout
{
    public enum Environment
    {
        [Environment("https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token")]
        Sandbox,

        [Environment("https://api.checkout.com/", "https://access.checkout.com/connect/token")]
        Production
    }
}