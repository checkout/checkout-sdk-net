using System.Collections.Generic;

namespace Checkout.Sessions
{
    public class DsPublicKeys
    {
        public string DsPublic { get; set; }

        public string CaPublic { get; set; }
        
        public IList<object> CaPublicAll { get; set; }
    }
}