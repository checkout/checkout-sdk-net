using System.Collections.Generic;
using Checkout.Accounts.Payout;
using Checkout.Accounts.Payout.Request;
using Checkout.Accounts.Payout.Response;
using Shouldly;
using Xunit;

namespace Checkout.Accounts
{
    /// <summary>
    /// Covers the SaaS seller (ISV) payout schedule fields added by the 2026-08-05 spec, plus
    /// payment_instrument_id, which was missing from both sides of this schedule.
    /// </summary>
    public class PayoutScheduleIsvSerializationTest
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        [Fact]
        public void ShouldSerializeIsvScheduleFields()
        {
            var request = new UpdateScheduleRequest
            {
                Enabled = true,
                Threshold = 100,
                PaymentInstrumentId = "ppi_w4jelhppmfiufdnatam37wrfc4",
                BalanceMinimum = 500,
                CarryForwardEnabled = true,
                Recurrence = new ScheduleFrequencyWeeklyRequest
                {
                    ByDay = new List<DaySchedule> { DaySchedule.Monday }
                }
            };

            var json = _serializer.Serialize(request);

            json.ShouldContain("\"balance_minimum\":500");
            json.ShouldContain("\"carry_forward_enabled\":true");
            json.ShouldContain("\"payment_instrument_id\":\"ppi_w4jelhppmfiufdnatam37wrfc4\"");
        }

        /// <summary>
        /// A standard sub-entity has no balance minimum, no carry-forward and may have no payout
        /// destination on the schedule, so none of them may appear in its request body. If they
        /// leaked in as nulls or zeros the API would read a standard schedule as an ISV one.
        /// </summary>
        [Fact]
        public void ShouldOmitIsvFieldsWhenUnset()
        {
            var request = new UpdateScheduleRequest
            {
                Enabled = true,
                Threshold = 100,
                Recurrence = new ScheduleFrequencyMonthlyRequest
                {
                    ByMonthDay = new List<int> { 1, 15 }
                }
            };

            var json = _serializer.Serialize(request);

            json.ShouldNotContain("balance_minimum");
            json.ShouldNotContain("carry_forward_enabled");
            json.ShouldNotContain("payment_instrument_id");
            json.ShouldContain("\"by_month_day\":[1,15]");
        }

        [Fact]
        public void ShouldDeserializeIsvScheduleFields()
        {
            const string json = "{\"enabled\":true,\"threshold\":100,\"balance_minimum\":500," +
                                "\"carry_forward_enabled\":true," +
                                "\"payment_instrument_id\":\"ppi_w4jelhppmfiufdnatam37wrfc4\"," +
                                "\"recurrence\":{\"frequency\":\"Weekly\",\"by_day\":[\"monday\"]}}";

            var schedule = (CurrencySchedule)_serializer.Deserialize(json, typeof(CurrencySchedule));

            schedule.Enabled.ShouldBe(true);
            schedule.Threshold.ShouldBe(100);
            schedule.BalanceMinimum.ShouldBe(500);
            schedule.CarryForwardEnabled.ShouldBe(true);
            schedule.PaymentInstrumentId.ShouldBe("ppi_w4jelhppmfiufdnatam37wrfc4");
        }

        /// <summary>
        /// A standard schedule omits these, and they must come back null rather than 0/false:
        /// a caller cannot otherwise tell "not applicable" from "set to zero".
        /// </summary>
        [Fact]
        public void ShouldLeaveIsvFieldsNullForAStandardSchedule()
        {
            const string json = "{\"enabled\":true,\"threshold\":100," +
                                "\"recurrence\":{\"frequency\":\"Daily\"}}";

            var schedule = (CurrencySchedule)_serializer.Deserialize(json, typeof(CurrencySchedule));

            schedule.BalanceMinimum.ShouldBeNull();
            schedule.CarryForwardEnabled.ShouldBeNull();
            schedule.PaymentInstrumentId.ShouldBeNull();
        }
    }
}
