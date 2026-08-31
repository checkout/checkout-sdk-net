using Xunit;
using Checkout;

namespace CheckoutSdkTest
{
    public class SubdomainFunctionalityTest
    {
        [Fact]
        public void ShouldCreateEnvironmentSubdomainWithBothApiAndAuthorizationUris()
        {
            var subdomain = "testmerchant";
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);
            
            // Verify API URI has subdomain
            var expectedApiUri = $"https://{subdomain}.api.sandbox.checkout.com/";
            Assert.Equal(expectedApiUri, environmentSubdomain.ApiUri.ToString());
            
            // Verify Authorization URI has subdomain
            var expectedAuthUri = $"https://{subdomain}.access.sandbox.checkout.com/connect/token";
            Assert.Equal(expectedAuthUri, environmentSubdomain.AuthorizationUri.ToString());
        }
        
        [Fact]
        public void ShouldCreateEnvironmentSubdomainWithProductionEnvironment()
        {
            var subdomain = "prodmerchant";
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Production, subdomain);
            
            // Verify API URI has subdomain
            var expectedApiUri = $"https://{subdomain}.api.checkout.com/";
            Assert.Equal(expectedApiUri, environmentSubdomain.ApiUri.ToString());
            
            // Verify Authorization URI has subdomain
            var expectedAuthUri = $"https://{subdomain}.access.checkout.com/connect/token";
            Assert.Equal(expectedAuthUri, environmentSubdomain.AuthorizationUri.ToString());
        }
        
        [Fact]
        public void ShouldThrowForInvalidSubdomainFormat()
        {
            var invalidSubdomain = "invalid_subdomain!";

            var exception = Assert.Throws<CheckoutArgumentException>(
                () => new EnvironmentSubdomain(Environment.Sandbox, invalidSubdomain));

            Assert.Contains("invalid environment subdomain", exception.Message);
        }

        /// <summary>
        /// A value read from a file often carries a trailing newline; with ^/$ anchors it
        /// used to pass validation and blow up later as an unparseable host.
        /// </summary>
        [Fact]
        public void ShouldThrowForSubdomainWithTrailingNewline()
        {
            var exception = Assert.Throws<CheckoutArgumentException>(
                () => new EnvironmentSubdomain(Environment.Sandbox, "vkuhvk4v\n"));

            Assert.Contains("invalid environment subdomain", exception.Message);
        }

        [Fact]
        public void ShouldThrowForNullSubdomain()
        {
            var exception = Assert.Throws<CheckoutArgumentException>(
                () => new EnvironmentSubdomain(Environment.Sandbox, null));

            Assert.Contains("invalid environment subdomain", exception.Message);
        }

        [Fact]
        public void ShouldAddSubdomainForPrivateLinkPrefix()
        {
            var subdomain = "pl-vkuhvk4v";
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, subdomain);

            Assert.Equal($"https://{subdomain}.api.sandbox.checkout.com/", environmentSubdomain.ApiUri.ToString());
            Assert.Equal($"https://{subdomain}.access.sandbox.checkout.com/connect/token", environmentSubdomain.AuthorizationUri.ToString());
        }
    }
}