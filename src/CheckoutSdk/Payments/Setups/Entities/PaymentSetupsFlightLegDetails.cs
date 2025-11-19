namespace Checkout.Payments.Setups.Entities
{
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