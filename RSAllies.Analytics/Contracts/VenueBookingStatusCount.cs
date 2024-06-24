namespace RSAllies.Analytics.Contracts
{
    public class VenueBookingStatusCount
    {
        public string VenueName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
