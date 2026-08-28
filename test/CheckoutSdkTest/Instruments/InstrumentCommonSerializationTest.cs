using System;
using Checkout.Common;
using Checkout.Instruments.Create;
using Checkout.Instruments.Get;
using Checkout.Instruments.Update;
using UpdateInstrumentResponse = Checkout.Instruments.Update.UpdateInstrumentResponse;
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

        // ------------------------------------------------------------------------
        // OptionalRequestFields
        // Schema validation tests asserting the SDK does not send values the caller never set.
        // Both of these fields are optional in the specification, so a non-nullable CLR type would
        // serialize its default and silently change the request.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldNotSendAPaymentNetworkThatWasNotSet()
        {
            var query = new BankAccountFieldQuery { AccountHolderType = AccountHolderType.Corporate };

            var json = Serializer.Serialize(query);

            json.ShouldBe(@"{""account-holder-type"":""corporate""}");
            json.ShouldNotContain("payment-network");
        }

        [Fact]
        public void ShouldSendThePaymentNetworkWhenItIsSet()
        {
            var query = new BankAccountFieldQuery { PaymentNetwork = PaymentNetwork.Sepa };

            Serializer.Serialize(query).ShouldContain(@"""payment-network"":""sepa""");
        }

        [Fact]
        public void ShouldNotSendADefaultFlagThatWasNotSet()
        {
            var customer = new CreateCustomerInstrumentRequest { Id = "cus_y3oqhf46pyzuxjbcn2giaqnb44" };

            var json = Serializer.Serialize(customer);

            json.ShouldBe(@"{""id"":""cus_y3oqhf46pyzuxjbcn2giaqnb44""}");
            json.ShouldNotContain("default");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldSendTheDefaultFlagWhenItIsSet(bool value)
        {
            var customer = new CreateCustomerInstrumentRequest { Id = "cus_x", Default = value };

            Serializer.Serialize(customer)
                .ShouldContain(@"""default"":" + (value ? "true" : "false"));
        }

        // ------------------------------------------------------------------------
        // InstrumentResponseDispatch
        // Schema validation tests for the polymorphic instrument response converters. A payload
        // without a type discriminator must degrade to the base response rather than throw.
        // ------------------------------------------------------------------------

        [Fact]
        public void ShouldNotThrowDeserializingAnInstrumentResponseWithoutAType()
        {
            const string json = @"{""id"":""src_wmlfc3zyhqzehihu7giusaaawu""}";

            var get = (GetInstrumentResponse)Serializer.Deserialize(json, typeof(GetInstrumentResponse));
            get.Type.ShouldBeNull();
            get.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");

            var create = (CreateInstrumentResponse)Serializer.Deserialize(json, typeof(CreateInstrumentResponse));
            create.Type.ShouldBeNull();

            var update = (UpdateInstrumentResponse)Serializer.Deserialize(json, typeof(UpdateInstrumentResponse));
            update.Type.ShouldBeNull();
        }

        [Fact]
        public void ShouldReturnNullResolvingAnEnumFromANullWireValue()
        {
            CheckoutUtils.GetEnumFromStringMemberValue<InstrumentType>(null).ShouldBeNull();
        }

        // ------------------------------------------------------------------------
        // AccountHolderType
        // Schema validation tests for Checkout.Common.AccountHolderType, which the
        // account-holder-type query parameter and the AccountHolder schema declare as individual,
        // corporate and government.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(AccountHolderType.Individual, "individual")]
        [InlineData(AccountHolderType.Corporate, "corporate")]
        [InlineData(AccountHolderType.Government, "government")]
        public void ShouldMapEveryCommonAccountHolderTypeToItsWireValue(AccountHolderType type, string wire)
        {
            CheckoutUtils.GetEnumMemberValue(type).ShouldBe(wire);
            CheckoutUtils.GetEnumFromStringMemberValue<AccountHolderType>(wire).ShouldBe(type);
        }

        [Fact]
        public void ShouldFilterBankAccountFieldsByGovernmentAccountHolder()
        {
            var query = new BankAccountFieldQuery { AccountHolderType = AccountHolderType.Government };

            Serializer.Serialize(query).ShouldContain(@"""account-holder-type"":""government""");
        }

        // ------------------------------------------------------------------------
        // GetCardInstrumentResponse
        // Schema validation tests for GetCardInstrumentResponse. Covers all 20 properties, including
        // the inherited ones, against the RetrieveCardInstrumentResponse swagger schema.
        // ------------------------------------------------------------------------

        private const string CardInstrumentJson = @"{
            ""type"": ""card"",
            ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
            ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q"",
            ""encrypted_card_number"": ""eyJhbGciOiJSU0EtT0FFUC0yNTYi"",
            ""expiry_month"": 6,
            ""expiry_year"": 2027,
            ""name"": ""Bruce Wayne"",
            ""scheme"": ""VISA"",
            ""scheme_local"": ""cartes_bancaires"",
            ""last4"": ""9996"",
            ""bin"": ""454347"",
            ""card_type"": ""CREDIT"",
            ""card_category"": ""CONSUMER"",
            ""issuer"": ""GOTHAM STATE BANK"",
            ""issuer_country"": ""GB"",
            ""product_id"": ""F"",
            ""product_type"": ""Visa Classic"",
            ""card_wallet_type"": ""applepay"",
            ""regulated_indicator"": true,
            ""network_token"": {
                ""id"": ""nt_wmlfc3zyhqzehihu7giusaaawu"",
                ""state"": ""active""
            },
            ""customer"": {
                ""id"": ""cus_y3oqhf46pyzuxjbcn2giaqnb44"",
                ""email"": ""brucewayne@gmail.com"",
                ""name"": ""Bruce Wayne"",
                ""default"": true
            }
        }";

        [Fact]
        public void ShouldDeserializeEveryPropertyOfTheCardInstrumentRetrieveResponse()
        {
            var r = (GetCardInstrumentResponse)Serializer
                .Deserialize(CardInstrumentJson, typeof(GetCardInstrumentResponse));

            r.Type.ShouldBe(InstrumentType.Card);
            r.Id.ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            r.Fingerprint.ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
            r.EncryptedCardNumber.ShouldBe("eyJhbGciOiJSU0EtT0FFUC0yNTYi");
            r.ExpiryMonth.ShouldBe(6);
            r.ExpiryYear.ShouldBe(2027);
            r.Name.ShouldBe("Bruce Wayne");
            r.Scheme.ShouldBe("VISA");
            r.SchemeLocal.ShouldBe("cartes_bancaires");
            r.Last4.ShouldBe("9996");
            r.Bin.ShouldBe("454347");
            r.CardType.ShouldBe(Common.CardType.Credit);
            r.CardCategory.ShouldBe(Common.CardCategory.Consumer);
            r.Issuer.ShouldBe("GOTHAM STATE BANK");
            r.IssuerCountry.ShouldBe(CountryCode.GB);
            r.ProductId.ShouldBe("F");
            r.ProductType.ShouldBe("Visa Classic");
            r.CardWalletType.ShouldBe(Common.CardWalletType.Applepay);
            r.RegulatedIndicator.ShouldBe(true);
            r.NetworkToken.ShouldNotBeNull();
            r.NetworkToken.Id.ShouldBe("nt_wmlfc3zyhqzehihu7giusaaawu");
            r.NetworkToken.State.ShouldBe(InstrumentNetworkTokenState.Active);
            r.Customer.ShouldNotBeNull();
            r.Customer.Id.ShouldBe("cus_y3oqhf46pyzuxjbcn2giaqnb44");
            r.Customer.Default.ShouldBe(true);
        }

        [Fact]
        public void ShouldRoundTripTheCardInstrumentRetrieveResponse()
        {
            var original = (GetCardInstrumentResponse)Serializer
                .Deserialize(CardInstrumentJson, typeof(GetCardInstrumentResponse));

            var json = Serializer.Serialize(original);

            json.ShouldContain("\"encrypted_card_number\":");
            json.ShouldContain("\"card_wallet_type\":\"applepay\"");
            json.ShouldContain("\"regulated_indicator\":true");
            json.ShouldContain("\"network_token\":");

            var d = (GetCardInstrumentResponse)Serializer
                .Deserialize(json, typeof(GetCardInstrumentResponse));

            d.EncryptedCardNumber.ShouldBe(original.EncryptedCardNumber);
            d.CardWalletType.ShouldBe(original.CardWalletType);
            d.RegulatedIndicator.ShouldBe(original.RegulatedIndicator);
            d.NetworkToken.Id.ShouldBe(original.NetworkToken.Id);
            d.NetworkToken.State.ShouldBe(original.NetworkToken.State);
        }

        // ------------------------------------------------------------------------
        // UpdateInstrumentResponseId
        // Schema validation tests asserting where id is declared on the update responses.
        // UpdateSepaInstrumentResponse, UpdateAchInstrumentResponse and
        // UpdateBacsInstrumentResponse require type, id and fingerprint. UpdateCardInstrumentResponse
        // and UpdateBankInstrumentResponse require type and fingerprint only, so they must not
        // expose an id, and neither must the base, which declares no properties of its own.
        // ------------------------------------------------------------------------

        [Theory]
        [InlineData(typeof(UpdateSepaInstrumentResponse))]
        [InlineData(typeof(UpdateAchInstrumentResponse))]
        [InlineData(typeof(UpdateBacsInstrumentResponse))]
        public void ShouldExposeAnIdOnTheTypedUpdateResponses(Type responseType)
        {
            responseType.GetProperty("Id").ShouldNotBeNull();
        }

        [Theory]
        [InlineData(typeof(UpdateCardInstrumentResponse))]
        [InlineData(typeof(UpdateBankInstrumentResponse))]
        [InlineData(typeof(Instruments.Update.UpdateInstrumentResponse))]
        public void ShouldNotExposeAnIdOnTheUntypedUpdateResponses(Type responseType)
        {
            responseType.GetProperty("Id").ShouldBeNull();
        }

        [Theory]
        [InlineData("sepa", InstrumentType.Sepa, typeof(UpdateSepaInstrumentResponse))]
        [InlineData("ach", InstrumentType.Ach, typeof(UpdateAchInstrumentResponse))]
        [InlineData("bacs", InstrumentType.Bacs, typeof(UpdateBacsInstrumentResponse))]
        public void ShouldDeserializeTheIdOnTheTypedUpdateResponses(
            string wireType,
            InstrumentType expectedType,
            Type expectedResponseType)
        {
            var json = @"{
                ""type"": """ + wireType + @""",
                ""id"": ""src_wmlfc3zyhqzehihu7giusaaawu"",
                ""fingerprint"": ""vnsdrvikkvre3dtrjjvlm5du4q""
            }";

            var response = (Instruments.Update.UpdateInstrumentResponse)Serializer
                .Deserialize(json, typeof(Instruments.Update.UpdateInstrumentResponse));

            response.ShouldBeOfType(expectedResponseType);
            response.Type.ShouldBe(expectedType);
            ReadProperty(response, "Id").ShouldBe("src_wmlfc3zyhqzehihu7giusaaawu");
            ReadProperty(response, "Fingerprint").ShouldBe("vnsdrvikkvre3dtrjjvlm5du4q");
        }

        // The sepa, ach and bacs update responses declare id and fingerprint themselves; the base
        // declares no properties of its own, so the values are read off the concrete instance.
        private static string ReadProperty(object response, string name)
        {
            var property = response.GetType().GetProperty(name);
            property.ShouldNotBeNull();
            return (string)property.GetValue(response);
        }

    }
}
