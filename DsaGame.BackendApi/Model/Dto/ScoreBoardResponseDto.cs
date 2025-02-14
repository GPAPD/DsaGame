using System.ComponentModel.DataAnnotations.Schema;

namespace DsaGame.BackendApi.Model.Dto
{
    public class ScoreBoardResponseDto
    {
        public long ScoreId { get; set; }

        public UserDto User { get; set; }

        public int GameLevel { get; set; }

        public int Points { get; set; }

        public bool IsLegit { get; set; }

        public DateTime RecordDate { get; set; }

    }
}
