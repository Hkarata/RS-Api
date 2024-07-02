namespace RSAllies.Test.Contracts.Responses
{
    public record AllQuestionDto
    {
        public string Scenario { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public string Question { get; init; } = string.Empty;
        public bool IsEnglish { get; init; }
        public List<AllChoiceDto>? Choices { get; init; }
    }
}
