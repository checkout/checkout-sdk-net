using Checkout.Issuing.Cards.Requests.Revoke;
using Checkout.Issuing.Cards.Requests.Suspend;
using Shouldly;
using Xunit;

namespace Checkout.Issuing.Cards
{
    /// <summary>
    /// Serialization of revoke-card-request and suspend-card-request: every reason value
    /// roundtrips, the wire key is <c>reason</c>, the swagger example deserializes, and an empty
    /// request serializes to <c>{}</c>.
    /// </summary>
    public class CardRevokeSuspendRequestSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Theory]
        [InlineData(RevokeReason.Expired, "expired")]
        [InlineData(RevokeReason.ReportedLost, "reported_lost")]
        [InlineData(RevokeReason.ReportedStolen, "reported_stolen")]
        public void ShouldRoundTripRevokeCardRequest(RevokeReason reason, string wire)
        {
            var original = new RevokeCardRequest { Reason = reason };

            var json = _serializer.Serialize(original);
            json.ShouldBe("{\"reason\":\"" + wire + "\"}");

            var deserialized = (RevokeCardRequest)_serializer.Deserialize(json, typeof(RevokeCardRequest));
            deserialized.Reason.ShouldBe(reason);
        }

        [Fact]
        public void ShouldDeserializeRevokeCardRequestSwaggerExample()
        {
            var request = (RevokeCardRequest)_serializer.Deserialize("{\"reason\":\"reported_lost\"}", typeof(RevokeCardRequest));

            request.Reason.ShouldBe(RevokeReason.ReportedLost);
        }

        [Fact]
        public void ShouldSerializeEmptyRevokeCardRequest()
        {
            _serializer.Serialize(new RevokeCardRequest()).ShouldBe("{}");
            ((RevokeCardRequest)_serializer.Deserialize("{}", typeof(RevokeCardRequest))).Reason.ShouldBeNull();
        }

        [Theory]
        [InlineData(SuspendReason.SuspectedLost, "suspected_lost")]
        [InlineData(SuspendReason.SuspectedStolen, "suspected_stolen")]
        public void ShouldRoundTripSuspendCardRequest(SuspendReason reason, string wire)
        {
            var original = new SuspendCardRequest { Reason = reason };

            var json = _serializer.Serialize(original);
            json.ShouldBe("{\"reason\":\"" + wire + "\"}");

            var deserialized = (SuspendCardRequest)_serializer.Deserialize(json, typeof(SuspendCardRequest));
            deserialized.Reason.ShouldBe(reason);
        }

        [Fact]
        public void ShouldDeserializeSuspendCardRequestSwaggerExample()
        {
            var request = (SuspendCardRequest)_serializer.Deserialize("{\"reason\":\"suspected_lost\"}", typeof(SuspendCardRequest));

            request.Reason.ShouldBe(SuspendReason.SuspectedLost);
        }

        [Fact]
        public void ShouldSerializeEmptySuspendCardRequest()
        {
            _serializer.Serialize(new SuspendCardRequest()).ShouldBe("{}");
            ((SuspendCardRequest)_serializer.Deserialize("{}", typeof(SuspendCardRequest))).Reason.ShouldBeNull();
        }
    }
}
