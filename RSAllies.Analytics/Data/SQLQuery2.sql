SELECT 
                q.QuestionText as Question, 
                COUNT(*) AS TotalResponses,
                SUM(CASE WHEN sc.IsChoiceCorrect = 1 THEN 1 ELSE 0 END) AS CorrectResponses,
                SUM(CASE WHEN sc.IsChoiceCorrect = 0 THEN 1 ELSE 0 END) AS IncorrectResponses
            FROM 
                Test.Questions q
            JOIN 
                Test.Choices c ON q.Id = c.QuestionId
            JOIN 
                Test.SelectedChoices sc ON sc.QuestionId = q.Id AND sc.ChoiceId = c.Id
            GROUP BY 
                q.QuestionText;