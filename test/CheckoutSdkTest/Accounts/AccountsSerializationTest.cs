using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.Documents;
using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Common;
using Newtonsoft.Json.Linq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
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
        // Representative documents (company.representatives[].documents)
        // The EEA Sole Trader (3.0) keys, the company-variant certified authorised
        // signatory, and the v2.0 US representative identification. The representative
        // object is strict on the API, so the exact key set matters.
        // ------------------------------------------------------------------------

        // Regression: EEA Sole Trader (3.0) needs proof_of_residential_address and
        // proof_of_registration on the representative, with bank_verification alone at the top level.
        [Fact]
        public void ShouldSerializeEeaSoleTraderRepresentativeDocuments()
        {
            var request = new OnboardEntityRequest
            {
                Reference = "ref_sole_trader",
                Company = new Company
                {
                    BusinessType = BusinessType.IndividualOrSoleProprietorship,
                    Representatives = new List<Representative>
                    {
                        new Representative
                        {
                            Individual = new Individual { FirstName = "Jane", LastName = "Doe" },
                            Roles = new List<EntityRoles> { EntityRoles.Ubo },
                            Documents = BuildEeaSoleTraderRepresentativeDocuments()
                        }
                    }
                },
                Documents = new Checkout.Accounts.Entities.Common.Documents.Documents
                {
                    BankVerification = new BankVerification
                    {
                        Type = BankVerificationType.BankStatement, Front = "file_bankverificationaaaaaaaaaa"
                    }
                }
            };

            var body = Serializer.Serialize(request);
            var json = JObject.Parse(body);

            JToken.DeepEquals(json["company"]["representatives"][0]["documents"], JObject.Parse(@"{
                ""identity_verification"":        { ""type"": ""passport"",                    ""front"": ""file_identityverificationaaaaaa"" },
                ""proof_of_residential_address"": { ""type"": ""proof_of_address"",            ""front"": ""file_proofofresidentialaddressa"" },
                ""proof_of_registration"":        { ""type"": ""extract_from_trade_register"", ""front"": ""file_proofofregistrationaaaaaaa"" }
            }")).ShouldBeTrue();
            ((JObject)json["documents"]).Properties().Select(p => p.Name)
                .ShouldBe(new[] { "bank_verification" });

            // Key-level check on the raw body, so a naming-strategy change cannot pass silently.
            body.ShouldContain("\"proof_of_residential_address\":{");
            body.ShouldContain("\"proof_of_registration\":{");
        }

        [Fact]
        public void ShouldRoundTripRepresentativeDocuments()
        {
            var original = new Representative
            {
                Roles = new List<EntityRoles> { EntityRoles.Ubo },
                Documents = BuildEeaSoleTraderRepresentativeDocuments()
            };

            var deserialized = (Representative)Serializer
                .Deserialize(Serializer.Serialize(original), typeof(Representative));

            var documents = deserialized.Documents;
            documents.IdentityVerification.Type.ShouldBe(IdentityVerificationType.Passport);
            documents.IdentityVerification.Front.ShouldBe("file_identityverificationaaaaaa");
            documents.ProofOfResidentialAddress.Type.ShouldBe(ProofOfResidentialAddressType.ProofOfAddress);
            documents.ProofOfResidentialAddress.Front.ShouldBe("file_proofofresidentialaddressa");
            documents.ProofOfRegistration.Type.ShouldBe(ProofOfRegistrationType.ExtractFromTradeRegister);
            documents.ProofOfRegistration.Front.ShouldBe("file_proofofregistrationaaaaaaa");
        }

        [Fact]
        public void ShouldDeserializeProofOfRegistrationOtherType()
        {
            const string json =
                @"{ ""proof_of_registration"": { ""type"": ""other"", ""front"": ""file_proofofregistrationaaaaaaa"" } }";

            var documents = (Checkout.Accounts.Entities.Common.Documents.Documents)Serializer
                .Deserialize(json, typeof(Checkout.Accounts.Entities.Common.Documents.Documents));

            documents.ProofOfRegistration.Type.ShouldBe(ProofOfRegistrationType.Other);
        }

        [Fact]
        public void ShouldSerializeCertifiedAuthorisedSignatoryWithTypeAndFrontOnly()
        {
            var representative = new Representative
            {
                Roles = new List<EntityRoles> { EntityRoles.LegalRepresentative },
                Documents = new Checkout.Accounts.Entities.Common.Documents.Documents
                {
                    CertifiedAuthorisedSignatory = new CertifiedAuthorisedSignatory
                    {
                        Type = CertifiedAuthorisedSignatoryType.PowerOfAttorney,
                        Front = "file_signatoryaaaaaaaaaaaaaaaaa"
                    }
                }
            };

            var json = JObject.Parse(Serializer.Serialize(representative));

            JToken.DeepEquals(json["documents"], JObject.Parse(@"{
                ""certified_authorised_signatory"": { ""type"": ""power_of_attorney"", ""front"": ""file_signatoryaaaaaaaaaaaaaaaaa"" }
            }")).ShouldBeTrue();
        }

        [Fact]
        public void ShouldSerializeV2UsRepresentativeIdentification()
        {
            var representative = new Representative
            {
                FirstName = "John",
                LastName = "Doe",
                Identification = new Identification { NationalIdNumber = "123456789" }
            };

            var json = JObject.Parse(Serializer.Serialize(representative));

            JToken.DeepEquals(json["identification"], JObject.Parse(@"{ ""national_id_number"": ""123456789"" }"))
                .ShouldBeTrue();
        }

        // Every property of Documents, so a naming-strategy change on any key (the digit in
        // additional_document1 included) cannot pass silently, then a full round trip.
        [Fact]
        public void ShouldSerializeAndRoundTripEveryDocumentsProperty()
        {
            const string file = "file_aaaaaaaaaaaaaaaaaaaaaaaaaa";
            var documents = new Checkout.Accounts.Entities.Common.Documents.Documents
            {
                ArticlesOfAssociation = new ArticlesOfAssociation
                    { Type = ArticlesOfAssociationType.ArticlesOfAssociation, Front = file },
                ShareholderStructure = new ShareholderStructure
                    { Type = ShareholderStructureType.CertifiedShareholderStructure, Front = file },
                CompanyVerification = new CompanyVerification
                    { Type = CompanyVerificationType.IncorporationDocument, Front = file },
                BankVerification = new BankVerification { Type = BankVerificationType.BankStatement, Front = file },
                ProofOfLegality = new ProofOfLegality { Type = ProofOfLegalityType.ProofOfLegality, Front = file },
                ProofOfPrincipalAddress = new ProofOfPrincipalAddress
                    { Type = ProofOfPrincipalAddressType.ProofOfAddress, Front = file },
                AdditionalDocument1 = new AdditionalDocument { Front = file },
                AdditionalDocument2 = new AdditionalDocument { Front = file },
                AdditionalDocument3 = new AdditionalDocument { Front = file },
                TaxVerification = new TaxVerification { Type = TaxVerificationType.EinLetter, Front = file },
                FinancialVerification = new FinancialVerification
                    { Type = FinancialVerificationType.FinancialStatement, Front = file },
                FinancialStatements = new FinancialStatements
                    { Type = FinancialStatementsType.FinancialStatements, Front = file },
                IdentityVerification = new IdentityVerification
                    { Type = IdentityVerificationType.Passport, Front = file, Back = file },
                CertifiedAuthorisedSignatory = new CertifiedAuthorisedSignatory
                    { Type = CertifiedAuthorisedSignatoryType.PowerOfAttorney, Front = file },
                ProofOfResidentialAddress = new ProofOfResidentialAddress
                    { Type = ProofOfResidentialAddressType.ProofOfAddress, Front = file },
                ProofOfRegistration = new ProofOfRegistration
                    { Type = ProofOfRegistrationType.ExtractFromTradeRegister, Front = file }
            };

            var json = Serializer.Serialize(documents);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""articles_of_association"":        { ""type"": ""articles_of_association"",         ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""shareholder_structure"":          { ""type"": ""certified_shareholder_structure"", ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""company_verification"":           { ""type"": ""incorporation_document"",          ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""bank_verification"":              { ""type"": ""bank_statement"",                  ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""proof_of_legality"":              { ""type"": ""proof_of_legality"",               ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""proof_of_principal_address"":     { ""type"": ""proof_of_address"",                ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""additional_document1"":           { ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""additional_document2"":           { ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""additional_document3"":           { ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""tax_verification"":               { ""type"": ""ein_letter"",                      ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""financial_verification"":         { ""type"": ""financial_statement"",             ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""financial_statements"":           { ""type"": ""financial_statements"",            ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""identity_verification"":          { ""type"": ""passport"", ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"", ""back"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""certified_authorised_signatory"": { ""type"": ""power_of_attorney"",               ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""proof_of_residential_address"":   { ""type"": ""proof_of_address"",                ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" },
                ""proof_of_registration"":          { ""type"": ""extract_from_trade_register"",     ""front"": ""file_aaaaaaaaaaaaaaaaaaaaaaaaaa"" }
            }")).ShouldBeTrue(json);

            var roundTripped = (Checkout.Accounts.Entities.Common.Documents.Documents)Serializer
                .Deserialize(json, typeof(Checkout.Accounts.Entities.Common.Documents.Documents));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        private static Checkout.Accounts.Entities.Common.Documents.Documents BuildEeaSoleTraderRepresentativeDocuments()
        {
            return new Checkout.Accounts.Entities.Common.Documents.Documents
            {
                IdentityVerification = new IdentityVerification
                {
                    Type = IdentityVerificationType.Passport, Front = "file_identityverificationaaaaaa"
                },
                ProofOfResidentialAddress = new ProofOfResidentialAddress
                {
                    Type = ProofOfResidentialAddressType.ProofOfAddress, Front = "file_proofofresidentialaddressa"
                },
                ProofOfRegistration = new ProofOfRegistration
                {
                    Type = ProofOfRegistrationType.ExtractFromTradeRegister, Front = "file_proofofregistrationaaaaaaa"
                }
            };
        }

        // ------------------------------------------------------------------------
        // OnboardEntityDetailsResponse.ProcessingDetails
        // Regression: the amounts are integers in minor units with no maximum. Typed as
        // int, any value above 2,147,483,647 (about 21.4 million in a two-decimal
        // currency) made the whole GET /accounts/entities/{id} fail to deserialize.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeProcessingDetailsAmountsAboveIntRange()
        {
            const string json = @"{ ""processing_details"": {
                ""settlement_country"": ""GB"", ""target_countries"": [""GB""], ""currency"": ""USD"",
                ""annual_processing_volume"": 3000000000,
                ""average_transaction_value"": 2500000000,
                ""highest_transaction_value"": 9000000000 } }";

            var response = (OnboardEntityDetailsResponse)Serializer.Deserialize(json, typeof(OnboardEntityDetailsResponse));

            response.ProcessingDetails.AnnualProcessingVolume.ShouldBe(3000000000L);
            response.ProcessingDetails.AverageTransactionValue.ShouldBe(2500000000L);
            response.ProcessingDetails.HighestTransactionValue.ShouldBe(9000000000L);
            response.ProcessingDetails.Currency.ShouldBe(Currency.USD);
            response.ProcessingDetails.SettlementCountry.ShouldBe("GB");
            response.ProcessingDetails.TargetCountries.ShouldBe(new[] { "GB" });
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
