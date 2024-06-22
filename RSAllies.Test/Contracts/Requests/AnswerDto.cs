namespace RSAllies.Test.Contracts.Requests
{
    public record AnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid ChoiceId { get; set; }
    }
}
