using System;
using System.Collections.Generic;
using Checkout.Common;
using Checkout.Payments.Setups;
using Checkout.Payments.Setups.Entities;
using Shouldly;
using Xunit;

namespace Checkout.HandlePaymentsAndPayouts.PaymentSetups
{
    /// <summary>
    /// Covers the 2026-06-29 PaymentSetup additions: billing_descriptor, presentment_details, terminal,
    /// latest_payment, the bacs/card_present/pay_by_bank/stablecoin payment methods, and order amount_allocations.
    /// </summary>
    public class PaymentSetupsNewFieldsSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeNewTopLevelFields()
        {
            var response = new PaymentSetupsResponse
            {
                Id = "pay_test",
                BillingDescriptor = new PaymentSetupBillingDescriptor
                {
                    Name = "Checkout.com", City = "London", Reference = "Payment for order 123456"
                },
                PresentmentDetails = new PaymentSetupPresentmentDetails { Amount = 110, Currency = Currency.EUR },
                Terminal = new PaymentSetupTerminal { Id = "12345678", LocalDateTime = new DateTime(2026, 5, 26, 13, 5, 14) },
                LatestPayment = new Dictionary<string, object> { { "id", "pay_123" } }
            };

            var json = _serializer.Serialize(response);

            json.ShouldContain("\"billing_descriptor\"");
            json.ShouldContain("\"presentment_details\"");
            json.ShouldContain("\"terminal\"");
            json.ShouldContain("\"latest_payment\"");
        }

        [Fact]
        public void ShouldDeserializeNewTopLevelFields()
        {
            const string json = @"{
                ""id"": ""pay_test"",
                ""billing_descriptor"": { ""name"": ""Checkout.com"", ""city"": ""London"", ""reference"": ""Payment for order 123456"" },
                ""presentment_details"": { ""amount"": 110, ""currency"": ""EUR"" },
                ""terminal"": { ""id"": ""12345678"", ""local_date_time"": ""2026-05-26T13:05:14+01:00"" }
            }";

            var response = (PaymentSetupsResponse)_serializer.Deserialize(json, typeof(PaymentSetupsResponse));

            response.BillingDescriptor.ShouldNotBeNull();
            response.BillingDescriptor.Name.ShouldBe("Checkout.com");
            response.BillingDescriptor.City.ShouldBe("London");
            response.BillingDescriptor.Reference.ShouldBe("Payment for order 123456");
            response.PresentmentDetails.ShouldNotBeNull();
            response.PresentmentDetails.Amount.ShouldBe(110);
            response.PresentmentDetails.Currency.ShouldBe(Currency.EUR);
            response.Terminal.ShouldNotBeNull();
            response.Terminal.Id.ShouldBe("12345678");
            response.Terminal.LocalDateTime.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldSerializeNewPaymentMethods()
        {
            var paymentMethods = new Checkout.Payments.Setups.Entities.PaymentMethods
            {
                Bacs = new Bacs
                {
                    InstrumentId = "src_test",
                    AccountHolder = new BacsAccountHolder
                    {
                        Type = BacsAccountHolderType.Individual, FirstName = "John", LastName = "Smith",
                        Email = "john.smith@example.com"
                    },
                    AccountNumber = "12345678", BankCode = "010203", Country = CountryCode.GB, Currency = "GBP",
                    AllowPartialMatch = false
                },
                CardPresent = new CardPresent
                {
                    Track2 = "track2data", Emv = "emvdata", EntryMode = "contactless",
                    Pin = new CardPresentPin { KeySetId = "ks_1", Block = "blk", BlockFormat = "iso0" },
                    StoreForFutureUse = true, Name = "John Smith"
                },
                PayByBank = new PayByBank
                {
                    BankId = "ob-natwest",
                    Action = new PayByBankAction
                    {
                        Type = "select_bank",
                        Banks = new List<PayByBankBank>
                        {
                            new PayByBankBank { BankId = "ob-natwest", DisplayName = "NatWest", LogoUrl = "https://cdn.token.io/banks/natwest.png", Available = true }
                        }
                    }
                },
                Stablecoin = new Stablecoin()
            };

            var json = _serializer.Serialize(paymentMethods);

            json.ShouldContain("\"bacs\"");
            json.ShouldContain("\"card_present\"");
            json.ShouldContain("\"pay_by_bank\"");
            json.ShouldContain("\"stablecoin\"");
            json.ShouldContain("\"account_holder\"");
            json.ShouldContain("\"select_bank\"");
        }

        [Fact]
        public void ShouldDeserializeNewPaymentMethods()
        {
            const string json = @"{
                ""bacs"": { ""instrument_id"": ""src_test"", ""account_holder"": { ""type"": ""corporate"", ""company_name"": ""Acme Ltd"" }, ""country"": ""GB"", ""currency"": ""GBP"", ""allow_partial_match"": true },
                ""card_present"": { ""entry_mode"": ""contactless"", ""store_for_future_use"": true },
                ""pay_by_bank"": { ""bank_id"": ""ob-natwest"", ""action"": { ""type"": ""select_bank"", ""banks"": [{ ""bank_id"": ""ob-natwest"", ""display_name"": ""NatWest"", ""available"": true }] } },
                ""stablecoin"": { ""status"": ""available"" }
            }";

            var paymentMethods = (Checkout.Payments.Setups.Entities.PaymentMethods)
                _serializer.Deserialize(json, typeof(Checkout.Payments.Setups.Entities.PaymentMethods));

            paymentMethods.Bacs.ShouldNotBeNull();
            paymentMethods.Bacs.InstrumentId.ShouldBe("src_test");
            paymentMethods.Bacs.AccountHolder.Type.ShouldBe(BacsAccountHolderType.Corporate);
            paymentMethods.Bacs.AccountHolder.CompanyName.ShouldBe("Acme Ltd");
            paymentMethods.Bacs.AllowPartialMatch.ShouldBe(true);
            paymentMethods.CardPresent.ShouldNotBeNull();
            paymentMethods.CardPresent.EntryMode.ShouldBe("contactless");
            paymentMethods.CardPresent.StoreForFutureUse.ShouldBe(true);
            paymentMethods.PayByBank.ShouldNotBeNull();
            paymentMethods.PayByBank.BankId.ShouldBe("ob-natwest");
            paymentMethods.PayByBank.Action.Type.ShouldBe("select_bank");
            paymentMethods.PayByBank.Action.Banks.Count.ShouldBe(1);
            paymentMethods.PayByBank.Action.Banks[0].DisplayName.ShouldBe("NatWest");
            paymentMethods.Stablecoin.ShouldNotBeNull();
        }

        [Fact]
        public void ShouldSerializeAndDeserializeOrderAmountAllocations()
        {
            var order = new Order
            {
                AmountAllocations = new List<PaymentSetupAmountAllocation>
                {
                    new PaymentSetupAmountAllocation
                    {
                        Id = "ent_w4jelhppmfiufdnatam37wrfc4", Amount = 1000, Reference = "ORD-5023-4E89",
                        Commission = new AmountAllocationCommission { Amount = 1000, Percentage = 1.125 }
                    }
                }
            };

            var json = _serializer.Serialize(order);
            json.ShouldContain("\"amount_allocations\"");
            json.ShouldContain("\"commission\"");

            var deserialized = (Order)_serializer.Deserialize(json, typeof(Order));
            deserialized.AmountAllocations.Count.ShouldBe(1);
            deserialized.AmountAllocations[0].Id.ShouldBe("ent_w4jelhppmfiufdnatam37wrfc4");
            deserialized.AmountAllocations[0].Amount.ShouldBe(1000);
            deserialized.AmountAllocations[0].Reference.ShouldBe("ORD-5023-4E89");
            deserialized.AmountAllocations[0].Commission.Amount.ShouldBe(1000);
            deserialized.AmountAllocations[0].Commission.Percentage.ShouldBe(1.125);
        }
    }
}
