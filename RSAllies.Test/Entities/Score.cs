using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("Scores", Schema = "Test")]
    internal class Score
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ScoreValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
