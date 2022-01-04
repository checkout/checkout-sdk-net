namespace Checkout
{
    public abstract class UnitTestFixture
    {
        // Default
        protected const string ValidDefaultSk = "sk_test_fde517a8-3f01-41ef-b4bd-4282384b0a64";

        protected const string ValidDefaultPk = "pk_test_fe70ff27-7c32-4ce1-ae90-5691a188ee7b";
        protected const string InvalidDefaultSk = "sk_test_asdsad3q4dq";
        protected const string InvalidDefaultPk = "pk_test_q414dasds";

        // FOUR
        protected const string ValidFourSk = "sk_sbox_m73dzbpy7cf3gfd46xr4yj5xo4e";

        protected const string ValidFourPk = "pk_sbox_pkhpdtvmkgf7hdnpwnbhw7r2uic";
        protected const string InvalidFourSk = "sk_sbox_m73dzbpy7c-f3gfd46xr4yj5xo4e";
        protected const string InvalidFourPk = "pk_sbox_pkh";

        protected static string BuildPath(params string[] pathParams)
        {
            return string.Join("/", pathParams);
        }
    }
}