using Newtonsoft.Json;

namespace Checkout.Sessions.Completion
{
    public class HostedCompletionInfo : CompletionInfo
    {        
        public string CallbackUrl { get; set; }
        
        public string SuccessUrl { get; set; }
        
        public string FailureUrl { get; set; }

        public HostedCompletionInfo(string callbackUrl, string successUrl, string failureUrl) : base(CompletionInfoType.Hosted)
        {
            CallbackUrl = callbackUrl;
            SuccessUrl = successUrl;
            FailureUrl = failureUrl;
        }

        public HostedCompletionInfo() : base(CompletionInfoType.NonHosted)
        {
        }
    }
}