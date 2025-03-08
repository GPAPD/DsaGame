namespace DsaGame.Web.Utility
{
    public class SD
    {
        public static string? BackEndAPI { get; set; }
        public static string? BananaAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JwtAuthToken";

        //game over and scoreboard start
        public const string ScoreBoard = "Score Board";
        public const string Home = "Home";

        public const string GameOver = "Game Over";
        public const string TryAgain = "Try Again";
        //game over and scoreboard ends

        public const int BasicPoint = 100;

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
