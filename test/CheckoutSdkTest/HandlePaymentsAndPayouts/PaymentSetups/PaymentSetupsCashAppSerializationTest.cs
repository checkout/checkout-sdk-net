using Checkout.Common;
using Checkout.Payments.Setups.Entities;
using Checkout.Payments.Setups.Requests;
using Checkout.Payments.Setups.Responses;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;
using SetupPaymentMethods = Checkout.Payments.Setups.Entities.PaymentMethods;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    /// <summary>
    /// Schema validation tests for the Cash App Pay payment method and the customer device fields on
    /// Payment Setups. A sub-domain file: PaymentSetupsRequestSerializationTest is past the soft cap.
    /// </summary>
    public class PaymentSetupsCashAppSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        private const string RedirectUrl =
            "https://sandbox.api.cash.app/customer-request/v1/requests/GRR_f5xg6wrxhtv3p4w24g0wrexa/interstitial?validity_token=bap03y";

        private const string CashAppResponseJson = @"{
            ""status"": ""action_required"",
            ""flags"": [],
            ""initialization"": ""enabled"",
            ""customer_profile_sharing"": true,
            ""reference"": ""ORDER-99"",
            ""action"": {
                ""type"": ""redirect"",
                ""redirect_url"": """ + RedirectUrl + @"""
            },
            ""customer_profile"": {
                ""customer_id"": ""CST_AYVkuLzfsRqEhf4OyQFxQNv22m7IjNFjO6f2J5CDE2nxAC4-21wJ2H8_2kvsdIsDZMN4"",
                ""cashtag"": ""$CASHTAG_C_TOKEN"",
                ""reference_id"": ""value"",
                ""full_name"": ""John Middle Doe"",
                ""given_name"": ""John"",
                ""middle_name"": ""Middle"",
                ""family_name"": ""Doe"",
                ""suffix"": ""Jr."",
                ""birth_date"": ""1990-01-01T00:00:00.0000000"",
                ""address"": {
                    ""address_line_1"": ""123 Main St"",
                    ""address_line_2"": ""Apt 2"",
                    ""address_line_3"": ""Floor 3"",
                    ""locality"": ""Springfield"",
                    ""sublocality"": ""Downtown"",
                    ""administrative_district_level_1"": ""IL"",
                    ""postal_code"": ""62701"",
                    ""country"": ""US""
                },
                ""phone_number"": ""5555555555"",
                ""email_address"": ""cash@cash.com"",
                ""customer_since"": ""1970-01-18T12:46:04.8000000+00:00""
            }
        }";

        private static PaymentSetupsRequest CreateCashAppRequest(CustomerDeviceClient client, CustomerDeviceOs os)
        {
            return new PaymentSetupsRequest
            {
                PaymentMethods = new SetupPaymentMethods
                {
                    CashApp = new CashApp
                    {
                        Initialization = PaymentMethodInitialization.Enabled,
                        CustomerProfileSharing = true
                    }
                },
                Customer = new Customer
                {
                    Device = new CustomerDevice
                    {
                        Locale = "en_GB",
                        Fingerprint = "fp_abc123xyz",
                        Ipv4 = "203.0.113.0",
                        Ipv6 = "2001:db8:85a3::8a2e:370:7334",
                        Client = client,
                        Os = os
                    }
                }
            };
        }

        private static string DeviceField(CustomerDeviceClient client, CustomerDeviceOs os, string field)
        {
            var json = JObject.Parse(Serializer.Serialize(CreateCashAppRequest(client, os)));
            return (string)json["customer"]["device"][field];
        }

        // ------------------------------------------------------------------------
        // CashApp (request side)
        // payment_methods.cashapp: the wire key is one lowercase word.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeCashAppUnderSingleWordKey()
        {
            var json = Serializer.Serialize(CreateCashAppRequest(CustomerDeviceClient.MobileWeb, CustomerDeviceOs.Android));
            var methods = (JObject)JObject.Parse(json)["payment_methods"];

            methods.ContainsKey("cashapp").ShouldBeTrue();
            // Ordinal checks: Shouldly's ShouldNotContain ignores case and would match "cashapp".
            json.Contains("cash_app").ShouldBeFalse();
            json.Contains("cashApp").ShouldBeFalse();
            var cashapp = (JObject)methods["cashapp"];
            ((string)cashapp["initialization"]).ShouldBe("enabled");
            ((bool)cashapp["customer_profile_sharing"]).ShouldBeTrue();
            cashapp.ContainsKey("customerProfileSharing").ShouldBeFalse();
        }

        // ------------------------------------------------------------------------
        // CustomerDevice
        // customer.device: locale plus fingerprint, ipv4, ipv6, client and os.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeCustomerDeviceWireKeysAndEnumValues()
        {
            var device = (JObject)JObject.Parse(Serializer.Serialize(
                CreateCashAppRequest(CustomerDeviceClient.MobileWeb, CustomerDeviceOs.Android)))["customer"]["device"];

            device.Count.ShouldBe(6);
            ((string)device["locale"]).ShouldBe("en_GB");
            ((string)device["fingerprint"]).ShouldBe("fp_abc123xyz");
            ((string)device["ipv4"]).ShouldBe("203.0.113.0");
            ((string)device["ipv6"]).ShouldBe("2001:db8:85a3::8a2e:370:7334");
            ((string)device["client"]).ShouldBe("mobile_web");
            ((string)device["os"]).ShouldBe("android");
        }

        [Fact]
        public void ShouldSerializeEveryDeviceClientAndOsValueExactly()
        {
            DeviceField(CustomerDeviceClient.Web, CustomerDeviceOs.Ios, "client").ShouldBe("web");
            DeviceField(CustomerDeviceClient.MobileWeb, CustomerDeviceOs.Ios, "client").ShouldBe("mobile_web");
            DeviceField(CustomerDeviceClient.App, CustomerDeviceOs.Ios, "client").ShouldBe("app");
            DeviceField(CustomerDeviceClient.Web, CustomerDeviceOs.Android, "os").ShouldBe("android");
            DeviceField(CustomerDeviceClient.Web, CustomerDeviceOs.Ios, "os").ShouldBe("ios");
        }

        [Fact]
        public void ShouldRoundTripCustomerDeviceEnums()
        {
            foreach (CustomerDeviceClient client in System.Enum.GetValues(typeof(CustomerDeviceClient)))
            {
                foreach (CustomerDeviceOs os in System.Enum.GetValues(typeof(CustomerDeviceOs)))
                {
                    var original = new CustomerDevice
                    {
                        Fingerprint = "fp", Ipv4 = "1.2.3.4", Ipv6 = "::1", Client = client, Os = os
                    };
                    var copy = (CustomerDevice)Serializer.Deserialize(Serializer.Serialize(original), typeof(CustomerDevice));

                    copy.Fingerprint.ShouldBe("fp");
                    copy.Ipv4.ShouldBe("1.2.3.4");
                    copy.Ipv6.ShouldBe("::1");
                    copy.Client.ShouldBe(client);
                    copy.Os.ShouldBe(os);
                }
            }
        }

        [Fact]
        public void ShouldOmitNewDeviceFieldsWhenNotSet()
        {
            Serializer.Serialize(new CustomerDevice { Locale = "en_GB" }).ShouldBe("{\"locale\":\"en_GB\"}");
        }

        // ------------------------------------------------------------------------
        // CashApp (response side)
        // action.redirect_url, reference and the once-only customer_profile, from the
        // swagger examples.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeFullCashAppResponse()
        {
            var json = @"{ ""id"": ""set_cashapp"",
                ""customer"": { ""device"": { ""client"": ""app"", ""os"": ""ios"", ""ipv4"": ""203.0.113.0"",
                    ""ipv6"": ""2001:db8::1"", ""fingerprint"": ""fp_1"", ""locale"": ""en_GB"" } },
                ""payment_methods"": { ""cashapp"": " + CashAppResponseJson + " } }";

            var response = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            response.Customer.Device.Client.ShouldBe(CustomerDeviceClient.App);
            response.Customer.Device.Os.ShouldBe(CustomerDeviceOs.Ios);
            response.Customer.Device.Ipv4.ShouldBe("203.0.113.0");
            response.Customer.Device.Ipv6.ShouldBe("2001:db8::1");
            response.Customer.Device.Fingerprint.ShouldBe("fp_1");

            var cashApp = response.PaymentMethods.CashApp;
            cashApp.ShouldNotBeNull();
            cashApp.Status.ShouldBe(PaymentMethodStatus.ActionRequired);
            cashApp.Flags.ShouldBeEmpty();
            cashApp.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            cashApp.CustomerProfileSharing.ShouldBe(true);
            cashApp.Reference.ShouldBe("ORDER-99");
            cashApp.Action.Type.ShouldBe(CashAppActionType.Redirect);
            cashApp.Action.RedirectUrl.ShouldBe(RedirectUrl);

            var profile = cashApp.CustomerProfile;
            profile.CustomerId.ShouldBe("CST_AYVkuLzfsRqEhf4OyQFxQNv22m7IjNFjO6f2J5CDE2nxAC4-21wJ2H8_2kvsdIsDZMN4");
            profile.Cashtag.ShouldBe("$CASHTAG_C_TOKEN");
            profile.ReferenceId.ShouldBe("value");
            profile.FullName.ShouldBe("John Middle Doe");
            profile.GivenName.ShouldBe("John");
            profile.MiddleName.ShouldBe("Middle");
            profile.FamilyName.ShouldBe("Doe");
            profile.Suffix.ShouldBe("Jr.");
            profile.BirthDate.ShouldBe("1990-01-01T00:00:00.0000000");
            profile.PhoneNumber.ShouldBe("5555555555");
            profile.EmailAddress.ShouldBe("cash@cash.com");
            profile.CustomerSince.ShouldBe("1970-01-18T12:46:04.8000000+00:00");
            profile.Address.AddressLine1.ShouldBe("123 Main St");
            profile.Address.AddressLine2.ShouldBe("Apt 2");
            profile.Address.AddressLine3.ShouldBe("Floor 3");
            profile.Address.Locality.ShouldBe("Springfield");
            profile.Address.Sublocality.ShouldBe("Downtown");
            profile.Address.AdministrativeDistrictLevel1.ShouldBe("IL");
            profile.Address.PostalCode.ShouldBe("62701");
            profile.Address.Country.ShouldBe(CountryCode.US);
        }

        [Fact]
        public void ShouldDeserializeCashAppWithoutProfileOnSubsequentResponses()
        {
            var cashApp = (CashApp)Serializer.Deserialize(
                @"{ ""status"": ""ready"", ""initialization"": ""enabled"", ""customer_profile_sharing"": true, ""reference"": ""ORDER-99"" }",
                typeof(CashApp));

            cashApp.Status.ShouldBe(PaymentMethodStatus.Ready);
            cashApp.CustomerProfile.ShouldBeNull();
            cashApp.Action.ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripCashAppWithEveryProperty()
        {
            var original = (CashApp)Serializer.Deserialize(CashAppResponseJson, typeof(CashApp));

            var json = Serializer.Serialize(original);
            var cashapp = JObject.Parse(json);
            var profile = (JObject)cashapp["customer_profile"];
            var address = (JObject)profile["address"];

            cashapp.Count.ShouldBe(7);
            ((string)cashapp["action"]["type"]).ShouldBe("redirect");
            ((string)cashapp["action"]["redirect_url"]).ShouldBe(RedirectUrl);
            profile.Count.ShouldBe(13);
            profile.ContainsKey("customer_id").ShouldBeTrue();
            profile.ContainsKey("reference_id").ShouldBeTrue();
            profile.ContainsKey("customer_since").ShouldBeTrue();
            address.Count.ShouldBe(8);
            ((string)address["address_line_1"]).ShouldBe("123 Main St");
            ((string)address["address_line_2"]).ShouldBe("Apt 2");
            ((string)address["address_line_3"]).ShouldBe("Floor 3");
            ((string)address["administrative_district_level_1"]).ShouldBe("IL");
            address.ContainsKey("address_line1").ShouldBeFalse();
            address.ContainsKey("administrative_district_level1").ShouldBeFalse();

            var copy = (CashApp)Serializer.Deserialize(json, typeof(CashApp));
            copy.Status.ShouldBe(original.Status);
            copy.Flags.ShouldBe(original.Flags);
            copy.Initialization.ShouldBe(original.Initialization);
            copy.CustomerProfileSharing.ShouldBe(original.CustomerProfileSharing);
            copy.Reference.ShouldBe(original.Reference);
            copy.Action.Type.ShouldBe(original.Action.Type);
            copy.Action.RedirectUrl.ShouldBe(original.Action.RedirectUrl);

            var copyProfile = copy.CustomerProfile;
            var originalProfile = original.CustomerProfile;
            copyProfile.CustomerId.ShouldBe(originalProfile.CustomerId);
            copyProfile.Cashtag.ShouldBe(originalProfile.Cashtag);
            copyProfile.ReferenceId.ShouldBe(originalProfile.ReferenceId);
            copyProfile.FullName.ShouldBe(originalProfile.FullName);
            copyProfile.GivenName.ShouldBe(originalProfile.GivenName);
            copyProfile.MiddleName.ShouldBe(originalProfile.MiddleName);
            copyProfile.FamilyName.ShouldBe(originalProfile.FamilyName);
            copyProfile.Suffix.ShouldBe(originalProfile.Suffix);
            copyProfile.BirthDate.ShouldBe(originalProfile.BirthDate);
            copyProfile.PhoneNumber.ShouldBe(originalProfile.PhoneNumber);
            copyProfile.EmailAddress.ShouldBe(originalProfile.EmailAddress);
            copyProfile.CustomerSince.ShouldBe(originalProfile.CustomerSince);

            var copyAddress = copyProfile.Address;
            var originalAddress = originalProfile.Address;
            copyAddress.AddressLine1.ShouldBe(originalAddress.AddressLine1);
            copyAddress.AddressLine2.ShouldBe(originalAddress.AddressLine2);
            copyAddress.AddressLine3.ShouldBe(originalAddress.AddressLine3);
            copyAddress.Locality.ShouldBe(originalAddress.Locality);
            copyAddress.Sublocality.ShouldBe(originalAddress.Sublocality);
            copyAddress.AdministrativeDistrictLevel1.ShouldBe(originalAddress.AdministrativeDistrictLevel1);
            copyAddress.PostalCode.ShouldBe(originalAddress.PostalCode);
            copyAddress.Country.ShouldBe(originalAddress.Country);
        }

        [Fact]
        public void ShouldDeserializeCashAppInConfirmResponse()
        {
            var json = @"{ ""id"": ""set_cashapp"", ""payment_methods"": { ""cashapp"": " + CashAppResponseJson + " } }";

            var response = (PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse));
            var cashApp = response.PaymentMethods.CashApp;

            cashApp.Status.ShouldBe(PaymentMethodStatus.ActionRequired);
            cashApp.Reference.ShouldBe("ORDER-99");
            cashApp.Action.Type.ShouldBe(CashAppActionType.Redirect);
            cashApp.Action.RedirectUrl.ShouldBe(RedirectUrl);
        }

        // ------------------------------------------------------------------------
        // PaymentMethodStatus
        // Every status value the spec defines must read as a member, not as null.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData("unavailable", PaymentMethodStatus.Unavailable)]
        [InlineData("action_required", PaymentMethodStatus.ActionRequired)]
        [InlineData("ready", PaymentMethodStatus.Ready)]
        [InlineData("initialization_required", PaymentMethodStatus.InitializationRequired)]
        [InlineData("invalid", PaymentMethodStatus.Invalid)]
        public void ShouldDeserializeEverySpecPaymentMethodStatus(string wireValue, PaymentMethodStatus expected)
        {
            var cashApp = (CashApp)Serializer.Deserialize("{\"status\":\"" + wireValue + "\"}", typeof(CashApp));

            cashApp.Status.ShouldBe(expected);
        }

        [Theory]
        [InlineData(PaymentMethodStatus.ActionRequired, "action_required")]
        [InlineData(PaymentMethodStatus.Ready, "ready")]
        [InlineData(PaymentMethodStatus.InitializationRequired, "initialization_required")]
        [InlineData(PaymentMethodStatus.Invalid, "invalid")]
        public void ShouldSerializeNewPaymentMethodStatusValues(PaymentMethodStatus status, string wireValue)
        {
            ((string)JObject.Parse(Serializer.Serialize(new CashApp { Status = status }))["status"]).ShouldBe(wireValue);
        }
    }
}
