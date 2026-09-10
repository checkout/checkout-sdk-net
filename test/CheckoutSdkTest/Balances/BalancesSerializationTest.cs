using Checkout.Common;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Balances
{
    public class BalancesSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // TopUpInstructionsResponse / TopUpBankDetails / TopUpFundingDetails
        //
        // Covers GET /entities/{entityId}/currency-accounts/{currencyAccountId}/top-up-instructions.
        // Every value below is taken from the spec's field-level "example" values in
        // shared/swagger-latest.json; the response schema carries no top-level example.
        //
        // The spec is explicit that neither funding rail is guaranteed: TopUpBankDetails declares
        // no "required" array at all, so domestic-only, international-only and empty bank_details
        // are all legal 200 bodies. Each has its own test.
        // ------------------------------------------------------------------------

        private const string BothRailsJson = @"{
            ""currency_account_id"": ""ca_g5y7d6jo4e2urgforcbf2ey5jm"",
            ""currency"": ""USD"",
            ""payment_reference"": ""TP-ABC123"",
            ""bank_details"": {
                ""domestic"": {
                    ""beneficiary_account_name"": ""Acme Inc"",
                    ""beneficiary_address"": ""1 Example Street, Exampleville, EX, 00000, US"",
                    ""bank_name"": ""Example Bank"",
                    ""bank_address"": ""1 Example Street, Exampleville, EX, 00000, US"",
                    ""account_number"": ""1234567890"",
                    ""sort_code"": ""000000"",
                    ""routing_number"": ""000000000"",
                    ""iban"": ""GB00EXAM00000000000000"",
                    ""swift_code"": ""TESTUS00XXX""
                },
                ""international"": {
                    ""beneficiary_account_name"": ""Acme Inc"",
                    ""beneficiary_address"": ""1 Example Street, Exampleville, EX, 00000, US"",
                    ""bank_name"": ""Example Bank"",
                    ""bank_address"": ""1 Example Street, Exampleville, EX, 00000, US"",
                    ""account_number"": ""1234567890"",
                    ""sort_code"": ""000000"",
                    ""routing_number"": ""000000000"",
                    ""iban"": ""GB00EXAM00000000000000"",
                    ""swift_code"": ""TESTUS00XXX""
                }
            }
        }";

        private static TopUpFundingDetails CreateFullyPopulatedFundingDetails()
        {
            return new TopUpFundingDetails
            {
                BeneficiaryAccountName = "Acme Inc",
                BeneficiaryAddress = "1 Example Street, Exampleville, EX, 00000, US",
                BankName = "Example Bank",
                BankAddress = "1 Example Street, Exampleville, EX, 00000, US",
                AccountNumber = "1234567890",
                SortCode = "000000",
                RoutingNumber = "000000000",
                Iban = "GB00EXAM00000000000000",
                SwiftCode = "TESTUS00XXX"
            };
        }

        private static TopUpInstructionsResponse CreateFullyPopulatedTopUpInstructions()
        {
            return new TopUpInstructionsResponse
            {
                CurrencyAccountId = "ca_g5y7d6jo4e2urgforcbf2ey5jm",
                Currency = Common.Currency.USD,
                PaymentReference = "TP-ABC123",
                BankDetails = new TopUpBankDetails
                {
                    Domestic = CreateFullyPopulatedFundingDetails(),
                    International = CreateFullyPopulatedFundingDetails()
                }
            };
        }

        private static void AssertFullyPopulatedFundingDetails(TopUpFundingDetails details)
        {
            details.ShouldNotBeNull();
            details.BeneficiaryAccountName.ShouldBe("Acme Inc");
            details.BeneficiaryAddress.ShouldBe("1 Example Street, Exampleville, EX, 00000, US");
            details.BankName.ShouldBe("Example Bank");
            details.BankAddress.ShouldBe("1 Example Street, Exampleville, EX, 00000, US");
            details.AccountNumber.ShouldBe("1234567890");
            details.SortCode.ShouldBe("000000");
            details.RoutingNumber.ShouldBe("000000000");
            details.Iban.ShouldBe("GB00EXAM00000000000000");
            details.SwiftCode.ShouldBe("TESTUS00XXX");
        }

        [Fact]
        public void ShouldSerializeWithRequiredPropertiesForTopUpInstructionsResponse()
        {
            var response = new TopUpInstructionsResponse
            {
                CurrencyAccountId = "ca_g5y7d6jo4e2urgforcbf2ey5jm",
                Currency = Common.Currency.USD,
                PaymentReference = "TP-ABC123",
                BankDetails = new TopUpBankDetails()
            };

            Should.NotThrow(() => Serializer.Serialize(response));
        }

        [Fact]
        public void ShouldSerializeAllPropertiesToSnakeCaseForTopUpInstructionsResponse()
        {
            var json = Serializer.Serialize(CreateFullyPopulatedTopUpInstructions());

            json.ShouldContain("\"currency_account_id\":\"ca_g5y7d6jo4e2urgforcbf2ey5jm\"");
            json.ShouldContain("\"currency\":\"USD\"");
            json.ShouldContain("\"payment_reference\":\"TP-ABC123\"");
            json.ShouldContain("\"bank_details\":");
            json.ShouldContain("\"domestic\":");
            json.ShouldContain("\"international\":");
            json.ShouldContain("\"beneficiary_account_name\":\"Acme Inc\"");
            json.ShouldContain("\"beneficiary_address\":\"1 Example Street, Exampleville, EX, 00000, US\"");
            json.ShouldContain("\"bank_name\":\"Example Bank\"");
            json.ShouldContain("\"bank_address\":\"1 Example Street, Exampleville, EX, 00000, US\"");
            json.ShouldContain("\"account_number\":\"1234567890\"");
            json.ShouldContain("\"sort_code\":\"000000\"");
            json.ShouldContain("\"routing_number\":\"000000000\"");
            json.ShouldContain("\"iban\":\"GB00EXAM00000000000000\"");
            json.ShouldContain("\"swift_code\":\"TESTUS00XXX\"");
        }

        [Fact]
        public void ShouldDeserializeSpecExampleForTopUpInstructionsResponse()
        {
            var response = (TopUpInstructionsResponse)Serializer
                .Deserialize(BothRailsJson, typeof(TopUpInstructionsResponse));

            response.ShouldNotBeNull();
            response.CurrencyAccountId.ShouldBe("ca_g5y7d6jo4e2urgforcbf2ey5jm");
            response.Currency.ShouldBe(Common.Currency.USD);
            response.PaymentReference.ShouldBe("TP-ABC123");
            response.BankDetails.ShouldNotBeNull();
            AssertFullyPopulatedFundingDetails(response.BankDetails.Domestic);
            AssertFullyPopulatedFundingDetails(response.BankDetails.International);
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForTopUpInstructionsResponse()
        {
            var original = CreateFullyPopulatedTopUpInstructions();

            var json = Serializer.Serialize(original);
            var deserialized = (TopUpInstructionsResponse)Serializer
                .Deserialize(json, typeof(TopUpInstructionsResponse));

            deserialized.CurrencyAccountId.ShouldBe(original.CurrencyAccountId);
            deserialized.Currency.ShouldBe(original.Currency);
            deserialized.PaymentReference.ShouldBe(original.PaymentReference);
            AssertFullyPopulatedFundingDetails(deserialized.BankDetails.Domestic);
            AssertFullyPopulatedFundingDetails(deserialized.BankDetails.International);
        }

        [Fact]
        public void ShouldDeserializeDomesticOnlyResponseForTopUpInstructionsResponse()
        {
            // A United States domestic rail: account_number + routing_number, per the spec's
            // "Returned for United States domestic transfers" on routing_number.
            const string json = @"{
                ""currency_account_id"": ""ca_g5y7d6jo4e2urgforcbf2ey5jm"",
                ""currency"": ""USD"",
                ""payment_reference"": ""TP-ABC123"",
                ""bank_details"": {
                    ""domestic"": {
                        ""beneficiary_account_name"": ""Acme Inc"",
                        ""bank_name"": ""Example Bank"",
                        ""account_number"": ""1234567890"",
                        ""routing_number"": ""000000000""
                    }
                }
            }";

            var response = (TopUpInstructionsResponse)Serializer
                .Deserialize(json, typeof(TopUpInstructionsResponse));

            response.BankDetails.ShouldNotBeNull();
            response.BankDetails.International.ShouldBeNull();
            response.BankDetails.Domestic.ShouldNotBeNull();
            response.BankDetails.Domestic.BeneficiaryAccountName.ShouldBe("Acme Inc");
            response.BankDetails.Domestic.BankName.ShouldBe("Example Bank");
            response.BankDetails.Domestic.AccountNumber.ShouldBe("1234567890");
            response.BankDetails.Domestic.RoutingNumber.ShouldBe("000000000");
            response.BankDetails.Domestic.SortCode.ShouldBeNull();
            response.BankDetails.Domestic.Iban.ShouldBeNull();
            response.BankDetails.Domestic.SwiftCode.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeInternationalOnlyResponseForTopUpInstructionsResponse()
        {
            // An international rail: iban + swift_code, per the spec's "Returned for
            // international transfers" on swift_code.
            const string json = @"{
                ""currency_account_id"": ""ca_g5y7d6jo4e2urgforcbf2ey5jm"",
                ""currency"": ""USD"",
                ""payment_reference"": ""TP-ABC123"",
                ""bank_details"": {
                    ""international"": {
                        ""beneficiary_account_name"": ""Acme Inc"",
                        ""bank_name"": ""Example Bank"",
                        ""iban"": ""GB00EXAM00000000000000"",
                        ""swift_code"": ""TESTUS00XXX""
                    }
                }
            }";

            var response = (TopUpInstructionsResponse)Serializer
                .Deserialize(json, typeof(TopUpInstructionsResponse));

            response.BankDetails.ShouldNotBeNull();
            response.BankDetails.Domestic.ShouldBeNull();
            response.BankDetails.International.ShouldNotBeNull();
            response.BankDetails.International.BeneficiaryAccountName.ShouldBe("Acme Inc");
            response.BankDetails.International.BankName.ShouldBe("Example Bank");
            response.BankDetails.International.Iban.ShouldBe("GB00EXAM00000000000000");
            response.BankDetails.International.SwiftCode.ShouldBe("TESTUS00XXX");
            response.BankDetails.International.AccountNumber.ShouldBeNull();
            response.BankDetails.International.RoutingNumber.ShouldBeNull();
            response.BankDetails.International.SortCode.ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeEmptyBankDetailsForTopUpInstructionsResponse()
        {
            // TopUpBankDetails declares no required properties, so an empty object is legal.
            const string json = @"{
                ""currency_account_id"": ""ca_g5y7d6jo4e2urgforcbf2ey5jm"",
                ""currency"": ""USD"",
                ""payment_reference"": ""TP-ABC123"",
                ""bank_details"": {}
            }";

            var response = (TopUpInstructionsResponse)Serializer
                .Deserialize(json, typeof(TopUpInstructionsResponse));

            response.ShouldNotBeNull();
            response.BankDetails.ShouldNotBeNull();
            response.BankDetails.Domestic.ShouldBeNull();
            response.BankDetails.International.ShouldBeNull();
        }

        [Fact]
        public void ShouldOmitUnsetOptionalFundingFieldsForTopUpFundingDetails()
        {
            var response = new TopUpInstructionsResponse
            {
                CurrencyAccountId = "ca_g5y7d6jo4e2urgforcbf2ey5jm",
                Currency = Common.Currency.USD,
                PaymentReference = "TP-ABC123",
                BankDetails = new TopUpBankDetails
                {
                    Domestic = new TopUpFundingDetails
                    {
                        BeneficiaryAccountName = "Acme Inc",
                        BankName = "Example Bank"
                    }
                }
            };

            var json = Serializer.Serialize(response);

            json.ShouldContain("\"beneficiary_account_name\":\"Acme Inc\"");
            json.ShouldContain("\"bank_name\":\"Example Bank\"");
            json.ShouldNotContain("international");
            json.ShouldNotContain("sort_code");
            json.ShouldNotContain("routing_number");
            json.ShouldNotContain("iban");
            json.ShouldNotContain("swift_code");
            json.ShouldNotContain("beneficiary_address");
            json.ShouldNotContain("bank_address");
            json.ShouldNotContain("account_number");
        }

        // ------------------------------------------------------------------------
        // BalancesResponse / CurrencyAccountBalance / Balances / CollateralBreakdown
        //
        // Covers GET /balances/{id}. Values are the spec's field-level "example" values for the
        // Balance and collateral_breakdown schemas in shared/swagger-latest.json.
        //
        // Added when Balance.operational was found missing from the SDK during the INT-1692
        // review; review-integrity.mdc section 9 requires a serialization test for a new property
        // on an existing class.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSpecExampleForBalancesResponse()
        {
            const string json = @"{
                ""data"": [
                    {
                        ""currency_account_id"": ""ca_g5y7d6jo4e2urgforcbf2ey5jm"",
                        ""descriptor"": ""Revenue Account 1"",
                        ""holding_currency"": ""USD"",
                        ""balances_as_of"": ""2026-05-06T13:59:59Z"",
                        ""balances"": {
                            ""pending"": 23000,
                            ""available"": 50000,
                            ""payable"": 2700,
                            ""collateral"": 6000,
                            ""operational"": 7000,
                            ""collateral_breakdown"": {
                                ""fixed_reserve"": 4000,
                                ""rolling_reserve"": 2000
                            }
                        }
                    }
                ]
            }";

            var response = (BalancesResponse)Serializer.Deserialize(json, typeof(BalancesResponse));

            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
            response.Data.Count.ShouldBe(1);

            var balance = response.Data[0];
            balance.CurrencyAccountId.ShouldBe("ca_g5y7d6jo4e2urgforcbf2ey5jm");
            balance.Descriptor.ShouldBe("Revenue Account 1");
            balance.HoldingCurrency.ShouldBe(Common.Currency.USD);
            balance.BalancesAsOf.ShouldBe(DateTime.Parse("2026-05-06T13:59:59Z").ToUniversalTime());

            balance.Balances.ShouldNotBeNull();
            balance.Balances.Pending.ShouldBe(23000L);
            balance.Balances.Available.ShouldBe(50000L);
            balance.Balances.Payable.ShouldBe(2700L);
            balance.Balances.Collateral.ShouldBe(6000L);
            balance.Balances.Operational.ShouldBe(7000L);
            balance.Balances.CollateralBreakdown.ShouldNotBeNull();
            balance.Balances.CollateralBreakdown.FixedReserve.ShouldBe(4000L);
            balance.Balances.CollateralBreakdown.RollingReserve.ShouldBe(2000L);
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForBalances()
        {
            var original = new Balances
            {
                Pending = 23000L,
                Available = 50000L,
                Payable = 2700L,
                Collateral = 6000L,
                Operational = 7000L,
                CollateralBreakdown = new CollateralBreakdown
                {
                    FixedReserve = 4000L,
                    RollingReserve = 2000L
                }
            };

            var json = Serializer.Serialize(original);
            var deserialized = (Balances)Serializer.Deserialize(json, typeof(Balances));

            json.ShouldContain("\"operational\":7000");
            json.ShouldContain("\"collateral_breakdown\":");
            deserialized.Pending.ShouldBe(original.Pending);
            deserialized.Available.ShouldBe(original.Available);
            deserialized.Payable.ShouldBe(original.Payable);
            deserialized.Collateral.ShouldBe(original.Collateral);
            deserialized.Operational.ShouldBe(original.Operational);
            deserialized.CollateralBreakdown.FixedReserve.ShouldBe(original.CollateralBreakdown.FixedReserve);
            deserialized.CollateralBreakdown.RollingReserve.ShouldBe(original.CollateralBreakdown.RollingReserve);
        }
    }
}
