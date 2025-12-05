using Checkout.Common;

using System;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class AirlineData
    {
        public Ticket Ticket { get; set; }

        public IList<Passenger> Passengers { get; set; }

        public IList<FlightLegDetails> FlightLegDetails { get; set; }
    }

    public class Ticket
    {
        public string Number { get; set; }

        public DateTime? IssueDate { get; set; }

        public string IssuingCarrierCode { get; set; }

        public string TravelPackageIndicator { get; set; }

        public string TravelAgencyName { get; set; }

        public string TravelAgencyCode { get; set; }
    }

    public class Passenger
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public PassengerAddress Address { get; set; }
    }

    public class PassengerAddress
    {
        public string Country { get; set; }
    }

    public class FlightLegDetails
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