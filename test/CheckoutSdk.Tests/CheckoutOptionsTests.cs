using Microsoft.Extensions.Configuration;
using Shouldly;
using Xunit;

namespace Checkout.Tests
{
    public class CheckoutOptionsTests
    {
        [Fact]
        public void CanCreateConfiguration()
        {
            var options = new CheckoutOptions
            {
                SecretKey = "sk_xxx",
                PublicKey = "pk_xxx",
                UseSandbox = true
            };
            
            var configuration = options.CreateConfiguration();
            configuration.SecretKey.ShouldBe(options.SecretKey);
            configuration.PublicKey.ShouldBe(options.PublicKey);
            configuration.Uri.ShouldBe(CheckoutConfiguration.SandboxUri);
        }

        [Fact]
        public void GivenSandboxFalseShouldUseProductionUri()
        {
            var options = new CheckoutOptions
            {
                SecretKey = "sk_xxx",
                UseSandbox = false
            };
            
            var configuration = options.CreateConfiguration();
            configuration.Uri.ShouldBe(CheckoutConfiguration.ProductionUri);   
        }

        [Fact]
        public void GivenUriProvidedShouldOverrideSandbox()
        {
            var options = new CheckoutOptions
            {
                SecretKey = "sk_xxx",
                UseSandbox = true,
                Uri = "https://api.com"
            };        

            var configuration = options.CreateConfiguration();
            configuration.Uri.ShouldBe(options.Uri);
        }

        [Theory]
        [InlineData(default(string))]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenUriNullOrEmptyShouldDefaultToSandbox(string uri)
        {
            var options = new CheckoutOptions
            {
                SecretKey = "sk_xxx",
                Uri = uri
            };

            var configuration = options.CreateConfiguration();
            configuration.Uri.ShouldBe(CheckoutConfiguration.SandboxUri);
        }
    }
}