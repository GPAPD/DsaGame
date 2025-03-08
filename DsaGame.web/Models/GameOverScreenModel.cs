using DsaGame.Web.Models.dtos;

namespace DsaGame.Web.Models
{
    public class GameOverScreenModel
    {

        public List<ScoreModel> ScoreList { get; set; }

        public string PageTitle { get; set; }

        public string GameOverPageButtonTitle { get; set; }

    }
}
