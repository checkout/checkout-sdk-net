using System;

namespace Checkout
{
    public class FourOAuthScopeAttribute : Attribute
    {
        public string Scope { get; }

        public FourOAuthScopeAttribute(string scope)
        {
            Scope = scope;
        }
    }
}