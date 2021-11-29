using System;

namespace Checkout
{
    public class EnvironmentAttribute : Attribute
    {
        public string ApiUri { get; }

        public EnvironmentAttribute(string apiUri)
        {
            ApiUri = apiUri;
        }
    }
}