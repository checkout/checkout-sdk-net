namespace Checkout
{
    public class CheckoutArgumentException : CheckoutException
    {
        public CheckoutArgumentException(string message) : base(message)
        {
        }

        public static CheckoutArgumentException WithMessage(string message)
        {
            return new CheckoutArgumentException(message);
        }
    }
}