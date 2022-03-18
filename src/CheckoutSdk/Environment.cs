namespace Checkout
{
    public enum Environment
    {
        [Environment("https://api.sandbox.checkout.com/",
            "https://access.sandbox.checkout.com/connect/token",
            "https://files.sandbox.checkout.com/",
            "https://transfers.sandbox.checkout.com/",
            "https://balances.sandbox.checkout.com/")]
        Sandbox,

        [Environment("https://api.checkout.com/",
            "https://access.checkout.com/connect/token",
            "https://files.checkout.com/",
            "https://transfers.checkout.com/",
            "https://balances.checkout.com/")]
        Production
    }
}