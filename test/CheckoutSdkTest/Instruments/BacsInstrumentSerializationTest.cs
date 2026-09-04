using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using Checkout.Payments;
using Shouldly;
using System.Text.RegularExpressions;
using System;
using Xunit;

namespace Checkout.Instruments
{
    /// <summary>
    /// Schema validation tests for Checkout.Instruments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class BacsInstrumentSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // CreateBacsInstrumentRequest
        // Schema validation tests for CreateBacsInstrumentRequest.
        // Covers all 20 properties, including nested objects, against the StoreBacsInstrumentRequest
        // swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeWithRequiredProperties()
        {
            var request = new CreateBacsInstrumentRequest
            {
                Account = new CreateBacsInstrumentAccount
                {
                    ProcessingChannelId = "pc_q4dbxom5jbgudnjzjpz7j2z6uq"
                },
                InstrumentData = new CreateBacsInstrumentData
                {
                    AccountNumber = "86753246",
                    BankCode = "040004",
                    Country = CountryCode.GB,
                    Currency = Currency.GBP,
                    PaymentType = BacsPaymentType.Recurring
                },
                AccountHolder = new CreateBacsAccountHolder
                {
                    FirstName = "John",
                    LastName = "Smith",
                    BillingAddress = new CreateBacsBillingAddress
                    {
                        Country = CountryCode.GB
                    }
                }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"type\":\"bacs\"");
            json.ShouldContain("\"processing_channel_id\":\"pc_q4dbxom5jbgudnjzjpz7j2z6uq\"");
            json.ShouldContain("\"account_number\":\"86753246\"");
            json.ShouldContain("\"bank_code\":\"040004\"");
            json.ShouldContain("\"payment_type\":\"Recurring\"");
            json.ShouldContain("\"first_name\":\"John\"");
            json.ShouldContain("\"last_name\":\"Smith\"");
            json.ShouldNotContain("allow_partial_match");
            json.ShouldNotContain("customer");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForCreateBacsInstrumentRequest()
        {
            var original = CreateFullyPopulatedCreateBacsRequest();

            var json = Serializer.Serialize(original);
            var deserialized = (CreateBacsInstrumentRequest)Serializer
                .Deserialize(json, typeof(CreateBacsInstrumentRequest));

            deserialized.Type.ShouldBe(InstrumentType.Bacs);

            deserialized.Account.ProcessingChannelId.ShouldBe(original.Account.ProcessingChannelId);

            deserialized.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            deserialized.InstrumentData.BankCode.ShouldBe(original.InstrumentData.BankCode);
            deserialized.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            deserialized.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            deserialized.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            deserialized.InstrumentData.AllowPartialMatch.ShouldBe(original.InstrumentData.AllowPartialMatch);

            deserialized.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            deserialized.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            deserialized.AccountHolder.BillingAddress.AddressLine1
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine1);
            deserialized.AccountHolder.BillingAddress.AddressLine2
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine2);
            deserialized.AccountHolder.BillingAddress.City
                .ShouldBe(original.AccountHolder.BillingAddress.City);
            deserialized.AccountHolder.BillingAddress.Zip
                .ShouldBe(original.AccountHolder.BillingAddress.Zip);
            deserialized.AccountHolder.BillingAddress.Country
                .ShouldBe(original.AccountHolder.BillingAddress.Country);

            deserialized.Customer.Id.ShouldBe(original.Customer.Id);
            deserialized.Customer.Email.ShouldBe(original.Customer.Email);
            deserialized.Customer.Name.ShouldBe(original.Customer.Name);
            deserialized.Customer.Phone.CountryCode.ShouldBe(original.Customer.Phone.CountryCode);
            deserialized.Customer.Phone.Number.ShouldBe(original.Customer.Phone.Number);
            deserialized.Customer.Default.ShouldBe(original.Customer.Default);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForCreateBacsInstrumentRequest()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""account"": {
                    ""processing_channel_id"": ""pc_q4dbxom5jbgudnjzjpz7j2z6uq""
                },
                ""instrument_data"": {
                    ""account_number"": ""86753246"",
                    ""bank_code"": ""040004"",
                    ""country"": ""GB"",
                    ""currency"": ""GBP"",
                    ""payment_type"": ""Recurring"",
                    ""allow_partial_match"": false
                },
                ""account_holder"": {
                    ""first_name"": ""John"",
                    ""last_name"": ""Smith"",
                    ""billing_address"": {
                        ""address_line1"": ""Cloverfield St."",
                        ""address_line2"": ""23A"",
                        ""city"": ""London"",
                        ""zip"": ""SW1A 1AA"",
                        ""country"": ""GB""
                    }
                },
                ""customer"": {
                    ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                    ""email"": ""brucewayne@gmail.com"",
                    ""name"": ""Bruce Wayne"",
                    ""phone"": {
                        ""country_code"": ""+1"",
                        ""number"": ""415 555 2671""
                    },
                    ""default"": true
                }
            }";

            var request = (CreateBacsInstrumentRequest)Serializer
                .Deserialize(json, typeof(CreateBacsInstrumentRequest));

            request.ShouldNotBeNull();
            request.Type.ShouldBe(InstrumentType.Bacs);
            request.Account.ProcessingChannelId.ShouldBe("pc_q4dbxom5jbgudnjzjpz7j2z6uq");
            request.InstrumentData.AccountNumber.ShouldBe("86753246");
            request.InstrumentData.BankCode.ShouldBe("040004");
            request.InstrumentData.Country.ShouldBe(CountryCode.GB);
            request.InstrumentData.Currency.ShouldBe(Currency.GBP);
            request.InstrumentData.PaymentType.ShouldBe(BacsPaymentType.Recurring);
            request.InstrumentData.AllowPartialMatch.ShouldBe(false);
            request.AccountHolder.FirstName.ShouldBe("John");
            request.AccountHolder.LastName.ShouldBe("Smith");
            request.AccountHolder.BillingAddress.AddressLine1.ShouldBe("Cloverfield St.");
            request.AccountHolder.BillingAddress.AddressLine2.ShouldBe("23A");
            request.AccountHolder.BillingAddress.City.ShouldBe("London");
            request.AccountHolder.BillingAddress.Zip.ShouldBe("SW1A 1AA");
            request.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.GB);
            request.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            request.Customer.Email.ShouldBe("brucewayne@gmail.com");
            request.Customer.Name.ShouldBe("Bruce Wayne");
            request.Customer.Phone.CountryCode.ShouldBe("+1");
            request.Customer.Phone.Number.ShouldBe("415 555 2671");
            request.Customer.Default.ShouldBe(true);
        }

        private static CreateBacsInstrumentRequest CreateFullyPopulatedCreateBacsRequest()
        {
            return new CreateBacsInstrumentRequest
            {
                Account = new CreateBacsInstrumentAccount
                {
                    ProcessingChannelId = "pc_q4dbxom5jbgudnjzjpz7j2z6uq"
                },
                InstrumentData = new CreateBacsInstrumentData
                {
                    AccountNumber = "86753246",
                    BankCode = "040004",
                    Country = CountryCode.GB,
                    Currency = Currency.GBP,
                    PaymentType = BacsPaymentType.Regular,
                    AllowPartialMatch = true
                },
                AccountHolder = new CreateBacsAccountHolder
                {
                    FirstName = "John",
                    LastName = "Smith",
                    BillingAddress = new CreateBacsBillingAddress
                    {
                        AddressLine1 = "Cloverfield St.",
                        AddressLine2 = "23A",
                        City = "London",
                        Zip = "SW1A 1AA",
                        Country = CountryCode.GB
                    }
                },
                Customer = new CreateCustomerInstrumentRequest
                {
                    Id = "cus_y3oqhf46pyzuxjbcn2giaqnb44",
                    Email = "brucewayne@gmail.com",
                    Name = "Bruce Wayne",
                    Phone = new Phone
                    {
                        CountryCode = "+1",
                        Number = "415 555 2671"
                    },
                    Default = true
                }
            };
        }

        // ------------------------------------------------------------------------
        // UpdateBacsInstrumentRequest
        // Schema validation tests for UpdateBacsInstrumentRequest.
        // Covers all 19 properties, including nested objects, against the UpdateBacsInstrumentRequest
        // swagger schema.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeTypeOnly()
        {
            var request = new UpdateBacsInstrumentRequest();

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"type\":\"bacs\"");
            json.ShouldNotContain("instrument_data");
            json.ShouldNotContain("account_holder");
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForUpdateBacsInstrumentRequest()
        {
            var original = CreateFullyPopulatedUpdateBacsRequest();

            var json = Serializer.Serialize(original);
            var deserialized = (UpdateBacsInstrumentRequest)Serializer
                .Deserialize(json, typeof(UpdateBacsInstrumentRequest));

            deserialized.Type.ShouldBe(InstrumentType.Bacs);

            deserialized.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            deserialized.InstrumentData.BankCode.ShouldBe(original.InstrumentData.BankCode);
            deserialized.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            deserialized.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            deserialized.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            deserialized.InstrumentData.AllowPartialMatch.ShouldBe(original.InstrumentData.AllowPartialMatch);

            deserialized.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            deserialized.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            deserialized.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            deserialized.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            deserialized.AccountHolder.BillingAddress.AddressLine1
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine1);
            deserialized.AccountHolder.BillingAddress.AddressLine2
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine2);
            deserialized.AccountHolder.BillingAddress.City
                .ShouldBe(original.AccountHolder.BillingAddress.City);
            deserialized.AccountHolder.BillingAddress.Zip
                .ShouldBe(original.AccountHolder.BillingAddress.Zip);
            deserialized.AccountHolder.BillingAddress.Country
                .ShouldBe(original.AccountHolder.BillingAddress.Country);
        }

        [Fact]
        public void ShouldDeserializeSwaggerExampleForUpdateBacsInstrumentRequest()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""instrument_data"": {
                    ""account_number"": ""86753246"",
                    ""bank_code"": ""040004"",
                    ""country"": ""GB"",
                    ""currency"": ""GBP"",
                    ""payment_type"": ""Recurring"",
                    ""allow_partial_match"": true
                },
                ""account_holder"": {
                    ""first_name"": ""John"",
                    ""last_name"": ""Smith"",
                    ""company_name"": ""Wayne Enterprises"",
                    ""billing_address"": {
                        ""address_line1"": ""Cloverfield St."",
                        ""address_line2"": ""23A"",
                        ""city"": ""London"",
                        ""zip"": ""SW1A 1AA"",
                        ""country"": ""GB""
                    },
                    ""type"": ""corporate""
                }
            }";

            var request = (UpdateBacsInstrumentRequest)Serializer
                .Deserialize(json, typeof(UpdateBacsInstrumentRequest));

            request.ShouldNotBeNull();
            request.Type.ShouldBe(InstrumentType.Bacs);
            request.InstrumentData.AccountNumber.ShouldBe("86753246");
            request.InstrumentData.BankCode.ShouldBe("040004");
            request.InstrumentData.Country.ShouldBe(CountryCode.GB);
            request.InstrumentData.Currency.ShouldBe(Currency.GBP);
            request.InstrumentData.PaymentType.ShouldBe(BacsPaymentType.Recurring);
            request.InstrumentData.AllowPartialMatch.ShouldBe(true);
            request.AccountHolder.FirstName.ShouldBe("John");
            request.AccountHolder.LastName.ShouldBe("Smith");
            request.AccountHolder.CompanyName.ShouldBe("Wayne Enterprises");
            request.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Corporate);
            request.AccountHolder.BillingAddress.AddressLine1.ShouldBe("Cloverfield St.");
            request.AccountHolder.BillingAddress.AddressLine2.ShouldBe("23A");
            request.AccountHolder.BillingAddress.City.ShouldBe("London");
            request.AccountHolder.BillingAddress.Zip.ShouldBe("SW1A 1AA");
            request.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.GB);
        }

        private static UpdateBacsInstrumentRequest CreateFullyPopulatedUpdateBacsRequest()
        {
            return new UpdateBacsInstrumentRequest
            {
                InstrumentData = new UpdateBacsInstrumentData
                {
                    AccountNumber = "86753246",
                    BankCode = "040004",
                    Country = CountryCode.GB,
                    Currency = Currency.GBP,
                    PaymentType = BacsPaymentType.Regular,
                    AllowPartialMatch = true
                },
                AccountHolder = new UpdateBacsAccountHolder
                {
                    FirstName = "John",
                    LastName = "Smith",
                    CompanyName = "Wayne Enterprises",
                    Type = InstrumentAccountHolderType.Individual,
                    BillingAddress = new UpdateBacsBillingAddress
                    {
                        AddressLine1 = "Cloverfield St.",
                        AddressLine2 = "23A",
                        City = "London",
                        Zip = "SW1A 1AA",
                        Country = CountryCode.GB
                    }
                }
            };
        }

        // ------------------------------------------------------------------------
        // GetBacsInstrumentResponse
        // Schema validation tests for GetBacsInstrumentResponse.
        // Covers all 31 properties, including nested objects, against the RetrieveBacsInstrumentResponse
        // swagger schema.
        // ------------------------------------------------------------------------

        private const string FullResponseJson = @"{
            ""type"": ""bacs"",
            ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
            ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
            ""created_on"": ""2021-01-01T00:00:00Z"",
            ""vault_id"": ""vid_wmlfc3zyhqzehihu7giusaaawu"",
            ""modified_on"": ""2021-02-02T10:30:00Z"",
            ""account"": {
                ""client_id"": ""cli_memowvltf7aulpb3poehtiffei"",
                ""processing_channel_id"": ""pc_jcs4ufa6hrgepcrvhic4bfspay""
            },
            ""validations"": [
                { ""name"": ""account_number"", ""result"": ""passed"" }
            ],
            ""instrument_data"": {
                ""account_number"": ""86753246"",
                ""bank_code"": ""040004"",
                ""country"": ""GB"",
                ""currency"": ""GBP"",
                ""payment_type"": ""Recurring"",
                ""allow_partial_match"": true,
                ""status"": ""INVALID"",
                ""match_status"": ""no match"",
                ""description"": ""The name did not match with the account owner."",
                ""mandate_id"": ""6PZ6KFI3KW3UFHAM3J""
            },
            ""account_holder"": {
                ""first_name"": ""Hannah"",
                ""last_name"": ""Bret"",
                ""company_name"": ""Wayne Enterprises"",
                ""billing_address"": {
                    ""address_line1"": ""123 High St."",
                    ""address_line2"": ""Flat 456"",
                    ""city"": ""London"",
                    ""zip"": ""SW1A 1AA"",
                    ""country"": ""GB""
                },
                ""type"": ""corporate""
            },
            ""customer"": {
                ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""email"": ""brucewayne@gmail.com"",
                ""name"": ""Bruce Wayne"",
                ""default"": true
            }
        }";

        [Fact]
        public void ShouldDeserializeSwaggerExampleForGetBacsInstrumentResponse()
        {
            var response = (GetBacsInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetBacsInstrumentResponse));

            response.ShouldNotBeNull();
            response.Type.ShouldBe(InstrumentType.Bacs);
            response.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            response.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            response.CreatedOn?.ToUniversalTime()
                .ShouldBe(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            response.VaultId.ShouldBe("vid_wmlfc3zyhqzehihu7giusaaawu");
            response.ModifiedOn?.ToUniversalTime()
                .ShouldBe(new DateTime(2021, 2, 2, 10, 30, 0, DateTimeKind.Utc));

            response.Account.ClientId.ShouldBe("cli_memowvltf7aulpb3poehtiffei");
            response.Account.ProcessingChannelId.ShouldBe("pc_jcs4ufa6hrgepcrvhic4bfspay");

            response.Validations.Count.ShouldBe(1);
            response.Validations[0]["name"].ShouldBe("account_number");
            response.Validations[0]["result"].ShouldBe("passed");

            response.InstrumentData.AccountNumber.ShouldBe("86753246");
            response.InstrumentData.BankCode.ShouldBe("040004");
            response.InstrumentData.Country.ShouldBe(CountryCode.GB);
            response.InstrumentData.Currency.ShouldBe(Currency.GBP);
            response.InstrumentData.PaymentType.ShouldBe(BacsPaymentType.Recurring);
            response.InstrumentData.AllowPartialMatch.ShouldBe(true);
            response.InstrumentData.Status.ShouldBe("INVALID");
            response.InstrumentData.MatchStatus.ShouldBe("no match");
            response.InstrumentData.Description.ShouldBe("The name did not match with the account owner.");
            response.InstrumentData.MandateId.ShouldBe("6PZ6KFI3KW3UFHAM3J");

            response.AccountHolder.FirstName.ShouldBe("Hannah");
            response.AccountHolder.LastName.ShouldBe("Bret");
            response.AccountHolder.CompanyName.ShouldBe("Wayne Enterprises");
            response.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Corporate);
            response.AccountHolder.BillingAddress.AddressLine1.ShouldBe("123 High St.");
            response.AccountHolder.BillingAddress.AddressLine2.ShouldBe("Flat 456");
            response.AccountHolder.BillingAddress.City.ShouldBe("London");
            response.AccountHolder.BillingAddress.Zip.ShouldBe("SW1A 1AA");
            response.AccountHolder.BillingAddress.Country.ShouldBe(CountryCode.GB);

            response.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            response.Customer.Email.ShouldBe("brucewayne@gmail.com");
            response.Customer.Name.ShouldBe("Bruce Wayne");
            response.Customer.Default.ShouldBe(true);
        }

        [Fact]
        public void ShouldSerializeAccountHolderOnlyOnce()
        {
            // GetBacsInstrumentResponse hides the base GetInstrumentResponse.AccountHolder, whose
            // shared type carries fields the Bacs schema does not declare. Assert the hidden base
            // member does not produce a second account_holder key.
            var response = (GetBacsInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetBacsInstrumentResponse));

            var json = Serializer.Serialize(response);

            Regex.Matches(json, "\"account_holder\"").Count.ShouldBe(1);
        }

        [Fact]
        public void ShouldRoundTripSerializeAllPropertiesForGetBacsInstrumentResponse()
        {
            var original = (GetBacsInstrumentResponse)Serializer
                .Deserialize(FullResponseJson, typeof(GetBacsInstrumentResponse));

            var json = Serializer.Serialize(original);
            var deserialized = (GetBacsInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetBacsInstrumentResponse));

            deserialized.Type.ShouldBe(original.Type);
            deserialized.Id.ShouldBe(original.Id);
            deserialized.Fingerprint.ShouldBe(original.Fingerprint);
            deserialized.CreatedOn.ShouldBe(original.CreatedOn);
            deserialized.VaultId.ShouldBe(original.VaultId);
            deserialized.ModifiedOn.ShouldBe(original.ModifiedOn);

            deserialized.Account.ClientId.ShouldBe(original.Account.ClientId);
            deserialized.Account.ProcessingChannelId.ShouldBe(original.Account.ProcessingChannelId);

            deserialized.Validations.Count.ShouldBe(original.Validations.Count);
            deserialized.Validations[0]["name"].ShouldBe(original.Validations[0]["name"]);
            deserialized.Validations[0]["result"].ShouldBe(original.Validations[0]["result"]);

            deserialized.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            deserialized.InstrumentData.BankCode.ShouldBe(original.InstrumentData.BankCode);
            deserialized.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            deserialized.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            deserialized.InstrumentData.PaymentType.ShouldBe(original.InstrumentData.PaymentType);
            deserialized.InstrumentData.AllowPartialMatch.ShouldBe(original.InstrumentData.AllowPartialMatch);
            deserialized.InstrumentData.Status.ShouldBe(original.InstrumentData.Status);
            deserialized.InstrumentData.MatchStatus.ShouldBe(original.InstrumentData.MatchStatus);
            deserialized.InstrumentData.Description.ShouldBe(original.InstrumentData.Description);
            deserialized.InstrumentData.MandateId.ShouldBe(original.InstrumentData.MandateId);

            deserialized.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            deserialized.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            deserialized.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            deserialized.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            deserialized.AccountHolder.BillingAddress.AddressLine1
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine1);
            deserialized.AccountHolder.BillingAddress.AddressLine2
                .ShouldBe(original.AccountHolder.BillingAddress.AddressLine2);
            deserialized.AccountHolder.BillingAddress.City
                .ShouldBe(original.AccountHolder.BillingAddress.City);
            deserialized.AccountHolder.BillingAddress.Zip
                .ShouldBe(original.AccountHolder.BillingAddress.Zip);
            deserialized.AccountHolder.BillingAddress.Country
                .ShouldBe(original.AccountHolder.BillingAddress.Country);

            deserialized.Customer.Id.ShouldBe(original.Customer.Id);
            deserialized.Customer.Email.ShouldBe(original.Customer.Email);
            deserialized.Customer.Name.ShouldBe(original.Customer.Name);
            deserialized.Customer.Default.ShouldBe(original.Customer.Default);
        }

        // ------------------------------------------------------------------------
        // BacsInstrumentDispatch
        // Schema validation tests for the polymorphic dispatch of the bacs instrument type across the
        // create, update and get instrument responses.
        // ------------------------------------------------------------------------

        private const string FingerprintPattern = "^([a-z0-9]{26})$";


        [Fact]
        public void ShouldDispatchCreateResponseToBacsType()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q""
            }";

            var response = (CreateInstrumentResponse)Serializer
                .Deserialize(json, typeof(CreateInstrumentResponse));

            response.ShouldBeOfType<CreateBacsInstrumentResponse>();
            response.Type.ShouldBe(InstrumentType.Bacs);
            response.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            response.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            Regex.IsMatch(response.Fingerprint, FingerprintPattern).ShouldBeTrue();
        }

        [Fact]
        public void ShouldDispatchUpdateResponseToBacsType()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q""
            }";

            var response = (UpdateInstrumentResponse)Serializer
                .Deserialize(json, typeof(UpdateInstrumentResponse));

            var bacsResponse = response.ShouldBeOfType<UpdateBacsInstrumentResponse>();
            bacsResponse.Type.ShouldBe(InstrumentType.Bacs);
            bacsResponse.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            bacsResponse.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            Regex.IsMatch(bacsResponse.Fingerprint, FingerprintPattern).ShouldBeTrue();
        }

        [Theory]
        [InlineData("sepa", typeof(UpdateSepaInstrumentResponse))]
        [InlineData("ach", typeof(UpdateAchInstrumentResponse))]
        [InlineData("bacs", typeof(UpdateBacsInstrumentResponse))]
        public void ShouldDispatchUpdateResponseForEveryTypedVariant(string wireType, Type expected)
        {
            var json = "{\"type\":\"" + wireType + "\",\"id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"," +
                       "\"fingerprint\":\"vnsdrvikkvre3dtrjjvlm5du4q\"}";

            var response = (UpdateInstrumentResponse)Serializer
                .Deserialize(json, typeof(UpdateInstrumentResponse));

            response.ShouldBeOfType(expected);

            // sepa, ach and bacs each declare a required id, so the value must survive dispatch.
            var id = expected.GetProperty("Id");
            id.ShouldNotBeNull();
            id.GetValue(response).ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
        }

        [Theory]
        [InlineData("sepa", typeof(CreateSepaInstrumentResponse))]
        [InlineData("ach", typeof(CreateAchInstrumentResponse))]
        [InlineData("bacs", typeof(CreateBacsInstrumentResponse))]
        public void ShouldDispatchCreateResponseForEveryTypedVariant(string wireType, Type expected)
        {
            var json = "{\"type\":\"" + wireType + "\",\"id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"," +
                       "\"fingerprint\":\"vnsdrvikkvre3dtrjjvlm5du4q\"}";

            var response = (CreateInstrumentResponse)Serializer
                .Deserialize(json, typeof(CreateInstrumentResponse));

            response.ShouldBeOfType(expected);
        }

        [Theory]
        [InlineData("sepa", typeof(GetSepaInstrumentResponse))]
        [InlineData("ach", typeof(GetAchInstrumentResponse))]
        [InlineData("bacs", typeof(GetBacsInstrumentResponse))]
        public void ShouldDispatchGetResponseForEveryTypedVariant(string wireType, Type expected)
        {
            var json = "{\"type\":\"" + wireType + "\",\"id\":\"src_wmlfc3zyhqzehihu7giusaaawu\"," +
                       "\"fingerprint\":\"vnsdrvikkvre3dtrjjvlm5du4q\"}";

            var response = (GetInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetInstrumentResponse));

            response.ShouldBeOfType(expected);
        }

        [Fact]
        public void ShouldDispatchGetResponseToBacsType()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
                ""created_on"": ""2021-01-01T00:00:00Z"",
                ""vault_id"": ""vid_wmlfc3zyhqzehihu7giusaaawu""
            }";

            var response = (GetInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetInstrumentResponse));

            var bacsResponse = response.ShouldBeOfType<GetBacsInstrumentResponse>();
            bacsResponse.Type.ShouldBe(InstrumentType.Bacs);
            bacsResponse.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            bacsResponse.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            bacsResponse.VaultId.ShouldBe("vid_wmlfc3zyhqzehihu7giusaaawu");
            Regex.IsMatch(bacsResponse.Fingerprint, FingerprintPattern).ShouldBeTrue();
        }

        // ------------------------------------------------------------------------
        // BacsPaymentType
        // Schema validation tests for BacsPaymentType.
        // The Bacs wire values are capitalized, unlike the lowercase values the spec declares for the
        // SEPA instrument payment type. This test pins the casing so the two are not unified.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(BacsPaymentType.Recurring, "Recurring")]
        [InlineData(BacsPaymentType.Regular, "Regular")]
        public void ShouldSerializeCapitalizedWireValue(BacsPaymentType paymentType, string expected)
        {
            var instrumentData = new CreateBacsInstrumentData { PaymentType = paymentType };

            var json = Serializer.Serialize(instrumentData);

            // Asserted as an exact match: Shouldly's string containment checks are
            // case-insensitive, so they cannot pin the capitalization on their own.
            json.ShouldBe("{\"payment_type\":\"" + expected + "\"}");
        }

        [Theory]
        [InlineData("Recurring", BacsPaymentType.Recurring)]
        [InlineData("Regular", BacsPaymentType.Regular)]
        public void ShouldDeserializeCapitalizedWireValue(string wireValue, BacsPaymentType expected)
        {
            var json = "{\"payment_type\":\"" + wireValue + "\"}";

            var instrumentData = (CreateBacsInstrumentData)Serializer
                .Deserialize(json, typeof(CreateBacsInstrumentData));

            instrumentData.PaymentType.ShouldBe(expected);
        }

        [Theory]
        [InlineData(BacsPaymentType.Recurring, "Recurring")]
        [InlineData(BacsPaymentType.Regular, "Regular")]
        public void ShouldExposeCapitalizedEnumMemberValue(BacsPaymentType paymentType, string expected)
        {
            CheckoutUtils.GetEnumMemberValue(paymentType).ShouldBe(expected);
            CheckoutUtils.GetEnumFromStringMemberValue<BacsPaymentType>(expected).ShouldBe(paymentType);
        }

        [Fact]
        public void ShouldDeclareExactlyTheTwoBacsPaymentTypes()
        {
            // Guards against a future tidy-up replacing BacsPaymentType with the general
            // Checkout.Payments.PaymentType, which also accepts MOTO, Installment, PayLater and
            // Unscheduled. The Bacs instrument specification allows Recurring and Regular only.
            Enum.GetValues(typeof(BacsPaymentType)).Length.ShouldBe(2);
            Enum.GetValues(typeof(PaymentType)).Length.ShouldBe(6);
        }
    }
}
