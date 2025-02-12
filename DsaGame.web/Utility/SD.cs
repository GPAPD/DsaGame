namespace DsaGame.Web.Utility
{
    public class SD
    {
        public static string? AuthAPIBase { get; set; }
        public static string? BananaAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JwtAuthToken";

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
