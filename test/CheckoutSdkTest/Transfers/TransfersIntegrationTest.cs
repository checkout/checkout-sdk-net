using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Transfers
{
    public class TransfersIntegrationTest : SandboxTestFixture
    {
        public TransfersIntegrationTest() : base(PlatformType.DefaultOAuth)
        {
        }

        [Fact]
        private async Task ShouldInitiateAndRetrieveTransferOfFunds()
        {
            var idempotencyKey = Guid.NewGuid().ToString();
            
            var createTransferRequest =
                new CreateTransferRequest
                {
                    Source = new TransferSourceRequest {Amount = 200, Id = "ent_kidtcgc3ge5unf4a5i6enhnr5m"},
                    Destination = new TransferDestinationRequest {Id = "ent_w4jelhppmfiufdnatam37wrfc4"},
                    TransferType = TransferType.Commission
                };

            var createTransferResponse =
                await DefaultApi.TransfersClient().InitiateTransferOfFunds(createTransferRequest, idempotencyKey);

            createTransferResponse.ShouldNotBeNull();
            createTransferResponse.Id.ShouldNotBeNullOrEmpty();
            createTransferResponse.Status.ShouldNotBeNull();
            createTransferResponse.Links.ShouldNotBeNull();
            createTransferResponse.Links.ShouldNotBeEmpty();

            var transferDetailsResponse = await DefaultApi.TransfersClient().RetrieveATransfer(createTransferResponse.Id);
            transferDetailsResponse.ShouldNotBeNull();
            transferDetailsResponse.Status.ShouldNotBeNull();
            transferDetailsResponse.TransferType.ShouldNotBeNull();
            transferDetailsResponse.RequestedOn.ShouldNotBeNull();
            transferDetailsResponse.Source.ShouldNotBeNull();
            transferDetailsResponse.Source.EntityId.ShouldNotBeNull();
            transferDetailsResponse.Destination.ShouldNotBeNull();
            transferDetailsResponse.Destination.EntityId.ShouldNotBeNull();
        }

        [Fact]
        private async Task ShouldInitiateTransferOfFundsIdempotently()
        {
            var createTransferRequest =
                new CreateTransferRequest
                {
                    Source = new TransferSourceRequest {Amount = 100, Id = "ent_kidtcgc3ge5unf4a5i6enhnr5m"},
                    Destination = new TransferDestinationRequest {Id = "ent_w4jelhppmfiufdnatam37wrfc4"},
                    TransferType = TransferType.Commission
                };

            var idempotencyKey = Guid.NewGuid().ToString();

            var createTransferResponse =
                await DefaultApi.TransfersClient().InitiateTransferOfFunds(createTransferRequest, idempotencyKey);

            createTransferResponse.ShouldNotBeNull();
            createTransferResponse.Id.ShouldNotBeNullOrEmpty();
            createTransferResponse.Status.ShouldNotBeNull();
            createTransferResponse.Links.ShouldNotBeNull();
            createTransferResponse.Links.ShouldNotBeEmpty();

            try
            {
                await DefaultApi.TransfersClient().InitiateTransferOfFunds(createTransferRequest, idempotencyKey);
            }
            catch (Exception ex)
            {
                ex.ShouldNotBeNull();
                ex.ShouldBeAssignableTo(typeof(CheckoutApiException));
                ex.Message.ShouldBe("The API response status code (409) does not indicate success.");
            }
        }
    }
}