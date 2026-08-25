using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.Documents;
using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Request;
using Checkout.Common;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Accounts
{
    /// <summary>
    /// Schema validation tests for Checkout.Accounts.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class AccountsSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // AccountsV3
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeProcessingDetailsWithPayments()
        {
            var processingDetails = new ProcessingDetails
            {
                AnnualProcessingVolume = 1000000,
                AverageTransactionValue = 5000,
                AverageOrderFulfillmentTime = 3,
                HighestTransactionValue = 25000,
                Currency = Currency.GBP,
                SettlementCountry = "GB",
                TargetCountries = new List<string> { "GB" },
                Payments = new ProcessingDetailsPayments
                {
                    Ach = new ProcessingDetailsAch
                    {
                        AnnualAchVolume = 1000000,
                        AverageAchTransactionSize = 5000,
                        EstimatedMonthlyCreditVolume = 100000,
                        AverageCreditAmount = 5000
                    }
                }
            };

            var json = Serializer.Serialize(processingDetails);

            json.ShouldContain("\"annual_processing_volume\"");
            json.ShouldContain("\"average_order_fulfillment_time\"");
            json.ShouldContain("\"highest_transaction_value\"");
            json.ShouldContain("\"settlement_country\"");
            json.ShouldContain("\"target_countries\"");
            json.ShouldContain("\"payments\"");
            json.ShouldContain("\"ach\"");
            json.ShouldContain("\"annual_ach_volume\"");
            json.ShouldContain("\"average_ach_transaction_size\"");
            json.ShouldContain("\"estimated_monthly_credit_volume\"");
            json.ShouldContain("\"average_credit_amount\"");
        }

        [Fact]
        public void ShouldSerializeAgreedTerms()
        {
            var agreedTerms = new AgreedTerms
            {
                Date = "2026-07-20T10:00:00Z",
                IpAddress = "203.0.113.42",
                Name = "John Representative",
                Email = "john@example.com",
                Version = "1.0"
            };

            var json = Serializer.Serialize(agreedTerms);

            json.ShouldContain("\"date\"");
            json.ShouldContain("\"ip_address\"");
            json.ShouldContain("\"name\"");
            json.ShouldContain("\"email\"");
            json.ShouldContain("\"version\"");
        }

        [Fact]
        public void ShouldSerializeCompanyV3Fields()
        {
            var company = new Company
            {
                LegalName = "Super Hero Masks Inc.",
                TradingName = "Super Hero Masks",
                BusinessRegistrationNumber = "01234567",
                BusinessType = BusinessType.LimitedCompany,
                AdditionalTradingNames = new List<string> { "SHM" },
                IsRegisteredCompany = true,
                DateOfIncorporation = new DateOfIncorporation { Day = 1, Month = 6, Year = 2010 }
            };

            var json = Serializer.Serialize(company);

            json.ShouldContain("\"additional_trading_names\"");
            json.ShouldContain("\"is_registered_company\"");
            json.ShouldContain("\"business_type\"");
            json.ShouldContain("limited_company");
            json.ShouldContain("\"date_of_incorporation\"");
            json.ShouldContain("\"day\"");
        }

        [Fact]
        public void ShouldSerializeRepresentativeV3Fields()
        {
            var representative = new Representative
            {
                Id = "rep_00000000000000000000000000",
                Individual = new Individual
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Citizenships = new List<Citizenship>
                    {
                        new Citizenship { Type = "citizenship", Country = CountryCode.US }
                    },
                    NationalIdType = NationalIdType.Ssn,
                    NationalIdNumber = "AB123456C"
                },
                CompanyPosition = CompanyPositionType.CEO,
                OwnershipPercentage = 100,
                Roles = new List<EntityRoles>
                {
                    EntityRoles.Ubo,
                    EntityRoles.AuthorisedSignatory,
                    EntityRoles.Director,
                    EntityRoles.ControlPerson
                }
            };

            var json = Serializer.Serialize(representative);

            json.ShouldContain("\"individual\"");
            json.ShouldContain("\"citizenships\"");
            json.ShouldContain("\"country\"");
            json.ShouldContain("\"national_id_type\"");
            json.ShouldContain("ssn");
            json.ShouldContain("\"company_position\"");
            json.ShouldContain("ceo");
            json.ShouldContain("\"ownership_percentage\"");
            json.ShouldContain("director");
            json.ShouldContain("control_person");
        }

        [Fact]
        public void ShouldSerializeFinancialStatementsDocument()
        {
            var documents = new Checkout.Accounts.Entities.Common.Documents.Documents
            {
                FinancialStatements = new FinancialStatements
                {
                    Type = FinancialStatementsType.FinancialStatements,
                    Front = "file_00000000000000000000000000"
                }
            };

            var json = Serializer.Serialize(documents);

            json.ShouldContain("\"financial_statements\"");
            json.ShouldContain("\"front\"");
        }

        // ------------------------------------------------------------------------
        // InstrumentDetailsAch
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithAllProperties()
        {
            var details = new InstrumentDetailsAch
            {
                AccountNumber = "12345100",
                RoutingNumber = "026009593",
                AccountType = InstrumentAccountType.Savings
            };

            var json = Serializer.Serialize(details);

            json.ShouldContain("\"account_number\"");
            json.ShouldContain("12345100");
            json.ShouldContain("\"routing_number\"");
            json.ShouldContain("026009593");
            json.ShouldContain("\"account_type\"");
            json.ShouldContain("savings");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExample()
        {
            const string json = @"{ ""account_number"": ""12345100"", ""routing_number"": ""026009593"", ""account_type"": ""checking"" }";

            var details = (InstrumentDetailsAch)Serializer.Deserialize(json, typeof(InstrumentDetailsAch));

            details.ShouldNotBeNull();
            details.AccountNumber.ShouldBe("12345100");
            details.RoutingNumber.ShouldBe("026009593");
            details.AccountType.ShouldBe(InstrumentAccountType.Checking);
        }

        [Fact]
        public void ShouldRoundTripSerialize()
        {
            var original = new InstrumentDetailsAch
            {
                AccountNumber = "98765432", RoutingNumber = "123456789", AccountType = InstrumentAccountType.Savings
            };

            var deserialized = (InstrumentDetailsAch)Serializer
                .Deserialize(Serializer.Serialize(original), typeof(InstrumentDetailsAch));

            deserialized.AccountNumber.ShouldBe(original.AccountNumber);
            deserialized.RoutingNumber.ShouldBe(original.RoutingNumber);
            deserialized.AccountType.ShouldBe(original.AccountType);
        }
    }
}
