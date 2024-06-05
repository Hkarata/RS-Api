namespace RSAllies.Test.Contracts.Responses
{
    public record QuestionDto
    {
        public string Scenario { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public string Question { get; init; } = string.Empty;
        public List<ChoiceDto>? Choices { get; init; }
    }
}
