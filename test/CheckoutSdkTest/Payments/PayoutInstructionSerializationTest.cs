using Checkout.Payments.Response;
using Shouldly;
using Xunit;

namespace Checkout.Payments
{
    /// <summary>
    /// Covers instruction.funds_transfer_type on the card payout response, added by the 2026-08-05
    /// spec. The field already existed on the payout request via PaymentInstruction; this is the
    /// response side, where PaymentInstructionResponse carried only value_date.
    /// </summary>
    public class PayoutInstructionSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldDeserializeFundsTransferTypeOnThePayoutInstruction()
        {
            const string json = "{\"id\":\"pay_1\",\"status\":\"Accepted\",\"reference\":\"ORD-1\"," +
                                "\"instruction\":{\"value_date\":\"2026-08-05T10:00:00Z\"," +
                                "\"funds_transfer_type\":\"AA\"}}";

            var response = (PayoutResponse)_serializer.Deserialize(json, typeof(PayoutResponse));

            response.Instruction.ShouldNotBeNull();
            response.Instruction.FundsTransferType.ShouldBe("AA");
            response.Instruction.ValueDate.ShouldNotBeNull();
        }

        /// <summary>
        /// The scheme does not always categorise the client, so the field has to survive being
        /// absent rather than defaulting to something that reads as a real categorisation.
        /// </summary>
        [Fact]
        public void ShouldLeaveFundsTransferTypeNullWhenAbsent()
        {
            const string json = "{\"id\":\"pay_1\",\"status\":\"Accepted\"," +
                                "\"instruction\":{\"value_date\":\"2026-08-05T10:00:00Z\"}}";

            var response = (PayoutResponse)_serializer.Deserialize(json, typeof(PayoutResponse));

            response.Instruction.ShouldNotBeNull();
            response.Instruction.FundsTransferType.ShouldBeNull();
        }
    }
}
