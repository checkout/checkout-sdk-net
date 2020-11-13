namespace Checkout.Tests
{
    /// <summary>
    /// Extensions for <see cref="CheckoutHttpResponseMessage"/>.
    /// </summary>
    public static class CheckoutHttpResponseMessageExtensions
    {
        /// <summary>
        /// Adds mocked headers to the provided <paramref="checkoutHttpResponseMessage"/>.
        /// </summary>
        /// <param name="checkoutHttpResponseMessage">The <see cref="CheckoutHttpResponseMessage"/>.</param>
        /// <returns>The <see cref="CheckoutHttpResponseMessage"/> with added headers for mocking.</returns>
        public static CheckoutHttpResponseMessage<T> MockHeaders<T>(this CheckoutHttpResponseMessage<T> checkoutHttpResponseMessage)
        {
            checkoutHttpResponseMessage.Headers.Add("Cko-Request-Id", "bcfe03bd-2a8a-4340-8ce5-214761fae065");
            checkoutHttpResponseMessage.Headers.Add("Cko-Version", "3.46.1");

            return checkoutHttpResponseMessage;
        }
    }
}
