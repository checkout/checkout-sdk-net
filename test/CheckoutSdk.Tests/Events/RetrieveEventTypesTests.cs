using System.Threading.Tasks;
using Checkout.Events;
using Shouldly;
using Xunit;

namespace Checkout.Tests.Events
{
    public class RetrieveEventTypesTests : ApiTestFixture
    {
        [Fact]
        public async Task CanRetrieveEventTypes()
        {
            var eventTypesRetrievalResponse = await Api.Events.RetrieveEventTypes();

            eventTypesRetrievalResponse.ShouldNotBeNull();
            eventTypesRetrievalResponse.ShouldBeOfType<AvailableEventTypesResponse>();
            eventTypesRetrievalResponse.Count.ShouldBeGreaterThan(0);
        }
    }
}
