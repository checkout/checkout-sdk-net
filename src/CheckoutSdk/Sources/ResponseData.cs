using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Sources
{
    [Serializable]
    public sealed class ResponseData : Dictionary<string, object>
    {
        public ResponseData()
        {
        }

        private ResponseData(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}