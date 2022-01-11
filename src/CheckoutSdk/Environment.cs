namespace Checkout
{
    public enum Environment
    {
        [Environment("https://api.sandbox.checkout.com/", "https://access.sandbox.checkout.com/connect/token", "https://files.sandbox.checkout.com/")]
        Sandbox,

        [Environment("https://api.checkout.com/", "https://access.checkout.com/connect/token", "https://files.checkout.com/")]
        Production
    }
}