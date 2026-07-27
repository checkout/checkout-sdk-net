using System.Collections.Generic;
using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.Documents;
using Checkout.Accounts.Entities.Request;
using Checkout.Common;
using Shouldly;
using Xunit;

namespace Checkout.Accounts
{
    public class AccountsV3SerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

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

            var json = _serializer.Serialize(processingDetails);

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

            var json = _serializer.Serialize(agreedTerms);

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

            var json = _serializer.Serialize(company);

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

            var json = _serializer.Serialize(representative);

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

            var json = _serializer.Serialize(documents);

            json.ShouldContain("\"financial_statements\"");
            json.ShouldContain("\"front\"");
        }
    }
}
