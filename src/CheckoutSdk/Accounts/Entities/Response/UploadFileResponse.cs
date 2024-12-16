using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    public class UploadFileResponse : Resource
    {
        public string Id { get; set; }
        
        public long MaximumSizeInBytes { get; set; }

        public IList<string> DocumentTypesForPurpose { get; set; }
    }
}