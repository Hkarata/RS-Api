SELECT 
                DATEPART(HOUR, b.BookedAt) AS BookingHour, 
                COUNT(*) AS BookingCount
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                DATEPART(HOUR, b.BookedAt);