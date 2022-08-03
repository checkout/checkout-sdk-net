namespace Checkout.Payments
{
    public class Ticket
    {
        public string Number { get; set; }

        public string IssueDate { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyCode { get; set; }
    }
}