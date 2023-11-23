using System;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsTicket
    {
        public string Number { get; set; }

        public DateTime? IssueDate { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string TravelPackageIndicator { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyCode { get; set; }
    }
}