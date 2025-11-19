using System;

namespace Checkout.Payments
{
    public class FlightLegDetails
    {
        public long? FlightNumber { get; set; }

        public string CarrierCode { get; set; }

        public string ServiceClass { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }

        public string StopoverCode { get; set; }

        public string FareBasisCode { get; set; }
    }
}