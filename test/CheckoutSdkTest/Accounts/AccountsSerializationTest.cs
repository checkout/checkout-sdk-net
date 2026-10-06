using Checkout.Accounts.Entities.Common.Company;
using Checkout.Accounts.Entities.Common.ContactDetails;
using Checkout.Accounts.Entities.Common.Documents;
using Checkout.Accounts.Entities.Common;
using Checkout.Accounts.Entities.Request;
using Checkout.Accounts.Entities.Response;
using Checkout.Common;
using Checkout.Files;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
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
        public void ShouldSerializeAndRoundTripProcessingDetailsWithEveryProperty()
        {
            var processingDetails = new ProcessingDetails
            {
                AnnualProcessingVolume = 1000000,
                AverageTransactionValue = 5000,
                AverageOrderFulfillmentTime = 3,
                HighestTransactionValue = 25000,
                Currency = Currency.GBP,
                SettlementCountry = "GB",
                TargetCountries = new List<string> { "GB", "IE" },
                Payments = new ProcessingDetailsPayments
                {
                    Ach = new ProcessingDetailsAch
                    {
                        AnnualAchVolume = 1000000,
                        AverageAchTransactionSize = 5000,
                        EstimatedMonthlyCreditVolume = 100000,
                        AverageCreditAmount = 4000
                    }
                }
            };

            var json = Serializer.Serialize(processingDetails);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""annual_processing_volume"": 1000000,
                ""average_transaction_value"": 5000,
                ""average_order_fulfillment_time"": 3,
                ""highest_transaction_value"": 25000,
                ""currency"": ""GBP"",
                ""settlement_country"": ""GB"",
                ""target_countries"": [""GB"", ""IE""],
                ""payments"": { ""ach"": {
                    ""annual_ach_volume"": 1000000,
                    ""average_ach_transaction_size"": 5000,
                    ""estimated_monthly_credit_volume"": 100000,
                    ""average_credit_amount"": 4000 } }
            }")).ShouldBeTrue(json);

            var roundTripped = (ProcessingDetails)Serializer.Deserialize(json, typeof(ProcessingDetails));
            Serializer.Serialize(roundTripped).ShouldBe(json);
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

            // The date is a plain string on the wire, so compare the raw text too.
            json.ShouldContain("\"date\":\"2026-07-20T10:00:00Z\"");
            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""date"": ""2026-07-20T10:00:00Z"",
                ""ip_address"": ""203.0.113.42"",
                ""name"": ""John Representative"",
                ""email"": ""john@example.com"",
                ""version"": ""1.0""
            }")).ShouldBeTrue(json);

            var roundTripped = (AgreedTerms)Serializer.Deserialize(json, typeof(AgreedTerms));
            Serializer.Serialize(roundTripped).ShouldBe(json);
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

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""legal_name"": ""Super Hero Masks Inc."",
                ""trading_name"": ""Super Hero Masks"",
                ""business_registration_number"": ""01234567"",
                ""business_type"": ""limited_company"",
                ""additional_trading_names"": [""SHM""],
                ""is_registered_company"": true,
                ""date_of_incorporation"": { ""day"": 1, ""month"": 6, ""year"": 2010 }
            }")).ShouldBeTrue(json);
        }

        [Fact]
        public void ShouldSerializeRepresentativeV3Fields()
        {
            var representative = new Representative
            {
                Id = "rep_el68fmd58vhtmybdpgprrsjbi1",
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

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""id"": ""rep_el68fmd58vhtmybdpgprrsjbi1"",
                ""individual"": {
                    ""first_name"": ""John"",
                    ""last_name"": ""Doe"",
                    ""citizenships"": [ { ""type"": ""citizenship"", ""country"": ""US"" } ],
                    ""national_id_type"": ""ssn"",
                    ""national_id_number"": ""AB123456C""
                },
                ""company_position"": ""ceo"",
                ""ownership_percentage"": 100,
                ""roles"": [""ubo"", ""authorised_signatory"", ""director"", ""control_person""]
            }")).ShouldBeTrue(json);
        }

        [Fact]
        public void ShouldSerializeFinancialStatementsDocument()
        {
            var documents = new Checkout.Accounts.Entities.Common.Documents.Documents
            {
                FinancialStatements = new FinancialStatements
                {
                    Type = FinancialStatementsType.FinancialStatements,
                    Front = "file_lar6adgusdx3brx7wsd6shelfv"
                }
            };

            var json = Serializer.Serialize(documents);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""financial_statements"": { ""type"": ""financial_statements"", ""front"": ""file_lar6adgusdx3brx7wsd6shelfv"" }
            }")).ShouldBeTrue(json);
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
        // EmailAddresses
        // US ISV Seller (3.0) requires pci_compliance_contact alongside primary.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAndRoundTripEmailAddressesWithPciComplianceContact()
        {
            var original = new EmailAddresses
            {
                Primary = "admin@example.com", PciComplianceContact = "pci@example.com"
            };

            var json = Serializer.Serialize(original);

            json.ShouldContain("\"primary\":\"admin@example.com\"");
            json.ShouldContain("\"pci_compliance_contact\":\"pci@example.com\"");

            var deserialized = (EmailAddresses)Serializer.Deserialize(json, typeof(EmailAddresses));

            deserialized.Primary.ShouldBe(original.Primary);
            deserialized.PciComplianceContact.ShouldBe(original.PciComplianceContact);
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

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""account_number"": ""12345100"", ""routing_number"": ""026009593"", ""account_type"": ""savings""
            }")).ShouldBeTrue(json);
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

        // ------------------------------------------------------------------------
        // OnboardEntityRequest swagger examples
        // The full request examples of USISVSellerCompany3-0 and USISVSellerSoleTrader3-0,
        // copied verbatim from the spec. Deserializing then serializing must give back the
        // same document, so no key is dropped, renamed or reshaped on the way.
        // ------------------------------------------------------------------------

        private const string UsIsvSellerCompanySwaggerExample = @"{
              ""reference"": ""isv-seller-example001"",
              ""agreed_terms"": {
                ""date"": ""2026-07-02T10:30:00.0000000+00:00"",
                ""ip_address"": ""8.8.8.8"",
                ""name"": ""Toby Arden"",
                ""email"": ""toby.arden@example.com"",
                ""version"": ""cko-platform-terms-1.0.0""
              },
              ""seller_category"": ""cat_retail_001"",
              ""processing_details"": {
                ""annual_processing_volume"": 1000,
                ""average_transaction_value"": 2000,
                ""average_order_fulfillment_time"": 3,
                ""target_countries"": [""US""],
                ""currency"": ""USD"",
                ""payments"": {
                  ""ach"": {
                    ""annual_ach_volume"": 100000,
                    ""average_ach_transaction_size"": 5000,
                    ""estimated_monthly_credit_volume"": 50000,
                    ""average_credit_amount"": 2500
                  }
                }
              },
              ""contact_details"": {
                ""phone"": {""number"": ""4155678900"", ""country_code"": ""US""},
                ""email_addresses"": {""primary"": ""toby.arden@example.com"", ""pci_compliance_contact"": ""pci.contact@example.com""}
              },
              ""profile"": {
                ""urls"": [""https://www.isv-seller-example.com""],
                ""mccs"": [""5551""],
                ""holding_currencies"": [""USD""],
                ""default_holding_currency"": ""USD""
              },
              ""company"": {
                ""business_registration_number"": ""12-3456789"",
                ""business_type"": ""private_corporation"",
                ""legal_name"": ""ISV Seller Example Inc"",
                ""trading_name"": ""ISV Seller Example"",
                ""registered_address"": {
                  ""address_line1"": ""123 Main Street"",
                  ""city"": ""San Francisco"",
                  ""state"": ""CA"",
                  ""zip"": ""94105"",
                  ""country"": ""US""
                },
                ""principal_address"": {
                  ""address_line1"": ""123 Main Street"",
                  ""city"": ""San Francisco"",
                  ""state"": ""CA"",
                  ""zip"": ""94105"",
                  ""country"": ""US""
                },
                ""date_of_incorporation"": {""year"": 2025, ""month"": 10, ""day"": 1},
                ""representatives"": [
                  {
                    ""roles"": [""ubo"", ""control_person""],
                    ""ownership_percentage"": 25,
                    ""company_position"": ""ceo"",
                    ""individual"": {
                      ""first_name"": ""Toby"",
                      ""last_name"": ""Arden"",
                      ""email_address"": ""toby.arden@example.com"",
                      ""national_id_type"": ""ssn"",
                      ""national_id_number"": ""123456789"",
                      ""date_of_birth"": {""day"": 15, ""month"": 1, ""year"": 1990},
                      ""place_of_birth"": {""country"": ""US""},
                      ""citizenships"": [{""country"": ""US""}],
                      ""phone"": {""country_code"": ""US"", ""number"": ""4155678901""},
                      ""address"": {
                        ""address_line1"": ""123 Main Street"",
                        ""city"": ""San Francisco"",
                        ""state"": ""CA"",
                        ""zip"": ""94105"",
                        ""country"": ""US""
                      }
                    }
                  },
                  {
                    ""roles"": [""authorised_signatory""],
                    ""individual"": {
                      ""first_name"": ""Alex"",
                      ""last_name"": ""Morgan"",
                      ""email_address"": ""alex.morgan@example.com"",
                      ""national_id_type"": ""ssn"",
                      ""national_id_number"": ""987654321"",
                      ""date_of_birth"": {""day"": 22, ""month"": 6, ""year"": 1985},
                      ""place_of_birth"": {""country"": ""US""},
                      ""citizenships"": [{""country"": ""US""}],
                      ""phone"": {""country_code"": ""US"", ""number"": ""4155678902""},
                      ""address"": {
                        ""address_line1"": ""123 Main Street"",
                        ""city"": ""San Francisco"",
                        ""state"": ""CA"",
                        ""zip"": ""94105"",
                        ""country"": ""US""
                      }
                    }
                  }
                ]
              }
            }";

        private const string UsIsvSellerSoleTraderSwaggerExample = @"{
              ""reference"": ""isv-sole-trader-example001"",
              ""agreed_terms"": {
                ""date"": ""2026-07-02T10:30:00.0000000+00:00"",
                ""ip_address"": ""8.8.8.8"",
                ""name"": ""Hannah Bret"",
                ""email"": ""hannah.bret@example.com"",
                ""version"": ""cko-platform-terms-1.0.0""
              },
              ""seller_category"": ""cat_retail_001"",
              ""processing_details"": {
                ""annual_processing_volume"": 1000,
                ""average_transaction_value"": 2000,
                ""average_order_fulfillment_time"": 3,
                ""target_countries"": [""US""],
                ""currency"": ""USD"",
                ""payments"": {
                  ""ach"": {
                    ""annual_ach_volume"": 100000,
                    ""average_ach_transaction_size"": 5000,
                    ""estimated_monthly_credit_volume"": 50000,
                    ""average_credit_amount"": 2500
                  }
                }
              },
              ""contact_details"": {
                ""phone"": {""number"": ""4155678900"", ""country_code"": ""US""},
                ""email_addresses"": {""primary"": ""hannah.bret@example.com"", ""pci_compliance_contact"": ""pci.contact@example.com""}
              },
              ""profile"": {
                ""urls"": [""https://www.isv-sole-trader-example.com""],
                ""mccs"": [""5551""],
                ""holding_currencies"": [""USD""],
                ""default_holding_currency"": ""USD""
              },
              ""company"": {
                ""business_type"": ""individual_or_sole_proprietorship"",
                ""is_registered_company"": false,
                ""trading_name"": ""Hannah's Goods"",
                ""date_of_incorporation"": {""year"": 2025, ""month"": 10, ""day"": 1},
                ""principal_address"": {
                  ""address_line1"": ""123 Main Street"",
                  ""city"": ""San Francisco"",
                  ""state"": ""CA"",
                  ""zip"": ""94105"",
                  ""country"": ""US""
                },
                ""representatives"": [
                  {
                    ""roles"": [""ubo""],
                    ""ownership_percentage"": 100,
                    ""individual"": {
                      ""first_name"": ""Hannah"",
                      ""last_name"": ""Bret"",
                      ""email_address"": ""hannah.bret@example.com"",
                      ""national_id_type"": ""ssn"",
                      ""national_id_number"": ""123456789"",
                      ""date_of_birth"": {""day"": 15, ""month"": 1, ""year"": 1990},
                      ""place_of_birth"": {""country"": ""US""},
                      ""citizenships"": [{""country"": ""US""}],
                      ""phone"": {""country_code"": ""US"", ""number"": ""4155678901""},
                      ""address"": {
                        ""address_line1"": ""123 Main Street"",
                        ""city"": ""San Francisco"",
                        ""state"": ""CA"",
                        ""zip"": ""94105"",
                        ""country"": ""US""
                      }
                    }
                  }
                ]
              }
            }";

        [Fact]
        public void ShouldRoundTripSwaggerExampleForUsIsvSellerCompany()
        {
            var request = (OnboardEntityRequest)Serializer
                .Deserialize(UsIsvSellerCompanySwaggerExample, typeof(OnboardEntityRequest));

            var json = Serializer.Serialize(request);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(UsIsvSellerCompanySwaggerExample)).ShouldBeTrue(json);
            // agreed_terms.date is a string property, so the original text must survive unchanged.
            json.ShouldContain("\"date\":\"2026-07-02T10:30:00.0000000+00:00\"");
        }

        [Fact]
        public void ShouldRoundTripSwaggerExampleForUsIsvSellerSoleTrader()
        {
            var request = (OnboardEntityRequest)Serializer
                .Deserialize(UsIsvSellerSoleTraderSwaggerExample, typeof(OnboardEntityRequest));

            var json = Serializer.Serialize(request);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(UsIsvSellerSoleTraderSwaggerExample)).ShouldBeTrue(json);
            json.ShouldContain("\"date\":\"2026-07-02T10:30:00.0000000+00:00\"");
        }

        // ------------------------------------------------------------------------
        // ContactDetails
        // invitee is on the non US ISV variants, phone.country_code is v3.0 only.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAndRoundTripContactDetailsWithEveryProperty()
        {
            var contactDetails = new ContactDetails
            {
                Invitee = new Invitee { Email = "invitee@example.com" },
                Phone = new Phone { CountryCode = "GB", Number = "2079460000" },
                EmailAddresses = new EmailAddresses
                {
                    Primary = "admin@example.com", PciComplianceContact = "pci@example.com"
                }
            };

            var json = Serializer.Serialize(contactDetails);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""invitee"": { ""email"": ""invitee@example.com"" },
                ""phone"": { ""country_code"": ""GB"", ""number"": ""2079460000"" },
                ""email_addresses"": { ""primary"": ""admin@example.com"", ""pci_compliance_contact"": ""pci@example.com"" }
            }")).ShouldBeTrue(json);

            var roundTripped = (ContactDetails)Serializer.Deserialize(json, typeof(ContactDetails));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        // ------------------------------------------------------------------------
        // Company
        // One class covers every variant: v3.0 keys (additional_trading_names,
        // is_registered_company, regulatory_licence_number) and the v2.0 financial_details.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAndRoundTripCompanyWithEveryProperty()
        {
            var company = new Company
            {
                LegalName = "Super Hero Masks Ltd",
                TradingName = "Super Hero Masks",
                AdditionalTradingNames = new List<string> { "SHM", "Hero Masks" },
                IsRegisteredCompany = true,
                BusinessRegistrationNumber = "01234567",
                DateOfIncorporation = new DateOfIncorporation { Day = 1, Month = 6, Year = 2010 },
                PrincipalAddress = new Address
                {
                    AddressLine1 = "90 Tottenham Court Road",
                    AddressLine2 = "Floor 2",
                    City = "London",
                    State = "London",
                    Zip = "W1T 4TJ",
                    Country = CountryCode.GB
                },
                RegisteredAddress = new Address
                {
                    AddressLine1 = "1 Registered Street",
                    AddressLine2 = "Suite 5",
                    City = "Manchester",
                    State = "Greater Manchester",
                    Zip = "M1 1AA",
                    Country = CountryCode.GB
                },
                Representatives = new List<Representative>
                {
                    new Representative
                    {
                        Individual = new Individual { FirstName = "Jane", LastName = "Doe" },
                        Roles = new List<EntityRoles> { EntityRoles.Director }
                    }
                },
                BusinessType = BusinessType.LimitedCompany,
                FinancialDetails = new FinancialDetails
                {
                    AnnualProcessingVolume = 5000000000,
                    AverageTransactionValue = 2500,
                    HighestTransactionValue = 100000,
                    Currency = Currency.GBP
                },
                RegulatoryLicenceNumber = "FRN123456"
            };

            var json = Serializer.Serialize(company);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""legal_name"": ""Super Hero Masks Ltd"",
                ""trading_name"": ""Super Hero Masks"",
                ""additional_trading_names"": [""SHM"", ""Hero Masks""],
                ""is_registered_company"": true,
                ""business_registration_number"": ""01234567"",
                ""date_of_incorporation"": { ""day"": 1, ""month"": 6, ""year"": 2010 },
                ""principal_address"": {
                    ""address_line1"": ""90 Tottenham Court Road"", ""address_line2"": ""Floor 2"",
                    ""city"": ""London"", ""state"": ""London"", ""zip"": ""W1T 4TJ"", ""country"": ""GB"" },
                ""registered_address"": {
                    ""address_line1"": ""1 Registered Street"", ""address_line2"": ""Suite 5"",
                    ""city"": ""Manchester"", ""state"": ""Greater Manchester"", ""zip"": ""M1 1AA"", ""country"": ""GB"" },
                ""representatives"": [
                    { ""individual"": { ""first_name"": ""Jane"", ""last_name"": ""Doe"" }, ""roles"": [""director""] }
                ],
                ""business_type"": ""limited_company"",
                ""financial_details"": {
                    ""annual_processing_volume"": 5000000000,
                    ""average_transaction_value"": 2500,
                    ""highest_transaction_value"": 100000,
                    ""currency"": ""GBP"" },
                ""regulatory_licence_number"": ""FRN123456""
            }")).ShouldBeTrue(json);

            var roundTripped = (Company)Serializer.Deserialize(json, typeof(Company));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        // ------------------------------------------------------------------------
        // Representative
        // v3.0 is a oneOf: a person of interest (individual) or a controlling company.
        // v2.0 carries the person fields at the top level of the representative.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAndRoundTripRepresentativeV3PersonOfInterestWithEveryProperty()
        {
            var representative = new Representative
            {
                Id = "rep_jgulxdz9v580l6aowlq3u5ieik",
                Individual = new Individual
                {
                    FirstName = "John",
                    MiddleName = "Paul",
                    LastName = "Doe",
                    DateOfBirth = new DateOfBirth { Day = 5, Month = 6, Year = 1985 },
                    PlaceOfBirth = new PlaceOfBirth { Country = CountryCode.US },
                    EmailAddress = "john.doe@example.com",
                    Phone = new Phone { CountryCode = "US", Number = "4155678901" },
                    Address = new Address
                    {
                        AddressLine1 = "123 Main Street",
                        AddressLine2 = "Apt 4",
                        City = "San Francisco",
                        State = "CA",
                        Zip = "94105",
                        Country = CountryCode.US
                    },
                    Citizenships = new List<Citizenship>
                    {
                        new Citizenship { Type = "citizenship", Country = CountryCode.US },
                        new Citizenship { Type = "residency", Country = CountryCode.GB }
                    },
                    NationalIdType = NationalIdType.Ssn,
                    NationalIdNumber = "123456789"
                },
                CompanyPosition = CompanyPositionType.CFO,
                OwnershipPercentage = 25,
                Roles = new List<EntityRoles> { EntityRoles.Ubo, EntityRoles.ControlPerson },
                Documents = new Checkout.Accounts.Entities.Common.Documents.Documents
                {
                    IdentityVerification = new IdentityVerification
                    {
                        Type = IdentityVerificationType.Passport, Front = "file_identityverificationaaaaaa"
                    }
                }
            };

            var json = Serializer.Serialize(representative);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""id"": ""rep_jgulxdz9v580l6aowlq3u5ieik"",
                ""individual"": {
                    ""first_name"": ""John"",
                    ""middle_name"": ""Paul"",
                    ""last_name"": ""Doe"",
                    ""date_of_birth"": { ""day"": 5, ""month"": 6, ""year"": 1985 },
                    ""place_of_birth"": { ""country"": ""US"" },
                    ""email_address"": ""john.doe@example.com"",
                    ""phone"": { ""country_code"": ""US"", ""number"": ""4155678901"" },
                    ""address"": {
                        ""address_line1"": ""123 Main Street"", ""address_line2"": ""Apt 4"",
                        ""city"": ""San Francisco"", ""state"": ""CA"", ""zip"": ""94105"", ""country"": ""US"" },
                    ""citizenships"": [
                        { ""type"": ""citizenship"", ""country"": ""US"" },
                        { ""type"": ""residency"", ""country"": ""GB"" }
                    ],
                    ""national_id_type"": ""ssn"",
                    ""national_id_number"": ""123456789""
                },
                ""company_position"": ""cfo"",
                ""ownership_percentage"": 25,
                ""roles"": [""ubo"", ""control_person""],
                ""documents"": {
                    ""identity_verification"": { ""type"": ""passport"", ""front"": ""file_identityverificationaaaaaa"" }
                }
            }")).ShouldBeTrue(json);

            var roundTripped = (Representative)Serializer.Deserialize(json, typeof(Representative));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        [Fact]
        public void ShouldSerializeAndRoundTripRepresentativeV3ControllingCompany()
        {
            var representative = new Representative
            {
                Id = "rep_6ryr2q02k4cm69tqqv9q32gbn1",
                Company = new Company
                {
                    LegalName = "Parent Holdings Ltd",
                    TradingName = "Parent Holdings",
                    RegisteredAddress = new Address
                    {
                        AddressLine1 = "10 Holding Lane",
                        City = "London",
                        Zip = "EC1A 1BB",
                        Country = CountryCode.GB
                    }
                },
                OwnershipPercentage = 60
            };

            var json = Serializer.Serialize(representative);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""id"": ""rep_6ryr2q02k4cm69tqqv9q32gbn1"",
                ""company"": {
                    ""legal_name"": ""Parent Holdings Ltd"",
                    ""trading_name"": ""Parent Holdings"",
                    ""registered_address"": {
                        ""address_line1"": ""10 Holding Lane"", ""city"": ""London"", ""zip"": ""EC1A 1BB"", ""country"": ""GB"" }
                },
                ""ownership_percentage"": 60
            }")).ShouldBeTrue(json);

            var roundTripped = (Representative)Serializer.Deserialize(json, typeof(Representative));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        [Fact]
        public void ShouldSerializeAndRoundTripRepresentativeV2WithEveryProperty()
        {
            var representative = new Representative
            {
                Id = "rep_el68fmd58vhtmybdpgprrsjbi1",
                FirstName = "John",
                MiddleName = "Paul",
                LastName = "Doe",
                DateOfBirth = new DateOfBirth { Day = 5, Month = 6, Year = 1985 },
                Phone = new Phone { Number = "4155678901" },
                Address = new Address
                {
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = "Apt 4",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94105",
                    Country = CountryCode.US
                },
                PlaceOfBirth = new PlaceOfBirth { Country = CountryCode.US },
                Identification = new Identification { NationalIdNumber = "123456789" },
                Roles = new List<EntityRoles> { EntityRoles.Ubo, EntityRoles.AuthorisedSignatory },
                Documents = new Checkout.Accounts.Entities.Common.Documents.Documents
                {
                    IdentityVerification = new IdentityVerification
                    {
                        Type = IdentityVerificationType.DrivingLicense,
                        Front = "file_identityverificationaaaaaa",
                        Back = "file_iuhiyc5pcdbdmtcgyq5y3qhbl4"
                    }
                }
            };

            var json = Serializer.Serialize(representative);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""id"": ""rep_el68fmd58vhtmybdpgprrsjbi1"",
                ""first_name"": ""John"",
                ""middle_name"": ""Paul"",
                ""last_name"": ""Doe"",
                ""date_of_birth"": { ""day"": 5, ""month"": 6, ""year"": 1985 },
                ""phone"": { ""number"": ""4155678901"" },
                ""address"": {
                    ""address_line1"": ""123 Main Street"", ""address_line2"": ""Apt 4"",
                    ""city"": ""San Francisco"", ""state"": ""CA"", ""zip"": ""94105"", ""country"": ""US"" },
                ""place_of_birth"": { ""country"": ""US"" },
                ""identification"": { ""national_id_number"": ""123456789"" },
                ""roles"": [""ubo"", ""authorised_signatory""],
                ""documents"": {
                    ""identity_verification"": {
                        ""type"": ""driving_license"",
                        ""front"": ""file_identityverificationaaaaaa"",
                        ""back"": ""file_iuhiyc5pcdbdmtcgyq5y3qhbl4"" }
                }
            }")).ShouldBeTrue(json);

            var roundTripped = (Representative)Serializer.Deserialize(json, typeof(Representative));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        // ------------------------------------------------------------------------
        // Individual (v2.0 sole trader, top level of the request)
        // place_of_birth is EEA only; financial_details and identification are US only.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeAndRoundTripIndividualV2WithEveryProperty()
        {
            var individual = new Individual
            {
                FirstName = "Hannah",
                MiddleName = "Rose",
                LastName = "Bret",
                TradingName = "Hannah's Goods",
                RegisteredAddress = new Address
                {
                    AddressLine1 = "123 Main Street",
                    AddressLine2 = "Unit 7",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94105",
                    Country = CountryCode.US
                },
                DateOfBirth = new DateOfBirth { Day = 15, Month = 1, Year = 1990 },
                PlaceOfBirth = new PlaceOfBirth { Country = CountryCode.FR },
                FinancialDetails = new FinancialDetails
                {
                    AnnualProcessingVolume = 120000,
                    AverageTransactionValue = 1500,
                    HighestTransactionValue = 9000,
                    Currency = Currency.USD
                },
                Identification = new Identification { NationalIdNumber = "123456789" }
            };

            var json = Serializer.Serialize(individual);

            JToken.DeepEquals(JObject.Parse(json), JObject.Parse(@"{
                ""first_name"": ""Hannah"",
                ""middle_name"": ""Rose"",
                ""last_name"": ""Bret"",
                ""trading_name"": ""Hannah's Goods"",
                ""registered_address"": {
                    ""address_line1"": ""123 Main Street"", ""address_line2"": ""Unit 7"",
                    ""city"": ""San Francisco"", ""state"": ""CA"", ""zip"": ""94105"", ""country"": ""US"" },
                ""date_of_birth"": { ""day"": 15, ""month"": 1, ""year"": 1990 },
                ""place_of_birth"": { ""country"": ""FR"" },
                ""financial_details"": {
                    ""annual_processing_volume"": 120000,
                    ""average_transaction_value"": 1500,
                    ""highest_transaction_value"": 9000,
                    ""currency"": ""USD"" },
                ""identification"": { ""national_id_number"": ""123456789"" }
            }")).ShouldBeTrue(json);

            var roundTripped = (Individual)Serializer.Deserialize(json, typeof(Individual));
            Serializer.Serialize(roundTripped).ShouldBe(json);
        }

        // ------------------------------------------------------------------------
        // File DTOs (POST /files and GET /files/{file_id} on the Accounts files host)
        // UploadFileResponse is PlatformsFileUploadResponse, FileDetailsResponse is
        // PlatformsFileRetrieveResponse, AccountsFileRequest is PlatformsFileUpload.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeSwaggerExampleForUploadFileResponse()
        {
            const string json = @"{
                ""id"": ""file_6lbss42ezvoufcb2beo76rvwly"",
                ""maximum_size_in_bytes"": 4194304,
                ""document_types_for_purpose"": [""image/jpeg"", ""image/png"", ""image/jpg""],
                ""_links"": {
                    ""upload"": { ""href"": ""https://s3.eu-west-1.amazonaws.com/mp-files-api-staging-prod/ent_ociwguf5a5fe3ndmpnvpnwsi3e/file_6lbss42ezvoufcb2beo76rvwly?AWSAccessKeyId=ASIX4BFJOBCQFLAMPKU3&Expires=1661355993&x-amz-security-token=some_token"" },
                    ""self"": { ""href"": ""https://files.checkout.com/files/file_6lbss42ezvoufcb2beo76rvwly"" }
                }
            }";

            var response = (UploadFileResponse)Serializer.Deserialize(json, typeof(UploadFileResponse));

            response.Id.ShouldBe("file_6lbss42ezvoufcb2beo76rvwly");
            response.MaximumSizeInBytes.ShouldBe(4194304L);
            response.DocumentTypesForPurpose.ShouldBe(new[] { "image/jpeg", "image/png", "image/jpg" });
            response.GetUploadLink().Href.ShouldBe(
                "https://s3.eu-west-1.amazonaws.com/mp-files-api-staging-prod/ent_ociwguf5a5fe3ndmpnvpnwsi3e/file_6lbss42ezvoufcb2beo76rvwly?AWSAccessKeyId=ASIX4BFJOBCQFLAMPKU3&Expires=1661355993&x-amz-security-token=some_token");
            response.GetSelfLink().Href.ShouldBe("https://files.checkout.com/files/file_6lbss42ezvoufcb2beo76rvwly");
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForFileDetailsResponse()
        {
            const string json = @"{
                ""id"": ""file_6lbss42ezvoufcb2beo76rvwly"",
                ""status"": ""invalid"",
                ""status_reasons"": [""InvalidMimeType""],
                ""size"": 1024,
                ""mime_type"": ""application/pdf"",
                ""uploaded_on"": ""2020-12-01T15:01:01.0000000+00:00"",
                ""purpose"": ""identity_verification"",
                ""_links"": {
                    ""download"": { ""href"": ""https://s3.eu-west-1.amazonaws.com/mp-files-api-clean-prod/ent_ociwguf5a5fe3ndmpnvpnwsi3e/file_6lbss42ezvoufcb2beo76rvwly?X-Amz-Expires=3600&x-amz-security-token=some_token"" },
                    ""self"": { ""href"": ""https://files.checkout.com/files/file_6lbss42ezvoufcb2beo76rvwly"" }
                }
            }";

            var response = (FileDetailsResponse)Serializer.Deserialize(json, typeof(FileDetailsResponse));

            response.Id.ShouldBe("file_6lbss42ezvoufcb2beo76rvwly");
            response.Status.ShouldBe("invalid");
            response.StatusReasons.ShouldBe(new[] { "InvalidMimeType" });
            // The spec types size as a number; the model holds it as a string.
            response.Size.ShouldBe("1024");
            response.MimeType.ShouldBe("application/pdf");
            // Parsed with the local machine offset, so compare the instant in UTC.
            response.UploadedOn.ShouldNotBeNull();
            response.UploadedOn.Value.ToUniversalTime()
                .ShouldBe(new DateTime(2020, 12, 1, 15, 1, 1, DateTimeKind.Utc));
            response.Purpose.ShouldBe(AccountsFilePurpose.IdentityVerification);
            response.Filename.ShouldBeNull();
            response.GetLink("download").Href.ShouldBe(
                "https://s3.eu-west-1.amazonaws.com/mp-files-api-clean-prod/ent_ociwguf5a5fe3ndmpnvpnwsi3e/file_6lbss42ezvoufcb2beo76rvwly?X-Amz-Expires=3600&x-amz-security-token=some_token");
            response.GetSelfLink().Href.ShouldBe("https://files.checkout.com/files/file_6lbss42ezvoufcb2beo76rvwly");
        }

        // The 14 PlatformsFileUpload purposes. The inherited file and content type stay
        // out of the JSON body, so the body is exactly the purpose.
        [Theory]
        [InlineData(AccountsFilePurpose.AdditionalDocument, "additional_document")]
        [InlineData(AccountsFilePurpose.ArticlesOfAssociation, "articles_of_association")]
        [InlineData(AccountsFilePurpose.BankVerification, "bank_verification")]
        [InlineData(AccountsFilePurpose.CertifiedAuthorisedSignatory, "certified_authorised_signatory")]
        [InlineData(AccountsFilePurpose.CompanyOwnership, "company_ownership")]
        [InlineData(AccountsFilePurpose.CompanyVerification, "company_verification")]
        [InlineData(AccountsFilePurpose.FinancialVerification, "financial_verification")]
        [InlineData(AccountsFilePurpose.IdentityVerification, "identity_verification")]
        [InlineData(AccountsFilePurpose.ProofOfLegality, "proof_of_legality")]
        [InlineData(AccountsFilePurpose.ProofOfPrincipalAddress, "proof_of_principal_address")]
        [InlineData(AccountsFilePurpose.ShareholderStructure, "shareholder_structure")]
        [InlineData(AccountsFilePurpose.TaxVerification, "tax_verification")]
        [InlineData(AccountsFilePurpose.ProofOfResidentialAddress, "proof_of_residential_address")]
        [InlineData(AccountsFilePurpose.ProofOfRegistration, "proof_of_registration")]
        public void ShouldSerializeAccountsFileRequestWithPurposeOnly(AccountsFilePurpose purpose, string wireValue)
        {
            var json = Serializer.Serialize(new AccountsFileRequest { Purpose = purpose });

            json.ShouldBe("{\"purpose\":\"" + wireValue + "\"}");
        }

        // ------------------------------------------------------------------------
        // Enum wire values
        // Every value checked against the spec enum, in both directions.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(IdentityVerificationType.Passport, "passport")]
        [InlineData(IdentityVerificationType.NationalIdentityCard, "national_identity_card")]
        [InlineData(IdentityVerificationType.DrivingLicense, "driving_license")]
        [InlineData(IdentityVerificationType.CitizenCard, "citizen_card")]
        [InlineData(IdentityVerificationType.ResidencePermit, "residence_permit")]
        [InlineData(IdentityVerificationType.ElectoralId, "electoral_id")]
        public void ShouldSerializeIdentityVerificationTypeWireValue(IdentityVerificationType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(IdentityVerificationType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(CompanyVerificationType.IncorporationDocument, "incorporation_document")]
        [InlineData(CompanyVerificationType.ArticlesOfAssociation, "articles_of_association")]
        public void ShouldSerializeCompanyVerificationTypeWireValue(CompanyVerificationType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(CompanyVerificationType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(ArticlesOfAssociationType.MemorandumOfAssociation, "memorandum_of_association")]
        [InlineData(ArticlesOfAssociationType.ArticlesOfAssociation, "articles_of_association")]
        public void ShouldSerializeArticlesOfAssociationTypeWireValue(ArticlesOfAssociationType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(ArticlesOfAssociationType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(ProofOfRegistrationType.ExtractFromTradeRegister, "extract_from_trade_register")]
        [InlineData(ProofOfRegistrationType.Other, "other")]
        public void ShouldSerializeProofOfRegistrationTypeWireValue(ProofOfRegistrationType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(ProofOfRegistrationType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(NationalIdType.Ssn, "ssn")]
        [InlineData(NationalIdType.Itin, "itin")]
        [InlineData(NationalIdType.Passport, "passport")]
        [InlineData(NationalIdType.DrivingLicense, "driving_license")]
        [InlineData(NationalIdType.NationalIdCard, "national_id_card")]
        [InlineData(NationalIdType.ResidencePermit, "residence_permit")]
        [InlineData(NationalIdType.Other, "other")]
        public void ShouldSerializeNationalIdTypeWireValue(NationalIdType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(NationalIdType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(EntityRoles.Ubo, "ubo")]
        [InlineData(EntityRoles.AuthorisedSignatory, "authorised_signatory")]
        [InlineData(EntityRoles.Director, "director")]
        [InlineData(EntityRoles.ControlPerson, "control_person")]
        [InlineData(EntityRoles.LegalRepresentative, "legal_representative")]
        public void ShouldSerializeEntityRolesWireValue(EntityRoles value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(EntityRoles)).ShouldBe(value);
        }

        [Theory]
        [InlineData(CompanyPositionType.CEO, "ceo")]
        [InlineData(CompanyPositionType.CFO, "cfo")]
        [InlineData(CompanyPositionType.COO, "coo")]
        [InlineData(CompanyPositionType.ManagingMember, "managing_member")]
        [InlineData(CompanyPositionType.GeneralPartner, "general_partner")]
        [InlineData(CompanyPositionType.President, "president")]
        [InlineData(CompanyPositionType.VicePresident, "vice_president")]
        [InlineData(CompanyPositionType.Treasurer, "treasurer")]
        [InlineData(CompanyPositionType.OtherSeniorManagement, "other_senior_management")]
        [InlineData(CompanyPositionType.OtherExecutiveOfficer, "other_executive_officer")]
        [InlineData(CompanyPositionType.OtherNonExecutiveNonSenior, "other_non_executive_non_senior")]
        public void ShouldSerializeCompanyPositionTypeWireValue(CompanyPositionType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(CompanyPositionType)).ShouldBe(value);
        }

        [Theory]
        [InlineData(BusinessType.ScottishLimitedPartnership, "scottish_limited_partnership")]
        [InlineData(BusinessType.UnincorporatedAssociation, "unincorporated_association")]
        [InlineData(BusinessType.PrivateCorporation, "private_corporation")]
        [InlineData(BusinessType.LimitedLiabilityCorporation, "limited_liability_corporation")]
        [InlineData(BusinessType.PubliclyTradedCorporation, "publicly_traded_corporation")]
        [InlineData(BusinessType.RegulatedFinancialInstitution, "regulated_financial_institution")]
        [InlineData(BusinessType.SecRegisteredEntity, "sec_registered_entity")]
        [InlineData(BusinessType.CftcRegisteredEntity, "cftc_registered_entity")]
        [InlineData(BusinessType.IndividualOrSoleProprietorship, "individual_or_sole_proprietorship")]
        [InlineData(BusinessType.GovernmentAgency, "government_agency")]
        [InlineData(BusinessType.NonProfitEntity, "non_profit_entity")]
        [InlineData(BusinessType.Trust, "trust")]
        [InlineData(BusinessType.ClubOrSociety, "club_or_society")]
        [InlineData(BusinessType.GeneralPartnership, "general_partnership")]
        [InlineData(BusinessType.LimitedPartnership, "limited_partnership")]
        [InlineData(BusinessType.PublicLimitedCompany, "public_limited_company")]
        [InlineData(BusinessType.LimitedCompany, "limited_company")]
        [InlineData(BusinessType.ProfessionalAssociation, "professional_association")]
        [InlineData(BusinessType.AutoEntrepreneur, "auto_entrepreneur")]
        public void ShouldSerializeBusinessTypeWireValue(BusinessType value, string wireValue)
        {
            Serializer.Serialize(value).ShouldBe("\"" + wireValue + "\"");
            Serializer.Deserialize("\"" + wireValue + "\"", typeof(BusinessType)).ShouldBe(value);
        }
    }
}
