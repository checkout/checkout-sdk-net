using Checkout.AgenticCommerce.Entities;
using Checkout.AgenticCommerce.Requests;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.AgenticCommerce
{
    public class AgenticCommerceIntegrationTest : SandboxTestFixture
    {
        public AgenticCommerceIntegrationTest() : base(PlatformType.Default)
        {
        }

        [Fact(Skip = "Requires a valid HMAC signing key and merchant enabled for agentic commerce")]
        public async Task CreateDelegatedPayment_ShouldReturnToken()
        {
            var request = new DelegatedPaymentRequest
            {
                PaymentMethod = new DelegatedPaymentMethodCard
                {
                    CardNumberType = DelegatedCardNumberType.Fpan,
                    Number = "4242424242424242",
                    ExpMonth = "11",
                    ExpYear = "2026",
                    Metadata = new Dictionary<string, string> { { "issuing_bank", "test" } }
                },
                Allowance = new DelegatedPaymentAllowance
                {
                    Reason = DelegatedPaymentAllowanceReason.OneTime,
                    MaxAmount = 10000,
                    Currency = "USD",
                    MerchantId = "cli_vkuhvk4vjn2edkps7dfsq6emqm",
                    CheckoutSessionId = "1PQrsT",
                    ExpiresAt = DateTime.UtcNow.AddHours(1)
                },
                RiskSignals = new List<DelegatedPaymentRiskSignal>
                {
                    new DelegatedPaymentRiskSignal { Type = "card_testing", Score = 10, Action = "blocked" }
                },
                Metadata = new Dictionary<string, string> { { "campaign", "q4" } }
            };

            // Signature must be computed as: Base64(HMAC-SHA256(signingKey, Timestamp + RequestBody))
            var headers = new DelegatedPaymentHeaders
            {
                Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Signature = "computed-hmac-sha256-base64-signature"
            };

            var response = await DefaultApi.AgenticCommerceClient().CreateDelegatedPaymentToken(request, headers);

            response.ShouldNotBeNull();
            response.Id.ShouldNotBeNullOrEmpty();
            response.Created.ShouldNotBe(default);
            response.Metadata.ShouldNotBeNull();
        }
    }
}
