namespace Checkout.Sessions
{
    public class Installment
    {
        public long? NumberOfPayments { get; set; }

        public long? DaysBetweenPayments { get; set; }

        public string Expiry { get; set; }
    }
}