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

            eventTypesRetrievalResponse.Content.ShouldNotBeNull();
            eventTypesRetrievalResponse.Content.ShouldBeOfType<AvailableEventTypesResponse>();
            eventTypesRetrievalResponse.Content.Count.ShouldBeGreaterThan(0);
        }
    }
}
