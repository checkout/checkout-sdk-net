namespace Checkout
{
    public class CheckoutDefaultSdk
    {
        public StaticKeysCheckoutSdkBuilder StaticKeys()
        {
            return new StaticKeysCheckoutSdkBuilder();
        }

        public class StaticKeysCheckoutSdkBuilder : AbstractCheckoutSdkBuilder<ICheckoutApi>
        {
            private string _publicKey;
            private string _secretKey;

            public StaticKeysCheckoutSdkBuilder PublicKey(string publicKey)
            {
                _publicKey = publicKey;
                return this;
            }

            public StaticKeysCheckoutSdkBuilder SecretKey(string secretKey)
            {
                _secretKey = secretKey;
                return this;
            }

            protected override SdkCredentials GetSdkCredentials()
            {
                return new DefaultStaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            public override ICheckoutApi Build()
            {
                return new CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}