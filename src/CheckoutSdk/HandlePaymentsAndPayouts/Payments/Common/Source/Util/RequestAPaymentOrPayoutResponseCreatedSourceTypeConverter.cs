using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.Util
{
    public class RequestAPaymentOrPayoutResponseCreatedSourceTypeConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(AbstractSource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var target = Create(jObject);
            if (target != null)
            {
                // Check if the original type is an unknown value that would cause enum parsing to fail
                var originalType = jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();

                if (IsUnknownType(originalType) && target.GetType().Name == "CardSource")
                {
                    // This is a fallback case - replace the unknown type with "card" to allow proper deserialization
                    var modifiedJObject = (JObject)jObject.DeepClone();
                    modifiedJObject[CheckoutUtils.Type] = CheckoutUtils.GetEnumMemberValue(SourceType.Card);
                    serializer.Populate(modifiedJObject.CreateReader(), target);
                }
                else
                {
                    // Normal case - use original JSON
                    serializer.Populate(jObject.CreateReader(), target);
                }
            }

            return target;
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static AbstractSource Create(JToken jToken)
        {
            CheckoutUtils.ValidateParams("jToken", jToken);
            var sourceType = GetSourceType(jToken);
            return CreateResponse(sourceType);
        }

        private static AbstractSource CreateResponse(string sourceType)
        {
            if (CheckoutUtils.GetEnumMemberValue(SourceType.Card).Equals(sourceType))
            {
                return new CardSource.CardSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Afterpay).Equals(sourceType))
            {
                return new AfterpaySource.AfterpaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.AlipayCn).Equals(sourceType))
            {
                return new AlipayCnSource.AlipayCnSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.AlipayHk).Equals(sourceType))
            {
                return new AlipayHkSource.AlipayHkSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.AlipayPlus).Equals(sourceType))
            {
                return new AlipayPlusSource.AlipayPlusSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Alma).Equals(sourceType))
            {
                return new AlmaSource.AlmaSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Bancontact).Equals(sourceType))
            {
                return new BancontactSource.BancontactSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Benefit).Equals(sourceType))
            {
                return new BenefitSource.BenefitSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.CurrencyAccount).Equals(sourceType))
            {
                return new CurrencyAccountSource.CurrencyAccountSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Cvconnect).Equals(sourceType))
            {
                return new CvconnectSource.CvconnectSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Dana).Equals(sourceType))
            {
                return new DanaSource.DanaSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Eps).Equals(sourceType))
            {
                return new EpsSource.EpsSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Fawry).Equals(sourceType))
            {
                return new FawrySource.FawrySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Gcash).Equals(sourceType))
            {
                return new GcashSource.GcashSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Ideal).Equals(sourceType))
            {
                return new IdealSource.IdealSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Illicado).Equals(sourceType))
            {
                return new IllicadoSource.IllicadoSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Kakaopay).Equals(sourceType))
            {
                return new KakaopaySource.KakaopaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Klarna).Equals(sourceType))
            {
                return new KlarnaSource.KlarnaSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Knet).Equals(sourceType))
            {
                return new KnetSource.KnetSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Mbway).Equals(sourceType))
            {
                return new MbwaySource.MbwaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Mobilepay).Equals(sourceType))
            {
                return new MobilepaySource.MobilepaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Multibanco).Equals(sourceType))
            {
                return new MultibancoSource.MultibancoSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Octopus).Equals(sourceType))
            {
                return new OctopusSource.OctopusSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Paynow).Equals(sourceType))
            {
                return new PaynowSource.PaynowSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Paypal).Equals(sourceType))
            {
                return new PaypalSource.PaypalSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Postfinance).Equals(sourceType))
            {
                return new PostfinanceSource.PostfinanceSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.P24).Equals(sourceType))
            {
                return new P24Source.P24Source();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Qpay).Equals(sourceType))
            {
                return new QpaySource.QpaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Sepa).Equals(sourceType))
            {
                return new SepaSource.SepaSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Sequra).Equals(sourceType))
            {
                return new SequraSource.SequraSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Stcpay).Equals(sourceType))
            {
                return new StcpaySource.StcpaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Tamara).Equals(sourceType))
            {
                return new TamaraSource.TamaraSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Tng).Equals(sourceType))
            {
                return new TngSource.TngSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Truemoney).Equals(sourceType))
            {
                return new TruemoneySource.TruemoneySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Twint).Equals(sourceType))
            {
                return new TwintSource.TwintSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Vipps).Equals(sourceType))
            {
                return new VippsSource.VippsSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.Wechatpay).Equals(sourceType))
            {
                return new WechatpaySource.WechatpaySource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseGiropaySource).Equals(sourceType))
            {
                return new PaymentGetResponseGiropaySourceSource.PaymentGetResponseGiropaySourceSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseKlarnaSource).Equals(sourceType))
            {
                return new PaymentGetResponseKlarnaSourceSource.PaymentGetResponseKlarnaSourceSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseSEPAVFourSource).Equals(sourceType))
            {
                return new PaymentGetResponseSEPAVFourSourceSource.PaymentGetResponseSEPAVFourSourceSource();
            }

            if (CheckoutUtils.GetEnumMemberValue(SourceType.PaymentResponseSource).Equals(sourceType))
            {
                return new PaymentResponseSourceSource.PaymentResponseSourceSource();
            }

            throw new CheckoutApiException("Unsupported source type", HttpStatusCode.BadRequest, new Dictionary<string, object> { { "sourceType", sourceType } });
        }

        private static string GetSourceType(JToken jObject)
        {
            return jObject.SelectToken(CheckoutUtils.Type)?.Value<string>()?.ToLowerInvariant();
        }

        private static bool IsUnknownType(string sourceType)
        {
            if (string.IsNullOrEmpty(sourceType))
                return false;

            // Check if the source type matches any known enum value
            return !IsKnownSourceType(sourceType);
        }

        private static bool IsKnownSourceType(string sourceType)
        {
            // Check all known SourceType enum values
            return CheckoutUtils.GetEnumMemberValue(SourceType.Card).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Afterpay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.AlipayCn).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.AlipayHk).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.AlipayPlus).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Alma).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Bancontact).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Benefit).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Cvconnect).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Dana).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Eps).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Fawry).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Gcash).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Ideal).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Illicado).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Kakaopay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Klarna).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Knet).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Mbway).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Mobilepay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Multibanco).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Octopus).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Paynow).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Paypal).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Postfinance).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.P24).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Qpay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Sepa).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Sequra).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Stcpay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Tamara).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Tng).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Truemoney).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Twint).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Vipps).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.CurrencyAccount).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.Wechatpay).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseGiropaySource).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseKlarnaSource).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.PaymentGetResponseSEPAVFourSource).Equals(sourceType) ||
                   CheckoutUtils.GetEnumMemberValue(SourceType.PaymentResponseSource).Equals(sourceType);
        }
    }
}