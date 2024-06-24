SELECT 
                DATEPART(YEAR, b.BookedAt) AS Year, 
                COUNT(*) AS Bookings
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(YEAR, -5, GETDATE())
            GROUP BY 
                DATEPART(YEAR, b.BookedAt);
