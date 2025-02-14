using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DsaGame.BackendApi.Model
{
    public class ScoreBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ScoreId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public int GameLevel { get; set; }

        public int Points { get; set; }

        public bool IsLegit { get; set; }

        public DateTime RecordDate { get; set; }


        public ApplicationUser ApplicationUser { get; set; }


    }
}
