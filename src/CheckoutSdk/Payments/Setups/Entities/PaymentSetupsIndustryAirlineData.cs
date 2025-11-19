using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentSetupsIndustryAirlineData
    {
        public PaymentSetupsAirlineTicket Ticket { get; set; }

        public IList<PaymentSetupsAirlinePassenger> Passengers { get; set; }

        public IList<PaymentSetupsFlightLegDetails> FlightLegDetails { get; set; }
    }

    public class PaymentSetupsAirlineTicket
    {
        public string Number { get; set; }

        public string IssueDate { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string TravelPackageIndicator { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyCode { get; set; }
    }

    public class PaymentSetupsAirlinePassenger
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public Address Address { get; set; }
    }

    public class PaymentSetupsFlightLegDetails
    {
        public string FlightNumber { get; set; }

        public string CarrierCode { get; set; }

        public string ClassOfTravelling { get; set; }

        public string DepartureAirport { get; set; }

        public string DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalAirport { get; set; }

        public string StopOverCode { get; set; }

        public string FareBasisCode { get; set; }
    }
}