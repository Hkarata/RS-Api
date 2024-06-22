SELECT 
    CASE 
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
        ELSE '50+'
    END AS AgeGroup,
    COUNT(*) AS RetakeCount
FROM 
    (
        SELECT 
            UserId
        FROM 
            Test.Scores
        GROUP BY 
            UserId
        HAVING 
            COUNT(*) > 1
    ) AS UserRetakes
JOIN 
    Users.Users u ON UserRetakes.UserId = u.Id
GROUP BY 
    CASE 
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
        WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
        ELSE '50+'
    END;
