namespace Checkout
{
    public sealed class CheckoutSdk
    {
        private CheckoutSdk()
        {
        }

        public static CheckoutSdkBuilder Builder()
        {
            return new CheckoutSdkBuilder();
        }
    }
}