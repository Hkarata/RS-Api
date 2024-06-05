namespace RSAllies.Test.Contracts.Requests
{
    public record struct CreateScoreDto(Guid UserId, int ScoreValue);
}
