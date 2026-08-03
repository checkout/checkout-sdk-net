using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.Responses;
using Checkout.Common;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using AppliedType = Checkout.Authentication.Standalone.Common.Responses.Exemption.AppliedType;
using MetadataCardCategoryType =
    Checkout.Authentication.Standalone.Common.Responses.Card.Metadata.CardCategoryType;
using MetadataCardType = Checkout.Authentication.Standalone.Common.Responses.Card.Metadata.CardType;
using ResponseChallengeIndicatorType = Checkout.Authentication.Standalone.Common.Responses.ChallengeIndicatorType;
using SchemeInfoNameType = Checkout.Authentication.Standalone.Common.Responses.SchemeInfo.NameType;
using GetSessionDetailsResponseOk =
    Checkout.Authentication.Standalone.GETSessionsId.Responses.GetSessionDetailsResponseOk.GetSessionDetailsResponseOk;
using RequestASessionResponseAccepted =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseAccepted.
    RequestASessionResponseAccepted;
using RequestASessionResponseCreated =
    Checkout.Authentication.Standalone.POSTSessions.Responses.RequestASessionResponseCreated.
    RequestASessionResponseCreated;
using UpdateASessionResponseOk =
    Checkout.Authentication.Standalone.PUTSessionsIdCollectData.Responses.UpdateASessionResponseOk.
    UpdateASessionResponseOk;

namespace Checkout.Authentification.Standalone
{
    /// <summary>
    /// Full-property deserialization coverage for the four session response classes:
    /// the 201 and 202 responses of POST /sessions, the 200 of GET /sessions/{id}, and the 200 of
    /// PUT /sessions/{id}/collect-data.
    /// A single fixture populates every property of every class; a reflection guard then asserts that
    /// none was left unbound, so adding a property without extending the fixture fails the test.
    /// </summary>
    public class SessionResponsesSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

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
    }
}
