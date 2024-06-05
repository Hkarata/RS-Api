using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("Answers", Schema = "Test")]
    internal class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
    }
}
