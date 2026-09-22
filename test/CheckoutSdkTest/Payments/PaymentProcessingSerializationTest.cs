using Checkout.Common;
using Checkout.Payments.Contexts;
using Checkout.Payments.Response;
using Checkout.Payments;
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
    public class PaymentProcessingSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentProcessing
        // ------------------------------------------------------------------------

        private static PaymentProcessing CreateFullyPopulatedPaymentProcessing()
        {
            return new PaymentProcessing
            {
                RetrievalReferenceNumber = "909913440644",
                AcquirerTransactionId = "440644309099499894406",
                RecommendationCode = "02",
                Scheme = "Mastercard",
                PartnerMerchantAdviceCode = "24",
                PartnerResponseCode = "ER_WRONG_TICKET",
                PartnerOrderId = "5GK24544NA744002L",
                PartnerSessionId = "session_abc",
                PartnerClientToken = "token_abc",
                PartnerPaymentId = "440644309099499894406",
                PanTypeProcessed = PanProcessedType.DPAN,
                ContinuationPayload = "payload_abc",
                Pun = "pun_abc",
                PartnerStatus = "pending",
                PartnerTransactionId = "txn_abc",
                PartnerErrorCodes = new List<string> { "ERR_001", "ERR_002" },
                PartnerErrorMessage = "Payment declined",
                PartnerAuthorizationCode = "auth_123",
                PartnerAuthorizationResponseCode = "00",
                SurchargeAmount = 200L,
                FallbackSourceUsed = false,
                CkoNetworkTokenAvailable = true,
                PurchaseCountry = CountryCode.GB,
                ForeignRetailerAmount = 200L,
                MerchantCategoryCode = "5311",
                SchemeMerchantId = "123456",
                ReconciliationId = "4123495123",
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "9cf70789ba90123",
                    AggregatorIdVisa = "10012345",
                    AggregatorIdMc = "00000123456"
                },
                Aft = true,
                SchemeTransactionLinkId = "MTL-001"
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForPaymentProcessing()
        {
            var processing = new PaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForPaymentProcessing()
        {
            var processing = CreateFullyPopulatedPaymentProcessing();

            Should.NotThrow(() => Serializer.Serialize(processing));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForPaymentProcessing()
        {
            var original = CreateFullyPopulatedPaymentProcessing();

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            deserialized.ShouldNotBeNull();
            deserialized.RetrievalReferenceNumber.ShouldBe(original.RetrievalReferenceNumber);
            deserialized.AcquirerTransactionId.ShouldBe(original.AcquirerTransactionId);
            deserialized.RecommendationCode.ShouldBe(original.RecommendationCode);
            deserialized.Scheme.ShouldBe(original.Scheme);
            deserialized.PartnerMerchantAdviceCode.ShouldBe(original.PartnerMerchantAdviceCode);
            deserialized.PartnerResponseCode.ShouldBe(original.PartnerResponseCode);
            deserialized.PartnerOrderId.ShouldBe(original.PartnerOrderId);
            deserialized.PartnerSessionId.ShouldBe(original.PartnerSessionId);
            deserialized.PartnerClientToken.ShouldBe(original.PartnerClientToken);
            deserialized.PartnerPaymentId.ShouldBe(original.PartnerPaymentId);
            deserialized.PanTypeProcessed.ShouldBe(original.PanTypeProcessed);
            deserialized.ContinuationPayload.ShouldBe(original.ContinuationPayload);
            deserialized.Pun.ShouldBe(original.Pun);
            deserialized.PartnerStatus.ShouldBe(original.PartnerStatus);
            deserialized.PartnerTransactionId.ShouldBe(original.PartnerTransactionId);
            deserialized.PartnerErrorCodes.ShouldBe(original.PartnerErrorCodes);
            deserialized.PartnerErrorMessage.ShouldBe(original.PartnerErrorMessage);
            deserialized.PartnerAuthorizationCode.ShouldBe(original.PartnerAuthorizationCode);
            deserialized.PartnerAuthorizationResponseCode.ShouldBe(original.PartnerAuthorizationResponseCode);
            deserialized.SurchargeAmount.ShouldBe(original.SurchargeAmount);
            deserialized.FallbackSourceUsed.ShouldBe(original.FallbackSourceUsed);
            deserialized.CkoNetworkTokenAvailable.ShouldBe(original.CkoNetworkTokenAvailable);
            deserialized.PurchaseCountry.ShouldBe(original.PurchaseCountry);
            deserialized.ForeignRetailerAmount.ShouldBe(original.ForeignRetailerAmount);
            deserialized.MerchantCategoryCode.ShouldBe(original.MerchantCategoryCode);
            deserialized.SchemeMerchantId.ShouldBe(original.SchemeMerchantId);
            deserialized.ReconciliationId.ShouldBe(original.ReconciliationId);
            deserialized.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            deserialized.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            deserialized.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            deserialized.Aft.ShouldBe(original.Aft);
            deserialized.SchemeTransactionLinkId.ShouldBe(original.SchemeTransactionLinkId);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForPaymentProcessing()
        {
            const string json = @"{
                ""retrieval_reference_number"": ""909913440644"",
                ""acquirer_transaction_id"": ""440644309099499894406"",
                ""recommendation_code"": ""02"",
                ""scheme"": ""Mastercard"",
                ""partner_merchant_advice_code"": ""24"",
                ""partner_response_code"": ""ER_WRONG_TICKET"",
                ""partner_order_id"": ""5GK24544NA744002L"",
                ""partner_payment_id"": ""440644309099499894406"",
                ""partner_status"": ""pending"",
                ""partner_transaction_id"": ""txn_abc"",
                ""partner_error_codes"": [""ERR_001""],
                ""partner_error_message"": ""Payment declined"",
                ""partner_authorization_code"": ""auth_123"",
                ""partner_authorization_response_code"": ""00"",
                ""surcharge_amount"": 200,
                ""pan_type_processed"": ""dpan"",
                ""fallback_source_used"": false,
                ""cko_network_token_available"": false,
                ""purchase_country"": ""GB"",
                ""foreign_retailer_amount"": 200,
                ""scheme_merchant_id"": ""123456"",
                ""reconciliation_id"": ""4123495123"",
                ""aggregator"": {
                    ""sub_merchant_id"": ""9cf70789ba90123"",
                    ""aggregator_id_visa"": ""10012345"",
                    ""aggregator_id_mc"": ""00000123456""
                },
                ""scheme_transaction_link_id"": ""MTL-001""
            }";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.RetrievalReferenceNumber.ShouldBe("909913440644");
            processing.AcquirerTransactionId.ShouldBe("440644309099499894406");
            processing.RecommendationCode.ShouldBe("02");
            processing.Scheme.ShouldBe("Mastercard");
            processing.PartnerMerchantAdviceCode.ShouldBe("24");
            processing.PartnerResponseCode.ShouldBe("ER_WRONG_TICKET");
            processing.PartnerOrderId.ShouldBe("5GK24544NA744002L");
            processing.PartnerPaymentId.ShouldBe("440644309099499894406");
            processing.PartnerStatus.ShouldBe("pending");
            processing.PartnerTransactionId.ShouldBe("txn_abc");
            processing.PartnerErrorCodes.ShouldBe(new List<string> { "ERR_001" });
            processing.PartnerErrorMessage.ShouldBe("Payment declined");
            processing.PartnerAuthorizationCode.ShouldBe("auth_123");
            processing.PartnerAuthorizationResponseCode.ShouldBe("00");
            processing.SurchargeAmount.ShouldBe(200L);
            processing.PanTypeProcessed.ShouldBe(PanProcessedType.DPAN);
            processing.FallbackSourceUsed.ShouldBe(false);
            processing.CkoNetworkTokenAvailable.ShouldBe(false);
            processing.PurchaseCountry.ShouldBe(CountryCode.GB);
            processing.ForeignRetailerAmount.ShouldBe(200L);
            processing.SchemeMerchantId.ShouldBe("123456");
            processing.ReconciliationId.ShouldBe("4123495123");
            processing.Aggregator.ShouldNotBeNull();
            processing.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            processing.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldDeserializeAlphanumericSchemeMerchantId()
        {
            const string json = @"{""scheme_merchant_id"": ""MID-0012AB""}";

            var processing = (PaymentProcessing)Serializer.Deserialize(json, typeof(PaymentProcessing));

            processing.ShouldNotBeNull();
            processing.SchemeMerchantId.ShouldBe("MID-0012AB");
        }

        [Fact]
        public void ShouldSerializeSchemeTransactionLinkIdToSnakeCase()
        {
            var processing = new PaymentProcessing { SchemeTransactionLinkId = "MTL-001" };

            var json = Serializer.Serialize(processing);

            json.ShouldContain("\"scheme_transaction_link_id\":\"MTL-001\"");
        }

        // ------------------------------------------------------------------------
        // ProcessingData
        // ------------------------------------------------------------------------

        private static ProcessingData CreateFullyPopulatedProcessingData()
        {
            return new ProcessingData
            {
                PreferredScheme = PreferredSchema.Visa,
                AppId = "com.iap.linker_portal",
                PartnerCustomerId = "2102209000001106125F8",
                PartnerPaymentId = "440644309099499894406",
                TaxAmount = 1000L,
                PurchaseCountry = CountryCode.GB,
                Locale = "en-US",
                RetrievalReferenceNumber = "909913440644",
                PartnerOrderId = "ord_abc",
                PartnerStatus = "pending",
                PartnerTransactionId = "txn_abc",
                PartnerErrorCodes = new List<string> { "ERR_001", "ERR_002" },
                PartnerErrorMessage = "Payment declined",
                PartnerAuthorizationCode = "auth_123",
                PartnerAuthorizationResponseCode = "00",
                FraudStatus = "approved",
                ProviderAuthorizedPaymentMethod = new ProviderAuthorizedPaymentMethod
                {
                    Type = "pay_later",
                    Description = "Pay in 30 days",
                    NumberOfInstallments = 3L,
                    NumberOfDays = 30L
                },
                CustomPaymentMethodIds = new List<string> { "cpm_001" },
                Aft = true,
                MerchantCategoryCode = "5311",
                SchemeMerchantId = "123456",
                PanTypeProcessed = PanProcessedType.FPAN,
                CkoNetworkTokenAvailable = true,
                FallbackSourceUsed = false,
                FailureCode = "partner_error",
                PartnerCode = "999111",
                PartnerResponseCode = "ER_WRONG_TICKET",
                Scheme = "ACCEL",
                PartnerFraudStatus = "Pending",
                PartnerMerchantAdviceCode = "24",
                AccommodationData = new List<AccommodationData>
                {
                    new AccommodationData
                    {
                        Name = "Hotel California",
                        BookingReference = "BR-001",
                        CheckInDate = DateTime.Parse("2026-08-01"),
                        CheckOutDate = DateTime.Parse("2026-08-05"),
                        Address = new Address { AddressLine1 = "123 Beach Road", Zip = "10001" },
                        State = "FL",
                        Country = "USA",
                        City = "London",
                        NumberOfRooms = 2,
                        Guests = new List<PaymentContextsGuests>
                        {
                            new PaymentContextsGuests
                            {
                                FirstName = "Jane",
                                LastName = "Doe",
                                DateOfBirth = DateTime.Parse("1985-07-14")
                            }
                        },
                        Room = new List<PaymentContextsAccommodationRoom>
                        {
                            new PaymentContextsAccommodationRoom
                            {
                                Rate = "70",
                                NumberOfNightsAtRoomRate = "3"
                            }
                        },
                        PropertyPhone = new List<AccommodationPhone>
                        {
                            new AccommodationPhone { CountryCode = "44", Number = "7123456789" }
                        },
                        CustomerServicePhone = new List<AccommodationPhone>
                        {
                            new AccommodationPhone { CountryCode = "44", Number = "7987654321" }
                        }
                    }
                },
                AirlineData = new List<AirlineData>
                {
                    new AirlineData
                    {
                        Ticket = new Ticket
                        {
                            Number = "045-21351455613",
                            IssueDate = DateTime.Parse("2026-08-01"),
                            IssuingCarrierCode = "AI",
                            TravelPackageIndicator = "B",
                            TravelAgencyName = "World Tours",
                            TravelAgencyCode = "01"
                        },
                        Passenger = new List<Passenger>
                        {
                            new Passenger
                            {
                                FirstName = "John",
                                LastName = "White",
                                DateOfBirth = DateTime.Parse("1990-05-26"),
                                Address = new PassengerAddress { Country = CountryCode.US }
                            }
                        },
                        FlightLegDetails = new List<FlightLegDetails>
                        {
                            new FlightLegDetails
                            {
                                FlightNumber = "101",
                                CarrierCode = "BA",
                                ClassOfTravelling = "J",
                                DepartureAirport = "LHR",
                                DepartureDate = DateTime.Parse("2026-08-02"),
                                DepartureTime = "15:30",
                                ArrivalAirport = "LAX",
                                StopOverCode = "x",
                                FareBasisCode = "SPRSVR"
                            }
                        }
                    }
                },
                SchemeTransactionLinkId = "MTL-001"
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForProcessingData()
        {
            var data = new ProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldSerializeWithAllOptionalPropertiesForProcessingData()
        {
            var data = CreateFullyPopulatedProcessingData();

            Should.NotThrow(() => Serializer.Serialize(data));
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForProcessingData()
        {
            var original = CreateFullyPopulatedProcessingData();

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            deserialized.ShouldNotBeNull();
            deserialized.PreferredScheme.ShouldBe(original.PreferredScheme);
            deserialized.AppId.ShouldBe(original.AppId);
            deserialized.PartnerCustomerId.ShouldBe(original.PartnerCustomerId);
            deserialized.PartnerPaymentId.ShouldBe(original.PartnerPaymentId);
            deserialized.TaxAmount.ShouldBe(original.TaxAmount);
            deserialized.PurchaseCountry.ShouldBe(original.PurchaseCountry);
            deserialized.Locale.ShouldBe(original.Locale);
            deserialized.RetrievalReferenceNumber.ShouldBe(original.RetrievalReferenceNumber);
            deserialized.PartnerOrderId.ShouldBe(original.PartnerOrderId);
            deserialized.PartnerStatus.ShouldBe(original.PartnerStatus);
            deserialized.PartnerTransactionId.ShouldBe(original.PartnerTransactionId);
            deserialized.PartnerErrorCodes.ShouldBe(original.PartnerErrorCodes);
            deserialized.PartnerErrorMessage.ShouldBe(original.PartnerErrorMessage);
            deserialized.PartnerAuthorizationCode.ShouldBe(original.PartnerAuthorizationCode);
            deserialized.PartnerAuthorizationResponseCode.ShouldBe(original.PartnerAuthorizationResponseCode);
            deserialized.FraudStatus.ShouldBe(original.FraudStatus);
            deserialized.ProviderAuthorizedPaymentMethod.Type.ShouldBe("pay_later");
            deserialized.ProviderAuthorizedPaymentMethod.Description.ShouldBe("Pay in 30 days");
            deserialized.ProviderAuthorizedPaymentMethod.NumberOfInstallments.ShouldBe(3L);
            deserialized.ProviderAuthorizedPaymentMethod.NumberOfDays.ShouldBe(30L);
            deserialized.CustomPaymentMethodIds.ShouldBe(original.CustomPaymentMethodIds);
            deserialized.Aft.ShouldBe(original.Aft);
            deserialized.MerchantCategoryCode.ShouldBe(original.MerchantCategoryCode);
            deserialized.SchemeMerchantId.ShouldBe(original.SchemeMerchantId);
            deserialized.PanTypeProcessed.ShouldBe(original.PanTypeProcessed);
            deserialized.CkoNetworkTokenAvailable.ShouldBe(original.CkoNetworkTokenAvailable);
            deserialized.FallbackSourceUsed.ShouldBe(original.FallbackSourceUsed);
            deserialized.FailureCode.ShouldBe(original.FailureCode);
            deserialized.PartnerCode.ShouldBe(original.PartnerCode);
            deserialized.PartnerResponseCode.ShouldBe(original.PartnerResponseCode);
            deserialized.Scheme.ShouldBe(original.Scheme);
            deserialized.PartnerFraudStatus.ShouldBe(original.PartnerFraudStatus);
            deserialized.PartnerMerchantAdviceCode.ShouldBe(original.PartnerMerchantAdviceCode);
            deserialized.AccommodationData.Count.ShouldBe(1);
            deserialized.AccommodationData[0].Name.ShouldBe("Hotel California");
            deserialized.AccommodationData[0].City.ShouldBe("London");
            deserialized.AccommodationData[0].State.ShouldBe("FL");
            deserialized.AccommodationData[0].Country.ShouldBe("USA");
            deserialized.AccommodationData[0].Guests.Count.ShouldBe(1);
            deserialized.AccommodationData[0].Guests[0].DateOfBirth.ShouldBe(new DateTime(1985, 7, 14));
            deserialized.AccommodationData[0].Room.Count.ShouldBe(1);
            deserialized.AccommodationData[0].Room[0].NumberOfNightsAtRoomRate.ShouldBe("3");
            deserialized.AccommodationData[0].PropertyPhone.Count.ShouldBe(1);
            deserialized.AccommodationData[0].PropertyPhone[0].Number.ShouldBe("7123456789");
            deserialized.AccommodationData[0].CustomerServicePhone.Count.ShouldBe(1);
            deserialized.AccommodationData[0].CustomerServicePhone[0].Number.ShouldBe("7987654321");
            deserialized.AirlineData.Count.ShouldBe(1);
            deserialized.AirlineData[0].Ticket.Number.ShouldBe("045-21351455613");
            deserialized.AirlineData[0].Ticket.IssueDate.ShouldBe(new DateTime(2026, 8, 1));
            deserialized.AirlineData[0].Passenger.Count.ShouldBe(1);
            deserialized.AirlineData[0].Passenger[0].FirstName.ShouldBe("John");
            deserialized.AirlineData[0].Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
            deserialized.AirlineData[0].FlightLegDetails.Count.ShouldBe(1);
            deserialized.AirlineData[0].FlightLegDetails[0].FlightNumber.ShouldBe("101");
            deserialized.AirlineData[0].FlightLegDetails[0].ClassOfTravelling.ShouldBe("J");
            deserialized.AirlineData[0].FlightLegDetails[0].StopOverCode.ShouldBe("x");
            deserialized.SchemeTransactionLinkId.ShouldBe(original.SchemeTransactionLinkId);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForProcessingData()
        {
            const string json = @"{
                ""preferred_scheme"": ""visa"",
                ""app_id"": ""com.iap.linker_portal"",
                ""partner_customer_id"": ""2102209000001106125F8"",
                ""partner_payment_id"": ""440644309099499894406"",
                ""tax_amount"": 1000,
                ""locale"": ""en-US"",
                ""retrieval_reference_number"": ""909913440644"",
                ""partner_order_id"": ""ord_abc"",
                ""partner_status"": ""pending"",
                ""partner_transaction_id"": ""txn_abc"",
                ""partner_error_codes"": [""ERR_001""],
                ""partner_error_message"": ""Payment declined"",
                ""partner_authorization_code"": ""auth_123"",
                ""partner_authorization_response_code"": ""00"",
                ""custom_payment_method_ids"": [""cpm_001""],
                ""aft"": true,
                ""merchant_category_code"": ""5311"",
                ""scheme_merchant_id"": ""123456"",
                ""pan_type_processed"": ""fpan"",
                ""fallback_source_used"": false,
                ""failure_code"": ""partner_error"",
                ""partner_code"": ""999111"",
                ""partner_response_code"": ""ER_WRONG_TICKET"",
                ""scheme"": ""ACCEL"",
                ""partner_fraud_status"": ""Pending"",
                ""partner_merchant_advice_code"": ""24"",
                ""scheme_transaction_link_id"": ""MTL-001""
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.ShouldNotBeNull();
            result.PreferredScheme.ShouldBe(PreferredSchema.Visa);
            result.AppId.ShouldBe("com.iap.linker_portal");
            result.PartnerCustomerId.ShouldBe("2102209000001106125F8");
            result.PartnerPaymentId.ShouldBe("440644309099499894406");
            result.TaxAmount.ShouldBe(1000L);
            result.Locale.ShouldBe("en-US");
            result.RetrievalReferenceNumber.ShouldBe("909913440644");
            result.PartnerOrderId.ShouldBe("ord_abc");
            result.PartnerStatus.ShouldBe("pending");
            result.PartnerTransactionId.ShouldBe("txn_abc");
            result.PartnerErrorCodes.ShouldBe(new List<string> { "ERR_001" });
            result.PartnerErrorMessage.ShouldBe("Payment declined");
            result.PartnerAuthorizationCode.ShouldBe("auth_123");
            result.PartnerAuthorizationResponseCode.ShouldBe("00");
            result.CustomPaymentMethodIds.ShouldBe(new List<string> { "cpm_001" });
            result.Aft.ShouldBe(true);
            result.MerchantCategoryCode.ShouldBe("5311");
            result.SchemeMerchantId.ShouldBe("123456");
            result.PanTypeProcessed.ShouldBe(PanProcessedType.FPAN);
            result.FallbackSourceUsed.ShouldBe(false);
            result.FailureCode.ShouldBe("partner_error");
            result.PartnerCode.ShouldBe("999111");
            result.PartnerResponseCode.ShouldBe("ER_WRONG_TICKET");
            result.Scheme.ShouldBe("ACCEL");
            result.PartnerFraudStatus.ShouldBe("Pending");
            result.PartnerMerchantAdviceCode.ShouldBe("24");
            result.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldRoundTripSerializeSchemeAndPartnerResponseFields()
        {
            var original = new ProcessingData
            {
                Scheme = "ACCEL",
                PartnerFraudStatus = "Accepted",
                PartnerMerchantAdviceCode = "24"
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            json.ShouldContain("\"scheme\":\"ACCEL\"");
            json.ShouldContain("\"partner_fraud_status\":\"Accepted\"");
            json.ShouldContain("\"partner_merchant_advice_code\":\"24\"");
            deserialized.Scheme.ShouldBe("ACCEL");
            deserialized.PartnerFraudStatus.ShouldBe("Accepted");
            deserialized.PartnerMerchantAdviceCode.ShouldBe("24");
        }

        [Fact]
        public void ShouldDeserializeFallbackSourceUsed()
        {
            const string json = @"{
                ""fallback_source_used"": true,
                ""app_id"": ""app_123"",
                ""retrieval_reference_number"": ""rrn_456""
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.ShouldNotBeNull();
            result.FallbackSourceUsed.ShouldBe(true);
            result.AppId.ShouldBe("app_123");
            result.RetrievalReferenceNumber.ShouldBe("rrn_456");
        }

        // ------------------------------------------------------------------------
        // ProcessingSettings
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithSchemeTransactionLinkId()
        {
            var settings = new ProcessingSettings { SchemeTransactionLinkId = "MTL-001" };

            Should.NotThrow(() => Serializer.Serialize(settings));
        }

        [Fact]
        public void ShouldDeserializeSchemeTransactionLinkId()
        {
            const string json = @"{""scheme_transaction_link_id"": ""MTL-001""}";

            var result = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            result.ShouldNotBeNull();
            result.SchemeTransactionLinkId.ShouldBe("MTL-001");
        }

        [Fact]
        public void ShouldRoundTripSerializeSchemeTransactionLinkId()
        {
            var original = new ProcessingSettings { SchemeTransactionLinkId = "MTL-XYZ-789" };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            json.ShouldContain("\"scheme_transaction_link_id\":\"MTL-XYZ-789\"");
            deserialized.SchemeTransactionLinkId.ShouldBe("MTL-XYZ-789");
        }

        // ------------------------------------------------------------------------
        // ProcessingAggregator
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripSerializeAggregator()
        {
            var original = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "9cf70789ba90123",
                    AggregatorIdVisa = "10012345",
                    AggregatorIdMc = "00000123456"
                },
                ReconciliationId = "4123495123",
                ForeignRetailerAmount = 200,
                ServiceType = AchServiceType.Standard
            };

            var json = Serializer.Serialize(original);
            var deserialized = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            deserialized.Aggregator.ShouldNotBeNull();
            deserialized.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            deserialized.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            deserialized.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            deserialized.ReconciliationId.ShouldBe("4123495123");
            deserialized.ForeignRetailerAmount.ShouldBe(200L);
            deserialized.ServiceType.ShouldBe(AchServiceType.Standard);
        }

        [Fact]
        public void ShouldSerializeSnakeCaseKeys()
        {
            var settings = new ProcessingSettings
            {
                Aggregator = new ProcessingAggregator
                {
                    SubMerchantId = "sub123",
                    AggregatorIdVisa = "visa123",
                    AggregatorIdMc = "mc123"
                },
                ReconciliationId = "rec_001",
                ServiceType = AchServiceType.SameDay
            };

            var json = Serializer.Serialize(settings);

            json.ShouldContain("\"aggregator\"");
            json.ShouldContain("\"sub_merchant_id\"");
            json.ShouldContain("\"aggregator_id_visa\"");
            json.ShouldContain("\"aggregator_id_mc\"");
            json.ShouldContain("\"reconciliation_id\"");
            json.ShouldContain("\"service_type\"");
            json.ShouldContain("same_day");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForProcessingAggregator()
        {
            const string json = @"{
                ""aggregator"": {
                    ""sub_merchant_id"": ""9cf70789ba90123"",
                    ""aggregator_id_visa"": ""10012345"",
                    ""aggregator_id_mc"": ""00000123456""
                },
                ""reconciliation_id"": ""4123495123"",
                ""foreign_retailer_amount"": 200,
                ""service_type"": ""standard""
            }";

            var result = (ProcessingSettings)Serializer.Deserialize(json, typeof(ProcessingSettings));

            result.Aggregator.SubMerchantId.ShouldBe("9cf70789ba90123");
            result.Aggregator.AggregatorIdVisa.ShouldBe("10012345");
            result.Aggregator.AggregatorIdMc.ShouldBe("00000123456");
            result.ReconciliationId.ShouldBe("4123495123");
            result.ForeignRetailerAmount.ShouldBe(200L);
            result.ServiceType.ShouldBe(AchServiceType.Standard);
        }

        // ------------------------------------------------------------------------
        // AirlineData and AccommodationData cardinality
        //
        // processing.airline_data[].passenger is an array. AirlineData.Passenger was typed as a
        // single Passenger, so every GET /payments/{id} response carrying passenger data failed
        // to deserialize. Reported internally against another SDK; .NET carried the same defect
        // and additionally sent a single object on POST /payments, a shape the API never read.
        //
        // The fixture below is the swagger AirlineData / AccommodationData example values, and is
        // shared byte for byte with the equivalent test in the other SDKs so that seven languages
        // assert against one wire shape.
        // ------------------------------------------------------------------------

        private const string AirlineAndAccommodationJson = @"{
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
                ""booking_reference"": ""HOTEL123"",
                ""check_in_date"": ""2023-06-20"",
                ""check_out_date"": ""2023-06-23"",
                ""address"": { ""address_line1"": ""123 Beach Road"", ""zip"": ""10001"" },
                ""state"": ""FL"",
                ""country"": ""USA"",
                ""city"": ""Los Angeles"",
                ""number_of_rooms"": 2,
                ""guests"": [
                  { ""first_name"": ""Jane"", ""last_name"": ""Doe"", ""date_of_birth"": ""1985-07-14"" }
                ],
                ""room"": [
                  { ""rate"": ""70"", ""number_of_nights_at_room_rate"": ""3"" }
                ],
                ""property_phone"": [ { ""country_code"": ""44"", ""number"": ""7123456789"" } ],
                ""customer_service_phone"": [ { ""country_code"": ""44"", ""number"": ""7987654321"" } ]
              }
            ]
        }";

        [Fact]
        public void ShouldDeserializeAirlineDataWithPassengerAsAnArray()
        {
            var result = (ProcessingData)Serializer.Deserialize(
                AirlineAndAccommodationJson, typeof(ProcessingData));

            result.ShouldNotBeNull();
            result.AirlineData.ShouldNotBeNull();
            result.AirlineData.Count.ShouldBe(1);

            var airline = result.AirlineData[0];

            airline.Ticket.ShouldNotBeNull();
            airline.Ticket.Number.ShouldBe("045-21351455613");
            airline.Ticket.IssueDate.ShouldBe(new DateTime(2023, 5, 20));
            airline.Ticket.IssuingCarrierCode.ShouldBe("AI");
            airline.Ticket.TravelPackageIndicator.ShouldBe("B");
            airline.Ticket.TravelAgencyName.ShouldBe("World Tours");
            airline.Ticket.TravelAgencyCode.ShouldBe("01");

            airline.Passenger.ShouldNotBeNull();
            airline.Passenger.Count.ShouldBe(1);
            airline.Passenger[0].FirstName.ShouldBe("John");
            airline.Passenger[0].LastName.ShouldBe("White");
            airline.Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
            airline.Passenger[0].Address.ShouldNotBeNull();
            airline.Passenger[0].Address.Country.ShouldBe(CountryCode.US);

            airline.FlightLegDetails.ShouldNotBeNull();
            airline.FlightLegDetails.Count.ShouldBe(1);

            var leg = airline.FlightLegDetails[0];

            // flight_number is a string in the spec, not an integer.
            leg.FlightNumber.ShouldBe("101");
            leg.CarrierCode.ShouldBe("BA");
            // class_of_travelling, double l. The SDK used to ship service_class.
            leg.ClassOfTravelling.ShouldBe("J");
            leg.DepartureAirport.ShouldBe("LHR");
            leg.DepartureDate.ShouldBe(new DateTime(2023, 6, 19));
            leg.DepartureTime.ShouldBe("15:30");
            leg.ArrivalAirport.ShouldBe("LAX");
            // stop_over_code, three tokens. The SDK used to ship stopover_code.
            leg.StopOverCode.ShouldBe("x");
            leg.FareBasisCode.ShouldBe("SPRSVR");
        }

        [Fact]
        public void ShouldDeserializeAccommodationDataFromTheSameFixture()
        {
            var result = (ProcessingData)Serializer.Deserialize(
                AirlineAndAccommodationJson, typeof(ProcessingData));

            result.AccommodationData.ShouldNotBeNull();
            result.AccommodationData.Count.ShouldBe(1);

            var stay = result.AccommodationData[0];

            stay.Name.ShouldBe("The Sea View Hotel");
            stay.BookingReference.ShouldBe("HOTEL123");
            stay.CheckInDate.ShouldBe(new DateTime(2023, 6, 20));
            stay.CheckOutDate.ShouldBe(new DateTime(2023, 6, 23));
            stay.Address.ShouldNotBeNull();
            stay.Address.AddressLine1.ShouldBe("123 Beach Road");
            stay.Address.Zip.ShouldBe("10001");
            stay.City.ShouldBe("Los Angeles");
            stay.NumberOfRooms.ShouldBe(2);

            // state and country are free-form strings. Typed as the CountryCode enum they could
            // not carry "FL", a US state, or "USA", a three-letter code.
            stay.State.ShouldBe("FL");
            stay.Country.ShouldBe("USA");

            stay.Guests.ShouldNotBeNull();
            stay.Guests.Count.ShouldBe(1);
            stay.Guests[0].FirstName.ShouldBe("Jane");
            stay.Guests[0].LastName.ShouldBe("Doe");
            stay.Guests[0].DateOfBirth.ShouldBe(new DateTime(1985, 7, 14));

            stay.Room.ShouldNotBeNull();
            stay.Room.Count.ShouldBe(1);
            stay.Room[0].Rate.ShouldBe("70");
            // number_of_nights_at_room_rate is a string in the spec, not an integer.
            stay.Room[0].NumberOfNightsAtRoomRate.ShouldBe("3");

            stay.PropertyPhone.ShouldNotBeNull();
            stay.PropertyPhone.Count.ShouldBe(1);
            stay.PropertyPhone[0].CountryCode.ShouldBe("44");
            stay.PropertyPhone[0].Number.ShouldBe("7123456789");

            stay.CustomerServicePhone.ShouldNotBeNull();
            stay.CustomerServicePhone.Count.ShouldBe(1);
            stay.CustomerServicePhone[0].CountryCode.ShouldBe("44");
            stay.CustomerServicePhone[0].Number.ShouldBe("7987654321");
        }

        // The same fixture with passenger replaced by its first element, unchanged. This is the
        // shape PayPal sends; the spec allows it on PaymentInterfacesProcessingAirlineData with
        // the note "PayPal requires a single object".
        [Fact]
        public void ShouldDeserializeAirlineDataWithPassengerAsASingleObject()
        {
            const string json = @"{
                ""airline_data"": [
                  {
                    ""ticket"": { ""number"": ""045-21351455613"" },
                    ""passenger"": {
                      ""first_name"": ""John"",
                      ""last_name"": ""White"",
                      ""date_of_birth"": ""1990-05-26"",
                      ""address"": { ""country"": ""US"" }
                    }
                  }
                ]
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.AirlineData.Count.ShouldBe(1);

            // Normalized to a one-element list, so callers only handle one shape.
            result.AirlineData[0].Passenger.ShouldNotBeNull();
            result.AirlineData[0].Passenger.Count.ShouldBe(1);
            result.AirlineData[0].Passenger[0].FirstName.ShouldBe("John");
            result.AirlineData[0].Passenger[0].LastName.ShouldBe("White");
            result.AirlineData[0].Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
            result.AirlineData[0].Passenger[0].Address.Country.ShouldBe(CountryCode.US);
        }

        [Fact]
        public void ShouldDeserializeAirlineDataWithNoPassengerAtAll()
        {
            const string json = @"{
                ""airline_data"": [ { ""ticket"": { ""number"": ""045"" }, ""passenger"": null } ]
            }";

            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.AirlineData.Count.ShouldBe(1);
            result.AirlineData[0].Passenger.ShouldBeNull();
        }

        // Regression: GET /payments/{id} used to throw when processing.airline_data[].passenger
        // came back as an array, because AirlineData.Passenger was a single Passenger.
        // Reported internally.
        [Fact]
        public void ShouldNotThrowDeserializingAPaymentDetailsAirlinePassengerArray()
        {
            Should.NotThrow(() => Serializer.Deserialize(
                AirlineAndAccommodationJson, typeof(ProcessingData)));
        }

        // Guards the converter's CanWrite => false. If a writer is ever enabled on
        // SingleOrArrayConverter, every request carrying airline data throws here instead of in
        // a merchant's integration.
        [Fact]
        public void ShouldAlwaysSerializePassengerAsAnArray()
        {
            var settings = new ProcessingSettings
            {
                AirlineData = new List<AirlineData>
                {
                    new AirlineData
                    {
                        Passenger = new List<Passenger>
                        {
                            new Passenger { FirstName = "John", LastName = "White" }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(settings);

            json.ShouldContain("\"passenger\":[{");
            json.ShouldNotContain("\"passenger\":{");
        }

        [Fact]
        public void ShouldRoundTripSerializeAirlineDataPassengerArray()
        {
            var original = new ProcessingData
            {
                AirlineData = new List<AirlineData>
                {
                    new AirlineData
                    {
                        Ticket = new Ticket
                        {
                            Number = "045-21351455613",
                            IssueDate = new DateTime(2023, 5, 20)
                        },
                        Passenger = new List<Passenger>
                        {
                            new Passenger
                            {
                                FirstName = "John",
                                DateOfBirth = new DateTime(1990, 5, 26)
                            },
                            new Passenger { FirstName = "Jane" }
                        },
                        FlightLegDetails = new List<FlightLegDetails>
                        {
                            new FlightLegDetails
                            {
                                FlightNumber = "101",
                                ClassOfTravelling = "J",
                                StopOverCode = "x",
                                DepartureDate = new DateTime(2023, 6, 19)
                            }
                        }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var result = (ProcessingData)Serializer.Deserialize(json, typeof(ProcessingData));

            result.AirlineData.Count.ShouldBe(1);
            result.AirlineData[0].Passenger.Count.ShouldBe(2);
            result.AirlineData[0].Passenger[0].FirstName.ShouldBe("John");
            result.AirlineData[0].Passenger[0].DateOfBirth.ShouldBe(new DateTime(1990, 5, 26));
            result.AirlineData[0].Passenger[1].FirstName.ShouldBe("Jane");
            result.AirlineData[0].Ticket.Number.ShouldBe("045-21351455613");
            result.AirlineData[0].Ticket.IssueDate.ShouldBe(new DateTime(2023, 5, 20));
            result.AirlineData[0].FlightLegDetails[0].FlightNumber.ShouldBe("101");
            result.AirlineData[0].FlightLegDetails[0].ClassOfTravelling.ShouldBe("J");
            result.AirlineData[0].FlightLegDetails[0].StopOverCode.ShouldBe("x");
            result.AirlineData[0].FlightLegDetails[0].DepartureDate.ShouldBe(new DateTime(2023, 6, 19));
        }

        // Asserts on the serialized string, so a future rename cannot pass silently. These four
        // keys were all wrong at some point and each one was dropped by the gateway.
        [Fact]
        public void ShouldSerializeAirlineKeysExactlyAsTheSpecNamesThem()
        {
            var json = Serializer.Serialize(new AirlineData
            {
                Ticket = new Ticket { IssueDate = new DateTime(2023, 5, 20) },
                Passenger = new List<Passenger> { new Passenger { FirstName = "John" } },
                FlightLegDetails = new List<FlightLegDetails>
                {
                    new FlightLegDetails
                    {
                        FlightNumber = "101",
                        ClassOfTravelling = "J",
                        StopOverCode = "x",
                        DepartureDate = new DateTime(2023, 6, 19)
                    }
                }
            });

            json.ShouldContain("\"class_of_travelling\":\"J\"");
            json.ShouldContain("\"stop_over_code\":\"x\"");
            json.ShouldContain("\"flight_number\":\"101\"");
            json.ShouldContain("\"issue_date\":\"2023-05-20\"");
            json.ShouldContain("\"departure_date\":\"2023-06-19\"");

            // The deprecated members map to keys the API does not define.
            json.ShouldNotContain("service_class");
            json.ShouldNotContain("\"stopover_code\"");
        }

        [Fact]
        public void ShouldSerializeAccommodationStateAndCountryAsFreeFormStrings()
        {
            var json = Serializer.Serialize(new AccommodationData
            {
                Name = "The Sea View Hotel",
                State = "FL",
                Country = "USA",
                Room = new List<PaymentContextsAccommodationRoom>
                {
                    new PaymentContextsAccommodationRoom
                    {
                        Rate = "70",
                        NumberOfNightsAtRoomRate = "3"
                    }
                }
            });

            json.ShouldContain("\"state\":\"FL\"");
            json.ShouldContain("\"country\":\"USA\"");
            json.ShouldContain("\"number_of_nights_at_room_rate\":\"3\"");
        }

    }
}
