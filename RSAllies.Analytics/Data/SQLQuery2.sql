SELECT 
                g.GenderType, 
                COUNT(*) AS Total,
                SUM(CASE WHEN s.ScoreValue >= 16 THEN 1 ELSE 0 END) AS Passed,
                SUM(CASE WHEN s.ScoreValue < 16 THEN 1 ELSE 0 END) AS Failed
            FROM 
                Test.Scores s
            JOIN 
                Users.Users u ON s.UserId = u.Id
            JOIN 
                Users.Genders g ON u.GenderId = g.Id
            GROUP BY 
                g.GenderType