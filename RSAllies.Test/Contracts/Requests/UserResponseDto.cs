namespace RSAllies.Test.Contracts.Requests
{
    public record UserResponseDto
    {
        public Guid UserId { get; set; }
        public List<AnswerDto>? Answers { get; set; }
    }
}
