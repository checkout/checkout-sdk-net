using Checkout.Common;
using Checkout.Instruments.Create;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Customers
{
    public class CustomersIntegrationTest : SandboxTestFixture
    {
        public CustomersIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact]
        private async Task ShouldCreateAndGetCustomer()
        {
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone {CountryCode = "1", Number = "4155552671"}
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerDetails = await DefaultApi.CustomersClient().Get(customerResponse.Id);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
            customerDetails.Phone.ShouldNotBeNull();
            customerDetails.DefaultId.ShouldBeNull();
            customerDetails.Instruments.ShouldBeEmpty();
        }
        
        [Fact]
        private async Task ShouldCreateAndGetCustomerWithInstrument()
        {
            var cardToken = await RequestToken();

            var tokenInstrument = await CreateTokenInstrument(cardToken);
                
            var customerRequest = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone {CountryCode = "1", Number = "4155552671"},
                DefaultId = tokenInstrument.Id
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(customerRequest);
            

            var customerDetails = await DefaultApi.CustomersClient().Get(customerResponse.Id);
            customerDetails.ShouldNotBeNull();
            customerDetails.DefaultId.ShouldNotBeNull();
            customerDetails.Instruments.ShouldNotBeEmpty();
            customerDetails.Instruments[0].Id.ShouldBe(tokenInstrument.Id);
        }

        [Fact]
        private async Task ShouldCreateAndUpdateCustomer()
        {
            //Create Customer
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone {CountryCode = "1", Number = "4155552671"}
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();
            //Edit Customer
            request.Email = GenerateRandomEmail();
            request.Name = "Changed Name";

            var customerId = customerResponse.Id;
            await DefaultApi.CustomersClient().Update(customerId, request);

            var customerDetails = await DefaultApi.CustomersClient().Get(customerId);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
        }

        [Fact]
        private async Task ShouldCreateAndDeleteCustomer()
        {
            var request = new CustomerRequest
            {
                Email = GenerateRandomEmail(),
                Name = "Customer",
                Phone = new Phone {CountryCode = "1", Number = "4155552671"}
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerId = customerResponse.Id;
            var emptyResponse = await DefaultApi.CustomersClient().Delete(customerId);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            await AssertNotFound(DefaultApi.CustomersClient().Get(customerId));
        }
    }
}