namespace RSAllies.Test.Contracts.Responses
{
    public record ChoiceDto
    {
        public Guid Id { get; init; }
        public string ChoiceText { get; init; } = string.Empty;
    }
}
