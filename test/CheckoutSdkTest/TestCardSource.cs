namespace Checkout
{
    public class TestCardSource
    {
        public string Name;
        public string Number;
        public int? ExpiryMonth;
        public int? ExpiryYear;
        public string Cvv;

        static TestCardSource()
        {
            Visa.Name = "Mr. Test";
            Visa.Number = "4242424242424242";
            Visa.ExpiryMonth = 6;
            Visa.ExpiryYear = 2025;
            Visa.Cvv = "100";
        }

        public static readonly TestCardSource Visa = new TestCardSource();
    }
}