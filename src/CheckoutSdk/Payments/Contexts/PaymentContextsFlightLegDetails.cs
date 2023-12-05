using System;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsFlightLegDetails
    {
        public string FlightNumber { get; set; }

        public string CarrierCode { get; set; }

        public string ClassOfTravelling { get; set; }

        public string DepartureAirport { get; set; }

        public DateTime? DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalAirport { get; set; }

        public string StopOverCode { get; set; }

        public string FareBasisCode { get; set; }
    }
}