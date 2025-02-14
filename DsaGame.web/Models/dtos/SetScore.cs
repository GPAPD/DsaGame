using DsaGame.Web.Models.dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DsaGame.BackendApi.Model.Dto
{
    public class SetScore
    {
        public UserDto? user { get; set; }

        public string? UserId { get; set; }

        public int GameLevel { get; set; }

        public int Points { get; set; }

        public bool IsLegit { get; set; }

        public DateTime RecordDate { get; set; }

    }
}
