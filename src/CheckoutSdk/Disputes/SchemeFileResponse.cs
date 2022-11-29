using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class SchemeFileResponse : Resource
    {
        public string Id { get; set; }

        public IList<SchemeFile> Files { get; set; }
    }
}