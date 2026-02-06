namespace Checkout.Identities.Common
{
    public class ResourceLink
    {
        public string Href { get; set; }
    }

    public class ResourceLinks
    {
        public ResourceLink Self { get; set; }
        public ResourceLink Applicant { get; set; }
        public ResourceLink VerificationUrl { get; set; }
    }
}