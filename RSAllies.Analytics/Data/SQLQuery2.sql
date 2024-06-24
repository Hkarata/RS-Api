SELECT 
                v.Name AS VenueName, 
                CASE 
                    WHEN b.Status = 0 THEN 'Booked'
                    WHEN b.Status = 1 THEN 'Paid'
                    WHEN b.Status = 2 THEN 'Confirmed'
                    WHEN b.Status = 3 THEN 'Cancelled'
                    ELSE 'Unknown'
                END AS Status, 
                COUNT(b.Id) AS Count
            FROM 
                Venues.Venues v
            JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
            JOIN 
                Venues.Bookings b ON s.Id = b.SessionId
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                v.Name, 
                b.Status;