
namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    /// <summary>
    /// This class mapping created to get tokens from API
    /// </summary>
    public class TokenResult : BaseEntity<TokenModel>
    {

    }
    public class TokenModel
    {
        //it is a representation for token string in API
        public string Token { get; set; }
    }

}
