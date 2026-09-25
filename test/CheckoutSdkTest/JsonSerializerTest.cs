using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Response;
using Checkout.Common;
using Checkout.Financial;
using Newtonsoft.Json;
using Checkout.Issuing.Cards.Requests.Create;
using Checkout.Issuing.Common;
using Checkout.Issuing.Common.Responses;
using Checkout.Issuing.Controls.Requests.Create;
using Checkout.Issuing.Controls.Responses.Create;
using Checkout.Issuing.Transactions.Responses;
using Checkout.Payments.Contexts;
using Checkout.Payments.Response;
using Checkout.Payments.Response.Source;
using Checkout.Payments.Response.Source.Contexts;
using Checkout.Payments.Sender;
using System.Collections.Generic;
using System.IO;
using Checkout.Payments;
using Shouldly;
using System;
using Xunit;

namespace Checkout
{
    public class JsonSerializerTest : JsonTestFixture
    {
        public class DummyDateTime
        {
            public DateTime Datetime { get; set; }
        }

        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseIndividualSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseIndividualSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Individual);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentIndividualSender));
            PaymentIndividualSender sender = (PaymentIndividualSender)getPaymentResponse.Sender;
            sender.FirstName.ShouldNotBeEmpty();
            sender.LastName.ShouldNotBeEmpty();
        }

        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseCorporateSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseCorporateSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Corporate);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentCorporateSender));
            PaymentCorporateSender sender = (PaymentCorporateSender)getPaymentResponse.Sender;
            sender.CompanyName.ShouldNotBeEmpty();
        }

        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseInstrumentSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseInstrumentSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBe(PaymentSenderType.Instrument);
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(PaymentInstrumentSender));
        }

        [Fact]
        public void ShouldDeserializeDefaultGetPaymentResponseAlternativeSender()
        {
            var fileContent = GetJsonFileContent("./Resources/Sender/GetPaymentResponseAlternativeSender.json");
            GetPaymentResponse getPaymentResponse =
                (GetPaymentResponse)new JsonSerializer().Deserialize(fileContent, typeof(GetPaymentResponse));
            getPaymentResponse.ShouldNotBeNull();
            getPaymentResponse.Sender.ShouldNotBeNull();
            getPaymentResponse.Sender.Type().ShouldBeNull();
            getPaymentResponse.Sender.ShouldBeAssignableTo(typeof(ResponseAlternativeSender));
            ResponseAlternativeSender sender = (ResponseAlternativeSender)getPaymentResponse.Sender;
            sender["type"].ShouldBe("xyz");
        }

        [Fact]
        public void ShouldDeserializeDefaultCardControlTypeRequest()
        {
            var fileContent = GetJsonFileContent("./Resources/CardControlTypeRequest.json");
            VelocityCardControlRequest abstractCardControlRequest =
                (VelocityCardControlRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(VelocityCardControlRequest));
            abstractCardControlRequest.ShouldNotBeNull();
            abstractCardControlRequest.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardControlTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardControlTypeResponse.json");
            VelocityCardControlResponse cardControlResponse =
                (VelocityCardControlResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(VelocityCardControlResponse));
            cardControlResponse.ShouldNotBeNull();
            cardControlResponse.ControlType.ShouldBe(IssuingControlType.VelocityLimit);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardCTypeRequest()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeRequest.json");
            AbstractCardCreateRequest abstractCardCreateRequest =
                (AbstractCardCreateRequest)new JsonSerializer().Deserialize(fileContent,
                    typeof(VirtualCardCreateRequest));
            abstractCardCreateRequest.ShouldNotBeNull();
            abstractCardCreateRequest.Type.ShouldBe(IssuingCardType.Virtual);
        }

        [Fact]
        public void ShouldDeserializeDefaultCardCTypeResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/CardTypeResponse.json");
            AbstractCardResponse cardDetailsResponse =
                (AbstractCardResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(AbstractCardResponse));
            cardDetailsResponse.ShouldNotBeNull();
            cardDetailsResponse.Type.ShouldBe(IssuingCardType.Physical);
        }
        
        [Fact]
        public void ShouldDeserializeIssuingTransactionsDeclinedReasonResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/IssuingTransactionsDeclinedReason.json");
            Messages messages =
                (Messages)new JsonSerializer().Deserialize(fileContent,
                    typeof(Messages));
            messages.ShouldNotBeNull();
            messages.DeclineReason.ShouldBe("velocity_reached");
        }
        
        [Fact]
        public void ShouldDeserializeKnetResponse()
        {
            var fileContent = GetJsonFileContent("./Resources/KnetResponse.json");
            var paymentResponse =
                (PaymentResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(PaymentResponse));
            paymentResponse.ShouldNotBeNull();
            paymentResponse.Source.ShouldBeOfType(typeof(KnetResponseSource));
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/get_financial_actions_response.json");
            FinancialActionsQueryResponse financialActionsQueryResponse =
                (FinancialActionsQueryResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(FinancialActionsQueryResponse));
            financialActionsQueryResponse.ShouldNotBeNull();
            financialActionsQueryResponse.Data[0].ProcessedOn.ShouldNotBeNull();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityCompanyFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityCompanyResponse.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityGBCompany30FromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityGBCompany30Response.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializeOnBoardSubEntityEEACompany30FromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/OnBoardSubEntityEEACompany30Response.json");
            OnboardEntityDetailsResponse onboardEntityDetailsResponse =
                (OnboardEntityDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(OnboardEntityDetailsResponse));
            onboardEntityDetailsResponse.ShouldNotBeNull();
            onboardEntityDetailsResponse.Company.BusinessType.ShouldBeOfType<BusinessType>();
        }
        
        [Fact]
        public void ShouldDeserializePaymentContextsPayPalDetailsResponseFromJson()
        {
            var fileContent = GetJsonFileContent("./Resources/PaymentContextsPayPalDetailsResponse.json");
            PaymentContextDetailsResponse paymentContextsPayPalResponseSource =
                (PaymentContextDetailsResponse)new JsonSerializer().Deserialize(fileContent,
                    typeof(PaymentContextDetailsResponse));
            paymentContextsPayPalResponseSource.ShouldNotBeNull();
            paymentContextsPayPalResponseSource.PaymentRequest.Source.ShouldBeOfType<PaymentContextsPayPalResponseSource>();
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromStrings()
        {
            string[] dates =
            {
                "{\"processed_on\":\"2021-06-08T12:25:01.000Z\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01Z\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234567\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234560\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234500\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1234000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1230000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1200000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.1000000\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01.0000000\"}",
            };
            
            var dateTime = DateTime.Parse("2021-06-08T12:25:01").ToUniversalTime();

            foreach (var date in dates)
            {
                PaymentResponse paymentResponse =
                    (PaymentResponse)new JsonSerializer().Deserialize(date, typeof(PaymentResponse));

                paymentResponse.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldNotBeNull();
                DateTime.Parse(paymentResponse.ProcessedOn.ToString()).ToUniversalTime().ShouldBe(dateTime);
            }
        }

        [Fact]
        public void ShouldDeserializeDateTimeFormatsFromStringsUtc()
        {
            string[] dates =
            {
                "{\"processed_on\":\"2021-06-08T12:25:01+00:00\"}",
                "{\"processed_on\":\"2021-06-08T12:25:01+0000\"}", 
                "{\"processed_on\":\"2021-06-08T12:25:01+00\"}",
            };
            
            var dateTime = DateTime.Parse("2021-06-08T12:25:01Z");

            foreach (var date in dates)
            {
                PaymentResponse paymentResponse =
                    (PaymentResponse)new JsonSerializer().Deserialize(date, typeof(PaymentResponse));

                paymentResponse.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldNotBeNull();
                paymentResponse.ProcessedOn.ShouldBe(dateTime);
            }
        }

        [Fact]
        public void ShouldWorkWithLongDates()
        {
            // This method tests the custom IsoDateTimeConverter
            var serializer = new JsonSerializer();
            var date = "{ \"datetime\" : \"2025-10-30T23:59:59Z\" }";
            
            // Test deserialization
            DummyDateTime dummyDate = (DummyDateTime) serializer.Deserialize(date, typeof(DummyDateTime));
            
            // Test serialization as well
            var serializedJson = serializer.Serialize(dummyDate);
            
            Assert.NotNull(dummyDate);
            Assert.Equal(new DateTime(2025, 10, 30, 23, 59, 59, DateTimeKind.Utc), dummyDate.Datetime);
            Assert.Contains("2025-10-30T23:59:59Z", serializedJson);
        }

        [Fact]
        public void ShouldWorkWithShortDates()
        {
            // This method tests the custom ShortDateTimeConverter
            var serializer = new JsonSerializer();
            var date = "{ \"datetime\" : \"2025-10-30\" }";

            // Test deserialization
            DummyDateTime dummyDate = (DummyDateTime) serializer.Deserialize(date, typeof(DummyDateTime));

            // Test serialization as well
            var serializedJson = serializer.Serialize(dummyDate);

            Assert.NotNull(dummyDate);
            Assert.Equal(new DateTime(2025, 10, 30), dummyDate.Datetime);

            // DummyDateTime.Datetime carries no [JsonConverter], so it uses the GLOBAL
            // ShortDateTimeConverter registration, which is read-only: the short date parses,
            // but the value writes back as a full date-time. That asymmetry is deliberate --
            // see the write-mode section below.
            //
            // This was Assert.Contains("2025-10-30", ...), which also passes against
            // "2025-10-30T00:00:00" and so could not detect a wrong write format either way.
            Assert.Equal("{\"datetime\":\"2025-10-30T00:00:00\"}", serializedJson);
        }

        // ------------------------------------------------------------------------
        // ShortDateTimeConverter write mode
        // ------------------------------------------------------------------------

        private class ShortDateSubject
        {
            [JsonConverter(typeof(ShortDateTimeConverter))]
            [JsonProperty(PropertyName = "date")]
            public DateTime? Date { get; set; }
        }

        private class NonNullableShortDateSubject
        {
            [JsonConverter(typeof(ShortDateTimeConverter))]
            [JsonProperty(PropertyName = "date")]
            public DateTime Date { get; set; }
        }

        // The time is deliberately not midnight, so this proves truncation rather than passing
        // because the caller happened to supply a zero time.
        [Fact]
        public void ShouldWriteAnAnnotatedPropertyAsADateOnly()
        {
            var json = new JsonSerializer().Serialize(
                new ShortDateSubject { Date = new DateTime(2026, 10, 1, 13, 45, 30) });

            Assert.Equal("{\"date\":\"2026-10-01\"}", json);
        }

        [Theory]
        [InlineData(2026, 1, 9, "2026-01-09")]
        [InlineData(2024, 2, 29, "2024-02-29")]
        [InlineData(2026, 12, 31, "2026-12-31")]
        public void ShouldPadAndWriteEveryDateAsIsoDateOnly(int y, int m, int d, string expected)
        {
            var json = new JsonSerializer().Serialize(
                new ShortDateSubject { Date = new DateTime(y, m, d, 23, 59, 59) });

            Assert.Equal("{\"date\":\"" + expected + "\"}", json);
        }

        // NullValueHandling.Ignore drops the property before the converter runs. This is the
        // assertion that would have caught the five non-nullable DateTime properties emitting
        // "0001-01-01" whenever their parent object was populated.
        [Fact]
        public void ShouldOmitAnAnnotatedPropertyWhenNull()
        {
            Assert.Equal("{}", new JsonSerializer().Serialize(new ShortDateSubject()));
        }

        [Fact]
        public void ShouldWriteTheDefaultDateForANonNullableAnnotatedProperty()
        {
            // A non-nullable DateTime cannot be omitted, which is why the SDK's date-only
            // properties are all nullable.
            Assert.Equal("{\"date\":\"0001-01-01\"}",
                new JsonSerializer().Serialize(new NonNullableShortDateSubject()));
        }

        [Fact]
        public void ShouldRoundTripAnAnnotatedDateOnlyProperty()
        {
            var serializer = new JsonSerializer();
            var subject = (ShortDateSubject)serializer.Deserialize(
                "{\"date\":\"2026-10-01\"}", typeof(ShortDateSubject));

            Assert.Equal(new DateTime(2026, 10, 1), subject.Date);
            Assert.Equal("{\"date\":\"2026-10-01\"}", serializer.Serialize(subject));
        }

        // The converter must stay opt-in for writing: CanConvert matches every DateTime, so a
        // global write registration would truncate the genuine format: date-time properties too.
        [Fact]
        public void ShouldNotWriteDateOnlyWhenTheConverterIsNotOptedIn()
        {
            Assert.False(new ShortDateTimeConverter(canWrite: false).CanWrite);
            Assert.True(new ShortDateTimeConverter().CanWrite);

            // An un-annotated DateTime keeps its time component.
            var json = new JsonSerializer().Serialize(
                new DummyDateTime { Datetime = new DateTime(2026, 10, 1, 13, 45, 30) });

            Assert.Equal("{\"datetime\":\"2026-10-01T13:45:30\"}", json);
        }

        [Fact]
        public void ShouldDeserializeKnownCardType()
        {
            const string json = @"{""type"":""card"",""card_type"":""Credit""}";
            var source = (CardResponseSource)new JsonSerializer().Deserialize(json, typeof(CardResponseSource));
            source.CardType.ShouldBe(CardType.Credit);
        }

        [Fact]
        public void ShouldReturnNullForUnrecognizedCardTypeEnumValue()
        {
            const string json = @"{""type"":""card"",""card_type"":""NOT_A_CARD_TYPE""}";
            var source = (CardResponseSource)new JsonSerializer().Deserialize(json, typeof(CardResponseSource));
            source.ShouldNotBeNull();
            source.CardType.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeFullPaymentResponseWithUnrecognizedCardType()
        {
            const string json = @"{
                ""id"": ""pay_talabat_incident"",
                ""status"": ""Authorized"",
                ""approved"": true,
                ""source"": {
                    ""type"": ""card"",
                    ""last4"": ""4242"",
                    ""card_type"": ""NOT_A_CARD_TYPE""
                }
            }";

            PaymentResponse response = null;
            Should.NotThrow(() =>
                response = (PaymentResponse)new JsonSerializer().Deserialize(json, typeof(PaymentResponse)));
            response.ShouldNotBeNull();
            response.Approved.ShouldBe(true);
            var source = response.Source.ShouldBeAssignableTo<CardResponseSource>();
            source.CardType.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeUnknownCardTypeAndCardCategory()
        {
            // UNKNOWN is a real value on card payout destinations, not an unrecognized one.
            const string json = @"{""type"":""card"",""card_type"":""UNKNOWN"",""card_category"":""UNKNOWN""}";
            var source = (CardResponseSource)new JsonSerializer().Deserialize(json, typeof(CardResponseSource));
            source.CardType.ShouldBe(CardType.Unknown);
            source.CardCategory.ShouldBe(CardCategory.Unknown);
        }


        // ------------------------------------------------------------------------
        // processing.airline_data[].passenger cardinality, on the full response envelope
        //
        // GET /payments/{id} deserializes into GetPaymentResponse, whose Processing is a
        // ProcessingData. AirlineData.Passenger was a single Passenger, so the whole call threw
        // "cannot deserialize the current JSON array" for any payment carrying passenger data.
        // The tests below drive the real envelope, not the nested type in isolation.
        // ------------------------------------------------------------------------

        private const string GetPaymentResponseWithAirlineJson = @"{
            ""id"": ""pay_mbabizu24mvu3mela5njyhpit4"",
            ""amount"": 6540,
            ""currency"": ""USD"",
            ""approved"": true,
            ""status"": ""Captured"",
            ""reference"": ""ORD-5023-4E89"",
            ""processing"": {
                ""retrieval_reference_number"": ""909913440644"",
                ""airline_data"": [
                  {
                    ""ticket"": {
                      ""number"": ""045-21351455613"",
                      ""issue_date"": ""2023-05-20"",
                      ""issuing_carrier_code"": ""AI"",
                      ""travel_package_indicator"": ""B"",
                      ""travel_agency_name"": ""World Tours"",
                      ""travel_agency_code"": ""01""
                    },
                    ""passenger"": [
                      {
                        ""first_name"": ""John"",
                        ""last_name"": ""White"",
                        ""date_of_birth"": ""1990-05-26"",
                        ""address"": { ""country"": ""US"" }
                      },
                      {
                        ""first_name"": ""Jane"",
                        ""last_name"": ""White"",
                        ""date_of_birth"": ""1992-01-03"",
                        ""address"": { ""country"": ""GB"" }
                      }
                    ],
                    ""flight_leg_details"": [
                      {
                        ""flight_number"": ""101"",
                        ""carrier_code"": ""BA"",
                        ""class_of_travelling"": ""J"",
                        ""departure_airport"": ""LHR"",
                        ""departure_date"": ""2023-06-19"",
                        ""departure_time"": ""15:30"",
                        ""arrival_airport"": ""LAX"",
                        ""stop_over_code"": ""x"",
                        ""fare_basis_code"": ""SPRSVR""
                      }
                    ]
                  }
                ],
                ""accommodation_data"": [
                  {
                    ""name"": ""The Sea View Hotel"",
                    ""check_in_date"": ""2023-06-20"",
                    ""check_out_date"": ""2023-06-23"",
                    ""state"": ""FL"",
                    ""country"": ""USA"",
                    ""city"": ""Los Angeles"",
                    ""room"": [ { ""rate"": ""70"", ""number_of_nights_at_room_rate"": ""3"" } ]
                  }
                ]
            }
        }";

        [Fact]
        public void ShouldDeserializeGetPaymentResponseWithAirlinePassengerArray()
        {
            var response = (GetPaymentResponse)new JsonSerializer()
                .Deserialize(GetPaymentResponseWithAirlineJson, typeof(GetPaymentResponse));

            response.ShouldNotBeNull();
            response.Id.ShouldBe("pay_mbabizu24mvu3mela5njyhpit4");
            response.Processing.ShouldNotBeNull();
            response.Processing.RetrievalReferenceNumber.ShouldBe("909913440644");

            response.Processing.AirlineData.Count.ShouldBe(1);
            var airline = response.Processing.AirlineData[0];

            airline.Ticket.Number.ShouldBe("045-21351455613");
            airline.Ticket.IssueDate.ShouldBe(new DateTime(2023, 5, 20));

            // Two passengers, so a single-object model could not have held this even by accident.
            airline.Passenger.Count.ShouldBe(2);
            airline.Passenger[0].FirstName.ShouldBe("John");
            airline.Passenger[0].Address.Country.ShouldBe(CountryCode.US);
            airline.Passenger[1].FirstName.ShouldBe("Jane");
            airline.Passenger[1].DateOfBirth.ShouldBe(new DateTime(1992, 1, 3));
            airline.Passenger[1].Address.Country.ShouldBe(CountryCode.GB);

            airline.FlightLegDetails[0].FlightNumber.ShouldBe("101");
            airline.FlightLegDetails[0].ClassOfTravelling.ShouldBe("J");
            airline.FlightLegDetails[0].StopOverCode.ShouldBe("x");

            response.Processing.AccommodationData.Count.ShouldBe(1);
            response.Processing.AccommodationData[0].State.ShouldBe("FL");
            response.Processing.AccommodationData[0].Country.ShouldBe("USA");
            response.Processing.AccommodationData[0].Room[0].NumberOfNightsAtRoomRate.ShouldBe("3");
        }

        [Fact]
        public void ShouldDeserializeGetPaymentResponseWithAirlinePassengerAsASingleObject()
        {
            const string json = @"{
                ""id"": ""pay_mbabizu24mvu3mela5njyhpit4"",
                ""processing"": {
                    ""airline_data"": [
                      {
                        ""ticket"": { ""number"": ""045-21351455613"" },
                        ""passenger"": { ""first_name"": ""John"", ""date_of_birth"": ""1990-05-26"" }
                      }
                    ]
                }
            }";

            var response = (GetPaymentResponse)new JsonSerializer()
                .Deserialize(json, typeof(GetPaymentResponse));

            response.Processing.AirlineData[0].Passenger.Count.ShouldBe(1);
            response.Processing.AirlineData[0].Passenger[0].FirstName.ShouldBe("John");
            response.Processing.AirlineData[0].Passenger[0].DateOfBirth
                .ShouldBe(new DateTime(1990, 5, 26));
        }

        // ------------------------------------------------------------------------
        // SingleOrArrayConverter
        // ------------------------------------------------------------------------

        private class SingleOrArrayHolder
        {
            [JsonConverter(typeof(SingleOrArrayConverter<Passenger>))]
            public IList<Passenger> Passenger { get; set; }
        }

        // The converter both reads and writes. Writing is what applies the cardinality rule the
        // live API requires: an object for one passenger, an array only for several.
        [Fact]
        public void ShouldReadAndWrite()
        {
            new SingleOrArrayConverter<Passenger>().CanRead.ShouldBeTrue();
            new SingleOrArrayConverter<Passenger>().CanWrite.ShouldBeTrue();
        }

        [Fact]
        public void ShouldWriteASinglePassengerAsAnObjectAndSeveralAsAnArray()
        {
            var one = new JsonSerializer().Serialize(new SingleOrArrayHolder
            {
                Passenger = new List<Passenger> { new Passenger { FirstName = "John" } }
            });
            one.ShouldContain("\"passenger\":{");
            one.ShouldNotContain("\"passenger\":[");

            var many = new JsonSerializer().Serialize(new SingleOrArrayHolder
            {
                Passenger = new List<Passenger>
                {
                    new Passenger { FirstName = "John" },
                    new Passenger { FirstName = "Jane" }
                }
            });
            many.ShouldContain("\"passenger\":[{");
        }

        [Fact]
        public void ShouldRoundTripASinglePassengerThroughTheObjectForm()
        {
            var original = new SingleOrArrayHolder
            {
                Passenger = new List<Passenger>
                {
                    new Passenger { FirstName = "John", DateOfBirth = new DateTime(1990, 5, 26) }
                }
            };

            var json = new JsonSerializer().Serialize(original);
            var result = (SingleOrArrayHolder)new JsonSerializer()
                .Deserialize(json, typeof(SingleOrArrayHolder));

            result.Passenger.Count.ShouldBe(1);
            result.Passenger[0].FirstName.ShouldBe("John");
            result.Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
        }

        [Fact]
        public void ShouldConvertOnlyForTheMatchingListType()
        {
            var converter = new SingleOrArrayConverter<Passenger>();

            converter.CanConvert(typeof(IList<Passenger>)).ShouldBeTrue();
            converter.CanConvert(typeof(List<Passenger>)).ShouldBeTrue();
            converter.CanConvert(typeof(Passenger)).ShouldBeFalse();
            converter.CanConvert(typeof(string)).ShouldBeFalse();
        }

        // The converter delegates element deserialization to the supplied serializer, so the
        // global snake_case naming strategy and ShortDateTimeConverter still apply to T. It must
        // never map property names itself.
        [Fact]
        public void ShouldHonourTheGlobalNamingPolicyAndDateConverterForElements()
        {
            const string json =
                @"{""passenger"":{""first_name"":""John"",""last_name"":""White"",""date_of_birth"":""1990-05-26""}}";

            var holder = (SingleOrArrayHolder)new JsonSerializer()
                .Deserialize(json, typeof(SingleOrArrayHolder));

            holder.Passenger.Count.ShouldBe(1);
            holder.Passenger[0].FirstName.ShouldBe("John");
            holder.Passenger[0].LastName.ShouldBe("White");
            holder.Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
        }

        [Fact]
        public void ShouldReadAnEmptyArrayAsAnEmptyList()
        {
            var holder = (SingleOrArrayHolder)new JsonSerializer()
                .Deserialize(@"{""passenger"":[]}", typeof(SingleOrArrayHolder));

            holder.Passenger.ShouldNotBeNull();
            holder.Passenger.Count.ShouldBe(0);
        }

        [Fact]
        public void ShouldReadNullAsNull()
        {
            var holder = (SingleOrArrayHolder)new JsonSerializer()
                .Deserialize(@"{""passenger"":null}", typeof(SingleOrArrayHolder));

            holder.Passenger.ShouldBeNull();
        }

    }
}