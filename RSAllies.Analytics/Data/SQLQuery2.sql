SELECT 
                v.Name AS VenueName, 
                CAST(SUM(CASE WHEN b.Status = 2 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(b.Id) AS ConfirmationRate
            FROM 
                Venues.Venues v
            JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
            JOIN 
                Venues.Bookings b ON s.Id = b.SessionId
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                v.Name;