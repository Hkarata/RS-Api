namespace RSAllies.Analytics.Contracts
{
    public class QuestionDifficultyDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public double CorrectPercentage { get; set; }
    }
}
