using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Entities.Response
{
    public class OnboardSubEntityDetailsResponse : Resource
    {
        public IList<SubEntityMemberData> Data { get; set; }
    }
}