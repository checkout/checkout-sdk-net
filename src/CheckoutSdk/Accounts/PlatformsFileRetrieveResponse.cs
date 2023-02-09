using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class PlatformsFileRetrieveResponse : Resource
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public IList<string> StatusReasons { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        public string UploadedOn { get; set; }
        public string Purpose { get; set; }
    }
}