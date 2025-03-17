using Xunit;

namespace Checkout
{
    public class OAuthServiceResponseTests
    {
        [Fact]
        public void ShouldReturnTrueWhenResponseIsValid()
        {
            var response = new OAuthServiceResponse
            {
                AccessToken = "valid_token",
                TokenType = "Bearer",
                ExpiresIn = 3600,
                Error = null
            };

            Assert.True(response.IsValid());
        }

        [Fact]
        public void ShouldReturnFalseWhenResponseIsInvalid()
        {
            var response = new OAuthServiceResponse
            {
                AccessToken = null,
                TokenType = "Bearer",
                ExpiresIn = 3600,
                Error = null
            };

            Assert.False(response.IsValid());
        }

        [Fact]
        public void ShouldReturnFalseWhenExpiresInIsNegative()
        {
            var response = new OAuthServiceResponse
            {
                AccessToken = "valid_token",
                TokenType = "Bearer",
                ExpiresIn = -1,
                Error = null
            };

            Assert.False(response.IsValid());
        }
    }
}