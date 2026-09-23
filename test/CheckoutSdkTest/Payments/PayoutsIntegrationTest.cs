using Checkout.Common;
using Checkout.Payments.Request;
using Checkout.Payments.Request.Destination;
using Checkout.Payments.Request.Source;
using Checkout.Payments.Response;
using Checkout.Payments.Sender;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Payments
{
    public class PayoutsIntegrationTest : SandboxTestFixture
    {
        public PayoutsIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldMakeCardPayoutPayments()
        {
            PayoutRequest request = new PayoutRequest
            {
                Source = new PayoutRequestCurrencyAccountSource {Id = "ca_qcc7x4nxxk6efeogm7yczdnsxu"},
                Destination =
                    new PaymentRequestCardDestination
                    {
                        Number = "5219565036325411",
                        ExpiryMonth = 12,
                        ExpiryYear = 2030,
                        AccountHolder = new AccountHolder
                        {
                            Type = AccountHolderType.Individual,
                            FirstName = "Jhon",
                            LastName = "Smith",
                            DateOfBirth = "1939-05-05",
                            CountryOfBirth = CountryCode.FR,
                            BillingAddress =
                                new Address
                                {
                                    AddressLine1 = "Checkout",
                                    AddressLine2 = "Shepherdless Walk",
                                    City = "London",
                                    Zip = "N17BQ",
                                    Country = CountryCode.GB
                                },
                            Phone = new Phone {CountryCode = "44", Number = "09876512412"},
                            Identification = new AccountHolderIdentification
                            {
                                Type = AccountHolderIdentificationType.Passport,
                                Number = "E2341",
                                IssuingCountry = CountryCode.FR,
                                DateOfExpiry = "2030-05-05"
                            },
                            Email = "jonh.smith@checkout.com"
                        }
                    },
                Amount = 10,
                // The sandbox currency account this test draws from (ca_qcc7x4...) settles GBP.
                // Asking for EUR is rejected with a 422 carrying an empty error_codes array, so
                // the cause is not visible in the response. GBP also matches the GB sender and
                // billing addresses below.
                Currency = Currency.GBP,
                Reference = "Pay-out to Card - Money Transfer",
                BillingDescriptor = new PayoutBillingDescriptor {Reference = "Pay-out to Card - Money Transfer"},
                Sender = new PaymentIndividualSender
                {
                    FirstName = "Hayley",
                    LastName = "Jones",
                    Address = new Address
                    {
                        AddressLine1 = "Checkout",
                        AddressLine2 = "Shepherdless Walk",
                        City = "London",
                        Zip = "N17BQ",
                        Country = CountryCode.GB
                    },
                    Reference = "1234567ABCDEFG",
                    ReferenceType = "other",
                    DateOfBirth = "1939-05-05",
                    SourceOfFunds = SourceOfFunds.Credit,
                },
                Instruction = new Request.PaymentInstruction
                {
                    Purpose = "pension", FundsTransferType = "C07", Mvv = "0123456789"
                },
                ProcessingChannelId = "pc_q727c4x6vtwujbiys3bb7wjpaa"
            };

            PayoutResponse response = await DefaultApi.PaymentsClient().RequestPayout(request);
            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNull();
            response.Status.ShouldNotBe(default);
            response.Instruction.ShouldNotBeNull();
            response.Instruction.ValueDate.ShouldNotBeNull();
        }
    }
}