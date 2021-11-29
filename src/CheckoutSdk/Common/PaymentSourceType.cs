using System.Runtime.Serialization;

namespace Checkout.Common
{
    public enum PaymentSourceType
    {
        [EnumMember(Value = "card")] Card,
        [EnumMember(Value = "id")] Id,
        [EnumMember(Value = "network_token")] NetworkToken,
        [EnumMember(Value = "token")] Token,
        [EnumMember(Value = "customer")] Customer,
        [EnumMember(Value = "dlocal")] DLocal,

        [EnumMember(Value = "currency_account")]
        CurrencyAccount,
        [EnumMember(Value = "baloto")] Baloto,
        [EnumMember(Value = "boleto")] Boleto,
        [EnumMember(Value = "fawry")] Fawry,
        [EnumMember(Value = "giropay")] Giropay,
        [EnumMember(Value = "ideal")] Ideal,
        [EnumMember(Value = "oxxo")] Oxxo,
        [EnumMember(Value = "pagofacil")] PagoFacil,
        [EnumMember(Value = "rapipago")] RapiPago,
        [EnumMember(Value = "klarna")] Klarna,
        [EnumMember(Value = "sofort")] Sofort,
    }
}