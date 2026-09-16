using System;
using Newtonsoft.Json;

namespace Checkout.Payments
{
    public class FlightLegDetails
    {
        public long? FlightNumber { get; set; }

        public string CarrierCode { get; set; }

        public string ServiceClass { get; set; }

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime? DepartureDate { get; set; }

        public string DepartureTime { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }

        public string StopoverCode { get; set; }

        public string FareBasisCode { get; set; }
    }
}