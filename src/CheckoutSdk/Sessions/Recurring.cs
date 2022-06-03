namespace Checkout.Sessions
{
    public class Recurring
    {
        public long? DaysBetweenPayments { get; set; }

        public string Expiry { get; set; }
    }
}