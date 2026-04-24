using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Payments
{
    public class AccommodationDataSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var data = new AccommodationData
            {
                Name = "Grand Hotel",
                CheckInDate = DateTime.Parse("2025-06-01"),
                CheckOutDate = DateTime.Parse("2025-06-05")
            };

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalProperties()
        {
            var data = new AccommodationData
            {
                Name = "Grand Hotel",
                BookingReference = "BK-12345",
                CheckInDate = DateTime.Parse("2025-06-01"),
                CheckOutDate = DateTime.Parse("2025-06-05"),
                Address = new Address
                {
                    AddressLine1 = "123 Main St",
                    AddressLine2 = "Floor 2",
                    City = "London",
                    State = "England",
                    Zip = "EC1A 1BB",
                    Country = CountryCode.GB
                },
                State = CountryCode.GB,
                Country = CountryCode.GB,
                City = "London",
                NumberOfRooms = 2,
                Guests = new List<PaymentContextsGuests>
                {
                    new PaymentContextsGuests
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        DateOfBirth = DateTime.Parse("1985-03-15")
                    }
                },
                Room = new List<PaymentContextsAccommodationRoom>
                {
                    new PaymentContextsAccommodationRoom
                    {
                        Rate = "150.00",
                        NumberOfNightsAtRoomRate = 4
                    }
                },
                PropertyPhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone
                    {
                        CountryCode = "44",
                        Number = "2071234567"
                    }
                },
                CustomerServicePhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone
                    {
                        CountryCode = "44",
                        Number = "8001234567"
                    }
                }
            };

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new AccommodationData
            {
                Name = "Grand Hotel",
                BookingReference = "BK-12345",
                CheckInDate = DateTime.Parse("2025-06-01"),
                CheckOutDate = DateTime.Parse("2025-06-05"),
                City = "London",
                NumberOfRooms = 2,
                PropertyPhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone { CountryCode = "44", Number = "2071234567" }
                },
                CustomerServicePhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone { CountryCode = "1", Number = "8001234567" }
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (AccommodationData)Serializer.Deserialize(json, typeof(AccommodationData));

            deserialized.Name.ShouldBe("Grand Hotel");
            deserialized.BookingReference.ShouldBe("BK-12345");
            deserialized.City.ShouldBe("London");
            deserialized.NumberOfRooms.ShouldBe(2);
            deserialized.PropertyPhone.ShouldNotBeNull();
            deserialized.PropertyPhone.Count.ShouldBe(1);
            deserialized.PropertyPhone[0].CountryCode.ShouldBe("44");
            deserialized.PropertyPhone[0].Number.ShouldBe("2071234567");
            deserialized.CustomerServicePhone.ShouldNotBeNull();
            deserialized.CustomerServicePhone.Count.ShouldBe(1);
            deserialized.CustomerServicePhone[0].CountryCode.ShouldBe("1");
            deserialized.CustomerServicePhone[0].Number.ShouldBe("8001234567");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{
                ""name"": ""Grand Hotel"",
                ""booking_reference"": ""BK-12345"",
                ""check_in_date"": ""2025-06-01T00:00:00"",
                ""check_out_date"": ""2025-06-05T00:00:00"",
                ""city"": ""London"",
                ""number_of_rooms"": 2,
                ""property_phone"": [
                    { ""country_code"": ""44"", ""number"": ""2071234567"" }
                ],
                ""customer_service_phone"": [
                    { ""country_code"": ""44"", ""number"": ""8001234567"" }
                ]
            }";

            var result = (AccommodationData)Serializer.Deserialize(json, typeof(AccommodationData));

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Grand Hotel");
            result.BookingReference.ShouldBe("BK-12345");
            result.City.ShouldBe("London");
            result.NumberOfRooms.ShouldBe(2);
            result.PropertyPhone.ShouldNotBeNull();
            result.PropertyPhone.Count.ShouldBe(1);
            result.PropertyPhone[0].CountryCode.ShouldBe("44");
            result.PropertyPhone[0].Number.ShouldBe("2071234567");
            result.CustomerServicePhone.ShouldNotBeNull();
            result.CustomerServicePhone[0].Number.ShouldBe("8001234567");
        }

        [Fact]
        public void ShouldSerializeSnakeCaseKeys()
        {
            var data = new AccommodationData
            {
                Name = "Hotel",
                PropertyPhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone { CountryCode = "44", Number = "123" }
                },
                CustomerServicePhone = new List<AccommodationPhone>
                {
                    new AccommodationPhone { CountryCode = "1", Number = "456" }
                }
            };

            var json = Serializer.Serialize(data);

            json.ShouldContain("\"property_phone\"");
            json.ShouldContain("\"customer_service_phone\"");
            json.ShouldContain("\"country_code\"");
        }
    }
}
