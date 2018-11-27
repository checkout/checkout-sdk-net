using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checkout.SampleApp.Models;
using Shouldly;
using Xunit;

namespace Checkout.SampleApp.Tests
{
    public class PaymentModelValidationTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanBeValid(bool doThreeDs)
        {
            var model = new PaymentModel()
            {
                Amount = 4,
                Currency = "USD",
                DoThreeDS = doThreeDs,
                CardToken = "test"
            };

            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, context, validationResults, true);

            result.ShouldBeTrue();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAmountInvalidModelIsInvalid(bool doThreeDs)
        {
            var model = new PaymentModel()
            {
                Amount = null,
                Currency = "USD",
                DoThreeDS = doThreeDs,
                CardToken = "test"
            };
            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, context, validationResults, true);

            result.ShouldBeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenCurrencyInvalidRenderIndexViewWithModelErrors(bool doThreeDs)
        {
            var model = new PaymentModel()
            {
                Amount = 3,
                Currency = null,
                DoThreeDS = doThreeDs,
                CardToken = "test"
            };

            var context = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(model, context, validationResults, true);

            result.ShouldBeFalse();
        }
    }
}
