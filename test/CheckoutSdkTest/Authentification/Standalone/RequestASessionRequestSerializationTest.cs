using Checkout.Authentication.Standalone.Common;
using Checkout.Authentication.Standalone.Common.AccountInfo;
using Checkout.Authentication.Standalone.Common.InitialTransaction;
using Checkout.Authentication.Standalone.Common.Installment;
using Checkout.Authentication.Standalone.Common.MerchantRiskInfo;
using Checkout.Authentication.Standalone.Common.Recurring;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.ChannelData.BrowserChannelData;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Completion.NonHostedCompletion;
using Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.Source.CardSource;
using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using RequestASessionRequestBody =
    Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.RequestASessionRequest;
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

namespace Checkout.Authentification.Standalone
{
    /// <summary>
    /// Full-property serialization coverage for the POST /sessions request body.
    /// Every one of the class's 24 properties is populated and asserted on the emitted JSON.
    /// </summary>
    public class RequestASessionRequestSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

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
    }
}
