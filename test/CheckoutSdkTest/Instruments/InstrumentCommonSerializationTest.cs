using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using Shouldly;
using Xunit;

namespace Checkout.Instruments
{
    /// <summary>
    /// Schema validation tests for Checkout.Instruments.
    /// Grouped by domain; each section below covers one subject.
    /// </summary>
    public class InstrumentCommonSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ------------------------------------------------------------------------
        // InstrumentResponseBase
        // Schema validation tests for the shared instrument response types, asserting they carry only
        // the properties their swagger schemas declare.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldNotExposeAPhoneOnTheInstrumentCustomerResponse()
        {
            // RetrieveInstrumentCustomerResponse declares id, email, name and default only.
            typeof(InstrumentCustomerResponse).GetProperty("Phone").ShouldBeNull();

            const string json = @"{
                ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""email"": ""brucewayne@gmail.com"",
                ""name"": ""Bruce Wayne"",
                ""default"": true
            }";

            var customer = (InstrumentCustomerResponse)Serializer
                .Deserialize(json, typeof(InstrumentCustomerResponse));

            customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            customer.Email.ShouldBe("brucewayne@gmail.com");
            customer.Name.ShouldBe("Bruce Wayne");
            customer.Default.ShouldBe(true);
        }

        [Fact]
        public void ShouldNotExposeACustomerOnTheCreateInstrumentResponseBase()
        {
            // Only the bank_account and card store variants declare a customer.
            typeof(CreateInstrumentResponse).GetProperty("Customer").ShouldBeNull();
            typeof(CreateBankAccountInstrumentResponse).GetProperty("Customer").ShouldNotBeNull();
            typeof(CreateTokenInstrumentResponse).GetProperty("Customer").ShouldNotBeNull();
            typeof(CreateSepaInstrumentResponse).GetProperty("Customer").ShouldBeNull();
            typeof(CreateAchInstrumentResponse).GetProperty("Customer").ShouldBeNull();
            typeof(CreateBacsInstrumentResponse).GetProperty("Customer").ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeCardStoreResponseWithAccountHolderAndNetworkToken()
        {
            const string json = @"{
                ""type"": ""card"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
                ""expiry_month"": 6,
                ""expiry_year"": 2025,
                ""scheme"": ""VISA"",
                ""last4"": ""9996"",
                ""bin"": ""454347"",
                ""account_holder"": {
                    ""first_name"": ""Hannah"",
                    ""last_name"": ""Bret"",
                    ""billing_address"": { ""city"": ""London"", ""country"": ""GB"" },
                    ""phone"": { ""country_code"": ""+44"", ""number"": ""7700900123"" }
                },
                ""customer"": { ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"" },
                ""network_token"": { ""id"": ""nt_y3oqhf46pyzuxjbcn2giaqnb44"", ""state"": ""active"" }
            }";

            var response = (CreateInstrumentResponse)Serializer
                .Deserialize(json, typeof(CreateInstrumentResponse));

            var card = response.ShouldBeOfType<CreateTokenInstrumentResponse>();
            card.AccountHolder.FirstName.ShouldBe("Hannah");
            card.AccountHolder.LastName.ShouldBe("Bret");
            card.AccountHolder.BillingAddress.City.ShouldBe("London");
            card.AccountHolder.Phone.Number.ShouldBe("7700900123");
            card.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            card.NetworkToken.Id.ShouldBe("nt_y3oqhf46pyzuxjbcn2giaqnb44");
            card.NetworkToken.State.ShouldBe(InstrumentNetworkTokenState.Active);
        }

        [Fact]
        public void ShouldNotExposeAPhoneOnTheGetInstrumentResponseCustomer()
        {
            const string json = @"{
                ""type"": ""bacs"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""customer"": { ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"", ""default"": true }
            }";

            var response = (GetInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetInstrumentResponse));

            response.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            response.Customer.Default.ShouldBe(true);
        }

        // ------------------------------------------------------------------------
        // InstrumentType
        // Schema validation tests for InstrumentType, value by value in both directions.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(InstrumentType.BankAccount, "bank_account")]
        [InlineData(InstrumentType.Token, "token")]
        [InlineData(InstrumentType.Card, "card")]
        [InlineData(InstrumentType.CardToken, "card_token")]
        [InlineData(InstrumentType.Sepa, "sepa")]
        [InlineData(InstrumentType.Ach, "ach")]
        [InlineData(InstrumentType.Bacs, "bacs")]
        public void ShouldMapEveryInstrumentTypeToItsWireValue(InstrumentType type, string wireValue)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wireValue);
            CheckoutUtils.GetEnumFromStringMemberValue<InstrumentType>(wireValue).ShouldBe(type);
        }

        // ------------------------------------------------------------------------
        // InstrumentAccountHolderType
        // Schema validation tests for InstrumentAccountHolderType, value by value in both directions.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(InstrumentAccountHolderType.Individual, "individual")]
        [InlineData(InstrumentAccountHolderType.Corporate, "corporate")]
        public void ShouldMapEveryAccountHolderTypeToItsWireValue(
            InstrumentAccountHolderType accountHolderType,
            string wireValue)
        {
            CheckoutUtils.GetEnumMemberValue(accountHolderType).ShouldBe(wireValue);
            CheckoutUtils.GetEnumFromStringMemberValue<InstrumentAccountHolderType>(wireValue)
                .ShouldBe(accountHolderType);
        }

        // ------------------------------------------------------------------------
        // SepaAndBacsPaymentType
        // Schema validation tests pinning the casing of the two instrument payment-type enums.
        // SEPA is lowercase and Bacs Direct Debit is capitalized in the specification. This is the
        // regression test that stops the two being unified.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(SepaPaymentType.Recurring, "recurring")]
        [InlineData(SepaPaymentType.Regular, "regular")]
        public void ShouldMapSepaPaymentTypeToItsLowercaseWireValue(SepaPaymentType type, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<SepaPaymentType>(wire).ShouldBe(type);
        }

        [Fact]
        public void ShouldKeepSepaLowercaseAndBacsCapitalized()
        {
            CheckoutUtils.GetEnumMemberValue(SepaPaymentType.Recurring).ShouldBe("recurring");
            CheckoutUtils.GetEnumMemberValue(SepaPaymentType.Regular).ShouldBe("regular");
            CheckoutUtils.GetEnumMemberValue(BacsPaymentType.Recurring).ShouldBe("Recurring");
            CheckoutUtils.GetEnumMemberValue(BacsPaymentType.Regular).ShouldBe("Regular");
        }

        [Theory]
        [InlineData(SepaMandateType.Core, "Core")]
        [InlineData(SepaMandateType.B2B, "B2B")]
        public void ShouldMapSepaMandateTypeToItsWireValue(SepaMandateType type, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<SepaMandateType>(wire).ShouldBe(type);
        }

        [Theory]
        [InlineData(AchAccountType.Savings, "savings")]
        [InlineData(AchAccountType.Checking, "checking")]
        public void ShouldMapAchAccountTypeToItsWireValue(AchAccountType type, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<AchAccountType>(wire).ShouldBe(type);
        }

        [Theory]
        [InlineData(InstrumentNetworkTokenState.Active, "active")]
        [InlineData(InstrumentNetworkTokenState.Suspended, "suspended")]
        [InlineData(InstrumentNetworkTokenState.Inactive, "inactive")]
        public void ShouldMapNetworkTokenStateToItsWireValue(InstrumentNetworkTokenState state, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(state).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<InstrumentNetworkTokenState>(wire).ShouldBe(state);
        }

        // ------------------------------------------------------------------------
        // BankAccountInstrumentBankDetails
        // Schema validation tests pinning the bank details field to the wire name the specification
        // declares. StoreBankAccountInstrumentRequest, UpdateBankInstrumentRequest and
        // RetrieveBankAccountInstrumentResponse all declare it as bank. These classes previously
        // exposed it as BankDetails, which serialized as bank_details and was never accepted or
        // populated.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldSerializeBankAsBankOnTheBankAccountStoreRequest()
        {
            var request = new CreateBankAccountInstrumentRequest
            {
                Currency = Currency.GBP,
                Country = CountryCode.GB,
                Bank = new BankDetails { Name = "Lloyds TSB", Branch = "Bournemouth" }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"bank\":");
            json.ShouldContain("\"name\":\"Lloyds TSB\"");
            json.ShouldContain("\"branch\":\"Bournemouth\"");
            json.ShouldNotContain("bank_details");
            typeof(CreateBankAccountInstrumentRequest).GetProperty("BankDetails").ShouldBeNull();
        }

        [Fact]
        public void ShouldSerializeBankAsBankOnTheBankAccountUpdateRequest()
        {
            var request = new UpdateBankInstrumentRequest
            {
                Bank = new BankDetails { Name = "Lloyds TSB", Branch = "Bournemouth" }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"bank\":");
            json.ShouldNotContain("bank_details");
            typeof(UpdateBankInstrumentRequest).GetProperty("BankDetails").ShouldBeNull();
        }

        [Fact]
        public void ShouldDeserializeBankFromBankOnTheBankAccountRetrieveResponse()
        {
            const string json = @"{
                ""type"": ""bank_account"",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
                ""currency"": ""GBP"",
                ""country"": ""GB"",
                ""bank"": {
                    ""name"": ""Lloyds TSB"",
                    ""branch"": ""Bournemouth""
                }
            }";

            var response = (GetBankAccountInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetBankAccountInstrumentResponse));

            response.Bank.ShouldNotBeNull();
            response.Bank.Name.ShouldBe("Lloyds TSB");
            response.Bank.Branch.ShouldBe("Bournemouth");
            typeof(GetBankAccountInstrumentResponse).GetProperty("BankDetails").ShouldBeNull();
        }

        // ------------------------------------------------------------------------
        // BankAccountField
        // Schema validation tests for the bank account field formatting response. BankAccountFields
        // declares the field length bounds as min_length and max_length. MaxLength was previously
        // spelled Maxlength, which serialized as maxlength and was never populated.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldDeserializeBankAccountFieldLengthBounds()
        {
            const string json = @"{
                ""sections"": [{
                    ""name"": ""Account details"",
                    ""fields"": [{
                        ""id"": ""accountNumber"",
                        ""section"": ""Account details"",
                        ""display"": ""Account number"",
                        ""help_text"": ""Enter the account number"",
                        ""type"": ""string"",
                        ""required"": true,
                        ""validation_regex"": ""^[0-9]{8}$"",
                        ""min_length"": 8,
                        ""max_length"": 8,
                        ""allowed_options"": [{ ""id"": ""opt_1"", ""display"": ""Option 1"" }],
                        ""dependencies"": [{ ""field_id"": ""country"", ""value"": ""GB"" }]
                    }]
                }]
            }";

            var response = (BankAccountFieldResponse)Serializer
                .Deserialize(json, typeof(BankAccountFieldResponse));

            var field = response.Sections.ShouldHaveSingleItem().Fields.ShouldHaveSingleItem();
            field.Id.ShouldBe("accountNumber");
            field.Section.ShouldBe("Account details");
            field.Display.ShouldBe("Account number");
            field.HelpText.ShouldBe("Enter the account number");
            field.Type.ShouldBe("string");
            field.Required.ShouldBe(true);
            field.ValidationRegex.ShouldBe("^[0-9]{8}$");
            field.MinLength.ShouldBe(8);
            field.MaxLength.ShouldBe(8);
            field.AllowedOptions.ShouldHaveSingleItem().Id.ShouldBe("opt_1");
            field.Dependencies.ShouldHaveSingleItem().FieldId.ShouldBe("country");
            typeof(BankAccountField).GetProperty("Maxlength").ShouldBeNull();
        }

        [Fact]
        public void ShouldRoundTripBankAccountFieldLengthBounds()
        {
            var original = new BankAccountField { Id = "iban", MinLength = 15, MaxLength = 34 };

            var json = Serializer.Serialize(original);

            json.ShouldContain("\"min_length\":15");
            json.ShouldContain("\"max_length\":34");
            json.ShouldNotContain("maxlength");

            var deserialized = (BankAccountField)Serializer.Deserialize(json, typeof(BankAccountField));
            deserialized.MinLength.ShouldBe(15);
            deserialized.MaxLength.ShouldBe(34);
        }
    }
}
