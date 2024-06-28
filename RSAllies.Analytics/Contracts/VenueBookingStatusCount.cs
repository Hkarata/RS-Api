namespace RSAllies.Analytics.Contracts
{
    public class VenueBookingStatusCount
    {
        public string VenueName { get; set; } = string.Empty;
        public int Booked { get; set; }
        public int Paid { get; set; }
        public int Confirmed { get; set; }
        public int Cancelled { get; set; }
    }
}
