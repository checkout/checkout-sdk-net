using Checkout.Common;
using Checkout.Payments.Contexts;
using Checkout.Payments.Request;
using Checkout.Payments;
using Product = Checkout.Payments.Request.Product;
using Shouldly;
using System.Collections.Generic;
using System;
using Xunit;

namespace Checkout.Payments
{
    /// <summary>
    /// Schema validation tests for Checkout.Payments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class PaymentRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // Product
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldOmitTypeAndSubTypeWhenNull()
        {
            var product = new Product
            {
                Name = "Gold Necklace",
                Quantity = 1,
                UnitPrice = 5000
            };

            var json = Serializer.Serialize(product);

            json.ShouldNotContain("\"type\"");
            json.ShouldNotContain("\"sub_type\"");
        }

        [Fact]
        public void ShouldSerializeTypeWhenSet()
        {
            var product = new Product
            {
                Type = ItemType.Physical,
                Name = "Gold Necklace",
                Quantity = 1,
                UnitPrice = 5000
            };

            var json = Serializer.Serialize(product);

            json.ShouldContain("\"type\":\"physical\"");
        }

        [Fact]
        public void ShouldSerializeSubTypeWhenSet()
        {
            var product = new Product
            {
                Type = ItemType.Digital,
                SubType = ItemSubType.Cryptocurrency,
                Name = "Bitcoin",
                Quantity = 1,
                UnitPrice = 10000
            };

            var json = Serializer.Serialize(product);

            json.ShouldContain("\"type\":\"digital\"");
            json.ShouldContain("\"sub_type\":\"cryptocurrency\"");
        }

        [Fact]
        public void ShouldRoundTripAllProperties()
        {
            var original = new Product
            {
                Type = ItemType.Physical,
                SubType = ItemSubType.Nft,
                Name = "Gold Necklace",
                Quantity = 2,
                UnitPrice = 5000,
                Reference = "858818ac",
                CommodityCode = "DEF123",
                UnitOfMeasure = "metres",
                TotalAmount = 29000,
                TaxAmount = 1000,
                TaxRate = 2000,
                DiscountAmount = 1000,
                WxpayGoodsId = "1001",
                ImageUrl = "https://example.com/image.jpg",
                Url = "https://example.com/product",
                Sku = "SKU-001"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (Product)Serializer.Deserialize(json, typeof(Product));

            deserialized.ShouldNotBeNull();
            deserialized.Type.ShouldBe(ItemType.Physical);
            deserialized.SubType.ShouldBe(ItemSubType.Nft);
            deserialized.Name.ShouldBe("Gold Necklace");
            deserialized.Quantity.ShouldBe(2L);
            deserialized.UnitPrice.ShouldBe(5000L);
            deserialized.Reference.ShouldBe("858818ac");
            deserialized.CommodityCode.ShouldBe("DEF123");
            deserialized.UnitOfMeasure.ShouldBe("metres");
            deserialized.TotalAmount.ShouldBe(29000L);
            deserialized.TaxAmount.ShouldBe(1000L);
            deserialized.TaxRate.ShouldBe(2000L);
            deserialized.DiscountAmount.ShouldBe(1000L);
            deserialized.WxpayGoodsId.ShouldBe("1001");
            deserialized.ImageUrl.ShouldBe("https://example.com/image.jpg");
            deserialized.Url.ShouldBe("https://example.com/product");
            deserialized.Sku.ShouldBe("SKU-001");
        }

        [Fact]
        public void ShouldDeserializeTypeAndSubTypeFromJson()
        {
            const string json = @"{
                ""type"": ""digital"",
                ""sub_type"": ""stablecoin"",
                ""name"": ""USDC"",
                ""quantity"": 1,
                ""unit_price"": 50
            }";

            var product = (Product)Serializer.Deserialize(json, typeof(Product));

            product.ShouldNotBeNull();
            product.Type.ShouldBe(ItemType.Digital);
            product.SubType.ShouldBe(ItemSubType.Stablecoin);
            product.Name.ShouldBe("USDC");
        }

        [Fact]
        public void ShouldDeserializeNullTypeAndSubTypeFromJsonWithoutThoseFields()
        {
            const string json = @"{""name"": ""Widget"", ""quantity"": 3, ""unit_price"": 100}";

            var product = (Product)Serializer.Deserialize(json, typeof(Product));

            product.ShouldNotBeNull();
            product.Type.ShouldBeNull();
            product.SubType.ShouldBeNull();
            product.Name.ShouldBe("Widget");
        }

        // ------------------------------------------------------------------------
        // AccommodationData
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var data = new AccommodationData
            {
                Name = "Grand Hotel",
                CheckInDate = DateTime.Parse("2025-06-01"),
                CheckOutDate = DateTime.Parse("2025-06-05")
            };

            var json = Serializer.Serialize(data);

            // check_in_date and check_out_date are format: date, so no time component may reach
            // the wire. Should.NotThrow alone could never fail, which is how the timestamp bug
            // survived review.
            json.ShouldContain("\"check_in_date\":\"2025-06-01\"");
            json.ShouldContain("\"check_out_date\":\"2025-06-05\"");
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
                State = "FL",
                Country = "USA",
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
                        NumberOfNightsAtRoomRate = "4"
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

        // ------------------------------------------------------------------------
        // PaymentRouting
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializePaymentRoutingWithAllSchemes()
        {
            var routing = new PaymentRouting
            {
                Attempts = new List<PaymentRoutingAttempt>
                {
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                }
            };

            var json = Serializer.Serialize(routing);

            json.ShouldContain("\"mastercard\"");
            json.ShouldContain("\"visa\"");
        }

        [Fact]
        public void ShouldRoundTripSerializePaymentRouting()
        {
            var original = new PaymentRouting
            {
                Attempts = new List<PaymentRoutingAttempt>
                {
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                    new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                }
            };

            var json = Serializer.Serialize(original);
            var result = (PaymentRouting)Serializer.Deserialize(json, typeof(PaymentRouting));

            result.ShouldNotBeNull();
            result.Attempts.Count.ShouldBe(2);
            result.Attempts[0].Scheme.ShouldBe(PaymentRoutingScheme.Mastercard);
            result.Attempts[1].Scheme.ShouldBe(PaymentRoutingScheme.Visa);
        }

        [Fact]
        public void ShouldDeserializeAllSchemeValues()
        {
            const string json = @"{
                ""attempts"": [
                    { ""scheme"": ""accel"" },
                    { ""scheme"": ""amex"" },
                    { ""scheme"": ""cartes_bancaires"" },
                    { ""scheme"": ""diners"" },
                    { ""scheme"": ""discover"" },
                    { ""scheme"": ""jcb"" },
                    { ""scheme"": ""mada"" },
                    { ""scheme"": ""maestro"" },
                    { ""scheme"": ""mastercard"" },
                    { ""scheme"": ""nyce"" },
                    { ""scheme"": ""omannet"" },
                    { ""scheme"": ""pulse"" },
                    { ""scheme"": ""shazam"" },
                    { ""scheme"": ""star"" },
                    { ""scheme"": ""upi"" },
                    { ""scheme"": ""visa"" }
                ]
            }";

            var result = (PaymentRouting)Serializer.Deserialize(json, typeof(PaymentRouting));

            result.ShouldNotBeNull();
            result.Attempts.Count.ShouldBe(16);
            result.Attempts[0].Scheme.ShouldBe(PaymentRoutingScheme.Accel);
            result.Attempts[1].Scheme.ShouldBe(PaymentRoutingScheme.Amex);
            result.Attempts[2].Scheme.ShouldBe(PaymentRoutingScheme.CartesBancaires);
            result.Attempts[3].Scheme.ShouldBe(PaymentRoutingScheme.Diners);
            result.Attempts[4].Scheme.ShouldBe(PaymentRoutingScheme.Discover);
            result.Attempts[5].Scheme.ShouldBe(PaymentRoutingScheme.Jcb);
            result.Attempts[6].Scheme.ShouldBe(PaymentRoutingScheme.Mada);
            result.Attempts[7].Scheme.ShouldBe(PaymentRoutingScheme.Maestro);
            result.Attempts[8].Scheme.ShouldBe(PaymentRoutingScheme.Mastercard);
            result.Attempts[9].Scheme.ShouldBe(PaymentRoutingScheme.Nyce);
            result.Attempts[10].Scheme.ShouldBe(PaymentRoutingScheme.Omannet);
            result.Attempts[11].Scheme.ShouldBe(PaymentRoutingScheme.Pulse);
            result.Attempts[12].Scheme.ShouldBe(PaymentRoutingScheme.Shazam);
            result.Attempts[13].Scheme.ShouldBe(PaymentRoutingScheme.Star);
            result.Attempts[14].Scheme.ShouldBe(PaymentRoutingScheme.Upi);
            result.Attempts[15].Scheme.ShouldBe(PaymentRoutingScheme.Visa);
        }

        [Fact]
        public void ShouldSerializeRoutingInsidePaymentRequest()
        {
            var request = new PaymentRequest
            {
                Amount = 1000,
                Currency = Common.Currency.GBP,
                Routing = new PaymentRouting
                {
                    Attempts = new List<PaymentRoutingAttempt>
                    {
                        new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Mastercard },
                        new PaymentRoutingAttempt { Scheme = PaymentRoutingScheme.Visa }
                    }
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"routing\"");
            json.ShouldContain("\"attempts\"");
            json.ShouldContain("\"mastercard\"");
            json.ShouldContain("\"visa\"");
        }

        // ------------------------------------------------------------------------
        // format: date properties
        // ------------------------------------------------------------------------

        // Times are deliberately not midnight, so these prove truncation rather than passing
        // because the caller happened to supply a zero time.
        [Fact]
        public void ShouldSerializeAccommodationDatesWithoutATimeComponent()
        {
            var json = Serializer.Serialize(new AccommodationData
            {
                Name = "Grand Hotel",
                CheckInDate = new DateTime(2026, 10, 1, 13, 45, 30),
                CheckOutDate = new DateTime(2026, 10, 5, 11, 30, 0),
                Guests = new List<PaymentContextsGuests>
                {
                    new PaymentContextsGuests
                    {
                        FirstName = "Jane",
                        DateOfBirth = new DateTime(1985, 7, 14, 23, 59, 59)
                    }
                }
            });

            json.ShouldContain("\"check_in_date\":\"2026-10-01\"");
            json.ShouldContain("\"check_out_date\":\"2026-10-05\"");
            json.ShouldContain("\"date_of_birth\":\"1985-07-14\"");

            // Asserted on the specific times supplied rather than a bare "T", because Shouldly's
            // ShouldNotContain is case-insensitive and would match the "t" in "Grand Hotel".
            json.ShouldNotContain("T13:45:30");
            json.ShouldNotContain("T11:30:00");
            json.ShouldNotContain("T23:59:59");
        }

        // CheckInDate, CheckOutDate and Guests[].DateOfBirth were non-nullable DateTime, so
        // NullValueHandling.Ignore could not suppress them: a merchant who populated
        // accommodation_data at all shipped "0001-01-01T00:00:00" for both dates whether they
        // meant to or not. This guards the nullability change.
        [Fact]
        public void ShouldOmitAccommodationDatesWhenNotSet()
        {
            var json = Serializer.Serialize(new AccommodationData { Name = "Grand Hotel" });

            json.ShouldNotContain("check_in_date");
            json.ShouldNotContain("check_out_date");
            json.ShouldNotContain("0001-01-01");
        }

        [Fact]
        public void ShouldDeserializeAccommodationDatesFromDateOnlyValues()
        {
            const string json =
                "{\"name\":\"Grand Hotel\",\"check_in_date\":\"2026-10-01\"," +
                "\"check_out_date\":\"2026-10-05\"}";

            var data = (AccommodationData)Serializer.Deserialize(json, typeof(AccommodationData));

            data.CheckInDate.ShouldBe(new DateTime(2026, 10, 1));
            data.CheckOutDate.ShouldBe(new DateTime(2026, 10, 5));
        }

        [Fact]
        public void ShouldSerializeAirlineDatesWithoutATimeComponent()
        {
            var leg = Serializer.Serialize(new FlightLegDetails
            {
                DepartureDate = new DateTime(2026, 6, 19, 6, 40, 0)
            });
            leg.ShouldContain("\"departure_date\":\"2026-06-19\"");
            leg.ShouldNotContain("T06:40");

            var passenger = Serializer.Serialize(new Passenger
            {
                FirstName = "John",
                DateOfBirth = new DateTime(1990, 5, 26, 17, 5, 0)
            });
            passenger.ShouldContain("\"date_of_birth\":\"1990-05-26\"");
            passenger.ShouldNotContain("T17:05");
        }

        [Fact]
        public void ShouldOmitAirlineDatesWhenNotSet()
        {
            Serializer.Serialize(new FlightLegDetails()).ShouldNotContain("departure_date");
            Serializer.Serialize(new Passenger { FirstName = "John" })
                .ShouldNotContain("date_of_birth");
        }

        [Fact]
        public void ShouldSerializeRefundOrderServiceEndsOnAsADate()
        {
            var json = Serializer.Serialize(new RefundOrder
            {
                Name = "Subscription",
                ServiceEndsOn = new DateTime(2025, 1, 1, 9, 30, 0)
            });

            json.ShouldContain("\"service_ends_on\":\"2025-01-01\"");
            json.ShouldNotContain("T09:30");
        }

        // ------------------------------------------------------------------------
        // FlightLegDetails -- corrected wire names
        // ------------------------------------------------------------------------

        // class_of_travelling and stop_over_code are the only spellings in the specification.
        // The SDK used to expose StopoverCode, which serialized as stopover_code, a key the API
        // does not define, so the value was discarded by the gateway; it has been removed. The
        // equivalent ServiceClass is still present but marked obsolete.
        [Fact]
        public void ShouldSerializeFlightLegWireNamesTheApiDefines()
        {
            var json = Serializer.Serialize(new FlightLegDetails
            {
                ClassOfTravelling = "J",
                StopOverCode = "O"
            });

            json.ShouldContain("\"class_of_travelling\":\"J\"");
            json.ShouldContain("\"stop_over_code\":\"O\"");
        }

        [Fact]
        public void ShouldOmitTheDeprecatedFlightLegPropertyWhenNotSet()
        {
            var json = Serializer.Serialize(new FlightLegDetails { ClassOfTravelling = "J" });

            json.ShouldNotContain("service_class");
        }

        // StopoverCode was removed, so stopover_code is now unreachable: there is no longer any
        // way for a caller to put that key on the wire.
        [Fact]
        public void ShouldNeverSerializeTheRemovedStopoverCodeKey()
        {
            var json = Serializer.Serialize(new FlightLegDetails
            {
                ClassOfTravelling = "J",
                StopOverCode = "O"
            });

            json.ShouldContain("\"stop_over_code\":\"O\"");
            json.ShouldNotContain("\"stopover_code\"");

            typeof(FlightLegDetails).GetProperty("StopoverCode").ShouldBeNull();
        }

        [Fact]
        public void ShouldStillSerializeTheDeprecatedFlightLegPropertyWhenSet()
        {
            // Retained for backwards compatibility: a caller still setting it must not break,
            // even though the API ignores the key.
#pragma warning disable CS0618 // the point of this test is the deprecated member
            var json = Serializer.Serialize(new FlightLegDetails { ServiceClass = "J" });
#pragma warning restore CS0618

            json.ShouldContain("\"service_class\":\"J\"");
        }

        [Fact]
        public void ShouldDeserializeFlightLegWireNamesTheApiDefines()
        {
            const string json = "{\"class_of_travelling\":\"J\",\"stop_over_code\":\"O\"}";

            var leg = (FlightLegDetails)Serializer.Deserialize(json, typeof(FlightLegDetails));

            leg.ClassOfTravelling.ShouldBe("J");
            leg.StopOverCode.ShouldBe("O");
        }

        [Fact]
        public void ShouldOmitRefundOrderServiceEndsOnWhenNotSet()
        {
            var json = Serializer.Serialize(new RefundOrder { Name = "Subscription" });

            json.ShouldNotContain("service_ends_on");
            json.ShouldNotContain("0001-01-01");
        }
    }
}
