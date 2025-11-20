namespace Checkout.Payments.Setups.Entities
{
    public class AirlineTicket
    {
        public string Number { get; set; }

        public string IssueDate { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string TravelPackageIndicator { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyCode { get; set; }
    }
}