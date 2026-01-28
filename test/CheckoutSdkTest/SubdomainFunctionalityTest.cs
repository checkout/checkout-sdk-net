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
        public void ShouldNotAddSubdomainForInvalidSubdomainFormat()
        {
            var invalidSubdomain = "invalid-subdomain";
            var environmentSubdomain = new EnvironmentSubdomain(Environment.Sandbox, invalidSubdomain);
            
            // Should fallback to original URLs without subdomain
            Assert.Equal("https://api.sandbox.checkout.com/", environmentSubdomain.ApiUri.ToString());
            Assert.Equal("https://access.sandbox.checkout.com/connect/token", environmentSubdomain.AuthorizationUri.ToString());
        }
    }
}