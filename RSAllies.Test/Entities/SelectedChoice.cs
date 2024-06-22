using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("SelectedChoices", Schema = "Test")]
    internal class SelectedChoice
    {
        public int Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
        public bool IsChoiceCorrect { get; set; }
    }
}
