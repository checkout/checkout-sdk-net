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
    /// Schema validation tests for the ACH instrument family, against the StoreAchInstrumentRequest,
    /// UpdateAchInstrumentRequest and RetrieveAchInstrumentResponse swagger schemas.
    /// </summary>
    public class AchInstrumentSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldRoundTripSerializeCreateRequest()
        {
            var original = new CreateAchInstrumentRequest
            {
                InstrumentData = new CreateAchInstrumentData
                {
                    AccountType = AchAccountType.Checking,
                    AccountNumber = "1234567890",
                    BankCode = "011075150",
                    Currency = Currency.USD,
                    Country = CountryCode.US
                },
                AccountHolder = new CreateAchAccountHolder
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    CompanyName = "Wayne Enterprises",
                    Type = InstrumentAccountHolderType.Corporate
                },
                Customer = new CreateCustomerInstrumentRequest
                {
                    Id = "cus_y3oqhf46pyzuxjbcn2giaqnb44",
                    Email = "brucewayne@gmail.com",
                    Name = "Bruce Wayne",
                    Phone = new Phone { CountryCode = "+1", Number = "415 555 2671" },
                    Default = true
                }
            };

            var json = _serializer.Serialize(original);
            var d = (CreateAchInstrumentRequest)_serializer
                .Deserialize(json, typeof(CreateAchInstrumentRequest));

            json.ShouldContain("\"type\":\"ach\"");
            d.Type.ShouldBe(InstrumentType.Ach);
            d.InstrumentData.AccountType.ShouldBe(original.InstrumentData.AccountType);
            d.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            d.InstrumentData.BankCode.ShouldBe(original.InstrumentData.BankCode);
            d.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            d.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            d.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
            d.Customer.Id.ShouldBe(original.Customer.Id);
            d.Customer.Email.ShouldBe(original.Customer.Email);
            d.Customer.Name.ShouldBe(original.Customer.Name);
            d.Customer.Phone.Number.ShouldBe(original.Customer.Phone.Number);
            d.Customer.Default.ShouldBe(original.Customer.Default);
        }

        [Fact]
        public void ShouldRoundTripSerializeUpdateRequest()
        {
            var original = new UpdateAchInstrumentRequest
            {
                InstrumentData = new UpdateAchInstrumentData
                {
                    AccountType = AchAccountType.Savings,
                    AccountNumber = "1234567890",
                    BankCode = "011075150",
                    Currency = Currency.USD,
                    Country = CountryCode.US
                },
                AccountHolder = new UpdateAchAccountHolder
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    CompanyName = "Wayne Enterprises",
                    Type = InstrumentAccountHolderType.Individual
                }
            };

            var json = _serializer.Serialize(original);
            var d = (UpdateAchInstrumentRequest)_serializer
                .Deserialize(json, typeof(UpdateAchInstrumentRequest));

            json.ShouldContain("\"account_type\":\"savings\"");
            d.Type.ShouldBe(InstrumentType.Ach);
            d.InstrumentData.AccountType.ShouldBe(original.InstrumentData.AccountType);
            d.InstrumentData.AccountNumber.ShouldBe(original.InstrumentData.AccountNumber);
            d.InstrumentData.BankCode.ShouldBe(original.InstrumentData.BankCode);
            d.InstrumentData.Currency.ShouldBe(original.InstrumentData.Currency);
            d.InstrumentData.Country.ShouldBe(original.InstrumentData.Country);
            d.AccountHolder.FirstName.ShouldBe(original.AccountHolder.FirstName);
            d.AccountHolder.LastName.ShouldBe(original.AccountHolder.LastName);
            d.AccountHolder.CompanyName.ShouldBe(original.AccountHolder.CompanyName);
            d.AccountHolder.Type.ShouldBe(original.AccountHolder.Type);
        }

        [Fact]
        public void ShouldDeserializeRetrieveResponseSwaggerExample()
        {
            const string json = @"{
                ""type"": ""ach"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
                ""created_on"": ""2021-01-01T00:00:00Z"",
                ""modified_on"": ""2021-02-02T10:30:00Z"",
                ""vault_id"": ""vid_wmlfc3zyhqzehihu7giusaaawu"",
                ""instrument_data"": {
                    ""account_type"": ""checking"",
                    ""account_number"": ""1234567890"",
                    ""bank_code"": ""011075150"",
                    ""currency"": ""USD"",
                    ""country"": ""US""
                },
                ""account_holder"": {
                    ""first_name"": ""Bruce"",
                    ""last_name"": ""Wayne"",
                    ""company_name"": ""Wayne Enterprises"",
                    ""type"": ""corporate""
                },
                ""customer"": {
                    ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                    ""email"": ""brucewayne@gmail.com"",
                    ""name"": ""Bruce Wayne"",
                    ""default"": true
                }
            }";

            var r = (GetAchInstrumentResponse)_serializer
                .Deserialize(json, typeof(GetAchInstrumentResponse));

            r.Type.ShouldBe(InstrumentType.Ach);
            r.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            r.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            r.CreatedOn?.ToUniversalTime().ShouldBe(new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            r.ModifiedOn?.ToUniversalTime().ShouldBe(new DateTime(2021, 2, 2, 10, 30, 0, DateTimeKind.Utc));
            r.VaultId.ShouldBe("vid_wmlfc3zyhqzehihu7giusaaawu");
            r.InstrumentData.AccountType.ShouldBe(AchAccountType.Checking);
            r.InstrumentData.AccountNumber.ShouldBe("1234567890");
            r.InstrumentData.BankCode.ShouldBe("011075150");
            r.InstrumentData.Currency.ShouldBe(Currency.USD);
            r.InstrumentData.Country.ShouldBe(CountryCode.US);
            r.AccountHolder.FirstName.ShouldBe("Bruce");
            r.AccountHolder.LastName.ShouldBe("Wayne");
            r.AccountHolder.CompanyName.ShouldBe("Wayne Enterprises");
            r.AccountHolder.Type.ShouldBe(InstrumentAccountHolderType.Corporate);
            r.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            r.Customer.Email.ShouldBe("brucewayne@gmail.com");
            r.Customer.Name.ShouldBe("Bruce Wayne");
            r.Customer.Default.ShouldBe(true);
        }
    }
}
