using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("Responses", Schema = "Test")]
    internal class Response
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
        public bool IsChoiceCorrect { get; set; }
    }
}
