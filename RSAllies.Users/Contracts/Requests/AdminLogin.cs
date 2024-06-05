namespace RSAllies.Users.Contracts.Requests
{
    public record AdminLogin
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
