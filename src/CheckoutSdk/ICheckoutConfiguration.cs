namespace Checkout
{
    public interface ICheckoutConfiguration
    {
        SdkCredentials SdkCredentials { get; }

        Environment Environment { get; }

        IHttpClientFactory HttpClientFactory { get; }

        CheckoutFilesConfiguration GetFilesApiConfiguration();
    }
}