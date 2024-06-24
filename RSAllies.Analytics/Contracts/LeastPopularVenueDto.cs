namespace RSAllies.Analytics.Contracts
{
    public class LeastPopularVenueDto
    {
        public string VenueName { get; set; } = string.Empty;
        public int Bookings { get; set; }
    }
}
