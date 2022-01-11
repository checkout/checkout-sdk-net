namespace Checkout.Sessions.Completion
{
    public abstract class CompletionInfo
    {
        public CompletionInfoType? Type { get; set; }

        protected CompletionInfo(CompletionInfoType type)
        {
            Type = type;
        }
    }
}