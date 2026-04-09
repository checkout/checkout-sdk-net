using Checkout.Common;
using Checkout.Metadata.Card;
using Checkout.Metadata.Card.Source;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Metadata
{
    public class CardMetadataResponseSerializationTest
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer();

        // ── Request serialization ──────────────────────────────────────────────

        [Fact]
        public void ShouldSerializeRequestWithCardSource()
        {
            var request = new CardMetadataRequest
            {
                Source = new CardMetadataCardSource { Number = "4543474002249996" },
                Format = CardMetadataFormatType.Basic,
                Reference = "ORD-5023-4E89"
            };

            var json = Serializer.Serialize(request);
            var result = (CardMetadataRequest)Serializer.Deserialize(json, typeof(CardMetadataRequest));

            result.Format.ShouldBe(CardMetadataFormatType.Basic);
            result.Reference.ShouldBe("ORD-5023-4E89");
        }

        [Fact]
        public void ShouldSerializeRequestWithBinSource()
        {
            var request = new CardMetadataRequest
            {
                Source = new CardMetadataBinSource { Bin = "454347" },
                Format = CardMetadataFormatType.CardPayouts
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"bin\"");
            json.ShouldContain("454347");
            json.ShouldContain("card_payouts");
        }

        [Fact]
        public void ShouldSerializeRequestWithTokenSource()
        {
            var request = new CardMetadataRequest
            {
                Source = new CardMetadataTokenSource { Token = "tok_ubfj2q76miwundwlk72vxt2i7q" }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"token\"");
            json.ShouldContain("tok_ubfj2q76miwundwlk72vxt2i7q");
        }

        [Fact]
        public void ShouldSerializeRequestWithIdSource()
        {
            var request = new CardMetadataRequest
            {
                Source = new CardMetadataIdSource { Id = "src_wmlfc3zyhqzehihu7giusaaawu" }
            };

            var json = Serializer.Serialize(request);

            json.ShouldContain("\"id\"");
            json.ShouldContain("src_wmlfc3zyhqzehihu7giusaaawu");
        }

        // ── Response deserialization ───────────────────────────────────────────

        [Fact]
        public void ShouldDeserializeFullResponse()
        {
            const string json = @"{
                ""bin"": ""45434720"",
                ""scheme"": ""visa"",
                ""local_schemes"": [""cartes_bancaires"", ""upi""],
                ""card_type"": ""credit"",
                ""card_category"": ""consumer"",
                ""currency"": ""EUR"",
                ""issuer"": ""STATE BANK OF MAURITIUS"",
                ""issuer_country"": ""MU"",
                ""issuer_country_name"": ""Mauritius"",
                ""is_combo_card"": true,
                ""product_id"": ""CLASSIC"",
                ""product_type"": ""F"",
                ""subproduct_id"": ""VA"",
                ""regulated_indicator"": true,
                ""regulated_type"": ""base_regulated"",
                ""is_reloadable_prepaid"": false,
                ""anonymous_prepaid_description"": ""Not prepaid or non-anonymous prepaid program/default"",
                ""card_payouts"": {
                    ""domestic_non_money_transfer"": ""standard"",
                    ""cross_border_non_money_transfer"": ""fast_funds"",
                    ""domestic_gambling"": ""not_supported"",
                    ""cross_border_gambling"": ""unknown"",
                    ""domestic_money_transfer"": ""standard"",
                    ""cross_border_money_transfer"": ""not_supported""
                },
                ""scheme_metadata"": {
                    ""accel"": [{ ""network_id"": ""aam"", ""bill_pay_indicator"": true, ""ecommerce_indicator"": true, ""money_transfer_indicator"": false, ""token_indicator"": false }],
                    ""pulse"": [{ ""network_id"": ""pls"", ""bill_pay_indicator"": false, ""ecommerce_indicator"": true, ""money_transfer_indicator"": true, ""token_indicator"": true }]
                },
                ""account_funding_transaction"": {
                    ""aft_indicator"": {
                        ""pull_funds"": {
                            ""cross_border"": true,
                            ""domestic"": false
                        }
                    }
                }
            }";

            var r = (CardMetadataResponse)Serializer.Deserialize(json, typeof(CardMetadataResponse));

            r.ShouldNotBeNull();
            r.Bin.ShouldBe("45434720");
            r.Scheme.ShouldBe("visa");
            r.LocalSchemes.Count.ShouldBe(2);
            r.LocalSchemes[0].ShouldBe(SchemeLocalType.CartesBancaires);
            r.LocalSchemes[1].ShouldBe(SchemeLocalType.Upi);
            r.CardType.ShouldBe(CardMetadataType.Credit);
            r.CardCategory.ShouldBe(CardCategory.Consumer);
            r.Currency.ShouldBe(Currency.EUR);
            r.Issuer.ShouldBe("STATE BANK OF MAURITIUS");
            r.IssuerCountry.ShouldBe(CountryCode.MU);
            r.IssuerCountryName.ShouldBe("Mauritius");
            r.IsComboCard.ShouldBe(true);
            r.ProductId.ShouldBe("CLASSIC");
            r.ProductType.ShouldBe("F");
            r.SubproductId.ShouldBe("VA");
            r.RegulatedIndicator.ShouldBe(true);
            r.RegulatedType.ShouldBe("base_regulated");
            r.IsReloadablePrepaid.ShouldBe(false);
            r.AnonymousPrepaidDescription.ShouldBe("Not prepaid or non-anonymous prepaid program/default");

            r.CardPayouts.ShouldNotBeNull();
            r.CardPayouts.DomesticNonMoneyTransfer.ShouldBe(PayoutsTransactionsType.Standard);
            r.CardPayouts.CrossBorderNonMoneyTransfer.ShouldBe(PayoutsTransactionsType.FastFunds);
            r.CardPayouts.DomesticGambling.ShouldBe(PayoutsTransactionsType.NotSupported);
            r.CardPayouts.CrossBorderGambling.ShouldBe(PayoutsTransactionsType.Unknown);
            r.CardPayouts.DomesticMoneyTransfer.ShouldBe(PayoutsTransactionsType.Standard);
            r.CardPayouts.CrossBorderMoneyTransfer.ShouldBe(PayoutsTransactionsType.NotSupported);

            r.SchemeMetadata.ShouldNotBeNull();
            r.SchemeMetadata.Accel.Count.ShouldBe(1);
            r.SchemeMetadata.Accel[0].NetworkId.ShouldBe("aam");
            r.SchemeMetadata.Accel[0].BillPayIndicator.ShouldBe(true);
            r.SchemeMetadata.Pulse.Count.ShouldBe(1);
            r.SchemeMetadata.Pulse[0].NetworkId.ShouldBe("pls");
            r.SchemeMetadata.Pulse[0].TokenIndicator.ShouldBe(true);

            r.AccountFundingTransaction.ShouldNotBeNull();
            r.AccountFundingTransaction.AftIndicator.ShouldNotBeNull();
            r.AccountFundingTransaction.AftIndicator.PullFunds.CrossBorder.ShouldBe(true);
            r.AccountFundingTransaction.AftIndicator.PullFunds.Domestic.ShouldBe(false);
        }

        [Fact]
        public void ShouldDeserializeMaestroLocalScheme()
        {
            const string json = @"{
                ""scheme"": ""visa"",
                ""bin"": ""47416530"",
                ""local_schemes"": [""maestro""],
                ""is_combo_card"": false,
                ""is_reloadable_prepaid"": true,
                ""anonymous_prepaid_description"": ""Anonymous prepaid program and not AMLD5 compliant"",
                ""account_funding_transaction"": {
                    ""aft_indicator"": {
                        ""pull_funds"": { ""cross_border"": true, ""domestic"": true }
                    }
                }
            }";

            var r = (CardMetadataResponse)Serializer.Deserialize(json, typeof(CardMetadataResponse));

            r.Bin.ShouldBe("47416530");
            r.IsComboCard.ShouldBe(false);
            r.IsReloadablePrepaid.ShouldBe(true);
            r.AnonymousPrepaidDescription.ShouldBe("Anonymous prepaid program and not AMLD5 compliant");
            r.LocalSchemes.Count.ShouldBe(1);
            r.LocalSchemes[0].ShouldBe(SchemeLocalType.Maestro);
            r.AccountFundingTransaction.AftIndicator.PullFunds.CrossBorder.ShouldBe(true);
        }

        [Fact]
        public void ShouldRoundTripSerializeResponse()
        {
            var original = new CardMetadataResponse
            {
                Bin = "45434720",
                Scheme = "visa",
                IsComboCard = true,
                IsReloadablePrepaid = false,
                AnonymousPrepaidDescription = "Not prepaid or non-anonymous prepaid program/default",
                LocalSchemes = new List<SchemeLocalType>
                {
                    SchemeLocalType.Maestro,
                    SchemeLocalType.CartesBancaires
                },
                RegulatedIndicator = true,
                RegulatedType = "base_regulated",
                CardPayouts = new CardMetadataPayouts
                {
                    DomesticNonMoneyTransfer = PayoutsTransactionsType.Standard,
                    CrossBorderNonMoneyTransfer = PayoutsTransactionsType.FastFunds,
                    DomesticGambling = PayoutsTransactionsType.NotSupported,
                    CrossBorderGambling = PayoutsTransactionsType.Unknown,
                    DomesticMoneyTransfer = PayoutsTransactionsType.Standard,
                    CrossBorderMoneyTransfer = PayoutsTransactionsType.NotSupported
                },
                SchemeMetadata = new SchemeMetadata
                {
                    Accel = new List<PinlessDebitSchemeMetadata>
                    {
                        new PinlessDebitSchemeMetadata
                        {
                            NetworkId = "aam",
                            NetworkDescription = "Accel Money Transfer Advantage",
                            BillPayIndicator = true,
                            EcommerceIndicator = true,
                            InterchangeFeeIndicator = "00",
                            MoneyTransferIndicator = false,
                            TokenIndicator = false
                        }
                    }
                },
                AccountFundingTransaction = new AccountFundingTransaction
                {
                    AftIndicator = new AftIndicator
                    {
                        PullFunds = new PullFunds { CrossBorder = true, Domestic = false }
                    }
                }
            };

            var json = Serializer.Serialize(original);
            var r = (CardMetadataResponse)Serializer.Deserialize(json, typeof(CardMetadataResponse));

            r.Bin.ShouldBe(original.Bin);
            r.Scheme.ShouldBe(original.Scheme);
            r.IsComboCard.ShouldBe(original.IsComboCard);
            r.IsReloadablePrepaid.ShouldBe(original.IsReloadablePrepaid);
            r.AnonymousPrepaidDescription.ShouldBe(original.AnonymousPrepaidDescription);
            r.LocalSchemes.Count.ShouldBe(2);
            r.LocalSchemes[0].ShouldBe(SchemeLocalType.Maestro);
            r.LocalSchemes[1].ShouldBe(SchemeLocalType.CartesBancaires);
            r.RegulatedIndicator.ShouldBe(original.RegulatedIndicator);
            r.RegulatedType.ShouldBe(original.RegulatedType);

            r.CardPayouts.DomesticNonMoneyTransfer.ShouldBe(PayoutsTransactionsType.Standard);
            r.CardPayouts.CrossBorderNonMoneyTransfer.ShouldBe(PayoutsTransactionsType.FastFunds);
            r.CardPayouts.DomesticGambling.ShouldBe(PayoutsTransactionsType.NotSupported);
            r.CardPayouts.CrossBorderGambling.ShouldBe(PayoutsTransactionsType.Unknown);
            r.CardPayouts.DomesticMoneyTransfer.ShouldBe(PayoutsTransactionsType.Standard);
            r.CardPayouts.CrossBorderMoneyTransfer.ShouldBe(PayoutsTransactionsType.NotSupported);

            r.SchemeMetadata.Accel[0].NetworkId.ShouldBe("aam");
            r.SchemeMetadata.Accel[0].NetworkDescription.ShouldBe("Accel Money Transfer Advantage");
            r.SchemeMetadata.Accel[0].BillPayIndicator.ShouldBe(true);
            r.SchemeMetadata.Accel[0].EcommerceIndicator.ShouldBe(true);
            r.SchemeMetadata.Accel[0].InterchangeFeeIndicator.ShouldBe("00");
            r.SchemeMetadata.Accel[0].MoneyTransferIndicator.ShouldBe(false);
            r.SchemeMetadata.Accel[0].TokenIndicator.ShouldBe(false);

            r.AccountFundingTransaction.AftIndicator.PullFunds.CrossBorder.ShouldBe(true);
            r.AccountFundingTransaction.AftIndicator.PullFunds.Domestic.ShouldBe(false);
        }
    }
}
