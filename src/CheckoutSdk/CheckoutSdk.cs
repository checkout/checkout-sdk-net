namespace Checkout
{
    public sealed class CheckoutSdk
    {
        private CheckoutSdk()
        {
        }

        public static CheckoutDefaultSdk DefaultSdk()
        {
            return new CheckoutDefaultSdk();
        }

        public static CheckoutFourSdk FourSdk()
        {
            return new CheckoutFourSdk();
        }
    }
}