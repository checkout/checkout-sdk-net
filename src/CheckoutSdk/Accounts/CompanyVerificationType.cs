using System.Runtime.Serialization;

namespace Checkout.Accounts
{
    public enum CompanyVerificationType
    {
        [EnumMember(Value = "incorporation_document")] IncorporationDocument,
        
        [EnumMember(Value = "articles_of_association")] ArticlesOfAssociation
    }
}