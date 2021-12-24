namespace Checkout.Sessions.Completion
{
    public class NonHostedCompletionInfo : CompletionInfo
    {
        public string CallbackUrl { get; private set; }

        public NonHostedCompletionInfo(string callbackUrl) : base(CompletionInfoType.NonHosted)
        {
            CallbackUrl = callbackUrl;
        }
    }
}