using System.Threading.Tasks;
using Checkout.Common;
using Shouldly;
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
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerDetails = await DefaultApi.CustomersClient().Get(customerResponse.Id);
            customerDetails.ShouldNotBeNull();
            customerDetails.Email.ShouldBe(request.Email);
            customerDetails.Name.ShouldBe(request.Name);
            customerDetails.Phone.ShouldBe(request.Phone);
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
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
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
                Phone = new Phone
                {
                    CountryCode = "1",
                    Number = "4155552671"
                }
            };
            var customerResponse = await DefaultApi.CustomersClient().Create(request);
            customerResponse.ShouldNotBeNull();

            var customerId = customerResponse.Id;
            await DefaultApi.CustomersClient().Delete(customerId);
            await AssertNotFound(DefaultApi.CustomersClient().Get(customerId));
        }
    }
}