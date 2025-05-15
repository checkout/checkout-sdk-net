using Checkout.Issuing.Controls.Responses.Create;
using System.Collections.Generic;

namespace Checkout.Issuing.Controls.Responses.Query
{
    public class CardControlsQueryResponse : HttpMetadata
    {
        public IList<AbstractCardControlResponse> Controls { get; set; }
    }
}