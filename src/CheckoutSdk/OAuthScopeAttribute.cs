using System;

namespace Checkout
{
    public class OAuthScopeAttribute : Attribute
    {
        public string Scope { get; }

        public OAuthScopeAttribute(string scope)
        {
            Scope = scope;
        }
    }
}