using AccountInfo = Checkout.Authentication.Standalone.Common.AccountInfo.AccountInfo;
using AppliedType = Checkout.Authentication.Standalone.Common.Responses.Exemption.AppliedType;
using Checkout.Authentication.Standalone.Common.AccountInfo;
using Checkout.Authentication.Standalone.Common.InitialTransaction;
using Checkout.Authentication.Standalone.Common.Installment;
using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Authentication.Standalone.Common.Recurring;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.NonHostedCompletion;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.CardSource;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.HomePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.MobilePhone;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.Common.WorkPhone;
using Checkout.Common;
using Checkout.Payments;
using CommonChallengeIndicatorType = Checkout.Common.ChallengeIndicatorType;
using GetSessionDetailsResponseOk =
    Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk.GetSessionDetailsResponseOk;
using MetadataCardCategoryType =
    Checkout.Authentication.Standalone.Common.Responses.Card.Metadata.CardCategoryType;
using MetadataCardType = Checkout.Authentication.Standalone.Common.Responses.Card.Metadata.CardType;
using RequestASessionRequestBody =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.RequestASessionRequest;
using RequestASessionResponseAccepted =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted.
    RequestASessionResponseAccepted;
using RequestASessionResponseCreated =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated.
    RequestASessionResponseCreated;
using RequestBillingDescriptor =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.BillingDescriptor.
    BillingDescriptor;
using RequestChallengeIndicatorType =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChallengeIndicatorType;
using RequestGoogleSpa =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.GoogleSpa.GoogleSpa;
using RequestMarketplace =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Marketplace.Marketplace;
using RequestOptimization =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Optimization.Optimization;
using RequestPreferredExperiencesType =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.PreferredExperiencesType;
using RequestShippingAddress =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ShippingAddress.ShippingAddress;
using ResponseChallengeIndicatorType = Checkout.Authentication.Standalone.Common.Responses.ChallengeIndicatorType;
using SchemeInfoNameType = Checkout.Authentication.Standalone.Common.Responses.SchemeInfo.NameType;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using UpdateASessionResponseOk =
    Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk.
    UpdateASessionResponseOk;
using Xunit;

namespace Checkout.Authentification.Standalone
{
    /// <summary>
    /// Schema validation tests for Checkout.Authentification.Standalone.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class StandaloneAuthenticationSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // RequestASessionRequest
        // Full-property serialization coverage for the POST /sessions request body.
        // Every one of the class's 24 properties is populated and asserted on the emitted JSON.
        // ------------------------------------------------------------------------

        private static RequestASessionRequestBody FullyPopulated()
        {
            return new RequestASessionRequestBody
            {
                Currency = Currency.USD,
                Source = new CardSource
                {
                    Number = "4485040371536584",
                    ExpiryMonth = 1,
                    ExpiryYear = 2030,
                    Name = "Bruce Wayne"
                },
                Completion = new NonHostedCompletion { CallbackUrl = "https://merchant.com/callback" },
                Amount = 6540L,
                ProcessingChannelId = "pc_q4dbxom5jbgudnjzjpz7iw4d0u",
                Marketplace = new RequestMarketplace { SubEntityId = "ent_ocw5i74vowfg2edpy66izhts2u" },
                AuthenticationType = AuthenticationType.Regular,
                AuthenticationCategory = AuthenticationCategoryType.Payment,
                AccountInfo = new AccountInfo { PurchaseCount = 10L, AddCardAttempts = 5L },
                ChallengeIndicator = RequestChallengeIndicatorType.TrustedListingPrompt,
                BillingDescriptor = new RequestBillingDescriptor { Name = "SUPERHEROES.COM" },
                Reference = "ORD-5023-4E89",
                MerchantRiskInfo = new MerchantRiskInfo
                {
                    DeliveryEmail = "bruce@wayne-enterprises.com",
                    IsPreorder = false,
                    IsReorder = false
                },
                TransactionType = TransactionType.GoodsService,
                ShippingAddress = new RequestShippingAddress
                {
                    AddressLine1 = "Checkout.com",
                    City = "London",
                    Zip = "W1T 4TJ"
                },
                ShippingAddressMatchesBilling = true,
                ChannelData = new BrowserChannelData
                {
                    AcceptHeader = "Accept:  *.*, q=0.1",
                    JavaEnabled = true,
                    JavascriptEnabled = true,
                    Language = "FR-fr",
                    UserAgent = "Mozilla/5.0"
                },
                Recurring = new Recurring { DaysBetweenPayments = 30, Expiry = "99991231" },
                Installment = new Installment { NumberOfPayments = 3, DaysBetweenPayments = 30 },
                Optimization = new RequestOptimization { Framework = "acceptance_rates" },
                InitialTransaction = new InitialTransaction { AcsTransactionId = "acs-txn-id" },
                GoogleSpa = new RequestGoogleSpa { ContinueUrl = "https://merchant.com/continue" },
                PreferredExperiences = new List<RequestPreferredExperiencesType> { RequestPreferredExperiencesType.Threeds },
                DeviceInformation = new DeviceInformation { DeviceId = "device-id", DeviceSessionId = "device-session" }
            };
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesOnly()
        {
            var request = new RequestASessionRequestBody
            {
                Currency = Currency.USD,
                Source = new CardSource { Number = "4485040371536584" },
                Completion = new NonHostedCompletion { CallbackUrl = "https://merchant.com/callback" }
            };

            Should.NotThrow(() => Serializer.Serialize(request));
        }

        /// <summary>
        /// Guards against a property being silently dropped: every declared property must be populated
        /// by the fixture, and every one must appear in the emitted JSON.
        /// </summary>
        [Fact]
        public void ShouldSerializeEveryDeclaredProperty()
        {
            var request = FullyPopulated();
            var json = Serializer.Serialize(request);

            var properties = typeof(RequestASessionRequestBody)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList();

            properties.Count.ShouldBe(24);

            foreach (var property in properties)
            {
                Assert.True(property.GetValue(request) != null,
                    $"fixture does not populate {property.Name}");

                var wireName = ToSnakeCase(property.Name);
                Assert.True(json.Contains($"\"{wireName}\":"),
                    $"property {property.Name} ({wireName}) is missing from the serialized JSON");
            }
        }

        [Fact]
        public void ShouldSerializeScalarsAndEnumsAsSnakeCase()
        {
            var json = Serializer.Serialize(FullyPopulated());

            json.ShouldContain("\"amount\":6540");
            json.ShouldContain("\"currency\":\"USD\"");
            json.ShouldContain("\"processing_channel_id\":\"pc_q4dbxom5jbgudnjzjpz7iw4d0u\"");
            json.ShouldContain("\"authentication_type\":\"regular\"");
            json.ShouldContain("\"authentication_category\":\"payment\"");
            json.ShouldContain("\"challenge_indicator\":\"trusted_listing_prompt\"");
            json.ShouldContain("\"reference\":\"ORD-5023-4E89\"");
            json.ShouldContain("\"transaction_type\":\"goods_service\"");
            json.ShouldContain("\"shipping_address_matches_billing\":true");
        }

        [Fact]
        public void ShouldSerializeNestedObjectContents()
        {
            var json = Serializer.Serialize(FullyPopulated());

            json.ShouldContain("\"sub_entity_id\":\"ent_ocw5i74vowfg2edpy66izhts2u\"");
            json.ShouldContain("\"purchase_count\":10");
            json.ShouldContain("\"name\":\"SUPERHEROES.COM\"");
            json.ShouldContain("\"delivery_email\":\"bruce@wayne-enterprises.com\"");
            json.ShouldContain("\"callback_url\":\"https://merchant.com/callback\"");
            json.ShouldContain("\"number_of_payments\":3");
            json.ShouldContain("\"framework\":\"acceptance_rates\"");
            json.ShouldContain("\"acs_transaction_id\":\"acs-txn-id\"");
            json.ShouldContain("\"continue_url\":\"https://merchant.com/continue\"");
            json.ShouldContain("\"device_session_id\":\"device-session\"");
            json.ShouldContain("\"preferred_experiences\":[\"3ds\"]");
        }

        private static string ToSnakeCase(string name)
        {
            return string.Concat(name.Select((c, i) =>
                char.IsUpper(c) && i > 0 ? "_" + char.ToLowerInvariant(c) : char.ToLowerInvariant(c).ToString()));
        }

        // ------------------------------------------------------------------------
        // SessionResponses
        // Full-property deserialization coverage for the four session response classes:
        // the 201 and 202 responses of POST /sessions, the 200 of GET /sessions/{id}, and the 200 of
        // PUT /sessions/{id}/collect-data.
        // A single fixture populates every property of every class; a reflection guard then asserts that
        // none was left unbound, so adding a property without extending the fixture fails the test.
        // ------------------------------------------------------------------------

        private static string FullPayload()
        {
            return "{"
                   + "\"id\":\"sid_y3oqhf46pyzuxjbcn2giaqnb44\","
                   + "\"session_secret\":\"sek_Dal7UyiH8rIFXA4PfgiIk2jUyQkVDeEWgVBEL4TsRTE=\","
                   + "\"transaction_id\":\"9aea641d-0549-4222-9ca9-d90b43a4f38c\","
                   + "\"scheme\":\"visa\","
                   + "\"amount\":6540,"
                   + "\"currency\":\"USD\","
                   + "\"authentication_type\":\"regular\","
                   + "\"authentication_category\":\"payment\","
                   + "\"status\":\"challenged\","
                   + "\"status_reason\":\"ares_status\","
                   + "\"protocol_version\":\"2.2.0\","
                   + "\"challenge_indicator\":\"trusted_listing\","
                   + "\"completed\":true,"
                   + "\"challenged\":true,"
                   + "\"approved\":true,"
                   + "\"certificates\":{\"ds_public\":\"ds-public\",\"ca_public\":\"ca-public\"},"
                   + "\"account_info\":{\"purchase_count\":10,\"add_card_attempts\":5},"
                   + "\"merchant_risk_info\":{\"delivery_email\":\"bruce@wayne-enterprises.com\"},"
                   + "\"reference\":\"ORD-5023-4E89\","
                   + "\"transaction_type\":\"goods_service\","
                   + "\"next_actions\":[\"collect_channel_data\"],"
                   + "\"ds\":{\"ds_id\":\"ds-id\",\"reference_number\":\"ds-ref\",\"transaction_id\":\"ds-txn\"},"
                   + "\"acs\":{\"reference_number\":\"acs-ref\",\"transaction_id\":\"acs-txn\","
                   + "\"challenge_mandated\":true,\"url\":\"https://acs.example.com/challenge\"},"
                   + "\"response_code\":\"Y\","
                   + "\"response_status_reason\":\"01\","
                   + "\"cryptogram\":\"MTIzNDU2Nzg5MDA5ODc2NTQzMjE=\","
                   + "\"eci\":\"05\","
                   + "\"xid\":\"XSUErNftqkiTdlkpSk8p32GWOFA\","
                   + "\"cardholder_info\":\"Card declined. Please contact your issuing bank.\","
                   + "\"card\":{\"instrument_id\":\"src_w4jelhppmfiufdnatndh3wtsfq\",\"fingerprint\":\"fp-1\","
                   + "\"metadata\":{\"card_type\":\"CREDIT\",\"card_category\":\"CONSUMER\","
                   + "\"issuer_name\":\"Checkout\",\"issuer_country\":\"GB\",\"product_id\":\"MDS\","
                   + "\"product_type\":\"Debit MasterCard Card\"}},"
                   + "\"recurring\":{\"days_between_payments\":30,\"expiry\":\"99991231\"},"
                   + "\"installment\":{\"number_of_payments\":3,\"days_between_payments\":30,\"expiry\":\"99991231\"},"
                   + "\"initial_transaction\":{\"acs_transaction_id\":\"acs-txn-id\"},"
                   + "\"customer_ip\":\"192.168.1.1\","
                   + "\"authentication_date\":\"2026-08-03T10:11:12Z\","
                   + "\"exemption\":{\"requested\":\"none\",\"applied\":\"low_value\",\"code\":\"cb-code\"},"
                   + "\"flow_type\":\"challenged\","
                   + "\"optimization\":{\"optimized\":true,\"framework\":\"acceptance_rates\","
                   + "\"optimized_properties\":[{\"field\":\"amount\"}]},"
                   + "\"scheme_info\":{\"name\":\"visa\",\"score\":\"0.5\",\"avalgo\":\"1\"},"
                   + "\"3ds\":{\"challenge_request\":\"creq\",\"interaction_counter\":\"03\"},"
                   + "\"preferred_experiences\":{\"google_spa\":{\"status\":\"available\"},"
                   + "\"threeds\":{\"status\":\"processed\"}},"
                   + "\"experience\":\"3ds\","
                   + "\"google_spa\":{\"challenge_url\":\"https://google.example/challenge\","
                   + "\"initial_timeout\":\"5\",\"max_timeout\":\"10\"},"
                   + "\"_links\":{\"self\":{\"href\":\"https://api.checkout.com/sessions/sid_y3oqhf46pyzuxjbcn2giaqnb44\"}}"
                   + "}";
        }

        private static T Deserialize<T>()
        {
            return (T)Serializer.Deserialize(FullPayload(), typeof(T));
        }

        /// <summary>
        /// The core full-property guard, run against every response class. Every declared property must
        /// be bound by the fixture; nullable value types must carry a value.
        /// </summary>
        [Theory]
        [InlineData(typeof(GetSessionDetailsResponseOk), 43)]
        [InlineData(typeof(RequestASessionResponseCreated), 43)]
        [InlineData(typeof(UpdateASessionResponseOk), 43)]
        [InlineData(typeof(RequestASessionResponseAccepted), 28)]
        public void ShouldBindEveryDeclaredPropertyOnEveryResponse(Type responseType, int expectedPropertyCount)
        {
            var response = Serializer.Deserialize(FullPayload(), responseType);

            response.ShouldNotBeNull();

            var properties = responseType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .ToList();

            properties.Count.ShouldBe(expectedPropertyCount);

            foreach (var property in properties)
            {
                var value = property.GetValue(response);

                Assert.True(value != null,
                    $"{responseType.Name}.{property.Name} was not deserialized (null)");

                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    Assert.True((bool)property.PropertyType
                            .GetProperty("HasValue")
                            .GetValue(value),
                        $"{responseType.Name}.{property.Name} is a nullable with no value");
                }
            }
        }

        [Fact]
        public void ShouldDeserializeScalarsOnGetSessionDetails()
        {
            var response = Deserialize<GetSessionDetailsResponseOk>();

            response.Id.ShouldBe("sid_y3oqhf46pyzuxjbcn2giaqnb44");
            response.SessionSecret.ShouldBe("sek_Dal7UyiH8rIFXA4PfgiIk2jUyQkVDeEWgVBEL4TsRTE=");
            response.TransactionId.ShouldBe("9aea641d-0549-4222-9ca9-d90b43a4f38c");
            response.Amount.ShouldBe(6540);
            response.ProtocolVersion.ShouldBe("2.2.0");
            response.Reference.ShouldBe("ORD-5023-4E89");
            response.ResponseStatusReason.ShouldBe("01");
            response.Eci.ShouldBe("05");
            response.CustomerIp.ShouldBe("192.168.1.1");
        }

        [Fact]
        public void ShouldDeserializeEveryEnumTypedPropertyOnGetSessionDetails()
        {
            var response = Deserialize<GetSessionDetailsResponseOk>();

            response.Scheme.ShouldBe(SchemeType.Visa);
            response.Currency.ShouldBe(Currency.USD);
            response.AuthenticationType.ShouldBe(AuthenticationType.Regular);
            response.AuthenticationCategory.ShouldBe(AuthenticationCategoryType.Payment);
            response.Status.ShouldBe(StatusType.Challenged);
            response.StatusReason.ShouldBe(StatusReasonType.AresStatus);
            response.TransactionType.ShouldBe(TransactionType.GoodsService);
            response.ResponseCode.ShouldBe(ResponseCodeType.Y);
            response.FlowType.ShouldBe(FlowType.Challenged);
            response.ChallengeIndicator.ShouldBe(ResponseChallengeIndicatorType.TrustedListing);
            response.Experience.ShouldBe(ExperienceType.Threeds);
            response.NextActions.ShouldContain(NextActionsType.CollectChannelData);
        }

        [Fact]
        public void ShouldDeserializeAuthenticationDate()
        {
            var response = Deserialize<GetSessionDetailsResponseOk>();

            response.AuthenticationDate.ShouldNotBeNull();
            response.AuthenticationDate.Value.ToUniversalTime()
                .ShouldBe(new DateTime(2026, 8, 3, 10, 11, 12, DateTimeKind.Utc));
        }

        [Fact]
        public void ShouldDeserializeNestedObjectsOnGetSessionDetails()
        {
            var response = Deserialize<GetSessionDetailsResponseOk>();

            response.Certificates.DsPublic.ShouldBe("ds-public");
            response.AccountInfo.PurchaseCount.ShouldBe(10);
            response.MerchantRiskInfo.DeliveryEmail.ShouldBe("bruce@wayne-enterprises.com");
            response.Ds.DsId.ShouldBe("ds-id");
            response.Acs.ReferenceNumber.ShouldBe("acs-ref");
            response.Card.InstrumentId.ShouldBe("src_w4jelhppmfiufdnatndh3wtsfq");
            response.Recurring.DaysBetweenPayments.ShouldBe(30);
            response.Installment.NumberOfPayments.ShouldBe(3);
            response.InitialTransaction.AcsTransactionId.ShouldBe("acs-txn-id");
            response.Exemption.Applied.ShouldBe(AppliedType.LowValue);
            response.SchemeInfo.Name.ShouldBe(SchemeInfoNameType.Visa);
            response.Optimization.Optimized.ShouldBe(true);
            response.Threeds.InteractionCounter.ShouldBe("03");
            response.GoogleSpa.ChallengeUrl.ShouldBe("https://google.example/challenge");
            response.PreferredExperiences.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldDeserializeAllSixCardMetadataProperties()
        {
            var metadata = Deserialize<GetSessionDetailsResponseOk>().Card.Metadata;

            metadata.ShouldNotBeNull();
            metadata.CardType.ShouldBe(MetadataCardType.CREDIT);
            metadata.CardCategory.ShouldBe(MetadataCardCategoryType.CONSUMER);
            metadata.IssuerName.ShouldBe("Checkout");
            metadata.IssuerCountry.ShouldBe(CountryCode.GB);
            metadata.ProductId.ShouldBe("MDS");
            metadata.ProductType.ShouldBe("Debit MasterCard Card");
        }

        [Fact]
        public void ShouldDeserializeInheritedLinks()
        {
            var response = Deserialize<GetSessionDetailsResponseOk>();

            response.GetSelfLink().ShouldNotBeNull();
            response.GetSelfLink().Href
                .ShouldBe("https://api.checkout.com/sessions/sid_y3oqhf46pyzuxjbcn2giaqnb44");
        }

        // ------------------------------------------------------------------------
        // BrowserChannelData
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeExplicitFalseFlags()
        {
            var data = new BrowserChannelData
            {
                JavaEnabled = false,
                JavascriptEnabled = false
            };

            var json = Serializer.Serialize(data);

            json.ShouldContain("\"java_enabled\":false");
            json.ShouldContain("\"javascript_enabled\":false");
        }

        [Fact]
        public void ShouldSerializeExplicitTrueFlags()
        {
            var data = new BrowserChannelData
            {
                JavaEnabled = true,
                JavascriptEnabled = true
            };

            var json = Serializer.Serialize(data);

            json.ShouldContain("\"java_enabled\":true");
            json.ShouldContain("\"javascript_enabled\":true");
        }

        [Fact]
        public void ShouldOmitUnsetFlags()
        {
            var data = new BrowserChannelData();

            var json = Serializer.Serialize(data);

            json.ShouldNotContain("java_enabled");
            json.ShouldNotContain("javascript_enabled");
        }

        [Fact]
        public void ShouldRoundTripExplicitFalse()
        {
            var original = new BrowserChannelData { JavaEnabled = false, JavascriptEnabled = false };

            var json = Serializer.Serialize(original);
            var deserialized = (BrowserChannelData)Serializer.Deserialize(json, typeof(BrowserChannelData));

            deserialized.JavaEnabled.ShouldBe(false);
            deserialized.JavascriptEnabled.ShouldBe(false);
        }

        // ------------------------------------------------------------------------
        // ChallengeIndicator
        // Covers the three challenge-indicator enums and their call sites:
        // the nine-value request enum on POST /sessions, the nine-value response enum returned by the
        // session responses, and the four-value shared enum used by the payments 3ds field.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(RequestChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(RequestChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        [InlineData(RequestChallengeIndicatorType.LowValue, "low_value")]
        [InlineData(RequestChallengeIndicatorType.TrustedListing, "trusted_listing")]
        [InlineData(RequestChallengeIndicatorType.TrustedListingPrompt, "trusted_listing_prompt")]
        [InlineData(RequestChallengeIndicatorType.TransactionRiskAssessment, "transaction_risk_assessment")]
        [InlineData(RequestChallengeIndicatorType.DataShare, "data_share")]
        public void ShouldSerializeEveryRequestValueOnSessionRequest(
            RequestChallengeIndicatorType value,
            string expected)
        {
            var request = new RequestASessionRequestBody { ChallengeIndicator = value };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"challenge_indicator\":\"" + expected + "\"");
        }

        [Theory]
        [InlineData(RequestChallengeIndicatorType.NoPreference)]
        [InlineData(RequestChallengeIndicatorType.NoChallengeRequested)]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequested)]
        [InlineData(RequestChallengeIndicatorType.ChallengeRequestedMandate)]
        [InlineData(RequestChallengeIndicatorType.LowValue)]
        [InlineData(RequestChallengeIndicatorType.TrustedListing)]
        [InlineData(RequestChallengeIndicatorType.TrustedListingPrompt)]
        [InlineData(RequestChallengeIndicatorType.TransactionRiskAssessment)]
        [InlineData(RequestChallengeIndicatorType.DataShare)]
        public void ShouldRoundTripEveryRequestValue(RequestChallengeIndicatorType value)
        {
            var json = Serializer.Serialize(new RequestASessionRequestBody { ChallengeIndicator = value });

            var deserialized = (RequestASessionRequestBody)Serializer.Deserialize(
                json, typeof(RequestASessionRequestBody));

            deserialized.ChallengeIndicator.ShouldBe(value);
        }

        [Fact]
        public void ShouldDefaultRequestChallengeIndicatorToNoPreference()
        {
            var request = new RequestASessionRequestBody();

            request.ChallengeIndicator.ShouldBe(RequestChallengeIndicatorType.NoPreference);
            Serializer.Serialize(request).ShouldContain("\"challenge_indicator\":\"no_preference\"");
        }

        /// <summary>
        /// The API Reference specifies only the four base values for the session response fields, but
        /// the request accepts nine. An exemption value echoed back must still deserialize: because
        /// the response property is a non-nullable enum, an unrecognised value would otherwise be
        /// tolerated into the default NoPreference and become indistinguishable from a real
        /// no_preference.
        /// </summary>
        [Theory]
        [InlineData(ResponseChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(ResponseChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(ResponseChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(ResponseChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        [InlineData(ResponseChallengeIndicatorType.LowValue, "low_value")]
        [InlineData(ResponseChallengeIndicatorType.TrustedListing, "trusted_listing")]
        [InlineData(ResponseChallengeIndicatorType.TrustedListingPrompt, "trusted_listing_prompt")]
        [InlineData(ResponseChallengeIndicatorType.TransactionRiskAssessment, "transaction_risk_assessment")]
        [InlineData(ResponseChallengeIndicatorType.DataShare, "data_share")]
        public void ShouldDeserializeEveryResponseValueOnEverySessionResponse(
            ResponseChallengeIndicatorType expected,
            string wireValue)
        {
            var json = "{\"challenge_indicator\":\"" + wireValue + "\"}";

            var created = (RequestASessionResponseCreated)Serializer.Deserialize(
                json, typeof(RequestASessionResponseCreated));
            var accepted = (RequestASessionResponseAccepted)Serializer.Deserialize(
                json, typeof(RequestASessionResponseAccepted));
            var details = (GetSessionDetailsResponseOk)Serializer.Deserialize(
                json, typeof(GetSessionDetailsResponseOk));
            var updated = (UpdateASessionResponseOk)Serializer.Deserialize(
                json, typeof(UpdateASessionResponseOk));

            created.ChallengeIndicator.ShouldBe(expected);
            accepted.ChallengeIndicator.ShouldBe(expected);
            details.ChallengeIndicator.ShouldBe(expected);
            updated.ChallengeIndicator.ShouldBe(expected);
        }

        [Theory]
        [InlineData(CommonChallengeIndicatorType.NoPreference, "no_preference")]
        [InlineData(CommonChallengeIndicatorType.NoChallengeRequested, "no_challenge_requested")]
        [InlineData(CommonChallengeIndicatorType.ChallengeRequested, "challenge_requested")]
        [InlineData(CommonChallengeIndicatorType.ChallengeRequestedMandate, "challenge_requested_mandate")]
        public void ShouldRoundTripEverySharedPaymentsValue(CommonChallengeIndicatorType value, string wireValue)
        {
            var request = new ThreeDsRequest { ChallengeIndicator = value };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"challenge_indicator\":\"" + wireValue + "\"");

            var deserialized = (ThreeDsRequest)Serializer.Deserialize(json, typeof(ThreeDsRequest));
            deserialized.ChallengeIndicator.ShouldBe(value);
        }

        /// <summary>
        /// The payments 3ds field must stay bound to the four-value shared enum. Assigning a sessions
        /// exemption value to it should not compile, so this guards the binding by type identity: if
        /// ThreeDsRequest.ChallengeIndicator were retyped to either sessions enum, this fails.
        /// </summary>
        [Fact]
        public void ShouldBindPaymentsThreeDsToTheSharedFourValueEnum()
        {
            var property = typeof(ThreeDsRequest).GetProperty(nameof(ThreeDsRequest.ChallengeIndicator));

            property.ShouldNotBeNull();
            Nullable.GetUnderlyingType(property.PropertyType).ShouldBe(typeof(CommonChallengeIndicatorType));
            Enum.GetValues(typeof(CommonChallengeIndicatorType)).Length.ShouldBe(4);
        }

        /// <summary>
        /// Guards the split: the shared payments enum must expose only the four base values, while
        /// both sessions enums expose all nine. If the exemption values leak back onto the shared
        /// enum they would be offered on POST /payments, where the API rejects them.
        /// </summary>
        [Fact]
        public void ShouldKeepTheSharedPaymentsEnumNarrowAndTheSessionsEnumsWide()
        {
            Enum.GetValues(typeof(CommonChallengeIndicatorType)).Length.ShouldBe(4);
            Enum.GetValues(typeof(RequestChallengeIndicatorType)).Length.ShouldBe(9);
            Enum.GetValues(typeof(ResponseChallengeIndicatorType)).Length.ShouldBe(9);
        }

        // ------------------------------------------------------------------------
        // SessionPhone
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeHomePhoneWithAllProperties()
        {
            var phone = new HomePhone { CountryCode = "44", Number = "2079460000" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripHomePhone()
        {
            var original = new HomePhone { CountryCode = "44", Number = "2079460000" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (HomePhone)serializer.Deserialize(json, typeof(HomePhone));

            deserialized.CountryCode.ShouldBe("44");
            deserialized.Number.ShouldBe("2079460000");
        }

        /// <summary>
        /// Regression test for GitHub issue #549 — CountryCode enum serialised as ISO country code (e.g. "GB"),
        /// causing country_code_invalid when the API expects a numeric ITU-E.164 dialing code (e.g. "44").
        /// </summary>
        [Fact]
        public void Issue549_HomePhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new HomePhone { CountryCode = "44", Number = "2079460000" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"44\"");
            json.ShouldNotContain("\"country_code\":\"GB\"");
        }

        [Fact]
        public void ShouldDeserializeHomePhoneFromSwaggerExample()
        {
            const string json = @"{""country_code"":""234"",""number"":""0204567895""}";

            var phone = (HomePhone)new JsonSerializer().Deserialize(json, typeof(HomePhone));

            phone.CountryCode.ShouldBe("234");
            phone.Number.ShouldBe("0204567895");
        }

        [Fact]
        public void ShouldSerializeMobilePhoneWithAllProperties()
        {
            var phone = new MobilePhone { CountryCode = "1", Number = "4155552671" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripMobilePhone()
        {
            var original = new MobilePhone { CountryCode = "1", Number = "4155552671" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (MobilePhone)serializer.Deserialize(json, typeof(MobilePhone));

            deserialized.CountryCode.ShouldBe("1");
            deserialized.Number.ShouldBe("4155552671");
        }

        /// <summary>
        /// Regression test for GitHub issue #549.
        /// </summary>
        [Fact]
        public void Issue549_MobilePhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new MobilePhone { CountryCode = "1", Number = "4155552671" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"1\"");
            json.ShouldNotContain("\"country_code\":\"US\"");
        }

        [Fact]
        public void ShouldSerializeWorkPhoneWithAllProperties()
        {
            var phone = new WorkPhone { CountryCode = "49", Number = "3012345678" };

            Should.NotThrow(() => new JsonSerializer().Serialize(phone));
        }

        [Fact]
        public void ShouldRoundTripWorkPhone()
        {
            var original = new WorkPhone { CountryCode = "49", Number = "3012345678" };
            var serializer = new JsonSerializer();

            var json = serializer.Serialize(original);
            var deserialized = (WorkPhone)serializer.Deserialize(json, typeof(WorkPhone));

            deserialized.CountryCode.ShouldBe("49");
            deserialized.Number.ShouldBe("3012345678");
        }

        /// <summary>
        /// Regression test for GitHub issue #549.
        /// </summary>
        [Fact]
        public void Issue549_WorkPhone_CountryCodeShouldSerializeAsNumericDialingCode()
        {
            var phone = new WorkPhone { CountryCode = "49", Number = "3012345678" };

            var json = new JsonSerializer().Serialize(phone);

            json.ShouldContain("\"country_code\":\"49\"");
            json.ShouldNotContain("\"country_code\":\"DE\"");
        }
    }
}
