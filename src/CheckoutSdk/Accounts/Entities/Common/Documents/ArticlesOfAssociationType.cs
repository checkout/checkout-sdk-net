using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum ArticlesOfAssociationType
    {
        [EnumMember(Value = "memorandum_of_association")]
        MemorandumOfAssociation,

        [EnumMember(Value = "articles_of_association")]
        ArticlesOfAssociation
    }
}