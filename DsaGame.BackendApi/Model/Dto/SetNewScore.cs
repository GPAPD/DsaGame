namespace DsaGame.BackendApi.Model.Dto
{
    public class SetNewScore
    {
        public string? Email { get; set; }

        public int GameLevel { get; set; }

        public int Points { get; set; }

        public bool IsLegit { get; set; }

        public DateTime RecordDate { get; set; }
    }
}
