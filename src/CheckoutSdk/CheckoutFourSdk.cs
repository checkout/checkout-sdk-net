using Checkout.Four;

namespace Checkout
{
    public class CheckoutFourSdk
    {
        public StaticKeysCheckoutSdkBuilder StaticKeys()
        {
            return new StaticKeysCheckoutSdkBuilder();
        }

        public class StaticKeysCheckoutSdkBuilder : AbstractCheckoutSdkBuilder<Four.ICheckoutApi>
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
                return new FourStaticKeysSdkCredentials(_secretKey, _publicKey);
            }

            public override Four.ICheckoutApi Build()
            {
                return new Four.CheckoutApi(GetCheckoutConfiguration());
            }
        }
    }
}