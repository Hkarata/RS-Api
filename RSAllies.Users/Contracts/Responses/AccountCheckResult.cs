namespace RSAllies.Users.Contracts.Responses
{
    public record AccountCheckResult
    {
        public bool PhoneNumberExists { get; set; }
        public bool EmailExists { get; set; }
    }
}
