using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class PlatformsFileUploadResponse : Resource
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public int MaximumSizeInBytes { get; set; }
        public IList<string> DocumentTypesForPurpose { get; set; }
    }
}