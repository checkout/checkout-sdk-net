namespace Checkout.Sessions
{
    public class Installment
    {
        public long? NumberOfPayments { get; set; }

        public long? DaysBetweenPayments { get; set; } = 1L;

        public string Expiry { get; set; } = "99991231";
    }
}