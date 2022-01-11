namespace Checkout.Sessions.Completion
{
    public class HostedCompletionInfo : CompletionInfo
    {
        public string CallbackUrl { get; set; }

        public string SuccessUrl { get; set; }

        public string FailureUrl { get; set; }

        public HostedCompletionInfo() : base(CompletionInfoType.Hosted)
        {
        }
    }
}