using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// The document types accepted as company verification. <c>articles_of_association</c> is
    /// accepted on the US Company (2.0) variants only; articles of association sent as their own
    /// document use <see cref="ArticlesOfAssociationType"/> instead.
    /// </summary>
    public enum CompanyVerificationType
    {
        [EnumMember(Value = "incorporation_document")]
        IncorporationDocument,
        
        [EnumMember(Value = "articles_of_association")]
        ArticlesOfAssociation
    }
}