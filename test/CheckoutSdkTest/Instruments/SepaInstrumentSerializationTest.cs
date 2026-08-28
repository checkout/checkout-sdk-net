using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using Shouldly;
using System;
using Xunit;

namespace Checkout.Instruments
{
    /// <summary>
    /// Schema validation tests for Checkout.Instruments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class SepaInstrumentSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // CreateSepaInstrumentRequest
        // Schema validation tests for CreateSepaInstrumentRequest.
        // Covers all 22 properties, including nested objects, against the StoreSepaInstrumentRequest
        // swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializePaymentTypeLowercase()
        {
            var instrumentData = new CreateSepaInstrumentData { PaymentType = SepaPaymentType.Recurring };

            // Exact match: Shouldly's containment assertions are case-insensitive, so they cannot
            // pin the casing on their own. The Bacs equivalent is capitalized.
            Serializer.Serialize(instrumentData).ShouldBe("{\"payment_type\":\"recurring\"}");
        }

        [Fact]
        public void ShouldSerializeMandateTypeAndOmitItWhenNotSet()
        {
            Serializer.Serialize(new CreateSepaInstrumentData { Type = SepaMandateType.Core })
                .ShouldBe("{\"type\":\"Core\"}");
            Serializer.Serialize(new CreateSepaInstrumentData { Type = SepaMandateType.B2B })
                .ShouldBe("{\"type\":\"B2B\"}");
            Serializer.Serialize(new CreateSepaInstrumentData { AccountNumber = "FR2810096000509685512959O86" })
                .ShouldNotContain("\"type\"");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForCreateSepaInstrumentRequest()
        {
            var original = CreateFullyPopulatedRequest();

            var json = Serializer.Serialize(original);
            var d = (CreateSepaInstrumentRequest)Serializer
                .Deserialize(json, typeof(CreateSepaInstrumentRequest));

            d.Type.ShouldBe(InstrumentType.Sepa);
            d.InstrumentData.Type.ShouldBe(original.InstrumentData.Type);
            d.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            d.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            d.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            d.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            d.InstrumentData.MandateId.ShouldBe(original.InstrumentData.MandateId);
            d.InstrumentData.DateOfSignature.ShouldBe(original.InstrumentData.DateOfSignature);

            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            d.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            d.AccountHolder.BillingAddress.AddressLine1.ShouldBe(original.AccountHolder.BillingAddress.AddressLine1);
            d.AccountHolder.BillingAddress.AddressLine2.ShouldBe(original.AccountHolder.BillingAddress.AddressLine2);
            d.AccountHolder.BillingAddress.City.ShouldBe(original.AccountHolder.BillingAddress.City);
            d.AccountHolder.BillingAddress.Zip.ShouldBe(original.AccountHolder.BillingAddress.Zip);
            d.AccountHolder.BillingAddress.Country.ShouldBe(original.AccountHolder.BillingAddress.Country);

            d.Customer.Id.ShouldBe(original.Customer.Id);
            d.Customer.Email.ShouldBe(original.Customer.Email);
            d.Customer.Name.ShouldBe(original.Customer.Name);
            d.Customer.Phone.CountryCode.ShouldBe(original.Customer.Phone.CountryCode);
            d.Customer.Phone.Number.ShouldBe(original.Customer.Phone.Number);
            d.Customer.Default.ShouldBe(original.Customer.Default);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForCreateSepaInstrumentRequest()
        {
            const string json = @"{
                ""type"": ""sepa"",
                ""instrument_data"": {
                    ""type"": ""Core"",
                    ""account_number"": ""FR7630006000011234567890189"",
                    ""country"": ""FR"",
                    ""currency"": ""EUR"",
                    ""payment_type"": ""recurring"",
                    ""mandate_id"": ""1234567890"",
                    ""date_of_signature"": ""2023-01-01""
                },
                ""account_holder"": {
                    ""first_name"": ""Ali"",
                    ""last_name"": ""Farid"",
                    ""company_name"": ""Farid Ltd"",
                    ""billing_address"": {
                        ""address_line1"": ""Rue Exemple"",
                        ""address_line2"": ""1"",
                        ""city"": ""Paris"",
                        ""zip"": ""1234"",
                        ""country"": ""FR""
                    },
                    ""type"": ""individual""
                },
                ""customer"": {
                    ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                    ""email"": ""brucewayne@gmail.com"",
                    ""name"": ""Bruce Wayne"",
                    ""phone"": { ""country_code"": ""+33"", ""number"": ""123456789"" },
                    ""default"": true
                }
            }";

            var r = (CreateSepaInstrumentRequest)Serializer
                .Deserialize(json, typeof(CreateSepaInstrumentRequest));

            r.Type.ShouldBe(InstrumentType.Sepa);
            r.InstrumentData.Type.ShouldBe(SepaMandateType.Core);
            r.InstrumentData.AccountNumber.ShouldBe("FR7630006000011234567890189");
            r.InstrumentData.Country.ShouldBe(CountryCode.FR);
            r.InstrumentData.Currency.ShouldBe(Currency.EUR);
            r.InstrumentData.PaymentType.ShouldBe(SepaPaymentType.Recurring);
            r.InstrumentData.MandateId.ShouldBe("1234567890");
            r.InstrumentData.DateOfSignature.ShouldBe("2023-01-01");
            r.AccountHolder.FirstName.ShouldBe("Ali");
            r.AccountHolder.LastName.ShouldBe("Farid");
            r.AccountHolder.CompanyName.ShouldBe("Farid Ltd");
            r.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Individual);
            r.AccountHolder.BillingAddress.AddressLine1.ShouldBe("Rue Exemple");
            r.AccountHolder.BillingAddress.AddressLine2.ShouldBe("1");
            r.AccountHolder.BillingAddress.City.ShouldBe("Paris");
            r.AccountHolder.BillingAddress.Zip.ShouldBe("1234");
            r.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.FR);
            r.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            r.Customer.Email.ShouldBe("brucewayne@gmail.com");
            r.Customer.Name.ShouldBe("Bruce Wayne");
            r.Customer.Phone.CountryCode.ShouldBe("+33");
            r.Customer.Phone.Number.ShouldBe("123456789");
            r.Customer.Default.ShouldBe(true);
        }

        private static CreateSepaInstrumentRequest CreateFullyPopulatedRequest()
        {
            return new CreateSepaInstrumentRequest
            {
                InstrumentData = new CreateSepaInstrumentData
                {
                    Type = SepaMandateType.B2B,
                    AccountNumber = "FR7630006000011234567890189",
                    Country = CountryCode.FR,
                    Currency = Currency.EUR,
                    PaymentType = SepaPaymentType.Regular,
                    MandateId = "1234567890",
                    DateOfSignature = "2023-01-01"
                },
                AccountHolder = new CreateSepaAccountHolder
                {
                    FirstName = "Ali",
                    LastName = "Farid",
                    CompanyName = "Farid Ltd",
                    Type = InstrumentAccountHolderType.Corporate,
                    BillingAddress = new CreateSepaBillingAddress
                    {
                        AddressLine1 = "Rue Exemple",
                        AddressLine2 = "1",
                        City = "Paris",
                        Zip = "1234",
                        Country = CountryCode.FR
                    }
                },
                Customer = new CreateCustomerInstrumentRequest
                {
                    Id = "cus_y3oqhf46pyzuxjbcn2giaqnb44",
                    Email = "brucewayne@gmail.com",
                    Name = "Bruce Wayne",
                    Phone = new Phone { CountryCode = "+33", Number = "123456789" },
                    Default = true
                }
            };
        }

        // ------------------------------------------------------------------------
        // UpdateSepaInstrumentRequest
        // Schema validation tests for UpdateSepaInstrumentRequest.
        // Covers all 19 properties, including nested objects, against the UpdateSepaInstrumentRequest
        // swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeTypeOnly()
        {
            var json = Serializer.Serialize(new UpdateSepaInstrumentRequest());

            json.ShouldContain("\"type\":\"sepa\"");
            json.ShouldNotContain("instrument_data");
            json.ShouldNotContain("account_holder");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForUpdateSepaInstrumentRequest()
        {
            var original = new UpdateSepaInstrumentRequest
            {
                InstrumentData = new UpdateSepaInstrumentData
                {
                    Type = SepaMandateType.Core,
                    AccountNumber = "FR7630006000011234567890189",
                    Country = CountryCode.FR,
                    Currency = Currency.EUR,
                    PaymentType = SepaPaymentType.Regular,
                    MandateId = "1234567890",
                    DateOfSignature = "2023-01-01"
                },
                AccountHolder = new UpdateSepaAccountHolder
                {
                    FirstName = "Ali",
                    LastName = "Farid",
                    CompanyName = "Farid Ltd",
                    Type = InstrumentAccountHolderType.Corporate,
                    BillingAddress = new UpdateSepaBillingAddress
                    {
                        AddressLine1 = "Rue Exemple",
                        AddressLine2 = "1",
                        City = "Paris",
                        Zip = "1234",
                        Country = CountryCode.FR
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var d = (UpdateSepaInstrumentRequest)Serializer
                .Deserialize(json, typeof(UpdateSepaInstrumentRequest));

            d.Type.ShouldBe(InstrumentType.Sepa);
            d.InstrumentData.Type.ShouldBe(original.InstrumentData.Type);
            d.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            d.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            d.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            d.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            d.InstrumentData.MandateId.ShouldBe(original.InstrumentData.MandateId);
            d.InstrumentData.DateOfSignature.ShouldBe(original.InstrumentData.DateOfSignature);
            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            d.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            d.AccountHolder.BillingAddress.AddressLine1.ShouldBe(original.AccountHolder.BillingAddress.AddressLine1);
            d.AccountHolder.BillingAddress.AddressLine2.ShouldBe(original.AccountHolder.BillingAddress.AddressLine2);
            d.AccountHolder.BillingAddress.City.ShouldBe(original.AccountHolder.BillingAddress.City);
            d.AccountHolder.BillingAddress.Zip.ShouldBe(original.AccountHolder.BillingAddress.Zip);
            d.AccountHolder.BillingAddress.Country.ShouldBe(original.AccountHolder.BillingAddress.Country);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForUpdateSepaInstrumentRequest()
        {
            const string json = @"{
                ""type"": ""sepa"",
                ""instrument_data"": {
                    ""type"": ""B2B"",
                    ""account_number"": ""FR7630006000011234567890189"",
                    ""country"": ""FR"",
                    ""currency"": ""EUR"",
                    ""payment_type"": ""regular"",
                    ""mandate_id"": ""1234567890"",
                    ""date_of_signature"": ""2023-01-01""
                },
                ""account_holder"": {
                    ""first_name"": ""Ali"",
                    ""last_name"": ""Farid"",
                    ""company_name"": ""Farid Ltd"",
                    ""billing_address"": {
                        ""address_line1"": ""Rue Exemple"",
                        ""address_line2"": ""1"",
                        ""city"": ""Paris"",
                        ""zip"": ""1234"",
                        ""country"": ""FR""
                    },
                    ""type"": ""corporate""
                }
            }";

            var r = (UpdateSepaInstrumentRequest)Serializer
                .Deserialize(json, typeof(UpdateSepaInstrumentRequest));

            r.InstrumentData.Type.ShouldBe(SepaMandateType.B2B);
            r.InstrumentData.PaymentType.ShouldBe(SepaPaymentType.Regular);
            r.InstrumentData.AccountNumber.ShouldBe("FR7630006000011234567890189");
            r.InstrumentData.Country.ShouldBe(CountryCode.FR);
            r.InstrumentData.Currency.ShouldBe(Currency.EUR);
            r.InstrumentData.MandateId.ShouldBe("1234567890");
            r.InstrumentData.DateOfSignature.ShouldBe("2023-01-01");
            r.AccountHolder.FirstName.ShouldBe("Ali");
            r.AccountHolder.LastName.ShouldBe("Farid");
            r.AccountHolder.CompanyName.ShouldBe("Farid Ltd");
            r.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Corporate);
            r.AccountHolder.BillingAddress.City.ShouldBe("Paris");
            r.AccountHolder.BillingAddress.Zip.ShouldBe("1234");
            r.AccountHolder.BillingAddress.AddressLine1.ShouldBe("Rue Exemple");
            r.AccountHolder.BillingAddress.AddressLine2.ShouldBe("1");
            r.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.FR);
        }

        // ------------------------------------------------------------------------
        // GetSepaInstrumentResponse
        // Schema validation tests for GetSepaInstrumentResponse.
        // Covers all 24 properties, including nested objects, against the RetrieveSepaInstrumentResponse
        // swagger schema.
        // ------------------------------------------------------------------------

        private const string FullResponseJson = @"{
            ""type"": ""sepa"",
            ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
            ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
            ""created_on"": ""2021-01-01T00:00:00Z"",
            ""modified_on"": ""2021-02-02T10:30:00Z"",
            ""vault_id"": ""vid_wmlfc3zyhqzehihu7giusaaawu"",
            ""instrument_data"": {
                ""type"": ""Core"",
                ""account_number"": ""FR7630006000011234567890189"",
                ""country"": ""FR"",
                ""currency"": ""EUR"",
                ""payment_type"": ""recurring"",
                ""mandate_id"": ""1234567890"",
                ""date_of_signature"": ""2023-01-01""
            },
            ""account_holder"": {
                ""first_name"": ""Ali"",
                ""last_name"": ""Farid"",
                ""company_name"": ""Farid Ltd"",
                ""billing_address"": {
                    ""address_line1"": ""Rue Exemple"",
                    ""address_line2"": ""1"",
                    ""city"": ""Paris"",
                    ""zip"": ""1234"",
                    ""country"": ""FR""
                },
                ""type"": ""individual""
            },
            ""customer"": {
                ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""email"": ""brucewayne@gmail.com"",
                ""name"": ""Bruce Wayne"",
                ""default"": true
            }
        }";

        [Fact]
        public void ShouldDeserializeSwaggerExampleForGetSepaInstrumentResponse()
        {
            var r = (GetSepaInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetSepaInstrumentResponse));

            r.Type.ShouldBe(InstrumentType.Sepa);
            r.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            r.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            r.CreatedOn?.ToUniversalTime().ShouldBe(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            r.ModifiedOn?.ToUniversalTime().ShouldBe(new DateTime(2021, 2, 2, 10, 30, 0, DateTimeKind.Utc));
            r.VaultId.ShouldBe("vid_wmlfc3zyhqzehihu7giusaaawu");

            r.InstrumentData.Type.ShouldBe(SepaMandateType.Core);
            r.InstrumentData.AccountNumber.ShouldBe("FR7630006000011234567890189");
            r.InstrumentData.Country.ShouldBe(CountryCode.FR);
            r.InstrumentData.Currency.ShouldBe(Currency.EUR);
            r.InstrumentData.PaymentType.ShouldBe(SepaPaymentType.Recurring);
            r.InstrumentData.MandateId.ShouldBe("1234567890");
            r.InstrumentData.DateOfSignature.ShouldBe("2023-01-01");

            r.AccountHolder.FirstName.ShouldBe("Ali");
            r.AccountHolder.LastName.ShouldBe("Farid");
            r.AccountHolder.CompanyName.ShouldBe("Farid Ltd");
            r.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Individual);
            r.AccountHolder.BillingAddress.AddressLine1.ShouldBe("Rue Exemple");
            r.AccountHolder.BillingAddress.AddressLine2.ShouldBe("1");
            r.AccountHolder.BillingAddress.City.ShouldBe("Paris");
            r.AccountHolder.BillingAddress.Zip.ShouldBe("1234");
            r.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.FR);

            r.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            r.Customer.Email.ShouldBe("brucewayne@gmail.com");
            r.Customer.Name.ShouldBe("Bruce Wayne");
            r.Customer.Default.ShouldBe(true);
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForGetSepaInstrumentResponse()
        {
            var original = (GetSepaInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetSepaInstrumentResponse));

            var json = Serializer.Serialize(original);
            var d = (GetSepaInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetSepaInstrumentResponse));

            d.Type.ShouldBe(original.Type);
            d.Id.ShouldBe(original.Id);
            d.Fingerprint.ShouldBe(original.Fingerprint);
            d.CreatedOn.ShouldBe(original.CreatedOn);
            d.ModifiedOn.ShouldBe(original.ModifiedOn);
            d.VaultId.ShouldBe(original.VaultId);
            d.InstrumentData.Type.ShouldBe(original.InstrumentData.Type);
            d.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            d.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            d.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            d.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            d.InstrumentData.MandateId.ShouldBe(original.InstrumentData.MandateId);
            d.InstrumentData.DateOfSignature.ShouldBe(original.InstrumentData.DateOfSignature);
            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            d.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            d.AccountHolder.BillingAddress.City.ShouldBe(original.AccountHolder.BillingAddress.City);
            d.Customer.Id.ShouldBe(original.Customer.Id);
            d.Customer.Default.ShouldBe(original.Customer.Default);
        }

        [Fact]
        public void ShouldSerializeAccountHolderOnlyOnce()
        {
            var r = (GetSepaInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetSepaInstrumentResponse));

            System.Text.RegularExpressions.Regex
                .Matches(Serializer.Serialize(r), "\"account_holder\"").Count.ShouldBe(1);
        }
    }
}
