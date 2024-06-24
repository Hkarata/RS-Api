namespace RSAllies.Analytics.Data
{
    internal static class Queries
    {
        public static string GenderCount = @"
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
        ";

        public static string AgeGroupCount = @"
            SELECT 
                CASE 
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 50 AND 57 THEN '50-57'
                    ELSE '58+'
                END AS AgeGroup,
                COUNT(u.Id) AS Count
            FROM 
                Users.Users u
            GROUP BY 
                CASE 
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 50 AND 57 THEN '50-57'
                    ELSE '58+'
                END;
        ";


        public static string EducationLevelCount = @"
            SELECT 
                e.Level, 
                COUNT(u.Id) AS Count
            FROM 
                Users.Users u
            INNER JOIN 
                Users.EducationLevels e ON u.EducationLevelId = e.Id
            GROUP BY 
                e.Level
        ";

        public static string LicenseClassCount = @"
            SELECT 
                l.Class, 
                COUNT(u.Id) AS Count
            FROM 
                Users.Users u
            INNER JOIN 
                Users.LicenseClasses l ON u.LicenseClassId = l.Id
            GROUP BY 
                l.Class;
        ";


        public static string AverageSessions = @"
            SELECT 
                v.Name AS VenueName, 
                ISNULL(CAST(AVG(s.SessionCount) AS FLOAT), 0) AS AverageSessions
            FROM 
                Venues.Venues v
            LEFT JOIN 
                (
                    SELECT 
                        s.VenueId, 
                        COUNT(*) AS SessionCount, 
                        DATEPART(MONTH, s.Date) AS Month, 
                        DATEPART(YEAR, s.Date) AS Year
                    FROM 
                        Venues.Sessions s
                    WHERE 
                        s.Date >= DATEADD(MONTH, -3, GETDATE())
                    GROUP BY 
                        s.VenueId, 
                        DATEPART(MONTH, s.Date), 
                        DATEPART(YEAR, s.Date)
                ) s ON v.Id = s.VenueId
            GROUP BY 
                v.Name
        ";

        public static string BookingRate = @"
            SELECT 
                v.Name AS VenueName, 
                ISNULL(CAST(AVG(b.BookingCount) AS FLOAT), 0) AS BookingRate
            FROM 
                Venues.Venues v
            LEFT JOIN 
                (
                    SELECT 
                        s.VenueId, 
                        COUNT(*) AS BookingCount, 
                        DATEPART(MONTH, s.Date) AS Month, 
                        DATEPART(YEAR, s.Date) AS Year
                    FROM 
                        Venues.Sessions s
                    INNER JOIN 
                        Venues.Bookings b ON s.Id = b.SessionId
                    WHERE 
                        s.Date >= DATEADD(MONTH, -3, GETDATE())
                    GROUP BY 
                        s.VenueId, 
                        DATEPART(MONTH, s.Date), 
                        DATEPART(YEAR, s.Date)
                ) b ON v.Id = b.VenueId
            GROUP BY 
                v.Name
        ";

        public static string PeakBookingTimes = @"
            SELECT 
                DATEPART(HOUR, b.BookedAt) AS BookingHour, 
                COUNT(*) AS BookingCount
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                DATEPART(HOUR, b.BookedAt)
        ";

        public static string PeakBookingDays = @"
            SELECT 
                DATENAME(WEEKDAY, b.BookedAt) AS Day, 
                COUNT(*) AS Bookings
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                DATENAME(WEEKDAY, b.BookedAt)
        ";

        public static string PeakBookingMonths = @"
            SELECT 
                DATENAME(MONTH, b.BookedAt) AS Month, 
                COUNT(*) AS Bookings
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(YEAR, -1, GETDATE())
            GROUP BY 
                DATENAME(MONTH, b.BookedAt)
        ";

        public static string PeakBookingYears = @"
            SELECT 
                DATEPART(YEAR, b.BookedAt) AS Year, 
                COUNT(*) AS Bookings
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(YEAR, -5, GETDATE())
            GROUP BY 
                DATEPART(YEAR, b.BookedAt)
        ";

        public static string MostPopularVenues = @"
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
        ";

        public static string LeastPopularVenues = @"
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
                COUNT(b.Id) ASC
        ";

        public static string CancellationRate = @"
           SELECT 
                v.Name AS VenueName, 
                CAST(SUM(CASE WHEN b.Status = 3 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(b.Id) AS CancellationRate
           FROM 
                Venues.Venues v
           JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
           JOIN 
                Venues.Bookings b ON s.Id = b.SessionId
           WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
           GROUP BY 
                v.Name
        ";

        public static string ConfirmationRate = @"
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
                v.Name
        ";

        public static string BookingStatusCount = @"
            SELECT 
                CASE 
                    WHEN b.Status = 0 THEN 'Booked'
                    WHEN b.Status = 1 THEN 'Paid'
                    WHEN b.Status = 2 THEN 'Confirmed'
                    WHEN b.Status = 3 THEN 'Cancelled'
                    ELSE 'Unknown'
                END AS Status, 
                COUNT(b.Id) AS Count
            FROM 
                Venues.Bookings b
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                b.Status
        ";

        public static string BookingStatusCountByVenue = @"
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
        ";

        public static string UserRepeatBookingRate = @"
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
                COUNT(b.Id) > 1;
        ";

        public static string TotalRepeatedBookingRate = @"
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
                ) AS RepeatedBookings
        ";

        public static string TotalRepeatedBookingRateByVenue = @"
            SELECT 
                v.Name AS VenueName, 
                COUNT(*) AS RepeatedBookingCount
            FROM 
                Venues.Venues v
            JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
            JOIN 
                Venues.Bookings b ON s.Id = b.SessionId
            WHERE 
                b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                v.Name
            HAVING 
                COUNT(DISTINCT b.UserId) > 1;
        ";

        public static string VenueUtilizationRate = @"
            SELECT 
                v.Name AS VenueName, 
                ISNULL(CAST(SUM(b.BookingCount) AS FLOAT) / (SUM(s.Capacity) * COUNT(DISTINCT s.Id)),0) AS UtilizationRate
            FROM 
                Venues.Venues v
            JOIN 
                Venues.Sessions s ON v.Id = s.VenueId
            LEFT JOIN 
                (
                    SELECT 
                        b.SessionId, 
                        COUNT(*) AS BookingCount
                    FROM 
                        Venues.Bookings b
                    WHERE 
                        b.Status != 3 AND
                        b.BookedAt >= DATEADD(MONTH, -3, GETDATE())
                    GROUP BY 
                        b.SessionId
                ) b ON s.Id = b.SessionId
            WHERE 
                s.Date >= DATEADD(MONTH, -3, GETDATE())
            GROUP BY 
                v.Name
        ";

        public static string GenderTestAnalysis = @"
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
        ";

        public static string QuestionAnalysis = @"
            SELECT 
                q.QuestionText as Question, 
                COUNT(*) AS TotalResponses,
                SUM(CASE WHEN sc.IsChoiceCorrect = 1 THEN 1 ELSE 0 END) AS CorrectResponses,
                SUM(CASE WHEN sc.IsChoiceCorrect = 0 THEN 1 ELSE 0 END) AS IncorrectResponses
            FROM 
                Test.Questions q
            JOIN 
                Test.Choices c ON q.Id = c.QuestionId
            JOIN 
                Test.SelectedChoices sc ON sc.QuestionId = q.Id AND sc.ChoiceId = c.Id
            GROUP BY 
                q.QuestionText;
        ";


        public static string QuestionDifficultyLevel = @"
            SELECT 
                q.QuestionText, 
                COUNT(*) AS TotalResponses,
                CAST(SUM(CASE WHEN sc.IsChoiceCorrect = 1 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*) * 100 AS CorrectPercentage
            FROM 
                Test.Questions q
            JOIN 
                Test.Choices c ON q.Id = c.QuestionId
            JOIN 
                Test.SelectedChoices sc ON sc.QuestionId = q.Id AND sc.ChoiceId = c.Id
            GROUP BY 
                q.QuestionText;
        ";

        public static string ScoreAnalysis = @"
            SELECT 
                CASE 
                    WHEN TotalScore BETWEEN 0 AND 5 THEN '0-5'
                    WHEN TotalScore BETWEEN 6 AND 10 THEN '6-10'
                    WHEN TotalScore BETWEEN 11 AND 15 THEN '11-15'
                    WHEN TotalScore BETWEEN 16 AND 20 THEN '16-20'
                    ELSE '21-25'
                END AS ScoreRange,
                COUNT(*) AS UserCount
            FROM 
                (
                    SELECT 
                        UserId, 
                        SUM(ScoreValue) AS TotalScore
                    FROM 
                        Test.Scores
                    GROUP BY 
                        UserId
                ) AS UserScores
            GROUP BY 
                CASE 
                    WHEN TotalScore BETWEEN 0 AND 5 THEN '0-5'
                    WHEN TotalScore BETWEEN 6 AND 10 THEN '6-10'
                    WHEN TotalScore BETWEEN 11 AND 15 THEN '11-15'
                    WHEN TotalScore BETWEEN 16 AND 20 THEN '16-20'
                    ELSE '21-25'
                END;
        ";

        public static string TestPassAgedGroupAnalysis = @"
            SELECT 
                CASE 
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                    ELSE '50+'
                END AS AgeGroup,
                COUNT(*) AS UserCount
            FROM 
                Test.Scores s
            JOIN 
                Users.Users u ON s.UserId = u.Id
            WHERE 
                s.ScoreValue >= 16
            GROUP BY 
                CASE 
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 26 AND 33 THEN '26-33'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 34 AND 41 THEN '34-41'
                    WHEN DATEDIFF(YEAR, u.DateOfBirth, GETDATE()) BETWEEN 42 AND 49 THEN '42-49'
                    ELSE '50+'
                END;
        ";

        public static string QuestionGenderAnalysis = @"
            SELECT 
                q.QuestionText as Question, 
                g.GenderType as Gender,
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
                Users.Users u ON u.Id = sc.ResponseUserId
            JOIN 
                Users.Genders g ON u.GenderId = g.Id
            GROUP BY 
                q.QuestionText, 
                g.GenderType;
        ";


        public static string QuestionAgeGroupAnalysis = @"
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
        ";

        public static string TestRetakeCount = @"
            SELECT 
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
                ) AS UserRetakes;
        ";

        public static string TestRetakeCountByGender = @"
            SELECT 
                g.GenderType, 
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
            JOIN 
                Users.Genders g ON u.GenderId = g.Id
            GROUP BY 
                g.GenderType;
        ";

        public static string TestRetakeByAgeGroup = @"
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

        ";
    }
}
