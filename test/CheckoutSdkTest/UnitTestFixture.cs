namespace Checkout
{
    public abstract class UnitTestFixture
    {
        // Previous
        protected static readonly string ValidPreviousPk = System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_PUBLIC_KEY");
        protected static readonly string ValidPreviousSk = System.Environment.GetEnvironmentVariable("CHECKOUT_PREVIOUS_SECRET_KEY");
        protected const string InvalidPreviousPk = "pk_test_q414dasds";
        protected const string InvalidPreviousSk = "sk_test_asdsad3q4dq";

        // Default
        protected static readonly string ValidDefaultPk = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_PUBLIC_KEY");
        protected static readonly string ValidDefaultSk = System.Environment.GetEnvironmentVariable("CHECKOUT_DEFAULT_SECRET_KEY");
        protected const string InvalidDefaultPk = "pk_sbox_pkh";
        protected const string InvalidDefaultSk = "sk_sbox_m73dzbpy7c-f3gfd46xr4yj5xo4e";
    }
}