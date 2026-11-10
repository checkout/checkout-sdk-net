using Shouldly;
using Xunit;

namespace Checkout
{
    public class AbstractClientTest
    {
        [Theory]
        [InlineData("pay_abcdefghijklmnopqrstuvwxyz", "pay_abcdefghijklmnopqrstuvwxyz")]
        [InlineData("ent_wxglze3wwywujg4nna5fb7ldli", "ent_wxglze3wwywujg4nna5fb7ldli")]
        [InlineData("6f1a2b3c-4d5e-6f70-8192-a3b4c5d6e7f8", "6f1a2b3c-4d5e-6f70-8192-a3b4c5d6e7f8")]
        [InlineData("ORD-1_2.3~4", "ORD-1_2.3~4")]
        [InlineData("klarna-external", "klarna-external")]
        [InlineData("...", "...")]
        [InlineData("x.", "x.")]
        public void ShouldKeepUnreservedCharacters(string value, string expected)
        {
            TestClient.Join(value).ShouldBe(expected);
        }

        [Theory]
        [InlineData("x_1?y=z&w", "x_1%3Fy%3Dz%26w")]
        [InlineData("x_1#y", "x_1%23y")]
        [InlineData("%2e%2e", "%252e%252e")]
        [InlineData("x_1%2Fy", "x_1%252Fy")]
        [InlineData("x 1+y", "x%201%2By")]
        [InlineData("x;1@y:z", "x%3B1%40y%3Az")]
        [InlineData("．．", "%EF%BC%8E%EF%BC%8E")]
        [InlineData("é", "%C3%A9")]
        [InlineData("😀", "%F0%9F%98%80")]
        [InlineData("x\u0080y", "x%C2%80y")]
        public void ShouldEncodeEachSegment(string value, string expected)
        {
            TestClient.Join(value).ShouldBe(expected);
        }

        [Fact]
        public void ShouldJoinEncodedSegments()
        {
            TestClient.Join("payments", "pay_x?y#z", "voids").ShouldBe("payments/pay_x%3Fy%23z/voids");
            TestClient.Join("payments", "pay_x", "voids").ShouldBe("payments/pay_x/voids");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(".")]
        [InlineData("..")]
        [InlineData("x_1/y")]
        [InlineData("../x_1")]
        [InlineData("/")]
        [InlineData("x_1\\y")]
        [InlineData("x_1\ry")]
        [InlineData("x_1\ny")]
        [InlineData("x_1\0")]
        [InlineData("x_1\ty")]
        [InlineData("x_1\u001fy")]
        [InlineData("x_1\u007fy")]
        public void ShouldRefuseValuesThatCannotBeOneSegment(string value)
        {
            var exception = Should.Throw<CheckoutArgumentException>(() => TestClient.Join("payments", value, "voids"));
            exception.Message.ShouldBe("The value is not a valid path segment");
        }

        [Fact]
        public void ShouldRefuseUnpairedSurrogates()
        {
            Should.Throw<CheckoutArgumentException>(() => TestClient.Join("x_1" + '\ud800'));
            Should.Throw<CheckoutArgumentException>(() => TestClient.Join('\udc00' + "x_1"));
            Should.Throw<CheckoutArgumentException>(() => TestClient.Join("x_1" + '\ud800' + '\ud800'));
        }

        [Fact]
        public void ShouldRefuseConstantsWithSeparators()
        {
            Should.Throw<CheckoutArgumentException>(() => TestClient.Join("applepay/certificates"));
        }

        [Fact]
        public void ShouldRefuseNullPathElements()
        {
            Should.Throw<CheckoutArgumentException>(() => TestClient.Join(null));
        }

        private sealed class TestClient : AbstractClient
        {
            private TestClient() : base(null, null, SdkAuthorizationType.SecretKey)
            {
            }

            public static string Join(params string[] elements) => BuildPath(elements);
        }
    }
}
