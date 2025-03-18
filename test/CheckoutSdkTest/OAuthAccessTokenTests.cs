using System;
using Xunit;

namespace Checkout
{
    public class OAuthAccessTokenTests
    {
        [Fact]
        public void ShouldReturnTrueAndTokenWhenResponseIsValid()
        {
            var response = new OAuthServiceResponse
            {
                AccessToken = "valid_token",
                TokenType = "Bearer",
                ExpiresIn = 3600
            };

            var token = OAuthAccessToken.FromOAuthServiceResponse(response);

            Assert.NotNull(token);
            Assert.Equal("valid_token", token.Token);
            Assert.True(token.IsValid());
        }

        [Fact]
        public void ShouldThrowExceptionWhenResponseIsInvalid()
        {
            var response = new OAuthServiceResponse
            {
                AccessToken = null,
                TokenType = "Bearer",
                ExpiresIn = 3600
            };

            Assert.Throws<ArgumentException>(() => OAuthAccessToken.FromOAuthServiceResponse(response));
        }
    }
}