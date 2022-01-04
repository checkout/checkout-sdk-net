namespace Checkout.Common
{
    public class Response
    {
        private int? StatusCode { get; set; }

        private string Body { get; set; }

        private string RequestId { get; set; }
    }
}