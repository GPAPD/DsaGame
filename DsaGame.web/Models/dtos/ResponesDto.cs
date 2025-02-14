namespace DsaGame.Web.Models.dtos
{
    public class ResponesDto
    {
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = "";
    }
}
