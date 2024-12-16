using System.Runtime.Serialization;

namespace Checkout.Accounts.Entities.Common.Documents
{
    public enum CompanyVerificationType
    {
        [EnumMember(Value = "incorporation_document")] IncorporationDocument,
        
        [EnumMember(Value = "articles_of_association")] ArticlesOfAssociation
    }
}