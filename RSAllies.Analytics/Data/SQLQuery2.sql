 SELECT 
                g.GenderType, 
                COUNT(u.Id) AS Count
            FROM 
                Users.Users u
            INNER JOIN 
                Users.Genders g ON u.GenderId = g.Id
            WHERE 
                g.GenderType IN ('Male', 'Female')
            GROUP BY 
                g.GenderType