namespace Checkout.Payments.Setups.Entities
{
    public class AirlineTicket
    {
        /// <summary>
        /// The airline ticket number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The date when the ticket was issued in YYYY-MM-DD format
        /// </summary>
        public string IssueDate { get; set; }

        /// <summary>
        /// The IATA code of the airline that issued the ticket
        /// </summary>
        public string IssuingCarrierCode { get; set; }

        /// <summary>
        /// Indicates if this is part of a travel package (Y/N)
        /// </summary>
        public string TravelPackageIndicator { get; set; }

        /// <summary>
        /// The name of the travel agency that sold the ticket
        /// </summary>
        public string TravelAgencyName { get; set; }

        /// <summary>
        /// The IATA code of the travel agency that sold the ticket
        /// </summary>
        public string TravelAgencyCode { get; set; }
    }
}