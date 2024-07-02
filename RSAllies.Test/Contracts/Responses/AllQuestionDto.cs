namespace RSAllies.Test.Contracts.Responses
{
    public record AllQuestionDto
    {
        public Guid Id { get; init; }
        public string Scenario { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public string QuestionText { get; init; } = string.Empty;
        public bool IsEnglish { get; init; }
        public List<AllChoiceDto>? Choices { get; init; }
    }
}
