using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments.Contexts
{
    /// <summary>
    /// Schema validation tests for Checkout.Payments.Contexts.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class PaymentContextsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentContextsCustomerSummary -- format: date properties
        // ------------------------------------------------------------------------

        // registration_date, first_transaction_date and last_payment_date are all declared
        // "type": "string", "format": "date". The times supplied below are deliberately not
        // midnight, so the assertions prove truncation rather than passing because the caller
        // happened to supply a zero time.
        [Fact]
        public void ShouldSerializeCustomerSummaryDatesWithoutATimeComponent()
        {
            var json = Serializer.Serialize(new PaymentContextsCustomerSummary
            {
                RegistrationDate = new DateTime(2023, 5, 1, 13, 59, 0),
                FirstTransactionDate = new DateTime(2023, 7, 1, 1, 2, 0),
                LastPaymentDate = new DateTime(2023, 8, 1, 23, 59, 59)
            });

            json.ShouldContain("\"registration_date\":\"2023-05-01\"");
            json.ShouldContain("\"first_transaction_date\":\"2023-07-01\"");
            json.ShouldContain("\"last_payment_date\":\"2023-08-01\"");
            json.ShouldNotContain("T13:59");
            json.ShouldNotContain("T23:59:59");
        }

        [Fact]
        public void ShouldOmitCustomerSummaryDatesWhenNotSet()
        {
            var json = Serializer.Serialize(new PaymentContextsCustomerSummary());

            json.ShouldNotContain("registration_date");
            json.ShouldNotContain("first_transaction_date");
            json.ShouldNotContain("last_payment_date");
        }

        [Fact]
        public void ShouldDeserializeCustomerSummaryDatesFromDateOnlyValues()
        {
            const string json =
                "{\"registration_date\":\"2023-05-01\"," +
                "\"first_transaction_date\":\"2023-07-01\"," +
                "\"last_payment_date\":\"2023-08-01\"}";

            var summary = (PaymentContextsCustomerSummary)Serializer.Deserialize(
                json, typeof(PaymentContextsCustomerSummary));

            summary.RegistrationDate.ShouldBe(new DateTime(2023, 5, 1));
            summary.FirstTransactionDate.ShouldBe(new DateTime(2023, 7, 1));
            summary.LastPaymentDate.ShouldBe(new DateTime(2023, 8, 1));
        }

        // The spec's CustomerSummary declares eight properties. is_premium_customer,
        // is_returning_customer and lifetime_value were missing from the SDK, so they were
        // unsendable
        [Fact]
        public void ShouldSerializeEveryCustomerSummaryProperty()
        {
            var json = Serializer.Serialize(new PaymentContextsCustomerSummary
            {
                RegistrationDate = new DateTime(2023, 5, 1),
                FirstTransactionDate = new DateTime(2023, 7, 1),
                LastPaymentDate = new DateTime(2023, 8, 1),
                TotalOrderCount = 15,
                LastPaymentAmount = 500,
                IsPremiumCustomer = true,
                IsReturningCustomer = true,
                LifetimeValue = 500
            });

            json.ShouldContain("\"total_order_count\":15");
            json.ShouldContain("\"last_payment_amount\":500");
            json.ShouldContain("\"is_premium_customer\":true");
            json.ShouldContain("\"is_returning_customer\":true");
            json.ShouldContain("\"lifetime_value\":500");
        }

        [Fact]
        public void ShouldDeserializeEveryCustomerSummaryProperty()
        {
            const string json =
                "{\"total_order_count\":15,\"last_payment_amount\":500.5," +
                "\"is_premium_customer\":true,\"is_returning_customer\":false," +
                "\"lifetime_value\":1234.56}";

            var summary = (PaymentContextsCustomerSummary)Serializer.Deserialize(
                json, typeof(PaymentContextsCustomerSummary));

            summary.TotalOrderCount.ShouldBe(15);
            summary.LastPaymentAmount.ShouldBe(500.5);
            summary.IsPremiumCustomer.ShouldBe(true);
            summary.IsReturningCustomer.ShouldBe(false);
            summary.LifetimeValue.ShouldBe(1234.56);
        }

        [Fact]
        public void ShouldOmitTheNewCustomerSummaryPropertiesWhenNotSet()
        {
            var json = Serializer.Serialize(new PaymentContextsCustomerSummary());

            json.ShouldNotContain("is_premium_customer");
            json.ShouldNotContain("is_returning_customer");
            json.ShouldNotContain("lifetime_value");
        }

        // ------------------------------------------------------------------------
        // Airline data -- format: date properties
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAirlineDatesWithoutATimeComponent()
        {
            Serializer.Serialize(new PaymentContextsTicket
            {
                Number = "045-21351455613",
                IssueDate = new DateTime(2023, 5, 20, 8, 15, 0)
            }).ShouldContain("\"issue_date\":\"2023-05-20\"");

            Serializer.Serialize(new PaymentContextsPassenger
            {
                FirstName = "John",
                DateOfBirth = new DateTime(1990, 5, 26, 17, 5, 0)
            }).ShouldContain("\"date_of_birth\":\"1990-05-26\"");

            Serializer.Serialize(new PaymentContextsFlightLegDetails
            {
                DepartureDate = new DateTime(2023, 6, 19, 6, 40, 0)
            }).ShouldContain("\"departure_date\":\"2023-06-19\"");
        }

        [Fact]
        public void ShouldOmitAirlineDatesWhenNotSet()
        {
            Serializer.Serialize(new PaymentContextsTicket { Number = "1" })
                .ShouldNotContain("issue_date");
            Serializer.Serialize(new PaymentContextsPassenger { FirstName = "John" })
                .ShouldNotContain("date_of_birth");
            Serializer.Serialize(new PaymentContextsFlightLegDetails())
                .ShouldNotContain("departure_date");
        }

        // ------------------------------------------------------------------------
        // PaymentContextsGuests -- format: date property
        // ------------------------------------------------------------------------

        // DateOfBirth was a non-nullable DateTime, so NullValueHandling.Ignore could not
        // suppress it and an unset guest date of birth shipped as "0001-01-01T00:00:00".
        [Fact]
        public void ShouldOmitGuestDateOfBirthWhenNotSet()
        {
            var json = Serializer.Serialize(new PaymentContextsGuests { FirstName = "Jane" });

            json.ShouldNotContain("date_of_birth");
            json.ShouldNotContain("0001-01-01");
        }

        [Fact]
        public void ShouldSerializeGuestDateOfBirthAsADate()
        {
            var json = Serializer.Serialize(new PaymentContextsGuests
            {
                FirstName = "Jane",
                DateOfBirth = new DateTime(1985, 7, 14, 23, 59, 59)
            });

            json.ShouldContain("\"date_of_birth\":\"1985-07-14\"");
            json.ShouldNotContain("T23:59:59");
        }

        // ------------------------------------------------------------------------
        // PaymentContextsAirlineData -- cardinality
        //
        // The spec's airline_data[].ticket is a single object and passenger is an array.
        // Ticket was typed IList<PaymentContextsTicket>, so the SDK sent ticket as an array.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAirlineTicketAndASinglePassengerAsObjects()
        {
            var json = Serializer.Serialize(new PaymentContextsAirlineData
            {
                Ticket = new PaymentContextsTicket
                {
                    Number = "045-21351455613",
                    IssueDate = new DateTime(2023, 5, 20)
                },
                Passenger = new List<PaymentContextsPassenger>
                {
                    new PaymentContextsPassenger { FirstName = "John" }
                },
                FlightLegDetails = new List<PaymentContextsFlightLegDetails>
                {
                    new PaymentContextsFlightLegDetails
                    {
                        FlightNumber = "101",
                        ClassOfTravelling = "J",
                        StopOverCode = "x"
                    }
                }
            });

            json.ShouldContain("\"ticket\":{");
            json.ShouldNotContain("\"ticket\":[");
            // POST /payment-contexts rejects the array form of passenger with
            // passenger_required, so one passenger serializes as an object.
            json.ShouldContain("\"passenger\":{");
            json.ShouldContain("\"class_of_travelling\":\"J\"");
            json.ShouldContain("\"stop_over_code\":\"x\"");
            json.ShouldContain("\"flight_number\":\"101\"");
        }

        [Fact]
        public void ShouldDeserializeAirlineTicketFromAnObject()
        {
            const string json = @"{
                ""ticket"": { ""number"": ""045-21351455613"", ""issue_date"": ""2023-05-20"" },
                ""passenger"": [ { ""first_name"": ""John"" } ]
            }";

            var result = (PaymentContextsAirlineData)Serializer.Deserialize(
                json, typeof(PaymentContextsAirlineData));

            result.Ticket.ShouldNotBeNull();
            result.Ticket.Number.ShouldBe("045-21351455613");
            result.Ticket.IssueDate.ShouldBe(new DateTime(2023, 5, 20));
            result.Passenger.Count.ShouldBe(1);
            result.Passenger[0].FirstName.ShouldBe("John");
        }

        // PayPal is a payment-contexts payment method and returns passenger as a bare object.
        [Fact]
        public void ShouldDeserializeAirlinePassengerFromASingleObject()
        {
            const string json = @"{
                ""ticket"": { ""number"": ""045"" },
                ""passenger"": { ""first_name"": ""John"", ""date_of_birth"": ""1990-05-26"" }
            }";

            var result = (PaymentContextsAirlineData)Serializer.Deserialize(
                json, typeof(PaymentContextsAirlineData));

            result.Passenger.ShouldNotBeNull();
            result.Passenger.Count.ShouldBe(1);
            result.Passenger[0].FirstName.ShouldBe("John");
            result.Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
        }

        [Fact]
        public void ShouldRoundTripSerializeAirlineData()
        {
            var original = new PaymentContextsAirlineData
            {
                Ticket = new PaymentContextsTicket { Number = "045", TravelPackageIndicator = "B" },
                Passenger = new List<PaymentContextsPassenger>
                {
                    new PaymentContextsPassenger { FirstName = "John" },
                    new PaymentContextsPassenger { FirstName = "Jane" }
                }
            };

            var json = Serializer.Serialize(original);
            var result = (PaymentContextsAirlineData)Serializer.Deserialize(
                json, typeof(PaymentContextsAirlineData));

            result.Ticket.Number.ShouldBe("045");
            result.Ticket.TravelPackageIndicator.ShouldBe("B");
            result.Passenger.Count.ShouldBe(2);
            result.Passenger[1].FirstName.ShouldBe("Jane");
        }

        // ------------------------------------------------------------------------
        // PaymentContextsAccommodationRoom -- number_of_nights_at_room_rate is a string
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeNumberOfNightsAtRoomRateAsAString()
        {
            var json = Serializer.Serialize(new PaymentContextsAccommodationRoom
            {
                Rate = "70",
                NumberOfNightsAtRoomRate = "3"
            });

            json.ShouldContain("\"rate\":\"70\"");
            json.ShouldContain("\"number_of_nights_at_room_rate\":\"3\"");
        }

        [Fact]
        public void ShouldDeserializeNumberOfNightsAtRoomRateFromAString()
        {
            const string json = @"{ ""rate"": ""70"", ""number_of_nights_at_room_rate"": ""3"" }";

            var result = (PaymentContextsAccommodationRoom)Serializer.Deserialize(
                json, typeof(PaymentContextsAccommodationRoom));

            result.Rate.ShouldBe("70");
            result.NumberOfNightsAtRoomRate.ShouldBe("3");
        }

    }
}
