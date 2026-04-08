using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;
using SetupAccommodationData = Checkout.Payments.Setups.Entities.AccommodationData;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsResponseSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldDeserializeFullResponse()
        {
            const string json = @"{
                ""id"": ""pay_abcdefghijklmnopqrstuvwxyz01"",
                ""processing_channel_id"": ""pc_abcdefghijklmnopqrstuvwxyz"",
                ""amount"": 25000,
                ""currency"": ""EUR"",
                ""payment_type"": ""Installment"",
                ""reference"": ""ORD-RESP-001"",
                ""description"": ""Response test"",
                ""billing"": {
                    ""address"": {
                        ""address_line1"": ""1 Berlin St"",
                        ""address_line2"": ""Apt 5"",
                        ""city"": ""Berlin"",
                        ""state"": ""Berlin"",
                        ""zip"": ""10115"",
                        ""country"": ""DE""
                    }
                },
                ""settings"": {
                    ""success_url"": ""https://example.com/ok"",
                    ""failure_url"": ""https://example.com/fail"",
                    ""capture"": true
                },
                ""customer"": {
                    ""name"": ""Max Mustermann"",
                    ""tax_number"": ""DE123456789"",
                    ""email"": { ""address"": ""max@example.de"", ""verified"": true },
                    ""phone"": { ""number"": ""491234567890"" },
                    ""billing_address"": { ""address_line1"": ""1 Berlin St"", ""country"": ""DE"" },
                    ""device"": { ""locale"": ""de-DE"" },
                    ""merchant_account"": {
                        ""id"": ""cust_resp_001"",
                        ""registration_date"": ""2021-03-15"",
                        ""last_modified"": ""2024-09-20"",
                        ""returning_customer"": true,
                        ""first_transaction_date"": ""2021-04-01"",
                        ""last_transaction_date"": ""2024-12-15"",
                        ""total_order_count"": 100,
                        ""last_payment_amount"": 250.50
                    }
                },
                ""order"": {
                    ""invoice_id"": ""INV-RESP-001"",
                    ""shipping_amount"": 1000,
                    ""discount_amount"": 500,
                    ""tax_amount"": 4750,
                    ""sub_merchants"": [
                        {
                            ""id"": ""sub_resp_001"",
                            ""product_category"": ""food"",
                            ""number_of_sales"": 300,
                            ""registration_date"": ""2020-01-01""
                        }
                    ]
                },
                ""industry"": {
                    ""accommodation_data"": {
                        ""name"": ""Alpine Lodge"",
                        ""booking_reference"": ""BK-RESP-001"",
                        ""check_in_date"": ""2025-12-20"",
                        ""check_out_date"": ""2025-12-27"",
                        ""number_of_rooms"": 3,
                        ""address"": {
                            ""address_line1"": ""5 Mountain Rd"",
                            ""city"": ""Innsbruck"",
                            ""state"": ""Tyrol"",
                            ""country"": ""AT"",
                            ""zip"": ""6020""
                        },
                        ""guests"": [
                            { ""first_name"": ""Max"", ""last_name"": ""Mustermann"", ""date_of_birth"": ""1988-11-05"" }
                        ],
                        ""room"": [
                            { ""rate"": 180.00, ""number_of_nights"": 7 }
                        ]
                    }
                },
                ""payment_methods"": {
                    ""klarna"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""flags"": [""express"", ""test""],
                        ""account_holder"": { ""billing_address"": { ""address_line1"": ""1 Klarna Weg"" } },
                        ""action"": { ""type"": ""sdk"", ""client_token"": ""tok_resp"", ""session_id"": ""sess_resp"" },
                        ""payment_method_options"": { ""sdk"": { ""id"": ""opt_resp_k"", ""status"": ""active"", ""flags"": [""f1""], ""action"": { ""type"": ""sdk"" } } }
                    },
                    ""paypal"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""user_action"": ""pay_now"",
                        ""brand_name"": ""Alpine Store"",
                        ""shipping_preference"": ""get_from_file"",
                        ""action"": { ""type"": ""redirect"", ""order_id"": ""PP-RESP-001"" }
                    },
                    ""tabby"": {
                        ""status"": ""unavailable"",
                        ""initialization"": ""disabled"",
                        ""payment_types"": [""pay_later""],
                        ""payment_method_options"": { ""installments"": { ""id"": ""opt_resp_t"" } }
                    }
                },
                ""available_payment_methods"": [""klarna"", ""paypal"", ""card""],
                ""_links"": {
                    ""self"": { ""href"": ""https://api.checkout.com/payments/setups/pay_abc"" }
                }
            }";

            var result = (PaymentSetupsResponse)_serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            // Top-level
            result.Id.ShouldBe("pay_abcdefghijklmnopqrstuvwxyz01");
            result.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            result.Amount.ShouldBe(25000L);
            result.Currency.ShouldBe(Currency.EUR);
            result.PaymentType.ShouldBe(PaymentType.Installment);
            result.Reference.ShouldBe("ORD-RESP-001");
            result.Description.ShouldBe("Response test");

            // Billing
            result.Billing.ShouldNotBeNull();
            result.Billing.Address.AddressLine1.ShouldBe("1 Berlin St");
            result.Billing.Address.AddressLine2.ShouldBe("Apt 5");
            result.Billing.Address.City.ShouldBe("Berlin");
            result.Billing.Address.State.ShouldBe("Berlin");
            result.Billing.Address.Zip.ShouldBe("10115");
            result.Billing.Address.Country.ShouldBe(CountryCode.DE);

            // Settings
            result.Settings.SuccessUrl.ShouldBe("https://example.com/ok");
            result.Settings.FailureUrl.ShouldBe("https://example.com/fail");
            result.Settings.Capture.ShouldBe(true);

            // Customer
            result.Customer.Name.ShouldBe("Max Mustermann");
            result.Customer.TaxNumber.ShouldBe("DE123456789");
            result.Customer.Email.Address.ShouldBe("max@example.de");
            result.Customer.Email.Verified.ShouldBe(true);
            result.Customer.Phone.Number.ShouldBe("491234567890");
            result.Customer.BillingAddress.AddressLine1.ShouldBe("1 Berlin St");
            result.Customer.BillingAddress.Country.ShouldBe(CountryCode.DE);
            result.Customer.Device.Locale.ShouldBe("de-DE");
            result.Customer.MerchantAccount.Id.ShouldBe("cust_resp_001");
            result.Customer.MerchantAccount.RegistrationDate.ShouldBe("2021-03-15");
            result.Customer.MerchantAccount.LastModified.ShouldBe("2024-09-20");
            result.Customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            result.Customer.MerchantAccount.FirstTransactionDate.ShouldBe("2021-04-01");
            result.Customer.MerchantAccount.LastTransactionDate.ShouldBe("2024-12-15");
            result.Customer.MerchantAccount.TotalOrderCount.ShouldBe(100);
            result.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(250.50m);

            // Order
            result.Order.InvoiceId.ShouldBe("INV-RESP-001");
            result.Order.ShippingAmount.ShouldBe(1000L);
            result.Order.DiscountAmount.ShouldBe(500L);
            result.Order.TaxAmount.ShouldBe(4750L);
            result.Order.SubMerchants[0].Id.ShouldBe("sub_resp_001");
            result.Order.SubMerchants[0].ProductCategory.ShouldBe("food");
            result.Order.SubMerchants[0].NumberOfSales.ShouldBe(300);
            result.Order.SubMerchants[0].RegistrationDate.ShouldBe("2020-01-01");

            // Industry
            result.Industry.AccommodationData.Name.ShouldBe("Alpine Lodge");
            result.Industry.AccommodationData.BookingReference.ShouldBe("BK-RESP-001");
            result.Industry.AccommodationData.CheckInDate.ShouldBe("2025-12-20");
            result.Industry.AccommodationData.CheckOutDate.ShouldBe("2025-12-27");
            result.Industry.AccommodationData.NumberOfRooms.ShouldBe(3);
            result.Industry.AccommodationData.Address.AddressLine1.ShouldBe("5 Mountain Rd");
            result.Industry.AccommodationData.Address.City.ShouldBe("Innsbruck");
            result.Industry.AccommodationData.Address.State.ShouldBe("Tyrol");
            result.Industry.AccommodationData.Address.Country.ShouldBe("AT");
            result.Industry.AccommodationData.Address.Zip.ShouldBe("6020");
            result.Industry.AccommodationData.Guests[0].FirstName.ShouldBe("Max");
            result.Industry.AccommodationData.Guests[0].LastName.ShouldBe("Mustermann");
            result.Industry.AccommodationData.Guests[0].DateOfBirth.ShouldBe("1988-11-05");
            result.Industry.AccommodationData.Room[0].Rate.ShouldBe(180.00m);
            result.Industry.AccommodationData.Room[0].NumberOfNights.ShouldBe(7);

            // PaymentMethods - Klarna
            result.PaymentMethods.Klarna.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Klarna.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            result.PaymentMethods.Klarna.Flags.Count.ShouldBe(2);
            result.PaymentMethods.Klarna.AccountHolder.BillingAddress.AddressLine1.ShouldBe("1 Klarna Weg");
            result.PaymentMethods.Klarna.Action.Type.ShouldBe("sdk");
            result.PaymentMethods.Klarna.Action.ClientToken.ShouldBe("tok_resp");
            result.PaymentMethods.Klarna.Action.SessionId.ShouldBe("sess_resp");
            result.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Id.ShouldBe("opt_resp_k");
            result.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Status.ShouldBe("active");
            result.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Flags.ShouldContain("f1");
            result.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Action.Type.ShouldBe("sdk");

            // PaymentMethods - Paypal
            result.PaymentMethods.Paypal.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            result.PaymentMethods.Paypal.UserAction.ShouldBe(PaypalUserAction.PayNow);
            result.PaymentMethods.Paypal.BrandName.ShouldBe("Alpine Store");
            result.PaymentMethods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.GetFromFile);
            result.PaymentMethods.Paypal.Action.Type.ShouldBe("redirect");
            result.PaymentMethods.Paypal.Action.OrderId.ShouldBe("PP-RESP-001");

            // PaymentMethods - Tabby
            result.PaymentMethods.Tabby.Status.ShouldBe(PaymentMethodStatus.Unavailable);
            result.PaymentMethods.Tabby.Initialization.ShouldBe(PaymentMethodInitialization.Disabled);
            result.PaymentMethods.Tabby.PaymentTypes.ShouldContain("pay_later");
            result.PaymentMethods.Tabby.PaymentMethodOptions.Installments.Id.ShouldBe("opt_resp_t");

            // AvailablePaymentMethods (response-only)
            result.AvailablePaymentMethods.ShouldNotBeNull();
            result.AvailablePaymentMethods.Count.ShouldBe(3);
            result.AvailablePaymentMethods.ShouldContain("klarna");
            result.AvailablePaymentMethods.ShouldContain("paypal");
            result.AvailablePaymentMethods.ShouldContain("card");

            // Links (inherited from Resource)
            result.Links.ShouldNotBeNull();
            result.GetSelfLink().ShouldNotBeNull();
            result.GetSelfLink().Href.ShouldBe("https://api.checkout.com/payments/setups/pay_abc");
        }

        [Fact]
        public void ShouldRoundTripSerializeResponse()
        {
            var original = new PaymentSetupsResponse
            {
                Id = "pay_roundtrip_test_12345678901",
                ProcessingChannelId = "pc_abcdefghijklmnopqrstuvwxyz",
                Amount = 7500,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Moto,
                Reference = "ROUND-001",
                Description = "Roundtrip test",
                Billing = new Billing
                {
                    Address = new Address
                    {
                        AddressLine1 = "10 Roundtrip Ln",
                        City = "London",
                        Country = CountryCode.GB
                    }
                },
                Settings = new Settings
                {
                    SuccessUrl = "https://example.com/s",
                    FailureUrl = "https://example.com/f",
                    Capture = false
                },
                Customer = new Customer
                {
                    Name = "Round Trip",
                    TaxNumber = "RT-TAX",
                    Email = new CustomerEmail { Address = "rt@example.com", Verified = false },
                    BillingAddress = new Address { AddressLine1 = "10 Roundtrip Ln", Country = CountryCode.GB },
                    Device = new CustomerDevice { Locale = "en-GB" },
                    MerchantAccount = new MerchantAccount
                    {
                        Id = "cust_rt",
                        RegistrationDate = "2024-01-01",
                        LastModified = "2024-06-01",
                        ReturningCustomer = true,
                        FirstTransactionDate = "2024-01-15",
                        LastTransactionDate = "2024-06-15",
                        TotalOrderCount = 10,
                        LastPaymentAmount = 75.00m
                    }
                },
                Order = new Order
                {
                    InvoiceId = "INV-RT",
                    ShippingAmount = 250,
                    DiscountAmount = 0,
                    TaxAmount = 1250
                },
                Industry = new Industry
                {
                    AccommodationData = new SetupAccommodationData
                    {
                        Name = "Roundtrip Hotel",
                        BookingReference = "BK-RT",
                        CheckInDate = "2025-08-01",
                        CheckOutDate = "2025-08-03",
                        NumberOfRooms = 1,
                        Address = new AccommodationAddress
                        {
                            AddressLine1 = "1 RT Rd",
                            City = "London",
                            State = "London",
                            Country = "GB",
                            Zip = "SW1A 1AA"
                        },
                        Guests = new List<AccommodationGuest>
                        {
                            new AccommodationGuest { FirstName = "Round", LastName = "Trip", DateOfBirth = "1992-01-01" }
                        },
                        Room = new List<AccommodationRoom>
                        {
                            new AccommodationRoom { Rate = 99.99m, NumberOfNights = 2 }
                        }
                    }
                },
                PaymentMethods = new Checkout.Payments.Setups.Entities.PaymentMethods
                {
                    Paypal = new Paypal
                    {
                        Status = PaymentMethodStatus.Available,
                        Initialization = PaymentMethodInitialization.Enabled,
                        UserAction = PaypalUserAction.Continue,
                        BrandName = "RT Store",
                        ShippingPreference = PaypalShippingPreference.NoShipping,
                        Action = new PaymentMethodAction
                        {
                            Type = "redirect",
                            OrderId = "PP-RT-001"
                        }
                    }
                },
                AvailablePaymentMethods = new List<string> { "paypal", "card" }
            };

            var json = _serializer.Serialize(original);
            var deserialized = (PaymentSetupsResponse)_serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            deserialized.Id.ShouldBe("pay_roundtrip_test_12345678901");
            deserialized.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            deserialized.Amount.ShouldBe(7500L);
            deserialized.Currency.ShouldBe(Currency.GBP);
            deserialized.PaymentType.ShouldBe(PaymentType.Moto);
            deserialized.Reference.ShouldBe("ROUND-001");
            deserialized.Description.ShouldBe("Roundtrip test");

            // Billing
            deserialized.Billing.Address.AddressLine1.ShouldBe("10 Roundtrip Ln");
            deserialized.Billing.Address.City.ShouldBe("London");
            deserialized.Billing.Address.Country.ShouldBe(CountryCode.GB);

            // Settings
            deserialized.Settings.SuccessUrl.ShouldBe("https://example.com/s");
            deserialized.Settings.FailureUrl.ShouldBe("https://example.com/f");
            deserialized.Settings.Capture.ShouldBe(false);

            // Customer
            deserialized.Customer.Name.ShouldBe("Round Trip");
            deserialized.Customer.TaxNumber.ShouldBe("RT-TAX");
            deserialized.Customer.Email.Address.ShouldBe("rt@example.com");
            deserialized.Customer.Email.Verified.ShouldBe(false);
            deserialized.Customer.BillingAddress.AddressLine1.ShouldBe("10 Roundtrip Ln");
            deserialized.Customer.Device.Locale.ShouldBe("en-GB");
            deserialized.Customer.MerchantAccount.Id.ShouldBe("cust_rt");
            deserialized.Customer.MerchantAccount.RegistrationDate.ShouldBe("2024-01-01");
            deserialized.Customer.MerchantAccount.LastModified.ShouldBe("2024-06-01");
            deserialized.Customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            deserialized.Customer.MerchantAccount.FirstTransactionDate.ShouldBe("2024-01-15");
            deserialized.Customer.MerchantAccount.LastTransactionDate.ShouldBe("2024-06-15");
            deserialized.Customer.MerchantAccount.TotalOrderCount.ShouldBe(10);
            deserialized.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(75.00m);

            // Order
            deserialized.Order.InvoiceId.ShouldBe("INV-RT");
            deserialized.Order.ShippingAmount.ShouldBe(250L);
            deserialized.Order.DiscountAmount.ShouldBe(0L);
            deserialized.Order.TaxAmount.ShouldBe(1250L);

            // Industry
            deserialized.Industry.AccommodationData.Name.ShouldBe("Roundtrip Hotel");
            deserialized.Industry.AccommodationData.BookingReference.ShouldBe("BK-RT");
            deserialized.Industry.AccommodationData.CheckInDate.ShouldBe("2025-08-01");
            deserialized.Industry.AccommodationData.CheckOutDate.ShouldBe("2025-08-03");
            deserialized.Industry.AccommodationData.NumberOfRooms.ShouldBe(1);
            deserialized.Industry.AccommodationData.Address.AddressLine1.ShouldBe("1 RT Rd");
            deserialized.Industry.AccommodationData.Address.City.ShouldBe("London");
            deserialized.Industry.AccommodationData.Address.State.ShouldBe("London");
            deserialized.Industry.AccommodationData.Address.Country.ShouldBe("GB");
            deserialized.Industry.AccommodationData.Address.Zip.ShouldBe("SW1A 1AA");
            deserialized.Industry.AccommodationData.Guests[0].FirstName.ShouldBe("Round");
            deserialized.Industry.AccommodationData.Guests[0].LastName.ShouldBe("Trip");
            deserialized.Industry.AccommodationData.Guests[0].DateOfBirth.ShouldBe("1992-01-01");
            deserialized.Industry.AccommodationData.Room[0].Rate.ShouldBe(99.99m);
            deserialized.Industry.AccommodationData.Room[0].NumberOfNights.ShouldBe(2);

            // Paypal
            deserialized.PaymentMethods.Paypal.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            deserialized.PaymentMethods.Paypal.UserAction.ShouldBe(PaypalUserAction.Continue);
            deserialized.PaymentMethods.Paypal.BrandName.ShouldBe("RT Store");
            deserialized.PaymentMethods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.NoShipping);
            deserialized.PaymentMethods.Paypal.Action.Type.ShouldBe("redirect");
            deserialized.PaymentMethods.Paypal.Action.OrderId.ShouldBe("PP-RT-001");

            // AvailablePaymentMethods
            deserialized.AvailablePaymentMethods.Count.ShouldBe(2);
            deserialized.AvailablePaymentMethods.ShouldContain("paypal");
            deserialized.AvailablePaymentMethods.ShouldContain("card");
        }
    }
}
