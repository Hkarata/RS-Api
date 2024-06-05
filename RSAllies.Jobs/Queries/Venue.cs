namespace RSAllies.Jobs.Queries
{
    internal class Venue
    {
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string VenueAddress { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}
