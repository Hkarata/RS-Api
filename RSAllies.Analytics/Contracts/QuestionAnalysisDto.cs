﻿namespace RSAllies.Analytics.Contracts
{
    public class QuestionAnalysisDto
    {
        public string Question { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public int CorrectResponses { get; set; }
        public int IncorrectResponses { get; set; }
    }
}
