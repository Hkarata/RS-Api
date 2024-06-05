using System.ComponentModel.DataAnnotations.Schema;

namespace RSAllies.Test.Entities
{
    [Table("Questions", Schema = "Test")]
    internal class Question
    {
        // TODO : create fillable fields for the question entity

        // TODO : check results after 30 mins
        public Guid Id { get; set; }
        public string? Scenario { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public required List<Choice> Choices { get; set; }
        public bool IsEnglish { get; set; }

        // Safe Delete Implementation
        public bool IsDeleted { get; set; }
    }
}
