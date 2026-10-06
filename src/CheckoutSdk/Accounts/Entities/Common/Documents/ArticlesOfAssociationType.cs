using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document types accepted as memorandum or articles of association.
    /// </summary>
    public enum ArticlesOfAssociationType
    {
        [EnumMember(Value = "memorandum_of_association")]
        MemorandumOfAssociation,

        [EnumMember(Value = "articles_of_association")]
        ArticlesOfAssociation
    }
}