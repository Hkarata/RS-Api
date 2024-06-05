namespace RSAllies.Test.Contracts.Responses
{
    public record ScoreDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ScoreValue { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
