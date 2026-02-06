using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Identities.Common
{
    public class SelectedDocument
    {
        public CountryCode Country { get; set; }
        public DocumentType DocumentType { get; set; }
    }

    public class ApplicantSessionInformation
    {
        public string IpAddress { get; set; }
        public List<SelectedDocument> SelectedDocuments { get; set; } = new List<SelectedDocument>();
    }
}