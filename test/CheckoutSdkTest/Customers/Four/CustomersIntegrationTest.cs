using Checkout.Common;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.Customers.Four
{
    public class CustomersIntegrationTest : SandboxTestFixture
    {
        public CustomersIntegrationTest() : base(PlatformType.Four)
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
            var customerResponse = await FourApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerDetails = await FourApi.CustomersClient().Get(customerResponse.Id);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
            customerDetails.Phone.ShouldNotBeNull();
            customerDetails.DefaultId.ShouldBeNull();
            customerDetails.Instruments.ShouldBeEmpty();
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
            var customerResponse = await FourApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();
            //Edit Customer
            request.Email = GenerateRandomEmail();
            request.Name = "Changed Name";

            var customerId = customerResponse.Id;
            await FourApi.CustomersClient().Update(customerId, request);

            var customerDetails = await FourApi.CustomersClient().Get(customerId);
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
            var customerResponse = await FourApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerId = customerResponse.Id;
            var emptyResponse = await FourApi.CustomersClient().Delete(customerId);
            emptyResponse.ShouldNotBeNull();
            emptyResponse.HttpStatusCode.ShouldNotBeNull();
            emptyResponse.ResponseHeaders.ShouldNotBeNull();

            await AssertNotFound(FourApi.CustomersClient().Get(customerId));
        }
    }
}