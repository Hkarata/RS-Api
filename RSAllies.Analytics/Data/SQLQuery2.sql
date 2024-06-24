SELECT 
                v.Name AS VenueName, 
                COUNT(b.Id) AS Bookings
            FROM 
                Venues.Venues v
            INNER JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
            INNER JOIN 
                Venues.Bookings b ON s.Id = b.SessionId
            WHERE 
                b.BookedAt >= DATEADD(YEAR, -1, GETDATE())
            GROUP BY 
                v.Name
            ORDER BY 
                COUNT(b.Id) DESC