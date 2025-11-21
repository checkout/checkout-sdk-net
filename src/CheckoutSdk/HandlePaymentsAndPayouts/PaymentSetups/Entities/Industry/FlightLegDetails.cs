namespace Checkout.Payments.Setups.Entities
{
    public class FlightLegDetails
    {
        /// <summary>
        /// The flight number for this leg of the journey
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// The IATA code of the airline operating this flight leg
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// The class of service (e.g., Y for Economy, C for Business, F for First)
        /// </summary>
        public string ClassOfTravelling { get; set; }

        /// <summary>
        /// The IATA code of the departure airport
        /// </summary>
        public string DepartureAirport { get; set; }

        /// <summary>
        /// The departure date in YYYY-MM-DD format
        /// </summary>
        public string DepartureDate { get; set; }

        /// <summary>
        /// The departure time in HH:MM format
        /// </summary>
        public string DepartureTime { get; set; }

        /// <summary>
        /// The IATA code of the arrival airport
        /// </summary>
        public string ArrivalAirport { get; set; }

        /// <summary>
        /// Code indicating if there are stopovers (O for stopover, X for no stopover)
        /// </summary>
        public string StopOverCode { get; set; }

        /// <summary>
        /// The fare basis code that determines the fare rules and restrictions
        /// </summary>
        public string FareBasisCode { get; set; }
    }
}