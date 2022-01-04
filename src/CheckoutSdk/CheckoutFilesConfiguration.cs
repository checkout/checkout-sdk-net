namespace Checkout
{
    public class CheckoutFilesConfiguration
    {
        private Environment? _environment { get; set; }

        public CheckoutFilesConfiguration(Environment? enviroment)
        {
            _environment = enviroment;
        }

        public CheckoutFilesConfiguration()
        {
        }


        public Environment? GetEnvironment()
        {
            return _environment;
        }
    }
}