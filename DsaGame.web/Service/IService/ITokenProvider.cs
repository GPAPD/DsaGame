namespace DsaGame.Web.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token);

        string? GetToken();

        void DeleteToken();
    }
}
