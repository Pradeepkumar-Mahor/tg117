namespace tg117.API.Models.Account
{
    public class Register
    {
        public string Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }

        public string? MiddelName { get; set; }

        public string? LastName { get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public string? SkillOrOccupation { get; set; } = string.Empty;

        public string? Biography { get; set; } = string.Empty;
        public string? StreetAddress { get; set; } = string.Empty;

        public string? ProfilePhoto { get; set; } = string.Empty;
        public string? CoverPhoto { get; set; } = string.Empty;
        public string? FacebookLink { get; set; } = string.Empty;
        public string? TwitterLink { get; set; } = string.Empty;
        public string? LinkedinLink { get; set; } = string.Empty;
        public string? WebsiteLink { get; set; } = string.Empty;
        public string? GitHubLink { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}