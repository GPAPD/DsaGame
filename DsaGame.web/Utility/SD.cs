namespace DsaGame.Web.Utility
{
    public class SD
    {
        public static string? BackEndAPI { get; set; }
        public static string? BananaAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JwtAuthToken";

        public const int BasicPoint = 100;

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
