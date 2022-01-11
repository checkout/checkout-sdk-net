namespace Checkout
{
    public class CheckoutFilesConfiguration
    {
        public Environment? Environment { get; }

        public CheckoutFilesConfiguration(Environment? environment)
        {
            Environment = environment;
        }
    }
}