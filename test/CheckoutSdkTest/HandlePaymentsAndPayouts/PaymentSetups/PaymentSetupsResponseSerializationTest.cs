// Customer.BillingAddress (outside the spec customer) and the legacy PaymentMethodOptions are obsolete but kept covered until they are removed.
#pragma warning disable CS0618
using Checkout.Common;
using Checkout.Payments.Setups.Entities;
using Checkout.Payments.Setups.Requests;
using Checkout.Payments.Setups.Responses;
using Checkout.Payments.Setups;
using Checkout.Payments;
using SetupAccommodationData = Checkout.Payments.Setups.Entities.AccommodationData;
using Shouldly;
using System.Collections.Generic;
using System;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    /// <summary>
    /// Schema validation tests for Checkout.HandlePaymentsAndPayouts.PaymentSetups.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class PaymentSetupsResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // PaymentSetupsResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeFullResponse()
        {
            const string json = @"{
                ""id"": ""pay_abcdefghijklmnopqrstuvwxyz01"",
                ""processing_channel_id"": ""pc_abcdefghijklmnopqrstuvwxyz"",
                ""amount"": 25000,
                ""currency"": ""EUR"",
                ""payment_type"": ""installment"",
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
                    ""tipping_amount"": 300,
                    ""surcharge_amount"": 150,
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
                    ""accommodation"": [{
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
                    }]
                },
                ""payment_methods"": {
                    ""klarna"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""flags"": [""express"", ""test""],
                        ""account_holder"": { ""name"": ""Hans Klarna"" },
                        ""action"": { ""type"": ""sdk"", ""client_token"": ""tok_resp"", ""session_id"": ""sess_resp"" },
                        ""iban"": ""DE89370400440532013000"",
                        ""payment_method_options"": { ""sdk"": { ""id"": ""opt_resp_k"", ""status"": ""active"", ""flags"": [""f1""], ""action"": { ""type"": ""sdk"" } } }
                    },
                    ""paypal"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""user_action"": ""pay_now"",
                        ""brand_name"": ""Alpine Store"",
                        ""shipping_preference"": ""get_from_file"",
                        ""action"": { ""type"": ""sdk"", ""order_id"": ""PP-RESP-001"" }
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

            var result = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            // Top-level
            result.Id.ShouldBe("pay_abcdefghijklmnopqrstuvwxyz01");
            result.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            result.Amount.ShouldBe(25000L);
            result.Currency.ShouldBe(Currency.EUR);
            result.PaymentType.ShouldBe(PaymentSetupPaymentType.Installment);
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
            result.Customer.MerchantAccount.RegistrationDate.ShouldBe(new DateTime(2021, 3, 15));
            result.Customer.MerchantAccount.LastModified.ShouldBe(new DateTime(2024, 9, 20));
            result.Customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            result.Customer.MerchantAccount.FirstTransactionDate.ShouldBe(new DateTime(2021, 4, 1));
            result.Customer.MerchantAccount.LastTransactionDate.ShouldBe(new DateTime(2024, 12, 15));
            result.Customer.MerchantAccount.TotalOrderCount.ShouldBe(100);
            result.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(250.50m);

            // Order
            result.Order.InvoiceId.ShouldBe("INV-RESP-001");
            result.Order.ShippingAmount.ShouldBe(1000L);
            result.Order.DiscountAmount.ShouldBe(500L);
            result.Order.TaxAmount.ShouldBe(4750L);
            result.Order.TippingAmount.ShouldBe(300L);
            result.Order.SurchargeAmount.ShouldBe(150L);
            result.Order.SubMerchants[0].Id.ShouldBe("sub_resp_001");
            result.Order.SubMerchants[0].ProductCategory.ShouldBe("food");
            result.Order.SubMerchants[0].NumberOfSales.ShouldBe(300);
            result.Order.SubMerchants[0].RegistrationDate.ShouldBe(new DateTime(2020, 1, 1));

            // Industry
            result.Industry.Accommodation[0].Name.ShouldBe("Alpine Lodge");
            result.Industry.Accommodation[0].BookingReference.ShouldBe("BK-RESP-001");
            result.Industry.Accommodation[0].CheckInDate.ShouldBe("2025-12-20");
            result.Industry.Accommodation[0].CheckOutDate.ShouldBe("2025-12-27");
            result.Industry.Accommodation[0].NumberOfRooms.ShouldBe(3);
            result.Industry.Accommodation[0].Address.AddressLine1.ShouldBe("5 Mountain Rd");
            result.Industry.Accommodation[0].Address.City.ShouldBe("Innsbruck");
            result.Industry.Accommodation[0].Address.State.ShouldBe("Tyrol");
            result.Industry.Accommodation[0].Address.Country.ShouldBe(CountryCode.AT);
            result.Industry.Accommodation[0].Address.Zip.ShouldBe("6020");
            result.Industry.Accommodation[0].Guests[0].FirstName.ShouldBe("Max");
            result.Industry.Accommodation[0].Guests[0].LastName.ShouldBe("Mustermann");
            result.Industry.Accommodation[0].Guests[0].DateOfBirth.ShouldBe("1988-11-05");
            result.Industry.Accommodation[0].Room[0].Rate.ShouldBe(180.00m);
            result.Industry.Accommodation[0].Room[0].NumberOfNights.ShouldBe(7);

            // PaymentMethods - Klarna
            result.PaymentMethods.Klarna.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Klarna.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            result.PaymentMethods.Klarna.Flags.Count.ShouldBe(2);
            result.PaymentMethods.Klarna.AccountHolder.Name.ShouldBe("Hans Klarna");
            result.PaymentMethods.Klarna.Action.Type.ShouldBe("sdk");
            result.PaymentMethods.Klarna.Action.ClientToken.ShouldBe("tok_resp");
            result.PaymentMethods.Klarna.Action.SessionId.ShouldBe("sess_resp");
            result.PaymentMethods.Klarna.Iban.ShouldBe("DE89370400440532013000");
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
            result.PaymentMethods.Paypal.Action.Type.ShouldBe("sdk");
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
                PaymentType = PaymentSetupPaymentType.Moto,
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
                        RegistrationDate = new DateTime(2024, 1, 1),
                        LastModified = new DateTime(2024, 6, 1),
                        ReturningCustomer = true,
                        FirstTransactionDate = new DateTime(2024, 1, 15),
                        LastTransactionDate = new DateTime(2024, 6, 15),
                        TotalOrderCount = 10,
                        LastPaymentAmount = 75.00m
                    }
                },
                Order = new Order
                {
                    InvoiceId = "INV-RT",
                    ShippingAmount = 250,
                    DiscountAmount = 0,
                    TaxAmount = 1250,
                    TippingAmount = 100,
                    SurchargeAmount = 50
                },
                Industry = new Industry
                {
                    Accommodation = new List<SetupAccommodationData>
                    {
                        new SetupAccommodationData
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
                                Country = CountryCode.GB,
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
                            Type = "sdk",
                            OrderId = "PP-RT-001"
                        }
                    }
                },
                AvailablePaymentMethods = new List<string> { "paypal", "card" }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            deserialized.Id.ShouldBe("pay_roundtrip_test_12345678901");
            deserialized.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            deserialized.Amount.ShouldBe(7500L);
            deserialized.Currency.ShouldBe(Currency.GBP);
            deserialized.PaymentType.ShouldBe(PaymentSetupPaymentType.Moto);
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
            deserialized.Customer.MerchantAccount.RegistrationDate.ShouldBe(new DateTime(2024, 1, 1));
            deserialized.Customer.MerchantAccount.LastModified.ShouldBe(new DateTime(2024, 6, 1));
            deserialized.Customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            deserialized.Customer.MerchantAccount.FirstTransactionDate.ShouldBe(new DateTime(2024, 1, 15));
            deserialized.Customer.MerchantAccount.LastTransactionDate.ShouldBe(new DateTime(2024, 6, 15));
            deserialized.Customer.MerchantAccount.TotalOrderCount.ShouldBe(10);
            deserialized.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(75.00m);

            // Order
            deserialized.Order.InvoiceId.ShouldBe("INV-RT");
            deserialized.Order.ShippingAmount.ShouldBe(250L);
            deserialized.Order.DiscountAmount.ShouldBe(0L);
            deserialized.Order.TaxAmount.ShouldBe(1250L);
            deserialized.Order.TippingAmount.ShouldBe(100L);
            deserialized.Order.SurchargeAmount.ShouldBe(50L);

            // Industry
            deserialized.Industry.Accommodation[0].Name.ShouldBe("Roundtrip Hotel");
            deserialized.Industry.Accommodation[0].BookingReference.ShouldBe("BK-RT");
            deserialized.Industry.Accommodation[0].CheckInDate.ShouldBe("2025-08-01");
            deserialized.Industry.Accommodation[0].CheckOutDate.ShouldBe("2025-08-03");
            deserialized.Industry.Accommodation[0].NumberOfRooms.ShouldBe(1);
            deserialized.Industry.Accommodation[0].Address.AddressLine1.ShouldBe("1 RT Rd");
            deserialized.Industry.Accommodation[0].Address.City.ShouldBe("London");
            deserialized.Industry.Accommodation[0].Address.State.ShouldBe("London");
            deserialized.Industry.Accommodation[0].Address.Country.ShouldBe(CountryCode.GB);
            deserialized.Industry.Accommodation[0].Address.Zip.ShouldBe("SW1A 1AA");
            deserialized.Industry.Accommodation[0].Guests[0].FirstName.ShouldBe("Round");
            deserialized.Industry.Accommodation[0].Guests[0].LastName.ShouldBe("Trip");
            deserialized.Industry.Accommodation[0].Guests[0].DateOfBirth.ShouldBe("1992-01-01");
            deserialized.Industry.Accommodation[0].Room[0].Rate.ShouldBe(99.99m);
            deserialized.Industry.Accommodation[0].Room[0].NumberOfNights.ShouldBe(2);

            // Paypal
            deserialized.PaymentMethods.Paypal.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            deserialized.PaymentMethods.Paypal.UserAction.ShouldBe(PaypalUserAction.Continue);
            deserialized.PaymentMethods.Paypal.BrandName.ShouldBe("RT Store");
            deserialized.PaymentMethods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.NoShipping);
            deserialized.PaymentMethods.Paypal.Action.Type.ShouldBe("sdk");
            deserialized.PaymentMethods.Paypal.Action.OrderId.ShouldBe("PP-RT-001");

            // AvailablePaymentMethods
            deserialized.AvailablePaymentMethods.Count.ShouldBe(2);
            deserialized.AvailablePaymentMethods.ShouldContain("paypal");
            deserialized.AvailablePaymentMethods.ShouldContain("card");
        }

        // ------------------------------------------------------------------------
        // Flattened payment methods: Klarna, PayPal, Tabby and stc pay
        // Each method carries its own read-only action (PaymentMethodAction). Klarna and PayPal return
        // type "sdk", stc pay returns type "otp". Klarna also returns iban. Values are the spec examples.
        // ------------------------------------------------------------------------

        private const string FlattenedPaymentMethodsResponseJson = @"{
            ""id"": ""psu_y3oqhf46pyzuxjbcn2giaqnb44"",
            ""processing_channel_id"": ""pc_q4dbxom5jbgudnjzjpz7j2z6uq"",
            ""amount"": 10000,
            ""currency"": ""GBP"",
            ""payment_methods"": {
                ""klarna"": {
                    ""status"": ""ready"",
                    ""initialization"": ""enabled"",
                    ""action"": {
                        ""type"": ""sdk"",
                        ""client_token"": ""eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ewogICJzZXNzaW9uX2lkIiA6ICIw"",
                        ""session_id"": ""0b1d9815-165e-42e2-8867-35bc03789e00""
                    },
                    ""account_holder"": { ""name"": ""John Smith"" },
                    ""iban"": ""DE89370400440532013000""
                },
                ""paypal"": {
                    ""status"": ""ready"",
                    ""initialization"": ""enabled"",
                    ""user_action"": ""pay_now"",
                    ""brand_name"": ""Acme Corporation"",
                    ""shipping_preference"": ""no_shipping"",
                    ""action"": {
                        ""type"": ""sdk"",
                        ""order_id"": ""dd4b1a85-31de-404b-99f4-f143f8ba35ba""
                    }
                },
                ""tabby"": {
                    ""status"": ""ready"",
                    ""initialization"": ""enabled"",
                    ""payment_types"": [""pay_later""]
                },
                ""stcpay"": {
                    ""status"": ""action_required"",
                    ""initialization"": ""enabled"",
                    ""action"": { ""type"": ""otp"" }
                }
            }
        }";

        private static void AssertFlattenedPaymentMethods(Checkout.Payments.Setups.Entities.PaymentMethods methods)
        {
            methods.ShouldNotBeNull();

            methods.Klarna.ShouldNotBeNull();
            methods.Klarna.Status.ShouldBe(PaymentMethodStatus.Ready);
            methods.Klarna.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            methods.Klarna.Action.ShouldNotBeNull();
            methods.Klarna.Action.Type.ShouldBe("sdk");
            methods.Klarna.Action.ClientToken.ShouldBe("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ewogICJzZXNzaW9uX2lkIiA6ICIw");
            methods.Klarna.Action.SessionId.ShouldBe("0b1d9815-165e-42e2-8867-35bc03789e00");
            methods.Klarna.Action.OrderId.ShouldBeNull();
            methods.Klarna.AccountHolder.Name.ShouldBe("John Smith");
            methods.Klarna.Iban.ShouldBe("DE89370400440532013000");

            methods.Paypal.ShouldNotBeNull();
            methods.Paypal.Status.ShouldBe(PaymentMethodStatus.Ready);
            methods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            methods.Paypal.UserAction.ShouldBe(PaypalUserAction.PayNow);
            methods.Paypal.BrandName.ShouldBe("Acme Corporation");
            methods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.NoShipping);
            methods.Paypal.Action.ShouldNotBeNull();
            methods.Paypal.Action.Type.ShouldBe("sdk");
            methods.Paypal.Action.OrderId.ShouldBe("dd4b1a85-31de-404b-99f4-f143f8ba35ba");
            methods.Paypal.Action.ClientToken.ShouldBeNull();
            methods.Paypal.Action.SessionId.ShouldBeNull();

            methods.Tabby.ShouldNotBeNull();
            methods.Tabby.Status.ShouldBe(PaymentMethodStatus.Ready);
            methods.Tabby.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            methods.Tabby.PaymentTypes.ShouldBe(new List<string> { "pay_later" });

            methods.Stcpay.ShouldNotBeNull();
            methods.Stcpay.Status.ShouldBe(PaymentMethodStatus.ActionRequired);
            methods.Stcpay.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            methods.Stcpay.Action.ShouldNotBeNull();
            methods.Stcpay.Action.Type.ShouldBe("otp");
            methods.Stcpay.Otp.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForFlattenedPaymentMethodsInPaymentSetupsResponse()
        {
            var result = (PaymentSetupsResponse)Serializer.Deserialize(FlattenedPaymentMethodsResponseJson,
                typeof(PaymentSetupsResponse));

            result.ShouldNotBeNull();
            AssertFlattenedPaymentMethods(result.PaymentMethods);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForFlattenedPaymentMethodsInPaymentSetupsConfirmResponse()
        {
            var result = (PaymentSetupsConfirmResponse)Serializer.Deserialize(FlattenedPaymentMethodsResponseJson,
                typeof(PaymentSetupsConfirmResponse));

            result.ShouldNotBeNull();
            AssertFlattenedPaymentMethods(result.PaymentMethods);
        }

        [Fact]
        public void ShouldRoundTripFlattenedPaymentMethodsInPaymentSetupsResponse()
        {
            var original = (PaymentSetupsResponse)Serializer.Deserialize(FlattenedPaymentMethodsResponseJson,
                typeof(PaymentSetupsResponse));

            var json = Serializer.Serialize(original);
            var deserialized = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            AssertFlattenedPaymentMethods(deserialized.PaymentMethods);

            var methods = Newtonsoft.Json.Linq.JObject.Parse(json)["payment_methods"];
            ((string)methods["klarna"]["iban"]).ShouldBe("DE89370400440532013000");
            ((string)methods["klarna"]["action"]["client_token"])
                .ShouldBe("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ewogICJzZXNzaW9uX2lkIiA6ICIw");
            ((string)methods["klarna"]["action"]["session_id"]).ShouldBe("0b1d9815-165e-42e2-8867-35bc03789e00");
            ((string)methods["paypal"]["action"]["type"]).ShouldBe("sdk");
            ((string)methods["paypal"]["action"]["order_id"]).ShouldBe("dd4b1a85-31de-404b-99f4-f143f8ba35ba");
            ((string)methods["stcpay"]["action"]["type"]).ShouldBe("otp");
        }

        // ------------------------------------------------------------------------
        // PaymentSetupsConfirmResponse
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeConfirmResponseWithSetupsEntitiesModels()
        {
            const string json = @"{
                ""id"": ""psu_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""processing_channel_id"": ""pc_q4dbxom5jbgudnjzjpz7j2z6uq"",
                ""amount"": 10000,
                ""currency"": ""GBP"",
                ""payment_type"": ""regular"",
                ""reference"": ""REF-0987-475"",
                ""customer"": {
                    ""name"": ""John Smith"",
                    ""email"": { ""address"": ""johnsmith@example.com"", ""verified"": true }
                }
            }";

            var result =
                (PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse));

            result.ShouldNotBeNull();
            result.Id.ShouldBe("psu_y3oqhf46pyzuxjbcn2giaqnb44");
            result.Amount.ShouldBe(10000L);
            result.Currency.ShouldBe(Currency.GBP);
            result.PaymentType.ShouldBe(PaymentSetupPaymentType.Regular);

            // The customer must be the setups Customer, whose email is a nested object
            // (the payments CustomerResponse models email as a plain string and would not deserialize this shape).
            result.Customer.ShouldNotBeNull();
            result.Customer.Name.ShouldBe("John Smith");
            result.Customer.Email.ShouldNotBeNull();
            result.Customer.Email.Address.ShouldBe("johnsmith@example.com");
            result.Customer.Email.Verified.ShouldBe(true);
        }

        // ------------------------------------------------------------------------
        // Customer
        // PaymentSetup.customer: all 8 spec properties, including id and country,
        // which the model did not carry before.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldRoundTripCustomerWithEveryProperty()
        {
            var original = new Customer
            {
                Id = "cus_123456789",
                Country = CountryCode.GB,
                TaxNumber = "GB123456789",
                Name = "John Smith",
                Email = new CustomerEmail { Address = "johnsmith@example.com", Verified = true },
                Phone = new Phone { CountryCode = "+44", Number = "207 946 0000" },
                Device = new CustomerDevice
                {
                    Locale = "en_GB",
                    Fingerprint = "fp_abc123xyz",
                    Ipv4 = "203.0.113.0",
                    Ipv6 = "2001:db8:85a3::8a2e:370:7334",
                    Client = CustomerDeviceClient.Web,
                    Os = CustomerDeviceOs.Android
                },
                MerchantAccount = new MerchantAccount
                {
                    Id = "acc_123",
                    RegistrationDate = new DateTime(2023, 1, 15),
                    LastModified = new DateTime(2024, 3, 10),
                    ReturningCustomer = true,
                    FirstTransactionDate = new DateTime(2023, 2, 20),
                    LastTransactionDate = new DateTime(2024, 3, 9),
                    TotalOrderCount = 5,
                    LastPaymentAmount = 1000
                }
            };

            var json = Serializer.Serialize(original);
            var customer = Newtonsoft.Json.Linq.JObject.Parse(json);

            customer.Count.ShouldBe(8);
            ((string)customer["id"]).ShouldBe("cus_123456789");
            ((string)customer["country"]).ShouldBe("GB");
            ((string)customer["tax_number"]).ShouldBe("GB123456789");
            customer.ContainsKey("billing_address").ShouldBeFalse();

            var copy = (Customer)Serializer.Deserialize(json, typeof(Customer));
            copy.Id.ShouldBe(original.Id);
            copy.Country.ShouldBe(original.Country);
            copy.TaxNumber.ShouldBe(original.TaxNumber);
            copy.Name.ShouldBe(original.Name);
            copy.Email.Address.ShouldBe(original.Email.Address);
            copy.Email.Verified.ShouldBe(original.Email.Verified);
            copy.Phone.CountryCode.ShouldBe(original.Phone.CountryCode);
            copy.Phone.Number.ShouldBe(original.Phone.Number);
            copy.Device.Client.ShouldBe(original.Device.Client);
            copy.Device.Os.ShouldBe(original.Device.Os);
            copy.MerchantAccount.Id.ShouldBe(original.MerchantAccount.Id);
            copy.MerchantAccount.RegistrationDate.ShouldBe(original.MerchantAccount.RegistrationDate);
            copy.MerchantAccount.LastModified.ShouldBe(original.MerchantAccount.LastModified);
            copy.MerchantAccount.ReturningCustomer.ShouldBe(original.MerchantAccount.ReturningCustomer);
            copy.MerchantAccount.FirstTransactionDate.ShouldBe(original.MerchantAccount.FirstTransactionDate);
            copy.MerchantAccount.LastTransactionDate.ShouldBe(original.MerchantAccount.LastTransactionDate);
            copy.MerchantAccount.TotalOrderCount.ShouldBe(original.MerchantAccount.TotalOrderCount);
            copy.MerchantAccount.LastPaymentAmount.ShouldBe(original.MerchantAccount.LastPaymentAmount);
        }

        [Fact]
        public void ShouldRoundTripDecimalLastPaymentAmount()
        {
            // The specification types last_payment_amount as a number with the example 55.99; the SDK keeps
            // the decimals in both directions.
            var original = new MerchantAccount { LastPaymentAmount = 55.99m };

            var json = Serializer.Serialize(original);
            var merchantAccount = Newtonsoft.Json.Linq.JObject.Parse(json);

            merchantAccount.Count.ShouldBe(1);
            merchantAccount["last_payment_amount"].Type.ShouldBe(Newtonsoft.Json.Linq.JTokenType.Float);
            json.ShouldContain("\"last_payment_amount\":55.99");

            var copy = (MerchantAccount)Serializer.Deserialize(json, typeof(MerchantAccount));
            copy.LastPaymentAmount.ShouldBe(55.99m);
        }

        [Fact]
        public void ShouldDeserializeCustomerSwaggerExample()
        {
            const string json = @"{
                ""country"": ""GB"",
                ""id"": ""cus_123456789"",
                ""email"": { ""address"": ""johnsmith@example.com"", ""verified"": true },
                ""name"": ""John Smith"",
                ""tax_number"": ""GB123456789"",
                ""phone"": { ""country_code"": ""+44"", ""number"": ""207 946 0000"" },
                ""device"": { ""locale"": ""en_GB"" },
                ""merchant_account"": {
                    ""id"": ""1234"",
                    ""registration_date"": ""2023-05-01T00:00:00.0000000"",
                    ""last_modified"": ""2023-05-01T00:00:00.0000000"",
                    ""returning_customer"": true,
                    ""first_transaction_date"": ""2023-09-15T00:00:00.0000000"",
                    ""last_transaction_date"": ""2025-03-28T00:00:00.0000000"",
                    ""total_order_count"": 6,
                    ""last_payment_amount"": 55.99
                }
            }";

            var customer = (Customer)Serializer.Deserialize(json, typeof(Customer));

            customer.Country.ShouldBe(CountryCode.GB);
            customer.Id.ShouldBe("cus_123456789");
            customer.TaxNumber.ShouldBe("GB123456789");
            customer.Name.ShouldBe("John Smith");
            customer.Email.Address.ShouldBe("johnsmith@example.com");
            customer.Email.Verified.ShouldBe(true);
            customer.Phone.CountryCode.ShouldBe("+44");
            customer.Phone.Number.ShouldBe("207 946 0000");
            customer.Device.Locale.ShouldBe("en_GB");
            customer.MerchantAccount.Id.ShouldBe("1234");
            customer.MerchantAccount.RegistrationDate.ShouldBe(new DateTime(2023, 5, 1));
            customer.MerchantAccount.LastModified.ShouldBe(new DateTime(2023, 5, 1));
            customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            customer.MerchantAccount.FirstTransactionDate.ShouldBe(new DateTime(2023, 9, 15));
            customer.MerchantAccount.LastTransactionDate.ShouldBe(new DateTime(2025, 3, 28));
            customer.MerchantAccount.TotalOrderCount.ShouldBe(6);
            customer.MerchantAccount.LastPaymentAmount.ShouldBe(55.99m);
        }

        // ------------------------------------------------------------------------
        // PaymentSetupsResponse: card, wallets, instrument, auto_confirm, settings and metadata
        // Covers the response side of payment_methods.card, applepay, googlepay, instrument and
        // auto_confirm (including their readOnly status, flags, schemes, card details and instrument
        // action), settings.excluded_payment_methods and the top-level metadata.
        // ------------------------------------------------------------------------

        private const string CardWalletsAndMetadataResponseJson = @"{
            ""id"": ""psu_y3oqhf46pyzuxjbcn2giaqnb44"",
            ""processing_channel_id"": ""pc_q4dbxom5jbgudnjzjpz7j2z6uq"",
            ""amount"": 10000,
            ""currency"": ""GBP"",
            ""payment_methods"": {
                ""auto_confirm"": ""card"",
                ""instrument"": {
                    ""status"": ""action_required"",
                    ""flags"": [""instrument_not_set""],
                    ""id"": ""src_wmlfc3zttb4uzmk6snpwb43jbi"",
                    ""type"": ""bacs"",
                    ""action"": { ""type"": ""set_instrument"" }
                },
                ""card"": {
                    ""status"": ""ready"",
                    ""flags"": [],
                    ""token"": { ""id"": ""tok_ubfj2q76miwundwlk72vxt2i7q"" },
                    ""details"": {
                        ""last4"": ""4242"",
                        ""bin"": ""424242"",
                        ""scheme"": ""visa"",
                        ""expiry_month"": 12,
                        ""expiry_year"": 2030,
                        ""expires_on"": ""2026-06-01T12:00:00Z"",
                        ""allow_update"": false
                    },
                    ""phone"": { ""country_code"": ""+44"", ""number"": ""207 946 0000"" },
                    ""account_holder"": {
                        ""type"": ""government"",
                        ""first_name"": ""John"",
                        ""last_name"": ""Smith"",
                        ""middle_name"": ""Paul"",
                        ""company_name"": ""Acme Ltd"",
                        ""account_name_inquiry"": false
                    },
                    ""schemes"": [""accel"", ""amex"", ""cartes_bancaires"", ""china_union_pay"", ""diners"", ""discover"", ""jcb"", ""mada"", ""maestro"", ""mastercard"", ""nyce"", ""omannet"", ""pulse"", ""shazam"", ""star"", ""upi"", ""visa""],
                    ""preferred_scheme"": ""cartes_bancaires""
                },
                ""applepay"": {
                    ""status"": ""initialization_required"",
                    ""token"": { ""id"": ""tok_ubfj2q76miwundwlk72vxt2i7q"", ""expires_on"": ""2026-06-01T12:00:00Z"" },
                    ""phone"": { ""country_code"": ""+44"", ""number"": ""207 946 0000"" },
                    ""account_holder"": { ""type"": ""individual"", ""first_name"": ""John"" },
                    ""schemes"": [""visa"", ""mastercard""],
                    ""preferred_scheme"": ""visa""
                },
                ""googlepay"": {
                    ""status"": ""invalid"",
                    ""flags"": [""token_expired""],
                    ""token"": { ""id"": ""tok_ubfj2q76miwundwlk72vxt2i7q"", ""expires_on"": ""2026-06-01T12:00:00Z"" },
                    ""phone"": { ""country_code"": ""+44"", ""number"": ""207 946 0000"" },
                    ""account_holder"": { ""type"": ""corporate"", ""company_name"": ""Acme Ltd"" },
                    ""schemes"": [""mastercard""],
                    ""preferred_scheme"": ""mastercard""
                }
            },
            ""settings"": {
                ""success_url"": ""https://example.com/payments/success"",
                ""failure_url"": ""https://example.com/payments/fail"",
                ""capture"": true,
                ""excluded_payment_methods"": [""card"", ""klarna"", ""stcpay""]
            },
            ""metadata"": { ""coupon_code"": ""NY2018"", ""partner_id"": 123989, ""vip"": true }
        }";

        [Fact]
        public void ShouldDeserializeCardWalletsInstrumentSettingsAndMetadataForPaymentSetupsResponse()
        {
            var response =
                (PaymentSetupsResponse)Serializer.Deserialize(CardWalletsAndMetadataResponseJson, typeof(PaymentSetupsResponse));

            AssertCardWalletsInstrumentSettingsAndMetadata(response.PaymentMethods, response.Settings, response.Metadata);
        }

        [Fact]
        public void ShouldDeserializeCardWalletsInstrumentSettingsAndMetadataForPaymentSetupsConfirmResponse()
        {
            var response = (PaymentSetupsConfirmResponse)Serializer.Deserialize(CardWalletsAndMetadataResponseJson,
                typeof(PaymentSetupsConfirmResponse));

            AssertCardWalletsInstrumentSettingsAndMetadata(response.PaymentMethods, response.Settings, response.Metadata);
        }

        [Fact]
        public void ShouldRoundTripCardWalletsInstrumentSettingsAndMetadataForPaymentSetupsResponse()
        {
            var original =
                (PaymentSetupsResponse)Serializer.Deserialize(CardWalletsAndMetadataResponseJson, typeof(PaymentSetupsResponse));

            var json = Serializer.Serialize(original);
            var copy = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            json.ShouldContain("\"auto_confirm\":\"card\"");
            json.ShouldContain("\"applepay\":{");
            json.ShouldContain("\"googlepay\":{");
            json.ShouldContain("\"last4\":\"4242\"");
            json.ShouldContain("\"action\":{\"type\":\"set_instrument\"}");
            json.ShouldContain("\"status\":\"initialization_required\"");
            json.ShouldContain("\"schemes\":[\"mastercard\"]");
            json.ShouldContain("\"excluded_payment_methods\":[\"card\",\"klarna\",\"stcpay\"]");
            json.ShouldContain("\"metadata\":{\"coupon_code\":\"NY2018\",\"partner_id\":123989,\"vip\":true}");
            AssertCardWalletsInstrumentSettingsAndMetadata(copy.PaymentMethods, copy.Settings, copy.Metadata);
        }

        [Fact]
        public void ShouldNotFailOnAnUnknownSchemeForPaymentSetupsResponse()
        {
            // schemes is read-only and the scheme list grows over time: a value the SDK does not know
            // yet must not break the whole response.
            const string json = @"{
                ""id"": ""ps_2Un2jYAVxLsfUfpOvLhEHRW8Ft1"",
                ""payment_methods"": {
                    ""card"": { ""status"": ""ready"", ""schemes"": [""visa"", ""elo""] },
                    ""applepay"": { ""status"": ""ready"", ""schemes"": [""elo"", ""mastercard""] },
                    ""googlepay"": { ""status"": ""ready"", ""schemes"": [""visa"", ""elo""] }
                }
            }";

            var response = (PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            response.Id.ShouldBe("ps_2Un2jYAVxLsfUfpOvLhEHRW8Ft1");
            response.PaymentMethods.Card.Schemes.ShouldBe(new List<PaymentSetupScheme?> { PaymentSetupScheme.Visa, null });
            response.PaymentMethods.ApplePay.Schemes.ShouldBe(new List<PaymentSetupScheme?> { null, PaymentSetupScheme.Mastercard });
            response.PaymentMethods.GooglePay.Schemes.ShouldBe(new List<PaymentSetupScheme?> { PaymentSetupScheme.Visa, null });
        }

        private static void AssertCardWalletsInstrumentSettingsAndMetadata(
            Checkout.Payments.Setups.Entities.PaymentMethods methods, Settings settings,
            IDictionary<string, object> metadata)
        {
            var expiresOn = new DateTime(2026, 6, 1, 12, 0, 0, DateTimeKind.Utc);

            methods.AutoConfirm.ShouldBe("card");

            methods.Instrument.Status.ShouldBe(PaymentMethodStatus.ActionRequired);
            methods.Instrument.Flags.ShouldBe(new List<string> { "instrument_not_set" });
            methods.Instrument.Id.ShouldBe("src_wmlfc3zttb4uzmk6snpwb43jbi");
            methods.Instrument.Type.ShouldBe("bacs");
            methods.Instrument.Action.Type.ShouldBe(PaymentSetupInstrumentActionType.SetInstrument);

            var card = methods.Card;
            card.Status.ShouldBe(PaymentMethodStatus.Ready);
            card.Flags.ShouldBeEmpty();
            card.Token.Id.ShouldBe("tok_ubfj2q76miwundwlk72vxt2i7q");
            card.Details.Last4.ShouldBe("4242");
            card.Details.Bin.ShouldBe("424242");
            card.Details.Scheme.ShouldBe("visa");
            card.Details.ExpiryMonth.ShouldBe(12);
            card.Details.ExpiryYear.ShouldBe(2030);
            card.Details.ExpiresOn.ShouldBe(expiresOn);
            card.Details.AllowUpdate.ShouldBe(false);
            card.Phone.CountryCode.ShouldBe("+44");
            card.Phone.Number.ShouldBe("207 946 0000");
            card.AccountHolder.Type.ShouldBe(PaymentSetupCardAccountHolderType.Government);
            card.AccountHolder.FirstName.ShouldBe("John");
            card.AccountHolder.LastName.ShouldBe("Smith");
            card.AccountHolder.MiddleName.ShouldBe("Paul");
            card.AccountHolder.CompanyName.ShouldBe("Acme Ltd");
            card.AccountHolder.AccountNameInquiry.ShouldBe(false);
            card.Schemes.ShouldBe(new List<PaymentSetupScheme?>
            {
                PaymentSetupScheme.Accel, PaymentSetupScheme.Amex, PaymentSetupScheme.CartesBancaires,
                PaymentSetupScheme.ChinaUnionPay, PaymentSetupScheme.Diners, PaymentSetupScheme.Discover,
                PaymentSetupScheme.Jcb, PaymentSetupScheme.Mada, PaymentSetupScheme.Maestro,
                PaymentSetupScheme.Mastercard, PaymentSetupScheme.Nyce, PaymentSetupScheme.Omannet,
                PaymentSetupScheme.Pulse, PaymentSetupScheme.Shazam, PaymentSetupScheme.Star,
                PaymentSetupScheme.Upi, PaymentSetupScheme.Visa
            });
            card.PreferredScheme.ShouldBe(PreferredSchema.CartesBancaires);

            var applePay = methods.ApplePay;
            applePay.Status.ShouldBe(PaymentMethodStatus.InitializationRequired);
            applePay.Token.Id.ShouldBe("tok_ubfj2q76miwundwlk72vxt2i7q");
            applePay.Token.ExpiresOn.ShouldBe(expiresOn);
            applePay.Phone.Number.ShouldBe("207 946 0000");
            applePay.AccountHolder.Type.ShouldBe(PaymentSetupCardAccountHolderType.Individual);
            applePay.AccountHolder.FirstName.ShouldBe("John");
            applePay.Schemes.ShouldBe(new List<PaymentSetupScheme?> { PaymentSetupScheme.Visa, PaymentSetupScheme.Mastercard });
            applePay.PreferredScheme.ShouldBe(PreferredSchema.Visa);

            var googlePay = methods.GooglePay;
            googlePay.Status.ShouldBe(PaymentMethodStatus.Invalid);
            googlePay.Flags.ShouldBe(new List<string> { "token_expired" });
            googlePay.Token.Id.ShouldBe("tok_ubfj2q76miwundwlk72vxt2i7q");
            googlePay.Token.ExpiresOn.ShouldBe(expiresOn);
            googlePay.Phone.CountryCode.ShouldBe("+44");
            googlePay.AccountHolder.Type.ShouldBe(PaymentSetupCardAccountHolderType.Corporate);
            googlePay.AccountHolder.CompanyName.ShouldBe("Acme Ltd");
            googlePay.Schemes.ShouldBe(new List<PaymentSetupScheme?> { PaymentSetupScheme.Mastercard });
            googlePay.PreferredScheme.ShouldBe(GooglePayPreferredScheme.Mastercard);

            settings.SuccessUrl.ShouldBe("https://example.com/payments/success");
            settings.FailureUrl.ShouldBe("https://example.com/payments/fail");
            settings.Capture.ShouldBe(true);
            settings.ExcludedPaymentMethods.ShouldBe(new List<string> { "card", "klarna", "stcpay" });

            metadata.Count.ShouldBe(3);
            metadata["coupon_code"].ShouldBe("NY2018");
            metadata["partner_id"].ShouldBe(123989L);
            metadata["vip"].ShouldBe(true);
        }

        // ------------------------------------------------------------------------
        // PaymentSetupPaymentType
        // payment_type uses the lowercase values of the Payment Setups spec. The payments PaymentType sent
        // "PayLater", which the API rejects (422), and could not read "pay_later" back.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(PaymentSetupPaymentType.Regular, "regular")]
        [InlineData(PaymentSetupPaymentType.Recurring, "recurring")]
        [InlineData(PaymentSetupPaymentType.Moto, "moto")]
        [InlineData(PaymentSetupPaymentType.Installment, "installment")]
        [InlineData(PaymentSetupPaymentType.PayLater, "pay_later")]
        [InlineData(PaymentSetupPaymentType.Unscheduled, "unscheduled")]
        public void ShouldSendPaymentTypeSpecValue(PaymentSetupPaymentType value, string wire)
        {
            var json = Newtonsoft.Json.Linq.JObject.Parse(Serializer.Serialize(new PaymentSetupsRequest { PaymentType = value }));

            ((string)json["payment_type"]).ShouldBe(wire);
        }

        [Fact]
        public void ShouldSendRegularPaymentTypeByDefault()
        {
            var json = Newtonsoft.Json.Linq.JObject.Parse(Serializer.Serialize(new PaymentSetupsRequest()));

            ((string)json["payment_type"]).ShouldBe("regular");
        }

        [Fact]
        public void ShouldReadPayLaterPaymentTypeFromResponses()
        {
            const string json = @"{ ""id"": ""set_pt"", ""payment_type"": ""pay_later"" }";

            ((PaymentSetupsResponse)Serializer.Deserialize(json, typeof(PaymentSetupsResponse)))
                .PaymentType.ShouldBe(PaymentSetupPaymentType.PayLater);
            ((PaymentSetupsConfirmResponse)Serializer.Deserialize(json, typeof(PaymentSetupsConfirmResponse)))
                .PaymentType.ShouldBe(PaymentSetupPaymentType.PayLater);
        }

        // ------------------------------------------------------------------------
        // Order amounts
        // order.tipping_amount and order.surcharge_amount (int64) under their spec keys.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSendTippingAndSurchargeAmountsUnderSpecKeys()
        {
            var request = new PaymentSetupsRequest { Order = new Order { TippingAmount = 2, SurchargeAmount = 3 } };

            var order = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JObject.Parse(Serializer.Serialize(request))["order"];

            order.Count.ShouldBe(2);
            ((long)order["tipping_amount"]).ShouldBe(2L);
            ((long)order["surcharge_amount"]).ShouldBe(3L);
        }
    }
}
