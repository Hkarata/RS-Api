namespace RSAllies.Users.Contracts.Requests
{
    public record AccountDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
