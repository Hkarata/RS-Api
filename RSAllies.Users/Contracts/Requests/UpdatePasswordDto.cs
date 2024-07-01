namespace RSAllies.Users.Contracts.Requests
{
    public record UpdatePasswordDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
