SELECT 
                COUNT(*) AS RepeatedBookingCount
            FROM 
                (
                    SELECT 
                        u.Id AS UserId, 
                        COUNT(b.Id) AS BookingCount
                    FROM 
                        Users.Users u
                    JOIN 
                        Venues.Bookings b ON u.Id = b.UserId
                    WHERE 
                        b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
                    GROUP BY 
                        u.Id
                    HAVING 
                        COUNT(b.Id) > 1
                ) AS RepeatedBookings;