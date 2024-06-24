SELECT 
                    q.QuestionText as Question, 
                    CASE 
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                        ELSE '50+'
                    END AS AgeGroup,
                    COUNT(*) AS TotalResponses,
                    SUM(CASE WHEN sc.IsChoiceCorrect = 1 THEN 1 ELSE 0 END) AS CorrectResponses,
                    SUM(CASE WHEN sc.IsChoiceCorrect = 0 THEN 1 ELSE 0 END) AS IncorrectResponses
                FROM 
                    Test.Questions q
                JOIN 
                    Test.Choices c ON q.Id = c.QuestionId
                JOIN 
                    Test.SelectedChoices sc ON sc.QuestionId = q.Id AND sc.ChoiceId = c.Id
                JOIN 
                    Users.Users u ON sc.ResponseUserId = u.Id
                GROUP BY 
                    q.QuestionText, 
                    CASE 
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                        ELSE '50+'
                    END;