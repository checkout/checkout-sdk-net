namespace Checkout
{
    public struct RequestMetrics
    {
        public string PrevRequestId { get; set; }
        public string RequestId { get; set; }
        public long PrevRequestDuration { get; set; }
    }
}