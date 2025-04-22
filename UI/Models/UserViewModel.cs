using System.ComponentModel.DataAnnotations;

namespace UI.Models.User
{
    public class LogInViewModel
    {
        [Required]
        public string UserNameEmail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class ReSetPasswordViewModel
    {
        [Required]
        public string UserNameEmail { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? MiddelName { get; set; }

        [Required]
        public string? LastName { get; set; } = string.Empty;

        [Required]
        public string? City { get; set; } = string.Empty;

        [Required]
        public string? State { get; set; } = string.Empty;

        [Required]
        public string? Country { get; set; } = string.Empty;

        [Required]
        public int? PostalCode { get; set; }

        [Required]
        public string? StreetAddress { get; set; } = string.Empty;

        public string? SkillOrOccupation { get; set; } = string.Empty;

        public string? Biography { get; set; } = string.Empty;

        public string? ProfilePhoto { get; set; } = string.Empty;

        public string? CoverPhoto { get; set; } = string.Empty;

        public string? FacebookLink { get; set; } = string.Empty;

        public string? TwitterLink { get; set; } = string.Empty;

        public string? LinkedinLink { get; set; } = string.Empty;

        public string? WebsiteLink { get; set; } = string.Empty;

        public string? GitHubLink { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<string>? Roles { get; set; }
    }

    public class UserDetailViewModel
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string[]? Roles { get; set; }

        public string? PhoneNumber { get; set; }

        public bool TwoFacotrEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }
    }
}