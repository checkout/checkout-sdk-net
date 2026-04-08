using Checkout.Common;
using Checkout.Payments;
using Checkout.Payments.Contexts;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;
using SetupAccommodationData = Checkout.Payments.Setups.Entities.AccommodationData;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    public class PaymentSetupsRequestSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldRoundTripSerializeWithAllProperties()
        {
            var original = CreateFullRequest();

            var json = _serializer.Serialize(original);
            var deserialized = (PaymentSetupsRequest)_serializer.Deserialize(json, typeof(PaymentSetupsRequest));

            deserialized.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            deserialized.Amount.ShouldBe(10000L);
            deserialized.Currency.ShouldBe(Currency.GBP);
            deserialized.PaymentType.ShouldBe(PaymentType.Regular);
            deserialized.Reference.ShouldBe("ORD-12345");
            deserialized.Description.ShouldBe("Test order");

            // Billing
            deserialized.Billing.ShouldNotBeNull();
            deserialized.Billing.Address.ShouldNotBeNull();
            deserialized.Billing.Address.AddressLine1.ShouldBe("1 London St");
            deserialized.Billing.Address.AddressLine2.ShouldBe("Flat 1");
            deserialized.Billing.Address.City.ShouldBe("London");
            deserialized.Billing.Address.State.ShouldBe("Greater London");
            deserialized.Billing.Address.Zip.ShouldBe("W1 1AA");
            deserialized.Billing.Address.Country.ShouldBe(CountryCode.GB);

            // Settings
            deserialized.Settings.ShouldNotBeNull();
            deserialized.Settings.SuccessUrl.ShouldBe("https://example.com/success");
            deserialized.Settings.FailureUrl.ShouldBe("https://example.com/failure");
            deserialized.Settings.Capture.ShouldBe(true);

            // Customer
            deserialized.Customer.ShouldNotBeNull();
            deserialized.Customer.Name.ShouldBe("John Doe");
            deserialized.Customer.TaxNumber.ShouldBe("TAX-123456");
            deserialized.Customer.Email.ShouldNotBeNull();
            deserialized.Customer.Email.Address.ShouldBe("john@example.com");
            deserialized.Customer.Email.Verified.ShouldBe(true);
            deserialized.Customer.Phone.ShouldNotBeNull();
            deserialized.Customer.Phone.Number.ShouldBe("1234567890");
            deserialized.Customer.BillingAddress.ShouldNotBeNull();
            deserialized.Customer.BillingAddress.AddressLine1.ShouldBe("1 London St");
            deserialized.Customer.BillingAddress.Country.ShouldBe(CountryCode.GB);
            deserialized.Customer.Device.ShouldNotBeNull();
            deserialized.Customer.Device.Locale.ShouldBe("en-GB");
            deserialized.Customer.MerchantAccount.ShouldNotBeNull();
            deserialized.Customer.MerchantAccount.Id.ShouldBe("cust_001");
            deserialized.Customer.MerchantAccount.RegistrationDate.ShouldBe("2023-01-15");
            deserialized.Customer.MerchantAccount.LastModified.ShouldBe("2024-06-01");
            deserialized.Customer.MerchantAccount.ReturningCustomer.ShouldBe(true);
            deserialized.Customer.MerchantAccount.FirstTransactionDate.ShouldBe("2023-02-10");
            deserialized.Customer.MerchantAccount.LastTransactionDate.ShouldBe("2024-12-01");
            deserialized.Customer.MerchantAccount.TotalOrderCount.ShouldBe(42);
            deserialized.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(99.99m);

            // Order
            deserialized.Order.ShouldNotBeNull();
            deserialized.Order.InvoiceId.ShouldBe("INV-2024-001");
            deserialized.Order.ShippingAmount.ShouldBe(500L);
            deserialized.Order.DiscountAmount.ShouldBe(200L);
            deserialized.Order.TaxAmount.ShouldBe(1500L);
            deserialized.Order.Items.ShouldNotBeNull();
            deserialized.Order.Items.Count.ShouldBe(1);
            deserialized.Order.SubMerchants.ShouldNotBeNull();
            deserialized.Order.SubMerchants.Count.ShouldBe(1);
            deserialized.Order.SubMerchants[0].Id.ShouldBe("sub_001");
            deserialized.Order.SubMerchants[0].ProductCategory.ShouldBe("electronics");
            deserialized.Order.SubMerchants[0].NumberOfSales.ShouldBe(150);
            deserialized.Order.SubMerchants[0].RegistrationDate.ShouldBe("2022-03-01");

            // Industry
            deserialized.Industry.ShouldNotBeNull();
            deserialized.Industry.AccommodationData.ShouldNotBeNull();
            deserialized.Industry.AccommodationData.Name.ShouldBe("Grand Hotel");
            deserialized.Industry.AccommodationData.BookingReference.ShouldBe("BK-12345");
            deserialized.Industry.AccommodationData.CheckInDate.ShouldBe("2025-06-15");
            deserialized.Industry.AccommodationData.CheckOutDate.ShouldBe("2025-06-20");
            deserialized.Industry.AccommodationData.NumberOfRooms.ShouldBe(2);
            deserialized.Industry.AccommodationData.Address.ShouldNotBeNull();
            deserialized.Industry.AccommodationData.Address.AddressLine1.ShouldBe("10 Hotel Rd");
            deserialized.Industry.AccommodationData.Address.City.ShouldBe("London");
            deserialized.Industry.AccommodationData.Address.State.ShouldBe("England");
            deserialized.Industry.AccommodationData.Address.Country.ShouldBe("GB");
            deserialized.Industry.AccommodationData.Address.Zip.ShouldBe("EC1A 1BB");
            deserialized.Industry.AccommodationData.Guests.ShouldNotBeNull();
            deserialized.Industry.AccommodationData.Guests.Count.ShouldBe(1);
            deserialized.Industry.AccommodationData.Guests[0].FirstName.ShouldBe("John");
            deserialized.Industry.AccommodationData.Guests[0].LastName.ShouldBe("Doe");
            deserialized.Industry.AccommodationData.Guests[0].DateOfBirth.ShouldBe("1990-05-20");
            deserialized.Industry.AccommodationData.Room.ShouldNotBeNull();
            deserialized.Industry.AccommodationData.Room.Count.ShouldBe(1);
            deserialized.Industry.AccommodationData.Room[0].Rate.ShouldBe(150.00m);
            deserialized.Industry.AccommodationData.Room[0].NumberOfNights.ShouldBe(5);

            // PaymentMethods
            deserialized.PaymentMethods.ShouldNotBeNull();

            // Klarna
            deserialized.PaymentMethods.Klarna.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Klarna.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            deserialized.PaymentMethods.Klarna.Flags.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.Flags.Count.ShouldBe(1);
            deserialized.PaymentMethods.Klarna.AccountHolder.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.AccountHolder.BillingAddress.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.AccountHolder.BillingAddress.AddressLine1.ShouldBe("1 Klarna St");
            deserialized.PaymentMethods.Klarna.Action.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.Action.Type.ShouldBe("sdk");
            deserialized.PaymentMethods.Klarna.Action.ClientToken.ShouldBe("klarna_token_123");
            deserialized.PaymentMethods.Klarna.Action.SessionId.ShouldBe("klarna_session_456");
            deserialized.PaymentMethods.Klarna.Action.OrderId.ShouldBeNull();
            deserialized.PaymentMethods.Klarna.PaymentMethodOptions.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.ShouldNotBeNull();
            deserialized.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Id.ShouldBe("opt_klarna_sdk");

            // Stcpay
            deserialized.PaymentMethods.Stcpay.ShouldNotBeNull();
            deserialized.PaymentMethods.Stcpay.Status.ShouldBe(PaymentMethodStatus.RequiresAction);
            deserialized.PaymentMethods.Stcpay.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            deserialized.PaymentMethods.Stcpay.Otp.ShouldBe("123456");
            deserialized.PaymentMethods.Stcpay.Action.ShouldNotBeNull();
            deserialized.PaymentMethods.Stcpay.Action.Type.ShouldBe("otp");
            deserialized.PaymentMethods.Stcpay.PaymentMethodOptions.ShouldNotBeNull();
            deserialized.PaymentMethods.Stcpay.PaymentMethodOptions.PayInFull.ShouldNotBeNull();
            deserialized.PaymentMethods.Stcpay.PaymentMethodOptions.PayInFull.Id.ShouldBe("opt_stc_full");

            // Tabby
            deserialized.PaymentMethods.Tabby.ShouldNotBeNull();
            deserialized.PaymentMethods.Tabby.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Tabby.PaymentTypes.ShouldNotBeNull();
            deserialized.PaymentMethods.Tabby.PaymentTypes.Count.ShouldBe(2);
            deserialized.PaymentMethods.Tabby.PaymentTypes.ShouldContain("installments");
            deserialized.PaymentMethods.Tabby.PaymentTypes.ShouldContain("pay_later");
            deserialized.PaymentMethods.Tabby.PaymentMethodOptions.ShouldNotBeNull();
            deserialized.PaymentMethods.Tabby.PaymentMethodOptions.Installments.ShouldNotBeNull();
            deserialized.PaymentMethods.Tabby.PaymentMethodOptions.Installments.Id.ShouldBe("opt_tabby_inst");

            // Bizum
            deserialized.PaymentMethods.Bizum.ShouldNotBeNull();
            deserialized.PaymentMethods.Bizum.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Bizum.PaymentMethodOptions.ShouldNotBeNull();
            deserialized.PaymentMethods.Bizum.PaymentMethodOptions.PayNow.ShouldNotBeNull();
            deserialized.PaymentMethods.Bizum.PaymentMethodOptions.PayNow.Id.ShouldBe("opt_bizum_now");

            // Paypal
            deserialized.PaymentMethods.Paypal.ShouldNotBeNull();
            deserialized.PaymentMethods.Paypal.Status.ShouldBe(PaymentMethodStatus.Available);
            deserialized.PaymentMethods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            deserialized.PaymentMethods.Paypal.UserAction.ShouldBe(PaypalUserAction.PayNow);
            deserialized.PaymentMethods.Paypal.BrandName.ShouldBe("Test Store");
            deserialized.PaymentMethods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.SetProvidedAddress);
            deserialized.PaymentMethods.Paypal.Action.ShouldNotBeNull();
            deserialized.PaymentMethods.Paypal.Action.Type.ShouldBe("redirect");
            deserialized.PaymentMethods.Paypal.Action.OrderId.ShouldBe("PAYPAL-ORD-789");
        }

        [Fact]
        public void ShouldDeserializeSnakeCaseJson()
        {
            const string json = @"{
                ""processing_channel_id"": ""pc_abcdefghijklmnopqrstuvwxyz"",
                ""amount"": 5000,
                ""currency"": ""USD"",
                ""payment_type"": ""Recurring"",
                ""reference"": ""REF-001"",
                ""description"": ""Subscription"",
                ""billing"": {
                    ""address"": {
                        ""address_line1"": ""123 Main St"",
                        ""city"": ""New York"",
                        ""zip"": ""10001"",
                        ""country"": ""US""
                    }
                },
                ""settings"": {
                    ""success_url"": ""https://example.com/ok"",
                    ""failure_url"": ""https://example.com/fail"",
                    ""capture"": false
                },
                ""customer"": {
                    ""name"": ""Jane Smith"",
                    ""tax_number"": ""TAX-999"",
                    ""email"": { ""address"": ""jane@example.com"", ""verified"": false },
                    ""billing_address"": { ""address_line1"": ""456 Oak Ave"", ""country"": ""US"" },
                    ""device"": { ""locale"": ""en-US"" },
                    ""merchant_account"": {
                        ""id"": ""cust_002"",
                        ""registration_date"": ""2022-06-15"",
                        ""last_modified"": ""2024-01-10"",
                        ""returning_customer"": false,
                        ""first_transaction_date"": ""2022-07-01"",
                        ""last_transaction_date"": ""2024-11-15"",
                        ""total_order_count"": 5,
                        ""last_payment_amount"": 49.99
                    }
                },
                ""order"": {
                    ""invoice_id"": ""INV-002"",
                    ""shipping_amount"": 300,
                    ""discount_amount"": 100,
                    ""tax_amount"": 800,
                    ""sub_merchants"": [
                        {
                            ""id"": ""sub_002"",
                            ""product_category"": ""clothing"",
                            ""number_of_sales"": 75,
                            ""registration_date"": ""2023-01-01""
                        }
                    ]
                },
                ""industry"": {
                    ""accommodation_data"": {
                        ""name"": ""Beach Resort"",
                        ""booking_reference"": ""BK-999"",
                        ""check_in_date"": ""2025-07-01"",
                        ""check_out_date"": ""2025-07-05"",
                        ""number_of_rooms"": 1,
                        ""address"": {
                            ""address_line1"": ""1 Beach Rd"",
                            ""city"": ""Miami"",
                            ""state"": ""FL"",
                            ""country"": ""US"",
                            ""zip"": ""33101""
                        },
                        ""guests"": [{ ""first_name"": ""Jane"", ""last_name"": ""Smith"", ""date_of_birth"": ""1985-03-10"" }],
                        ""room"": [{ ""rate"": 200.50, ""number_of_nights"": 4 }]
                    }
                },
                ""payment_methods"": {
                    ""klarna"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""flags"": [""express""],
                        ""account_holder"": { ""billing_address"": { ""address_line1"": ""1 Klarna St"" } },
                        ""action"": { ""type"": ""sdk"", ""client_token"": ""tok_abc"", ""session_id"": ""sess_def"" },
                        ""payment_method_options"": { ""sdk"": { ""id"": ""opt_k1"" } }
                    },
                    ""stcpay"": {
                        ""status"": ""requires_action"",
                        ""initialization"": ""enabled"",
                        ""otp"": ""654321"",
                        ""action"": { ""type"": ""otp"" },
                        ""payment_method_options"": { ""pay_in_full"": { ""id"": ""opt_s1"" } }
                    },
                    ""tabby"": {
                        ""status"": ""available"",
                        ""initialization"": ""disabled"",
                        ""payment_types"": [""installments""],
                        ""payment_method_options"": { ""installments"": { ""id"": ""opt_t1"" } }
                    },
                    ""bizum"": {
                        ""status"": ""available"",
                        ""initialization"": ""disabled"",
                        ""payment_method_options"": { ""pay_now"": { ""id"": ""opt_b1"" } }
                    },
                    ""paypal"": {
                        ""status"": ""available"",
                        ""initialization"": ""enabled"",
                        ""user_action"": ""continue"",
                        ""brand_name"": ""My Store"",
                        ""shipping_preference"": ""no_shipping"",
                        ""action"": { ""type"": ""redirect"", ""order_id"": ""PP-001"" }
                    }
                }
            }";

            var result = (PaymentSetupsRequest)_serializer.Deserialize(json, typeof(PaymentSetupsRequest));

            result.ProcessingChannelId.ShouldBe("pc_abcdefghijklmnopqrstuvwxyz");
            result.Amount.ShouldBe(5000L);
            result.Currency.ShouldBe(Currency.USD);
            result.PaymentType.ShouldBe(PaymentType.Recurring);
            result.Reference.ShouldBe("REF-001");
            result.Description.ShouldBe("Subscription");

            // Billing
            result.Billing.Address.AddressLine1.ShouldBe("123 Main St");
            result.Billing.Address.City.ShouldBe("New York");
            result.Billing.Address.Zip.ShouldBe("10001");
            result.Billing.Address.Country.ShouldBe(CountryCode.US);

            // Settings
            result.Settings.SuccessUrl.ShouldBe("https://example.com/ok");
            result.Settings.FailureUrl.ShouldBe("https://example.com/fail");
            result.Settings.Capture.ShouldBe(false);

            // Customer
            result.Customer.Name.ShouldBe("Jane Smith");
            result.Customer.TaxNumber.ShouldBe("TAX-999");
            result.Customer.Email.Address.ShouldBe("jane@example.com");
            result.Customer.Email.Verified.ShouldBe(false);
            result.Customer.BillingAddress.AddressLine1.ShouldBe("456 Oak Ave");
            result.Customer.BillingAddress.Country.ShouldBe(CountryCode.US);
            result.Customer.Device.Locale.ShouldBe("en-US");
            result.Customer.MerchantAccount.Id.ShouldBe("cust_002");
            result.Customer.MerchantAccount.RegistrationDate.ShouldBe("2022-06-15");
            result.Customer.MerchantAccount.LastModified.ShouldBe("2024-01-10");
            result.Customer.MerchantAccount.ReturningCustomer.ShouldBe(false);
            result.Customer.MerchantAccount.FirstTransactionDate.ShouldBe("2022-07-01");
            result.Customer.MerchantAccount.LastTransactionDate.ShouldBe("2024-11-15");
            result.Customer.MerchantAccount.TotalOrderCount.ShouldBe(5);
            result.Customer.MerchantAccount.LastPaymentAmount.ShouldBe(49.99m);

            // Order
            result.Order.InvoiceId.ShouldBe("INV-002");
            result.Order.ShippingAmount.ShouldBe(300L);
            result.Order.DiscountAmount.ShouldBe(100L);
            result.Order.TaxAmount.ShouldBe(800L);
            result.Order.SubMerchants[0].Id.ShouldBe("sub_002");
            result.Order.SubMerchants[0].ProductCategory.ShouldBe("clothing");
            result.Order.SubMerchants[0].NumberOfSales.ShouldBe(75);
            result.Order.SubMerchants[0].RegistrationDate.ShouldBe("2023-01-01");

            // Industry - AccommodationData
            result.Industry.AccommodationData.Name.ShouldBe("Beach Resort");
            result.Industry.AccommodationData.BookingReference.ShouldBe("BK-999");
            result.Industry.AccommodationData.CheckInDate.ShouldBe("2025-07-01");
            result.Industry.AccommodationData.CheckOutDate.ShouldBe("2025-07-05");
            result.Industry.AccommodationData.NumberOfRooms.ShouldBe(1);
            result.Industry.AccommodationData.Address.AddressLine1.ShouldBe("1 Beach Rd");
            result.Industry.AccommodationData.Address.City.ShouldBe("Miami");
            result.Industry.AccommodationData.Address.State.ShouldBe("FL");
            result.Industry.AccommodationData.Address.Country.ShouldBe("US");
            result.Industry.AccommodationData.Address.Zip.ShouldBe("33101");
            result.Industry.AccommodationData.Guests[0].FirstName.ShouldBe("Jane");
            result.Industry.AccommodationData.Guests[0].LastName.ShouldBe("Smith");
            result.Industry.AccommodationData.Guests[0].DateOfBirth.ShouldBe("1985-03-10");
            result.Industry.AccommodationData.Room[0].Rate.ShouldBe(200.50m);
            result.Industry.AccommodationData.Room[0].NumberOfNights.ShouldBe(4);

            // PaymentMethods - Klarna
            result.PaymentMethods.Klarna.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Klarna.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            result.PaymentMethods.Klarna.Flags.ShouldContain("express");
            result.PaymentMethods.Klarna.AccountHolder.BillingAddress.AddressLine1.ShouldBe("1 Klarna St");
            result.PaymentMethods.Klarna.Action.Type.ShouldBe("sdk");
            result.PaymentMethods.Klarna.Action.ClientToken.ShouldBe("tok_abc");
            result.PaymentMethods.Klarna.Action.SessionId.ShouldBe("sess_def");
            result.PaymentMethods.Klarna.PaymentMethodOptions.Sdk.Id.ShouldBe("opt_k1");

            // PaymentMethods - Stcpay
            result.PaymentMethods.Stcpay.Status.ShouldBe(PaymentMethodStatus.RequiresAction);
            result.PaymentMethods.Stcpay.Otp.ShouldBe("654321");
            result.PaymentMethods.Stcpay.Action.Type.ShouldBe("otp");
            result.PaymentMethods.Stcpay.PaymentMethodOptions.PayInFull.Id.ShouldBe("opt_s1");

            // PaymentMethods - Tabby
            result.PaymentMethods.Tabby.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Tabby.PaymentTypes.ShouldContain("installments");
            result.PaymentMethods.Tabby.PaymentMethodOptions.Installments.Id.ShouldBe("opt_t1");

            // PaymentMethods - Bizum
            result.PaymentMethods.Bizum.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Bizum.PaymentMethodOptions.PayNow.Id.ShouldBe("opt_b1");

            // PaymentMethods - Paypal
            result.PaymentMethods.Paypal.Status.ShouldBe(PaymentMethodStatus.Available);
            result.PaymentMethods.Paypal.Initialization.ShouldBe(PaymentMethodInitialization.Enabled);
            result.PaymentMethods.Paypal.UserAction.ShouldBe(PaypalUserAction.Continue);
            result.PaymentMethods.Paypal.BrandName.ShouldBe("My Store");
            result.PaymentMethods.Paypal.ShippingPreference.ShouldBe(PaypalShippingPreference.NoShipping);
            result.PaymentMethods.Paypal.Action.Type.ShouldBe("redirect");
            result.PaymentMethods.Paypal.Action.OrderId.ShouldBe("PP-001");
        }

        private static PaymentSetupsRequest CreateFullRequest()
        {
            return new PaymentSetupsRequest
            {
                ProcessingChannelId = "pc_abcdefghijklmnopqrstuvwxyz",
                Amount = 10000,
                Currency = Currency.GBP,
                PaymentType = PaymentType.Regular,
                Reference = "ORD-12345",
                Description = "Test order",
                Billing = new Billing
                {
                    Address = new Address
                    {
                        AddressLine1 = "1 London St",
                        AddressLine2 = "Flat 1",
                        City = "London",
                        State = "Greater London",
                        Zip = "W1 1AA",
                        Country = CountryCode.GB
                    }
                },
                Settings = new Settings
                {
                    SuccessUrl = "https://example.com/success",
                    FailureUrl = "https://example.com/failure",
                    Capture = true
                },
                Customer = new Customer
                {
                    Name = "John Doe",
                    TaxNumber = "TAX-123456",
                    Email = new CustomerEmail { Address = "john@example.com", Verified = true },
                    Phone = new Phone { Number = "1234567890" },
                    BillingAddress = new Address
                    {
                        AddressLine1 = "1 London St",
                        Country = CountryCode.GB
                    },
                    Device = new CustomerDevice { Locale = "en-GB" },
                    MerchantAccount = new MerchantAccount
                    {
                        Id = "cust_001",
                        RegistrationDate = "2023-01-15",
                        LastModified = "2024-06-01",
                        ReturningCustomer = true,
                        FirstTransactionDate = "2023-02-10",
                        LastTransactionDate = "2024-12-01",
                        TotalOrderCount = 42,
                        LastPaymentAmount = 99.99m
                    }
                },
                Order = new Order
                {
                    InvoiceId = "INV-2024-001",
                    ShippingAmount = 500,
                    DiscountAmount = 200,
                    TaxAmount = 1500,
                    Items = new List<PaymentContextsItems>
                    {
                        new PaymentContextsItems { Name = "Widget", Quantity = 2, UnitPrice = 5000 }
                    },
                    SubMerchants = new List<OrderSubMerchant>
                    {
                        new OrderSubMerchant
                        {
                            Id = "sub_001",
                            ProductCategory = "electronics",
                            NumberOfSales = 150,
                            RegistrationDate = "2022-03-01"
                        }
                    }
                },
                Industry = new Industry
                {
                    AccommodationData = new SetupAccommodationData
                    {
                        Name = "Grand Hotel",
                        BookingReference = "BK-12345",
                        CheckInDate = "2025-06-15",
                        CheckOutDate = "2025-06-20",
                        NumberOfRooms = 2,
                        Address = new AccommodationAddress
                        {
                            AddressLine1 = "10 Hotel Rd",
                            City = "London",
                            State = "England",
                            Country = "GB",
                            Zip = "EC1A 1BB"
                        },
                        Guests = new List<AccommodationGuest>
                        {
                            new AccommodationGuest { FirstName = "John", LastName = "Doe", DateOfBirth = "1990-05-20" }
                        },
                        Room = new List<AccommodationRoom>
                        {
                            new AccommodationRoom { Rate = 150.00m, NumberOfNights = 5 }
                        }
                    }
                },
                PaymentMethods = new Checkout.Payments.Setups.Entities.PaymentMethods
                {
                    Klarna = new Klarna
                    {
                        Status = PaymentMethodStatus.Available,
                        Initialization = PaymentMethodInitialization.Enabled,
                        Flags = new List<string> { "express" },
                        AccountHolder = new KlarnaAccountHolder
                        {
                            BillingAddress = new Address { AddressLine1 = "1 Klarna St" }
                        },
                        Action = new PaymentMethodAction
                        {
                            Type = "sdk",
                            ClientToken = "klarna_token_123",
                            SessionId = "klarna_session_456"
                        },
                        PaymentMethodOptions = new PaymentMethodOptions
                        {
                            Sdk = new PaymentMethodOption { Id = "opt_klarna_sdk" }
                        }
                    },
                    Stcpay = new Stcpay
                    {
                        Status = PaymentMethodStatus.RequiresAction,
                        Initialization = PaymentMethodInitialization.Enabled,
                        Otp = "123456",
                        Action = new PaymentMethodAction { Type = "otp" },
                        PaymentMethodOptions = new PaymentMethodOptions
                        {
                            PayInFull = new PaymentMethodOption { Id = "opt_stc_full" }
                        }
                    },
                    Tabby = new Tabby
                    {
                        Status = PaymentMethodStatus.Available,
                        PaymentTypes = new List<string> { "installments", "pay_later" },
                        PaymentMethodOptions = new PaymentMethodOptions
                        {
                            Installments = new PaymentMethodOption { Id = "opt_tabby_inst" }
                        }
                    },
                    Bizum = new Bizum
                    {
                        Status = PaymentMethodStatus.Available,
                        PaymentMethodOptions = new PaymentMethodOptions
                        {
                            PayNow = new PaymentMethodOption { Id = "opt_bizum_now" }
                        }
                    },
                    Paypal = new Paypal
                    {
                        Status = PaymentMethodStatus.Available,
                        Initialization = PaymentMethodInitialization.Enabled,
                        UserAction = PaypalUserAction.PayNow,
                        BrandName = "Test Store",
                        ShippingPreference = PaypalShippingPreference.SetProvidedAddress,
                        Action = new PaymentMethodAction
                        {
                            Type = "redirect",
                            OrderId = "PAYPAL-ORD-789"
                        }
                    }
                }
            };
        }
    }
}
