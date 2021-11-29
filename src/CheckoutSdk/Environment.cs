namespace Checkout
{
    public enum Environment
    {
        [Environment("https://api.sandbox.checkout.com/")]
        Sandbox,

        [Environment("https://api.checkout.com/")]
        Production,
    }
}