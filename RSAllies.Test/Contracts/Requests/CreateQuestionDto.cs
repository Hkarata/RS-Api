namespace RSAllies.Test.Contracts.Requests
{
    public record CreateQuestionDto(string? Scenario, string? ImageUrl, string Question, bool IsEnglish, List<CreateChoiceDto> Choices);
}
