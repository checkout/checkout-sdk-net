namespace Checkout.Workflows
{
    public class UpdateWorkflowResponse : HttpMetadata
    {
        public string Name { get; set; }
        
        public bool? Active { get; set; }
    }
}