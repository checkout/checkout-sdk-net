using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Checkout
{
    public class CheckoutExceptionTests
    {
        [Fact]
        public async Task ShouldThrowCheckoutArgumentExceptionWithMessage()
        {
            var ex = await Assert.ThrowsAsync<CheckoutArgumentException>(() =>
                throw new CheckoutArgumentException("Argument error occurred"));
            Assert.Equal("Argument error occurred", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowCheckoutArgumentExceptionWithoutMessage()
        {
            var ex = await Assert.ThrowsAsync<CheckoutArgumentException>(() =>
                throw new CheckoutArgumentException(null));
            Assert.Equal("Exception of type 'Checkout.CheckoutArgumentException' was thrown.", ex.Message);
        }

        [Fact]
        public async Task ShouldThrowCheckoutAuthorizationExceptionWithMessage()
        {
            var ex = await Assert.ThrowsAsync<CheckoutAuthorizationException>(() =>
                throw new CheckoutAuthorizationException("Authorization error occurred"));
            Assert.Equal("Authorization error occurred", ex.Message);
        }

        [Theory]
        [InlineData(SdkAuthorizationType.SecretKey)]
        [InlineData(SdkAuthorizationType.PublicKey)]
        [InlineData(SdkAuthorizationType.SecretKeyOrOAuth)]
        [InlineData(SdkAuthorizationType.PublicKeyOrOAuth)]
        [InlineData(SdkAuthorizationType.OAuth)]
        [InlineData(SdkAuthorizationType.Custom)]
        public async Task ShouldThrowInvalidAuthorization(SdkAuthorizationType authType)
        {
            var ex = await Assert.ThrowsAsync<CheckoutAuthorizationException>(() =>
                throw CheckoutAuthorizationException.InvalidAuthorization(authType));
            Assert.Contains($"{authType} authorization type", ex.Message);
        }

        [Fact]
        public void ShouldConstructApiExceptionWithFullData()
        {
            var details = new Dictionary<string, object>
            {
                { "error_type", "request_invalid" },
                { "error_codes", new[] { "invalid_field" } },
                { "request_id", "req_123456" }
            };

            var exception = new CheckoutApiException("req_123456", HttpStatusCode.BadRequest, details);

            Assert.Equal("req_123456", exception.RequestId);
            Assert.Equal(HttpStatusCode.BadRequest, exception.HttpStatusCode);
            Assert.Equal(details, exception.ErrorDetails);
            Assert.Contains("400", exception.Message);
        }

        [Fact]
        public void ShouldHandleApiExceptionWithoutDetails()
        {
            var exception = new CheckoutApiException("req_789", HttpStatusCode.InternalServerError, null);

            Assert.Equal("req_789", exception.RequestId);
            Assert.Equal(HttpStatusCode.InternalServerError, exception.HttpStatusCode);
            Assert.Null(exception.ErrorDetails);
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public void ShouldHandleApiExceptionWithVariousStatusCodes(HttpStatusCode code)
        {
            var exception = new CheckoutApiException($"req_{(int)code}", code, null);

            Assert.Equal(code, exception.HttpStatusCode);
            Assert.Equal($"req_{(int)code}", exception.RequestId);
        }
    }
}
