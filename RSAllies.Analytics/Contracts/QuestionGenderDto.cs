namespace RSAllies.Analytics.Contracts
{
    public class QuestionGenderDto
    {
        public string Question { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public int CorrectResponses { get; set; }
        public int IncorrectResponses { get; set; }
    }
}
